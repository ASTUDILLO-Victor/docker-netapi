using Microsoft.EntityFrameworkCore;
using TurnosApi.Data;
using TurnosApi.Models.Entities;
using TurnosApi.Repositories.Interfaces;

namespace TurnosApi.Repositories;

public class TokenBlacklistRepository : ITokenBlacklistRepository
{
    private readonly AppDbContext _context;

    public TokenBlacklistRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Agregar(TokenBlacklist token)
    {
        _context.TokenBlacklist.Add(token);
        _context.SaveChanges();
    }

    public bool EstaEnBlacklist(string token)
    {
        return _context.TokenBlacklist.Any(t => t.Token == token);
    }

    // ❌ Lo que tienes — trae todos a memoria y luego los borra
    // public void EliminarExpirados()
    // {
    //     var expirados = _context.TokenBlacklist
    //         .Where(t => t.FechaExpiracion < DateTime.UtcNow)
    //         .ToList();
    //     _context.TokenBlacklist.RemoveRange(expirados);
    //     _context.SaveChanges();
    // }

    // ✅ Con ExecuteSqlRaw — un solo query directo a la BD
    public void EliminarExpirados()
    {
        _context.Database.ExecuteSqlRaw(
            "DELETE FROM \"TokenBlacklist\" WHERE \"FechaExpiracion\" < {0}",
            DateTime.UtcNow);
    }
}