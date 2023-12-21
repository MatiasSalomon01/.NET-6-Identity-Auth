using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatorio")]
    [StringLength(50, ErrorMessage = "El {0}, debe estar entre al menos {2} caractereres de longitud", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; }

    [Required(ErrorMessage = "La confirmación contraseña es obligatorio")]
    [Compare("Password", ErrorMessage = "La contraseña y confirmacion no coinciden")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Contraseña")]
    public string ConfirmPassword { get; set; }
    public string Nombre { get; set; }
    public string Url { get; set; }
    public int CodigoPais { get; set; }
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El país es obligatorio")]
    public string Pais { get; set; }
    public string Ciudad { get; set; }
    public string Direccion { get; set; }

    [Display(Name = "Fecha de Nacimiento")]
    [Required(ErrorMessage = "La fecha de nacimiento es obligatorio")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    public bool Estado { get; set; }
}
