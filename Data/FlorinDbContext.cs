using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Data;

public class FlorinDbContext(DbContextOptions<FlorinDbContext> opt) : DbContext(opt)
{

}
