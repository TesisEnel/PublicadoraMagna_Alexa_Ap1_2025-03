using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class PagoPeriodista
  {
    [Key]
    public int PagoPeriodistaId { get; set; }
   public int PeriodistaId { get; set; }
   public DateTime Fecha { get; set; }
   public decimal Total { get; set; }

    public string Estado { get; set; }
   
   public Periodista Periodista { get; set; } = null!;
   public List<DetallePagoPeriodista> Detalles { get; set; } = new();
    }


