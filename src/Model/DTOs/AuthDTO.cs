using System.ComponentModel.DataAnnotations;

namespace TurnosApi.Models.DTOs;

public class RegistroDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MinLength(2, ErrorMessage = "Mínimo 2 caracteres")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MinLength(2, ErrorMessage = "Mínimo 2 caracteres")]
    public string Apellido { get; set; } = "";

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Debe confirmar la contraseña")]
    public string ConfirmarPassword { get; set; } = "";
}

public class LoginDTO
{

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = "";
}

public class AuthResponseDTO
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Email { get; set; } = "";
    public string Rol { get; set; } = "";
    public DateTime? UltimoLogin { get; set; }
}

public class RefreshTokenDTO
{
    public string RefreshToken { get; set; } = "";
}

public class PerfilResponseDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Email { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Rol { get; set; } = "";
    public DateTime FechaRegistro { get; set; }
    public DateTime? UltimoLogin { get; set; }
}

public class ActualizarPerfilDTO
{
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
}

public class CambiarPasswordDTO
{
    [Required(ErrorMessage = "La contraseña actual es obligatoria")]
    public string PasswordActual { get; set; } = "";

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
    public string NuevoPassword { get; set; } = "";

    [Required(ErrorMessage = "Debe confirmar la contraseña")]
    public string ConfirmarPassword { get; set; } = "";
}