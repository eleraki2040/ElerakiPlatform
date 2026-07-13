using Eleraki.SharedKernel.ValueObjects;

using Eleraki.SharedKernel.Primitives;

namespace Eleraki.FinanceEngine.Domain;

public sealed record Currency
{
    public string Code { get; }
    public Currency(string code)
    {
        Guard.NotNullOrEmpty(code, nameof(code));
        Code = code.Trim().ToUpperInvariant();
    }
    public static Currency From(string code) => new(code);
    public override string ToString() => Code;
}
