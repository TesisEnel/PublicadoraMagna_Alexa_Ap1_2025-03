using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class PagoService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
   
    public async Task ProcesarPagosPorAprobacion(int articuloId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

      
        var articulo = await contexto.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Institucion)
            .Include(a => a.Periodista)
            .Include(a => a.ServiciosPromocionales)
                .ThenInclude(asp => asp.ServicioPromocional)
            .FirstOrDefaultAsync(a => a.ArticuloId == articuloId);

        if (articulo == null)
            throw new Exception($"Artículo con ID {articuloId} no encontrado para el procesamiento de pagos.");

        await GenerarPagoInstitucion(articulo, contexto);

      
        await GenerarPagoPeriodista(articulo, contexto);

        await contexto.SaveChangesAsync();
    }

    private async Task GenerarPagoInstitucion(Articulo articulo, ApplicationDbContext contexto)
    {
      
        decimal montoIngreso = articulo.TotalAPagar;

        if (articulo.InstitucionId.HasValue)
        {
          
            var pagoMaestro = await contexto.PagosInstitucion
                .FirstOrDefaultAsync(p => p.InstitucionId == articulo.InstitucionId.Value && p.Estado == "Pendiente" && p.FechaPago.Date == DateTime.Now.Date);

            if (pagoMaestro == null)
            {
                pagoMaestro = new PagoInstitucion
                {
                    InstitucionId = articulo.InstitucionId.Value,
                    FechaPago = DateTime.Now,
                    Monto = 0, 
                    Estado = "Pendiente",
                    DetallesPagos = new List<DetallePagoInstitucion>()
                };
                contexto.PagosInstitucion.Add(pagoMaestro);
            }

         
            var detalle = new DetallePagoInstitucion
            {
                ArticuloId = articulo.ArticuloId,
                Monto = montoIngreso,
                PagoInstitucionId = pagoMaestro.PagoId 
            };

            pagoMaestro.DetallesPagos.Add(detalle);
            pagoMaestro.Monto += montoIngreso;
        }
    }


    private async Task GenerarPagoPeriodista(Articulo articulo, ApplicationDbContext contexto)
    {
        
        decimal montoEgreso = 0m;

       
        if (articulo.PeriodistaId.HasValue)
        {
            var tarifaBase = articulo.Periodista?.TarifaBase ?? 0m;

           
            if (articulo.EsLibre || articulo.InstitucionId.HasValue)
            {
                montoEgreso = tarifaBase;
            }
        }

        if (montoEgreso > 0)
        {
            int periodistaId = articulo.PeriodistaId!.Value;

            
            var pagoMaestro = await contexto.PagosPeriodistas
                .FirstOrDefaultAsync(p => p.PeriodistaId == periodistaId && p.Fecha.Date == DateTime.Now.Date && p.Total < 0); 

            if (pagoMaestro == null)
            {
                pagoMaestro = new PagoPeriodista
                {
                    PeriodistaId = periodistaId,
                    Fecha = DateTime.Now,
                    Total = 0,
                    Detalles = new List<DetallePagoPeriodista>()
                };
                contexto.PagosPeriodistas.Add(pagoMaestro);
            }

            var detalle = new DetallePagoPeriodista
            {
                ArticuloId = articulo.ArticuloId,
                Monto = montoEgreso,
                PagoPeriodistaId = pagoMaestro.PagoPeriodistaId
            };

            pagoMaestro.Detalles.Add(detalle);
            pagoMaestro.Total += montoEgreso; 
        }
    }
}

