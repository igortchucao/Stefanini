using Microsoft.AspNetCore.Mvc;
using Testestefanini.Application.Interfaces;
using Testestefanini.Infra;

namespace Testestefanini.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private IProdutosApplication _produtoApplication;

        public ProdutosController(ILogger<ProdutosController> logger, IProdutosApplication podutoApplication)
        {
            _logger = logger;
            _produtoApplication = podutoApplication;
        }

        /// <summary>
        /// Recebe o produto e seus itens. Contando com valor total também
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      GET  /Produtos (Para receber todos)
        ///      GET  /Produtos?idProduto=1 (Para receber apenas o produto 1)
        ///        
        /// </remarks>
        /// <param name="request">Id do Produto.</param>
        [HttpGet(Name = "Produtos")]
        public async Task<IActionResult> Get([FromQuery]uint? idProduto)
        {
            var produtos = await _produtoApplication.ReadProduto(idProduto);
            return Ok(produtos);
        }

        /// <summary>
        /// Edita o produto conforme body. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      POST  /Produtos 
        ///        
        /// </remarks>
        [HttpPost(Name = "Produtos")]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            await _produtoApplication.UpdateProduto(produto);
            return Ok(produto);
        }

        /// <summary>
        /// Cria um novo produto. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      PUT  /Produtos 
        ///        
        /// </remarks>
        [HttpPut(Name = "Produtos")]
        public async Task<IActionResult> Put([FromBody] Produto produto)
        {
            await _produtoApplication.CreateProdutos(produto);
            return Ok(produto);
        }

        /// <summary>
        /// Deleta um produto existente. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      DELETE  /Produtos?idProduto=1
        ///        
        /// </remarks>
        [HttpDelete(Name = "Produtos")]
        public async Task<IActionResult> Delete([FromQuery] uint idProduto)
        {
            await _produtoApplication.DeleteProduto(idProduto);
            return Ok();
        }
    }
}
