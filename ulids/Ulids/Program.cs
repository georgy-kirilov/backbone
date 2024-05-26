using Microsoft.EntityFrameworkCore;
using Shared.Ulids;
using Ulids;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost; Port=55432; Username=admin; Password=Qwerty1@; Database=pgdb;"));

builder.Services.AddUlids();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();
dbContext.Songs.AddRange(
[
    new Song { Id = new(Ulid.NewUlid()), Title = "Like a Rolling Stone" },
    new Song { Id = new(Ulid.NewUlid()), Title = "Imagine" },
    new Song { Id = new(Ulid.NewUlid()), Title = "What's Going On" },
]);
dbContext.SaveChanges();

app.MapGet("songs/all", (ApplicationDbContext db)
    => db.Songs.Select(s => new { s.Id.Value, s.Title }).ToArray());

app.MapGet("songs/{id:ulid}", (Ulid id, ApplicationDbContext db)
    => db.Songs.FirstOrDefault(s => s.Id == new SongId(id)));

app.Run();
