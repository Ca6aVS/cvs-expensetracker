using CabaVS.ExpenseTracker.Application.Abstractions.Persistence;
using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using MediatR;

namespace CabaVS.ExpenseTracker.Application.Features.Currencies.Commands;

public sealed record UpdateCurrencyCommand(
    Guid Id,
    CurrencyToUpsertModel ModelToUpdate) : IRequest<Result>;

public sealed class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrencyRepository _currencyRepository;

    public UpdateCurrencyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _currencyRepository = unitOfWork.BuildCurrencyRepository();
    }
    
    public async Task<Result> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _currencyRepository.GetById(request.Id, cancellationToken);
        if (currency is null)
        {
            return CurrencyErrors.NotFound(request.Id);
        }

        var currencyNameResult = CurrencyName.Create(request.ModelToUpdate.Name);
        if (currencyNameResult.IsFailure)
        {
            return currencyNameResult.Error;
        }

        var nameIsUnique = true;
        if (currency.Name != currencyNameResult.Value)
        {
            nameIsUnique = !await _currencyRepository.IsExistsByName(currencyNameResult.Value, cancellationToken);
        }

        var updateResult = currency.Update(
            currencyNameResult.Value,
            request.ModelToUpdate.Code,
            request.ModelToUpdate.Symbol,
            nameIsUnique);
        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        await _currencyRepository.Update(currency, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return Result.Success();
    }
}