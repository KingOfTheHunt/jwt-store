using JwtStore.Core.AccountContext.Exceptions;
using JwtStore.Core.SharedContext.ValueObjects;
using JwtStore.Core.Utils;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code => Guid.NewGuid().ToString("N")[..6].ToUpper();
    public TimeOnly? ExpiresAt { get; private set; } = TimeOnlyUtil.UtcNow().AddMinutes(5);
    public TimeOnly? VerifiedAt { get; private set; } = null;
    public bool IsActive => ExpiresAt == null && VerifiedAt != null;

    public void Verify(string code)
    {
        if (IsActive)
            throw new VerificationException("A conta já foi ativada.");

        if (ExpiresAt < TimeOnly.FromDateTime(DateTime.UtcNow))
            throw new ExpiredVerificationCodeException("O código de ativação já expirou.");

        if (string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase) == false)
            throw new InvalidVerificationCodeException("O código informado não é válido.");

        ExpiresAt = null;
        VerifiedAt = TimeOnlyUtil.UtcNow();
    }
}