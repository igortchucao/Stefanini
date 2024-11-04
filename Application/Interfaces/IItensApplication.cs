using Testestefanini.Infra;

namespace Testestefanini.Application.Interfaces
{
    public interface IItensApplication
    {
        Task CreateItens(ItensPedido? item);
        Task DeleteItem(uint idItem);
        Task<List<ItensPedido>> ReadItem(uint idPedido, uint? idItem);
        Task UpdateItem(ItensPedido idItem);
    }
}