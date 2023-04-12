using Microsoft.EntityFrameworkCore;
using ProdutosApi.DbContexts;
using ProdutosApi.Models;

namespace ProdutosApi.Repositories.Implementations
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync()
        {
            return await FindAll()
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await FindByCondition(c => c.CategoriaId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Categoria> GetCategoriaWithDetailsAsync(int id)
        {
            return await FindByCondition(c => c.CategoriaId.Equals(id))
                .Include(ac => ac.Produtos)
                .FirstOrDefaultAsync();
        }

        public void CreateCategoria(Categoria categoria)
        {
            Create(categoria);
        }

        public void DeleteCategoria(Categoria categoria)
        {
            Delete(categoria);
        }

        public void UpdateCategoria(Categoria categoria)
        {
            Update(categoria);
        }
    }
}
