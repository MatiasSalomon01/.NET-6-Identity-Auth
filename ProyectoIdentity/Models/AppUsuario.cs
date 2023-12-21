using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoIdentity.Models;

public class AppUsuario : IdentityUser
{
    public string Nombre { get; set; }
    public string Url { get; set; }
    public int CodigoPais { get; set; }
    public string Telefono { get; set; }
    public string Pais { get; set; }
    public string Ciudad { get; set; }
    public string Direccion { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime FechaNacimiento { get; set; }
    public bool Estado { get; set; }
}
