namespace ProdutosApi.Repositories
{
    public interface IRepositoryWrapper
    {
        ICategoriaRepository CategoriaRepo { get; }
        IProdutoRepository ProdutoRepo { get; }
        Task SaveAsync();
    }
}
