using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Validate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Validate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region Valida a requisição
        try
        {
            var res = Specification.Assert(request);

            if (res.IsValid == false)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch (Exception)
        {
            return new Response("Não foi possível processar a requisição", 500);
        }
        #endregion

        #region Obtém o usuário
        User? user;

        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Não há nenhum usuário com este e-mail cadastrado.", 404);
        }
        catch (Exception)
        {
            return new Response("Não foi possível obter o usuário no banco de dados.", 500);
        }
        #endregion

        #region Validando o código de ativação
        try
        {
            user.Email.Verification.Verify(request.VerificationCode);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 500);
        }
        #endregion

        #region Ativando a conta
        try
        {
            await _repository.ValidateAsync(user);
        }
        catch (Exception)
        {
            return new Response("Não foi possível ativar a conta", 500);
        }
        #endregion

        return new Response("Conta ativada com sucesso.", 200);
    }
}