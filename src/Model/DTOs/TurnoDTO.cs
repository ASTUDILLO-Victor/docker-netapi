using System.ComponentModel.DataAnnotations;

namespace TurnosApi.Models.DTOs;

public class TurnoCreateDTO
{
     [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio")]
    [MinLength(5, ErrorMessage = "El motivo debe tener al menos 5 caracteres")]
    [MaxLength(200, ErrorMessage = "El motivo no puede tener más de 200 caracteres")]
    public string Motivo { get; set; } = "";

    [Required(ErrorMessage = "El médico es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El Id del médico debe ser mayor a 0")]
    public int MedicoId { get; set; }

    [Required(ErrorMessage = "El paciente es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El Id del paciente debe ser mayor a 0")]
    public int PacienteId { get; set; }
}

public class TurnoResponseDTO
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Motivo { get; set; } = "";
    public string Estado { get; set; } = "";
    public string MedicoNombre { get; set; } = "";
    public string PacienteNombre { get; set; } = "";
}