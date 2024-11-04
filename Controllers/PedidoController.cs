using Microsoft.AspNetCore.Mvc;
using Testestefanini.Application.Interfaces;
using Testestefanini.Infra;

namespace Testestefanini.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private IPedidoApplication _pedidoApplication;

        public PedidoController(ILogger<PedidoController> logger, IPedidoApplication pedidoApplication)
        {
            _logger = logger;
            _pedidoApplication = pedidoApplication;
        }

        /// <summary>
        /// Recebe o pedido e seus itens. Contando com valor total também
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      GET  /Pedidos (Para receber todos)
        ///      GET  /Pedidos?idPedido=1 (Para receber apenas o pedido 1)
        ///        
        /// </remarks>
        /// <param name="request">Id do Pedido.</param>
        [HttpGet(Name = "Pedidos")]
        public async Task<IActionResult> Get([FromQuery]uint? idPedido)
        {
            var pedidos = await _pedidoApplication.ReadPedido(idPedido);
            return Ok(pedidos);
        }

        /// <summary>
        /// Edita o pedido conforme body. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      POST  /Pedidos 
        ///        
        /// </remarks>
        [HttpPost(Name = "Pedidos")]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            await _pedidoApplication.UpdatePedido(pedido);
            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      PUT  /Pedidos 
        ///        
        /// </remarks>
        [HttpPut(Name = "Pedidos")]
        public async Task<IActionResult> Put([FromBody] Pedido pedido)
        {
            await _pedidoApplication.CreatePedido(pedido);
            return Ok(pedido);
        }

        /// <summary>
        /// Deleta um pedido existente. 
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      DELETE  /Pedidos?idPedido=1
        ///        
        /// </remarks>
        [HttpDelete(Name = "Pedidos")]
        public async Task<IActionResult> Delete([FromQuery] uint idPedido)
        {
            await _pedidoApplication.DeletePedido(idPedido);
            return Ok();
        }
    }
}
