using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Rules;

public static class AccountRules
{
    public static void ThrowIfAccountNotFound(AccountEntity? acc)
    {
        if (acc == null)
            throw new NotFoundException("Tài khoản này không tồn tại.");
    }

    public static void ThrowIfAccountExist(AccountEntity? acc)
    {
        if (acc != null)
            throw new BadRequestException("Tài khoản hiện tại đã tồn tại.");
    }
}
