using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class DetallePagoInstitucion
{
    [Key]
    public int DetallePagoInstitucionId { get; set; }
    public int PagoInstitucionId { get; set; }
    public int ArticuloId { get; set; }
    public decimal Monto { get; set; }
    public PagoInstitucion Pago { get; set; } = null!;
    public Articulo Articulo { get; set; } = null!;

}
