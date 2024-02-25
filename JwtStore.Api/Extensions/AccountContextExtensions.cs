using MediatR;

namespace JwtStore.Api.Extensions;

public static class AccountContextExtensions
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Repository
        >();

        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Create.Service
        >();
        #endregion

        #region Authenticate
        builder.Services.AddTransient<
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            JwtStore.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository
        >();
        #endregion
    }

    public static void MapAccountContextEndpoints(this WebApplication app)
    {
        #region Create
        app.MapPost("api/v1/users", async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request request,
            IRequestHandler<JwtStore.Core.Contexts.AccountContext.UseCases.Create.Request, 
            JwtStore.Core.Contexts.AccountContext.UseCases.Create.Response> handler) =>  
        {
            var result = await handler.Handle(request, new CancellationToken());

            if (result.IsSuccess)
                return Results.Created($"api/v1/users/{result.Data?.Email}", result);
            
            return Results.Json(result, statusCode: result.Status);
        });
        #endregion

        #region Authenticate
        app.MapPost("api/v1/authenticate", async (
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Resquest resquest,
            IRequestHandler<JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Resquest,
            JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Response> handler) => 
        {
            var result = await handler.Handle(resquest, new CancellationToken());

            if (result.IsSuccess)
                return Results.Ok(result);
            
            return Results.Json(result, statusCode: result.Status);
        });
        #endregion
    }
}