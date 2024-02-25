using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate
{
    public class Handler : IRequestHandler<UseCases.Authenticate.Resquest, UseCases.Authenticate.Response>
    {
        private readonly IRepository _repository;

        public Handler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(Resquest request, CancellationToken cancellationToken)
        {
            #region Valida requisiçao
            try
            {
                var res = Specification.Assert(request);

                if (res.IsValid == false)
                    return new Response("Requisição inválida", 400, res.Notifications);
            }
            catch (Exception)
            {
                return new Response("Não foi possível validar a requisição", 500);
            }
            #endregion

            #region Recupera o usuário
            User? user;
            try
            {
                user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

                if (user is null)
                    return new Response("Perfil não encontrado.", 404);
            }
            catch (Exception)
            {
                return new Response("Não foi possível recuparar o perfil", 500);
            }
            #endregion

            #region Valida a senha
            try
            {
                if (user.Password.Challenge(request.Password) == false)
                    return new Response("Usuário e/ou senha inválido(s).", 400);
            }
            catch (Exception)
            {
                return new Response("Não foi possível validar a senha.", 500);
            }
            #endregion

            #region Verifica se a conta já foi ativada
            try
            {
                if (user.Email.Verification.IsActive == false)
                    return new Response("A conta está inativa", 400);
            }
            catch (Exception)
            {
                return new Response("Não foi possível verificar se a conta está ativa", 500);
            }
            #endregion

            #region Retorna os dados
            try
            {
                var data = new ResponseData 
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    Roles = Array.Empty<string>()
                };

                return new Response(string.Empty, data);
            }
            catch (Exception)
            {
                return new Response("Não foi possível recuperar os dados da sua conta.", 500);
            }
            #endregion
        }
    }
}