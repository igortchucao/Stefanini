using System.Text.Json.Serialization;

namespace Testestefanini.Infra;

public class ItensPedido
{
    public int Id { get; set; }
    public int IdPedido { get; set; }
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
    [JsonIgnore]
    public Pedido Pedido { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; }

    public void SetItem(ItensPedido item)
    {
        IdPedido = item.IdPedido;
        IdProduto = item.IdProduto;
        Quantidade = item.Quantidade;
    }
}
