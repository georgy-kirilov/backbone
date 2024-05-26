using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ulids;

public readonly record struct SongId(Ulid Value)
{
    public sealed class ValueConverter() : ValueConverter<SongId, string>(
        id => id.Value.ToString(),
        str => new(Ulid.Parse(str))
    );
}

public sealed class Song
{
    public required SongId Id { get; init; }

    public required string Title { get; init; }
}
