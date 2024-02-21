using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.Exceptions;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(Resquest resquest, CancellationToken cancellationToken)
    {
        // 1 - Validar a requisição
        try
        {
            var res = Specification.Assert(resquest);

            if (res.IsValid == false)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch (InvalidRequestException)
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }
        
        // 2 Gerar a entidade e os value objects.
        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(resquest.Email);
            password = new Password(resquest.Password);
            user = new User(resquest.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        // 3 - Verificar a existência do usuário no banco.
        try
        {
            var exists = await _repository.AnyAsync(email, cancellationToken);

            if (exists)
                return new Response("O e-mail já está em uso", 400);
        }
        catch (Exception)
        {
            return new Response("Falha ao verificar o e-mail", 500);
        }

        // 4 - Persistir o usuário no banco.
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Falha ao salvar os dados", 500);
        }

        // 5 - Enviar o código de ativação.
        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch (Exception)
        {
        }

        return new Response("Conta criada", new ResponseData(user.Id, user.Name, user.Email));
    }
}