using Microsoft.EntityFrameworkCore;
using Shared.Ulids;

namespace Ulids;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Song> Songs => Set<Song>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Ulid>().HaveConversion<UlidToStringValueConverter>();
        configurationBuilder.Properties<SongId>().HaveConversion<SongId.ValueConverter>();
    }
}
