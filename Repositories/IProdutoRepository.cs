using ProdutosApi.Models;

namespace ProdutosApi.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<bool> IsProdutoAtivo(string nome);
        Task<IEnumerable<Produto>> GetallProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        void CreateProduto(Produto produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(Produto produto);
    }
}
