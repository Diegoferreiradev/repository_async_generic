using Microsoft.EntityFrameworkCore;
using ProdutosApi.DbContexts;
using ProdutosApi.Models;

namespace ProdutosApi.Repositories.Implementations
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Produto>> GetallProdutosAsync()
        {
            return await FindAll()
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public Task<Produto> GetProdutoByIdAsync(int id)
        {
            return FindByCondition(p => p.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public void CreateProduto(Produto produto)
        {
            Create(produto);
        }

        public void UpdateProduto(Produto produto)
        {
            Update(produto);
        }

        public void DeleteProduto(Produto produto)
        {
            Delete(produto);
        }

        public Task<bool> IsProdutoAtivo(string nome)
        {
            var resultado = _dbContext.Produtos.Any(p => p.Nome.Equals(nome));
            return Task.FromResult(resultado);
        }
    }
}
