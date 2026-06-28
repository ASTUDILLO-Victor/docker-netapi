using Microsoft.EntityFrameworkCore;
using TurnosApi.Data;
using TurnosApi.Models.Entities;
using TurnosApi.Repositories.Interfaces;

namespace TurnosApi.Repositories;

public class TurnoRepository : ITurnoRepository
{
    private readonly AppDbContext _context;

    public TurnoRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Turno> ObtenerTodos()
    {
        return _context.Turnos
            .AsNoTracking()
            .Include(t => t.Medico)
            .Include(t => t.Paciente)
            .ToList();
    }

    public Turno? ObtenerPorId(int id)
    {
        return _context.Turnos
            .AsNoTracking()
            .Include(t => t.Medico)
            .Include(t => t.Paciente)
            .FirstOrDefault(t => t.Id == id);
    }

    public List<Turno> ObtenerPorMedico(int medicoId)
    {
        return _context.Turnos
            .AsNoTracking()
            .Include(t => t.Medico)
            .Include(t => t.Paciente)
            .Where(t => t.MedicoId == medicoId)
            .ToList();
    }

    public List<Turno> ObtenerPorPaciente(int pacienteId)
    {
        return _context.Turnos
            .AsNoTracking()
            .Include(t => t.Medico)
            .Include(t => t.Paciente)
            .Where(t => t.PacienteId == pacienteId)
            .ToList();
    }

    public bool ExisteTurnoEnFecha(int medicoId, DateTime fecha)
    {
        return _context.Turnos.Any(t =>
            t.MedicoId == medicoId &&
            t.Fecha == fecha &&
            t.Estado == "Programado");
    }

    public Turno Agregar(Turno turno)
    {
        _context.Turnos.Add(turno);
        _context.SaveChanges();
        return turno;
    }

    public Turno Actualizar(Turno turno)
    {
        _context.Turnos.Update(turno);
        _context.SaveChanges();
        return turno;
    }

    // Buscar turnos por fecha usando SQL directo
public List<Turno> BuscarPorFecha(DateTime fecha)
{
    return _context.Turnos
        .FromSqlRaw(
            "SELECT * FROM \"Turnos\" WHERE DATE(\"Fecha\") = DATE({0})", 
            fecha)
        .Include(t => t.Medico)
        .Include(t => t.Paciente)
        .ToList();
}

public List<Turno> ObtenerConFiltros(int? medicoId = null, int? pacienteId = null, DateTime? fecha = null)
{
    // Query base
    string sql = "SELECT * FROM \"Turnos\" WHERE 1=1";
    var parametros = new List<object>();
    int index = 0;

    // Agregar filtros dinámicamente
    if (medicoId.HasValue)
    {
        sql += $" AND \"MedicoId\" = {{{index}}}";
        parametros.Add(medicoId.Value);
        index++;
    }

    if (pacienteId.HasValue)
    {
        sql += $" AND \"PacienteId\" = {{{index}}}";
        parametros.Add(pacienteId.Value);
        index++;
    }

    if (fecha.HasValue)
    {
        sql += $" AND DATE(\"Fecha\") = DATE({{{index}}})";
        parametros.Add(fecha.Value);
        index++;
    }

    return _context.Turnos
        .FromSqlRaw(sql, parametros.ToArray())
        .Include(t => t.Medico)
        .Include(t => t.Paciente)
        .ToList();
}
}