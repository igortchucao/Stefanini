using Testestefanini.Infra;

namespace Testestefanini.Application.Interfaces
{
    public interface IProdutosApplication
    {
        Task CreateProdutos(Produto? produto);
        Task DeleteProduto(uint produto);
        Task<List<Produto>> ReadProduto(uint? idProduto);
        Task UpdateProduto(Produto pedido);
    }
}