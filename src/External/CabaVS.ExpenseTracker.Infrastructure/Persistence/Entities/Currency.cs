using CabaVS.ExpenseTracker.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabaVS.ExpenseTracker.Infrastructure.Persistence.Entities;

internal sealed class Currency
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Code { get; init; }
    public required string Symbol { get; init; }

    public static Currency FromDomainEntity(Domain.Entities.Currency currency) =>
        new()
        {
            Id = currency.Id,
            Name = currency.Name.Value,
            Code = currency.Code.Value,
            Symbol = currency.Symbol.Value
        };
}

internal sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(CurrencyName.MaxLength)
            .IsRequired();
        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.Property(c => c.Code)
            .HasMaxLength(CurrencyCode.MaxLength)
            .IsRequired();

        builder.Property(c => c.Symbol)
            .HasMaxLength(CurrencySymbol.MaxLength)
            .IsRequired();
    }
}