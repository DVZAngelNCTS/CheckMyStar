namespace CheckMyStar.Enumerations
{
    public static class EnumConverter
    {
        public static T ToEnum<T>(this int value) where T : struct, Enum
        {
            var type = typeof(T);

            if (type == typeof(EnumCivility))
            {
                return (T)(object)(value switch
                {
                    0 => EnumCivility.None,
                    1 => EnumCivility.Mister,
                    2 => EnumCivility.Madam,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            if (type == typeof(EnumRole))
            {
                return (T)(object)(value switch
                {
                    0 => EnumRole.None,
                    1 => EnumRole.Administrator,
                    2 => EnumRole.User,
                    3 => EnumRole.Inspector,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            throw new ArgumentException($"Type {type} is not supported.");
        }

        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            var type = typeof(T);

            if (type == typeof(EnumCivility))
            {
                return (T)(object)(value switch
                {
                    "None" => EnumCivility.None,
                    "Mister" => EnumCivility.Mister,
                    "Madam" => EnumCivility.Madam,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            if (type == typeof(EnumRole))
            {
                return (T)(object)(value switch
                {
                    "None" => EnumRole.None,
                    "Administrator" => EnumRole.Administrator,
                    "User" => EnumRole.User,
                    "Inspector" => EnumRole.Inspector,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            throw new ArgumentException($"Type {type} is not supported.");
        }

        public static int ToInt<T>(this T value) where T : struct, Enum
        {
            var type = typeof(T);

            if (type == typeof(EnumCivility))
            {
                return (value switch
                {
                    EnumCivility.None => 0,
                    EnumCivility.Mister => 1,
                    EnumCivility.Madam => 2,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            if (type == typeof(EnumRole))
            {
                return (value switch
                {
                    EnumRole.None => 0,
                    EnumRole.Administrator => 1,
                    EnumRole.User => 2,
                    EnumRole.Inspector => 3,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            throw new ArgumentException($"Type {type} is not supported.");
        }

        public static string ToStringValue<T>(this T value) where T : struct, Enum
        {
            var type = typeof(T);

            if (type == typeof(EnumCivility))
            {
                return (value switch
                {
                    EnumCivility.None => "None",
                    EnumCivility.Mister => "Mister",
                    EnumCivility.Madam => "Madam",
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            if (type == typeof(EnumRole))
            {
                return (value switch
                {
                    EnumRole.None => "None",
                    EnumRole.Administrator => "Administrator",
                    EnumRole.User => "User",
                    EnumRole.Inspector => "Inspector",
                    _ => throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' is not valid for enum '{type.Name}'.")
                });
            }

            throw new ArgumentException($"Type {type} is not supported.");
        }
    }
}
