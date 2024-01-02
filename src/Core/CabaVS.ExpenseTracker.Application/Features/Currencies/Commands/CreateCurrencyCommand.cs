using CabaVS.ExpenseTracker.Application.Abstractions.Persistence;
using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using CabaVS.ExpenseTracker.Domain.Entities;
using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using MediatR;

namespace CabaVS.ExpenseTracker.Application.Features.Currencies.Commands;

public sealed record CreateCurrencyCommand(
    CurrencyToUpsertModel ModelToCreate) : IRequest<Result<Guid>>;

public sealed class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrencyRepository _currencyRepository;

    public CreateCurrencyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _currencyRepository = unitOfWork.BuildCurrencyRepository();
    }
    
    public async Task<Result<Guid>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currencyNameResult = CurrencyName.Create(request.ModelToCreate.Name);
        if (currencyNameResult.IsFailure)
        {
            return currencyNameResult.Error;
        }

        var isExistsByName = await _currencyRepository.IsExistsByName(currencyNameResult.Value, cancellationToken);

        var currencyResult = Currency.Create(
            Guid.NewGuid(),
            currencyNameResult.Value,
            request.ModelToCreate.Code,
            request.ModelToCreate.Symbol,
            !isExistsByName);
        if (currencyResult.IsFailure)
        {
            return currencyResult.Error;
        }

        await _currencyRepository.Create(currencyResult.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return currencyResult.Value.Id;
    }
}