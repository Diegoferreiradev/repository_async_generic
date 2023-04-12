using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
using ProdutosApi.Repositories;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly IRepositoryWrapper _repoContext;

        public CategoriasController(ILogger<CategoriasController> logger, IRepositoryWrapper repoContext)
        {
            _logger = logger;
            _repoContext = repoContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                var categorias = await _repoContext.CategoriaRepo.GetAllCategoriasAsync();
                _logger.LogInformation($"Retornou todas as categorias do banco");
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar GetCategorias: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "CategoriaById")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            var categoria = await _repoContext.CategoriaRepo.GetCategoriaByIdAsync(id);

            if (categoria == null)
            {
                _logger.LogError($"Categoria com id: {id}, não encontrado no banco.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Retornado categoria com id: {id}");
                    return Ok(categoria);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    _logger.LogError($"Objeto categoria enviado ao cliente é nulo.");
                    return BadRequest("Objeto categoria inválido");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Objeto categoria inválido.");
                    return BadRequest("Objeto categoria inválido");
                }

                _repoContext.CategoriaRepo.CreateCategoria(categoria);
                await _repoContext.SaveAsync();

                return CreatedAtRoute("CategoriaById", new { id = categoria.CategoriaId }, categoria);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar CreateCategoria: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    _logger.LogError($"Objeto categoria enviado ao cliente é nulo.");
                    return BadRequest("Objeto categoria é null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Objeto categoria inválido.");
                    return BadRequest("Objeto categoria inválido");
                }

                var categoriaEntity = await _repoContext.CategoriaRepo.GetCategoriaByIdAsync(id);
                if (categoriaEntity == null)
                {
                    _logger.LogError($"Categoria com id: {id}, não encontrado no banco.");
                    return NotFound();
                }

                _repoContext.CategoriaRepo.UpdateCategoria(categoriaEntity);
                await _repoContext.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar UpdateCategoria: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _repoContext.CategoriaRepo.GetCategoriaByIdAsync(id);
                if (categoria == null)
                {
                    _logger.LogError($"Categoria com id: {id}, não encontrado.");
                    return NoContent();
                }

                _repoContext.CategoriaRepo.DeleteCategoria(categoria);
                await _repoContext.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar DeleteCategoria: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
