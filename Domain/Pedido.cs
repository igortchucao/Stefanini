using System.Text.Json.Serialization;

namespace Testestefanini.Infra;

public class Pedido
{
    public int Id { get; set;  }
    public string NomeCliente { get; set; }
    public string EmailCliente { get; set; }
    public DateTime DataCriacao{ get; set; }
    public bool Pago { get; set; }

    [JsonIgnore]
    public List<ItensPedido>? Itens { get; set; }

    public void SetPedido(Pedido pedido)
    {
        NomeCliente = pedido.NomeCliente;
        EmailCliente = pedido.EmailCliente;
        DataCriacao = pedido.DataCriacao;
        Pago = pedido.Pago;
    }
}
