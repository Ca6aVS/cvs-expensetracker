using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Queries;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CabaVS.ExpenseTracker.Presentation.Endpoints.Currencies;

internal sealed class GetAllCurrenciesEndpoint : EndpointWithoutRequest<Ok<CurrencyModel[]>>
{
    private readonly ISender _sender;

    public GetAllCurrenciesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/currencies");
        AllowAnonymous();
        Options(builder => builder.WithName(nameof(GetAllCurrenciesEndpoint)));
    }

    public override async Task<Ok<CurrencyModel[]>> ExecuteAsync(CancellationToken ct)
    {
        var query = new GetAllCurrenciesQuery();

        var result = await _sender.Send(query, ct);

        return TypedResults.Ok(result);
    }
}

internal sealed class GetAllCurrenciesEndpointSummary : Summary<GetAllCurrenciesEndpoint>
{
    public GetAllCurrenciesEndpointSummary()
    {
        Summary = Description = "Gets all Currencies.";
        
        Response(200, "OK response with body.",
            example: new[]
            {
                new CurrencyModel(
                    Id: new Guid("97631f65-3de8-43ad-9a65-116ad251959c"),
                    Name: "Ukraine Hryvnia",
                    Code: "UAH",
                    Symbol: "₴"),
                new CurrencyModel(
                    Id: new Guid("440c7935-7af3-47b2-9ee8-918bb27a3362"),
                    Name: "Poland Zloty",
                    Code: "PLN",
                    Symbol: "zł")
            });
    }
}