using Domain.Exceptions;

namespace Domain.Rules;

public static class BaseRules
{
    public static void ThrowIfIdIsInvalid(int id)
    {
        if (id <= 0)
            throw new BadRequestException("ID không hợp lệ.");
    }

    public static void ThrowIfStringIsNullOrEmpty(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BadRequestException($"{fieldName} không được để trống.");
    }

    public static void ThrowIfNegative(decimal value, string fieldName)
    {
        if (value < 0)
            throw new BadRequestException($"{fieldName} không được âm.");
    }
}
