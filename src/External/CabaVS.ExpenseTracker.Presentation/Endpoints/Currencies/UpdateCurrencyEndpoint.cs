using CabaVS.ExpenseTracker.Application.Features.Currencies.Commands;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using CabaVS.ExpenseTracker.Domain.Shared;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CabaVS.ExpenseTracker.Presentation.Endpoints.Currencies;

internal sealed class UpdateCurrencyEndpoint : Endpoint<UpdateCurrencyRequest, Results<Ok, BadRequest<Error>>>
{
    private readonly ISender _sender;

    public UpdateCurrencyEndpoint(ISender sender)
    {
        _sender = sender;
    }
    
    public override void Configure()
    {
        Put("api/currencies/{id:guid}");
        AllowAnonymous();
        Options(builder => builder.WithName(nameof(UpdateCurrencyEndpoint)));
    }

    public override async Task<Results<Ok, BadRequest<Error>>> ExecuteAsync(UpdateCurrencyRequest req, CancellationToken ct)
    {
        var command = new UpdateCurrencyCommand(req.Id, req.ModelToUpdate);

        var result = await _sender.Send(command, ct);

        return result.IsSuccess 
            ? TypedResults.Ok() 
            : TypedResults.BadRequest(result.Error);
    }
}

internal sealed record UpdateCurrencyRequest(
    [FromRoute] Guid Id,
    [property: FastEndpoints.FromBody] CurrencyToUpsertModel ModelToUpdate);

internal sealed class UpdateCurrencyEndpointSummary : Summary<UpdateCurrencyEndpoint>
{
    public UpdateCurrencyEndpointSummary()
    {
        Summary = Description = "Updates an existing Currency.";

        ExampleRequest = new CurrencyToUpsertModel(
            Name: "Ukraine Hryvnia",
            Code: "UAH",
            Symbol: "₴");
            
        Response(200, "Currency updated successfully.");
        
        Response(400, "Request model didn't pass validation.",
            example: new Error("Validation.Unspecified", "Some parameters are not valid in request model."));
    }
}