using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using MediatR;

namespace CabaVS.ExpenseTracker.Application.Features.Currencies.Queries;

public sealed record GetCurrencyByIdQuery(Guid Id) : IRequest<CurrencyModel?>;

public sealed class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, CurrencyModel?>
{
    private readonly ICurrencyReadRepository _currencyReadRepository;

    public GetCurrencyByIdQueryHandler(ICurrencyReadRepository currencyReadRepository)
    {
        _currencyReadRepository = currencyReadRepository;
    }
    
    public async Task<CurrencyModel?> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _currencyReadRepository.GetById(request.Id, cancellationToken);
        return model;
    }
}