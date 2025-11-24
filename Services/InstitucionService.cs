using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;

namespace PublicadoraMagna.Services;

public class InstitucionService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
}
