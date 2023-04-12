using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
using ProdutosApi.Repositories;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly IRepositoryWrapper _repoContext;

        public ProdutosController(ILogger<ProdutosController> logger, IRepositoryWrapper repoContext)
        {
            _logger = logger;
            _repoContext = repoContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            try
            {
                var produtos = await _repoContext.ProdutoRepo.GetallProdutosAsync();
                _logger.LogInformation($"Retornou todos os produtos do banco");
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar GetProdutos: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ProdutoById")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto = await _repoContext.ProdutoRepo.GetProdutoByIdAsync(id);

            if (produto == null)
            {
                _logger.LogError($"Produto com id: {id}, não encontrado no banco.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Retornado produto com id: {id}");
                return Ok(produto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    _logger.LogError("Objeto produto enviado ao cliente é nulo.");
                    return BadRequest("Objeto produto é null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Objeto produto inválido.");
                    return BadRequest("Objeto produto inválido.");
                }

                _repoContext.ProdutoRepo.CreateProduto(produto);
                await _repoContext.SaveAsync();

                return CreatedAtRoute("ProdutoById", new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar CreateProduto: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    _logger.LogError($"Objeto produto enviado ao cliente é nulo.");
                    return BadRequest("Objeto produto é null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Objeto produto inválido.");
                    return BadRequest("Objeto produto inválido.");
                }

                var produtoEntity = await _repoContext.ProdutoRepo.GetProdutoByIdAsync(id);
                if (produtoEntity == null)
                {
                    _logger.LogError($"Produto com id: {id}, não encontrado no banco.");
                    return NotFound();
                }

                _repoContext.ProdutoRepo.UpdateProduto(produtoEntity);
                await _repoContext.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar UpdateProduto: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var produto = await _repoContext.ProdutoRepo.GetProdutoByIdAsync(id);
                if (produto == null)
                {
                    _logger.LogError($"Produto com id: {id}, não encontrado.");
                    return NoContent();
                }

                _repoContext.ProdutoRepo.DeleteProduto(produto);
                await _repoContext.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar DeleteProduto: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
