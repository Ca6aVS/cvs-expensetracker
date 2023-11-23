using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.Entities;

public sealed class Currency : Entity
{
    public CurrencyName Name { get; private set; }
    public CurrencyCode Code { get; set; }
    public CurrencySymbol Symbol { get; set; }
    
    private Currency(Guid id, CurrencyName name, CurrencyCode code, CurrencySymbol symbol) : base(id)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
    }

    public static Result<Currency> Create(Guid id, CurrencyName name, string code, string symbol, bool nameIsUnique)
    {
        if (!nameIsUnique)
        {
            return CurrencyErrors.NameTaken(name);
        }

        var codeResult = CurrencyCode.Create(code);
        if (codeResult.IsFailure)
        {
            return codeResult.Error;
        }

        var symbolResult = CurrencySymbol.Create(symbol);
        if (symbolResult.IsFailure)
        {
            return symbolResult.Error;
        }

        return new Currency(id, name, codeResult.Value, symbolResult.Value);
    }

    public Result UpdateName(CurrencyName name, bool nameIsUnique)
    {
        if (name != Name && !nameIsUnique)
        {
            return CurrencyErrors.NameTaken(name);
        }

        Name = name;

        return Result.Success();
    } 
}