using CabaVS.ExpenseTracker.Application.Features.Currencies.Commands;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using CabaVS.ExpenseTracker.Domain.Shared;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CabaVS.ExpenseTracker.Presentation.Endpoints.Currencies;

internal sealed class CreateCurrencyEndpoint : Endpoint<CreateCurrencyRequest, Results<CreatedAtRoute, BadRequest<Error>>>
{
    private readonly ISender _sender;

    public CreateCurrencyEndpoint(ISender sender)
    {
        _sender = sender;
    }
    
    public override void Configure()
    {
        Post("api/currencies");
        AllowAnonymous();
        Options(builder => builder.WithName(nameof(CreateCurrencyEndpoint)));
    }

    public override async Task<Results<CreatedAtRoute, BadRequest<Error>>> ExecuteAsync(CreateCurrencyRequest req, CancellationToken ct)
    {
        var command = new CreateCurrencyCommand(req.ModelToCreate);

        var result = await _sender.Send(command, ct);

        return result.IsSuccess 
            ? TypedResults.CreatedAtRoute(nameof(GetCurrencyByIdEndpoint), new { Id = result.Value }) 
            : TypedResults.BadRequest(result.Error);
    }
}

internal sealed record CreateCurrencyRequest(
    [Microsoft.AspNetCore.Mvc.FromBody] CurrencyToCreateModel ModelToCreate);

internal sealed class CreateCurrencyEndpointSummary : Summary<CreateCurrencyEndpoint>
{
    public CreateCurrencyEndpointSummary()
    {
        Summary = Description = "Creates a new Currency.";

        ExampleRequest = new CreateCurrencyRequest(new CurrencyToCreateModel(
            Name: "Ukraine Hryvnia",
            Code: "UAH",
            Symbol: "₴"));
            
        Response(201, "Currency created successfully. Location header is filled.");
        
        Response(400, "Request model didn't pass validation.",
            example: new Error("Validation.Unspecified", "Some parameters are not valid in request model."));
    }
}