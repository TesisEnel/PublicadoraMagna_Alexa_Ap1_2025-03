using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class InstitucionService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
  
  
    public async Task<bool> Guardar(Institucion institucion)
    {
        if (string.IsNullOrWhiteSpace(institucion.Nombre))
            throw new Exception("El nombre de la institución es requerido");

        if (!await Existe(institucion.InstitucionId))
        {
            return await Insertar(institucion);
        }
        else
        {
            return await Modificar(institucion);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var entidad = await contexto.Instituciones
            .Include(i => i.Articulos)
            .FirstOrDefaultAsync(i => i.InstitucionId == id);

        if (entidad == null) return false;

        if (entidad.Articulos != null && entidad.Articulos.Any())
            throw new Exception("No se puede eliminar una institución que tiene artículos asociados.");

        contexto.Instituciones.Remove(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

  
    public async Task<Institucion?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Instituciones
            .FirstOrDefaultAsync(i => i.InstitucionId == id);
    }

    public async Task<List<Institucion>> Listar()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Instituciones.ToListAsync();
    }


    private async Task<bool> Insertar(Institucion institucion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Instituciones.Add(institucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Institucion institucion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Instituciones.Update(institucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Instituciones.AnyAsync(a => a.InstitucionId == id);
    }
}
