using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class PeriodistaService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
  
    public async Task<bool> Guardar(Periodista periodista)
    {
       
        if (string.IsNullOrWhiteSpace(periodista.Nombres))
            throw new Exception("El nombre del periodista es requerido");
        if (periodista.TarifaBase < 0)
            throw new Exception("La tarifa no puede ser negativa");

        if (!await Existe(periodista.PeriodistaId))
        {
            return await Insertar(periodista);
        }
        else
        {
            return await Modificar(periodista);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var entidad = await contexto.Periodistas
            .Include(p => p.Articulos)
            .FirstOrDefaultAsync(p => p.PeriodistaId == id);

        if (entidad == null) return false;

        if (entidad.Articulos != null && entidad.Articulos.Any())
            throw new Exception("No se puede eliminar un periodista que tiene artículos publicados");

        contexto.Periodistas.Remove(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

 
    public async Task<bool> CambiarEstado(int id, bool esActivo)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var periodista = await contexto.Periodistas.FindAsync(id);

        if (periodista == null) return false;

     
        periodista.EsActivo = esActivo;
        contexto.Periodistas.Update(periodista);
        return await contexto.SaveChangesAsync() > 0;
    }

   
    public async Task<Periodista?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas
            .Include(p => p.Articulos)
            .FirstOrDefaultAsync(p => p.PeriodistaId == id);
    }

    public async Task<List<Periodista>> Listar()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas
            .Include(p => p.Articulos)
            .ToListAsync();
    }

    public async Task<List<Periodista>> ConsultarPorFecha(DateTime fechaDesde, DateTime fechaHasta)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas
            .Where(p => p.FechaRegistro >= fechaDesde && p.FechaRegistro <= fechaHasta)
            .ToListAsync();
    }


    private async Task<bool> Insertar(Periodista periodista)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        periodista.FechaRegistro = DateTime.Now;
     
    
        contexto.Periodistas.Add(periodista);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Periodista periodista)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Periodistas.Update(periodista);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas.AnyAsync(a => a.PeriodistaId == id);
    }
}


