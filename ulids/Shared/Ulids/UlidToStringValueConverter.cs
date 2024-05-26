using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Ulids;

public sealed class UlidToStringValueConverter() : ValueConverter<Ulid, string>(
    ulid => ulid.ToString(),
    str => Ulid.Parse(str)
);
