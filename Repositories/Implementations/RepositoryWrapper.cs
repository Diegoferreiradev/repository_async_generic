using ProdutosApi.DbContexts;

namespace ProdutosApi.Repositories.Implementations
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _repoContext;
        private ICategoriaRepository _categoria;
        private IProdutoRepository _produto;

        public ICategoriaRepository CategoriaRepo
        {
            get 
            {
                if (_categoria == null)
                {
                    _categoria = new CategoriaRepository(_repoContext);
                }
                return _categoria;
            }
        }

        public IProdutoRepository ProdutoRepo
        {
            get
            {
                if (_produto == null)
                {
                    _produto = new ProdutoRepository(_repoContext);
                }
                return _produto;
            }
        }

        public RepositoryWrapper(AppDbContext repoContext)
        {
            _repoContext = repoContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
