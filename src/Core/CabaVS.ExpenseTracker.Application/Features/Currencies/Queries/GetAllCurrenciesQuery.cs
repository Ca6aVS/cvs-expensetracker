using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using MediatR;

namespace CabaVS.ExpenseTracker.Application.Features.Currencies.Queries;

public sealed record GetAllCurrenciesQuery : IRequest<CurrencyModel[]>;

public sealed class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, CurrencyModel[]>
{
    private readonly ICurrencyReadRepository _currencyReadRepository;

    public GetAllCurrenciesQueryHandler(ICurrencyReadRepository currencyReadRepository)
    {
        _currencyReadRepository = currencyReadRepository;
    }
    
    public async Task<CurrencyModel[]> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var models = await _currencyReadRepository.GetAll(cancellationToken);
        return models;
    }
}