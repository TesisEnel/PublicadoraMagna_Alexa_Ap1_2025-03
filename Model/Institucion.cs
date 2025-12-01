using PublicadoraMagna.Data;
using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class Institucion
{
    [Key]
    public int InstitucionId { get; set; }
   
    [Required(ErrorMessage = "El nombre de la institución es requerido")]
    [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public string Nombre { get; set; }
    [StringLength(50, ErrorMessage = "El RNC no puede exceder 50 caracteres")]
    public string Rnc { get; set; }
    [Required(ErrorMessage = "El email del administrador es requerido")]
    [EmailAddress(ErrorMessage = "El email no es válido")]

    public string EmailAdmin { get; set; }
    public string CorreoContacto { get; set; }
    [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
    public string Telefono { get; set; }
    public DateTime FechaRegistro { get; set; }
    public List<Articulo> Articulos { get; set; } = new();
    public ICollection<ApplicationUser> Usuarios { get; set; } =new List<ApplicationUser>();
}
