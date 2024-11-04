using Testestefanini.Domain.DTO;
using Testestefanini.Infra;

namespace Testestefanini.Application.Interfaces
{
    public interface IPedidoApplication
    {
        Task CreatePedido(Pedido? pedido);
        Task DeletePedido(uint pedido);
        Task<List<GetPedidoComItens>> ReadPedido(uint? idPedido);
        Task UpdatePedido(Pedido pedido);
    }
}