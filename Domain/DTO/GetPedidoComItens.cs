using Testestefanini.Infra;

namespace Testestefanini.Domain.DTO;

public class GetPedidoComItens
{
    public int id { get; set; }
    public string nomeCliente { get; set; }
    public string emailCliente { get; set; }
    public bool pago { get; set; }
    public decimal valorTotal { get; set; }
    public List<GetPedidoItem> itensPedido { get; set; }

    public GetPedidoComItens(Pedido pedido)
    {
        id = pedido.Id;
        nomeCliente = pedido.NomeCliente;
        emailCliente = pedido.EmailCliente;
        pago = pedido.Pago;
        valorTotal = pedido.Itens.Sum(x => x.Quantidade * x.Produto.Valor);

        itensPedido = new List<GetPedidoItem>();

        foreach(var item in pedido.Itens)
            itensPedido.Add(new GetPedidoItem(item));
    }

}

public class GetPedidoItem
{
    public int id { get; set; }
    public int idProduto { get; set; }
    public string nomeProduto { get; set; }
    public decimal valorUnitario { get; set; }
    public int quantidade { get; set; }

    public GetPedidoItem(ItensPedido item)
    {
        id = item.Id;
        idProduto = item.IdProduto;
        nomeProduto = item.Produto.NomeProduto;
        valorUnitario = item.Produto.Valor;
        quantidade = item.Quantidade;
    }
}