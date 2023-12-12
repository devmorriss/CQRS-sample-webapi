using API.ResultHandler;
using Application.QCards;
using FluentValidation;
using MediatR;

namespace API.Endpoints
{
    public static class MyEndpoints
    {
        public static RouteGroupBuilder MapCardApi(this RouteGroupBuilder group)
        {

            group.MapGet("/list", (IMediator mediator) =>
            {
                return HandleResult.MyHandleResult(mediator.Send(new List.Query()).Result);
            })
            .WithOpenApi();

            group.MapPost("/buy/{id?}", async (IMediator mediator, IValidator<Buy.Command> validator, string? id) =>
            {
                var requestDocumentId = new Buy.Command { Id = id };
                var validationResult = await validator.ValidateAsync(requestDocumentId);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                return HandleResult.MyHandleResult(mediator.Send(requestDocumentId).Result);
            })
            .WithOpenApi();

            group.MapPost("/use/{id}", (IMediator mediator, int id) =>
            {
                return HandleResult.MyHandleResult(mediator.Send(new Use.Command
                {
                    Id = id
                }).Result);
            })
            .WithOpenApi();

            group.MapPost("/reload/{id}/{amount}", (IMediator mediator, int id, int amount) =>
            {
                return HandleResult.MyHandleResult(mediator.Send(new Reload.Command
                {
                    Id = id,
                    AmountToPay = amount
                }).Result);
            })
            .WithOpenApi();

            return group;
        }
    }

}