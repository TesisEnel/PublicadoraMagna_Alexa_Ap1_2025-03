using Microsoft.AspNetCore.Identity;

namespace PublicadoraMagna.Model;


public class ApplicationUser : IdentityUser
{
    public int? InstitucionId { get; set; }
    public Institucion? Institucion { get; set; }

    public int? PeriodistaId { get; set; }
    public Periodista? Periodista { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public DateTime FechaRegistro {  get; set; }
}