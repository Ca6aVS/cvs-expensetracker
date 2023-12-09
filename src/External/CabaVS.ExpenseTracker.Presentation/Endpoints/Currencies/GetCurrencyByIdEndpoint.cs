using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Queries;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CabaVS.ExpenseTracker.Presentation.Endpoints.Currencies;

internal sealed class GetCurrencyByIdEndpoint : Endpoint<GetCurrencyByIdRequest, Results<Ok<CurrencyModel>, NotFound>>
{
    private readonly ISender _sender;

    public GetCurrencyByIdEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/currencies/{id:guid}");
        AllowAnonymous();
        Options(builder => builder.WithName(nameof(GetCurrencyByIdEndpoint)));
    }

    public override async Task<Results<Ok<CurrencyModel>, NotFound>> ExecuteAsync(GetCurrencyByIdRequest req, CancellationToken ct)
    {
        var query = new GetCurrencyByIdQuery(req.Id);

        var result = await _sender.Send(query, ct);

        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }
}

internal sealed record GetCurrencyByIdRequest(
    [FromRoute] Guid Id);

internal sealed class GetCurrencyByIdEndpointSummary : Summary<GetCurrencyByIdEndpoint>
{
    public GetCurrencyByIdEndpointSummary()
    {
        Summary = Description = "Gets Currency by ID.";

        Params[nameof(GetCurrencyByIdRequest.Id)] = "ID of Currency to lookup. Should be a GUID.";

        Response(200, "OK response with body.",
            example: new CurrencyModel(
                Id: new Guid("97631f65-3de8-43ad-9a65-116ad251959c"),
                Name: "Ukraine Hryvnia",
                Code: "UAH",
                Symbol: "₴"));
        
        Response(404, "Currency is not found with requested ID.");
    }
}