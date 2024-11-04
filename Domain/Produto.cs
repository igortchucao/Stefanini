namespace Testestefanini.Infra;

public class Produto
{
    public int Id { get; set; }
    public string NomeProduto { get; set; }
    public decimal Valor { get; set; }

    public void SetProduto(Produto produto)
    {
        NomeProduto = produto.NomeProduto;
        Valor = produto.Valor;
    }
}
