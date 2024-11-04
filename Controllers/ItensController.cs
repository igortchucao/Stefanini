using Microsoft.AspNetCore.Mvc;
using Testestefanini.Application.Interfaces;
using Testestefanini.Infra;

namespace Testestefanini.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItensPedidosController : ControllerBase
    {
        private readonly ILogger<ItensPedidosController> _logger;
        private IItensApplication _itensPedidoApplication;

        public ItensPedidosController(ILogger<ItensPedidosController> logger, IItensApplication podutoApplication)
        {
            _logger = logger;
            _itensPedidoApplication = podutoApplication;
        }

        /// <summary>
        /// Recebe o item e seus itens. Contando com valor total também
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      GET  /ItensPedidos?idPedido=1 (Para receber todos)
        ///      GET  /ItensPedidos?idPedido=1&idItensPedido=1 (Para receber apenas o itensPedido 1)
        ///        
        /// </remarks>
        /// <param name="request">Id do ItensPedido.</param>
        [HttpGet(Name = "ItensPedidos")]
        public async Task<IActionResult> Get([FromQuery] uint idPedido, [FromQuery]uint? idItensPedido)
        {
            var itensPedidos = await _itensPedidoApplication.ReadItem(idPedido, idItensPedido);
            return Ok(itensPedidos);
        }

        /// <summary>
        /// Edita o item conforme body. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      POST  /ItensPedidos 
        ///        
        /// </remarks>
        [HttpPost(Name = "ItensPedidos")]
        public async Task<IActionResult> Post([FromBody] ItensPedido itensPedido)
        {
            await _itensPedidoApplication.UpdateItem(itensPedido);
            return Ok(itensPedido);
        }

        /// <summary>
        /// Cria um novo Item. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      PUT  /ItensPedidos 
        ///        
        /// </remarks>
        [HttpPut(Name = "ItensPedidos")]
        public async Task<IActionResult> Put([FromBody] ItensPedido itensPedido)
        {
            await _itensPedidoApplication.CreateItens(itensPedido);
            return Ok(itensPedido);
        }

        /// <summary>
        /// Deleta um item existente. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      DELETE  /ItensPedidos?idItensPedido=1
        ///        
        /// </remarks>
        [HttpDelete(Name = "ItensPedidos")]
        public async Task<IActionResult> Delete([FromQuery] uint idItensPedido)
        {
            await _itensPedidoApplication.DeleteItem(idItensPedido);
            return Ok();
        }
    }
}
