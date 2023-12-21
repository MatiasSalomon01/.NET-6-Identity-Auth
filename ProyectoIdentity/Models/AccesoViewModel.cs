using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models;

public class AccesoViewModel
{
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatorio")]
    [StringLength(50, ErrorMessage = "El {0}, debe estar entre al menos {2} caractereres de longitud", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; }

    [Display(Name = "Recordar datos?")]
    public bool RememberMe { get; set; }
}
