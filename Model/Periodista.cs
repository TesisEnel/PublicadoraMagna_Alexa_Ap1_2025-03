using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class Periodista
{
    [Key]
    public int PeriodistaId { get; set; }
    public string Nombres { get; set; }
    public bool EsActivo { get; set; } = true;

    //implementar pago al periodista mas adelante
    public decimal TarifaBase { get; set; }

    public DateTime FechaRegistro { get; set; }

    public List<Articulo> Articulos { get; set; } = new();
    }



