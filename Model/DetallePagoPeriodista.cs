using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class DetallePagoPeriodista
{
    [Key]
    public int DetallePagoPeriodistaId { get; set; }
    public int PagoPeriodistaId { get; set; }
    public int ArticuloId { get; set; }
    public decimal Monto { get; set; }

    public PagoPeriodista Pago { get; set; } = null!;
    public Articulo Articulo { get; set; } = null!;
}

