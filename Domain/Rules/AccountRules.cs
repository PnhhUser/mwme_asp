using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Rules;

public static class AccountRules
{
    public static void ThrowIfAccountNotFound(AccountEntity? acc)
    {
        if (acc == null)
            throw new NotFoundException("Account không tồn tại");
    }
}
