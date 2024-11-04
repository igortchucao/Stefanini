using Microsoft.EntityFrameworkCore;
using Testestefanini.Application;
using Testestefanini.Infra;
using Testestefanini.Infrastructure.Exceptions;

public class PedidoApplicationTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PedidoApplication _pedidoApplication;

    public PedidoApplicationTests()
    {
        // Configuração do DbContext para usar o banco de dados em memória
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _pedidoApplication = new PedidoApplication(_context);
    }

    [Fact]
    public async Task CreatePedido_ShouldThrowBadRequestException_WhenPedidoIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _pedidoApplication.CreatePedido(null));
    }

    [Fact]
    public async Task CreatePedido_ShouldAddPedido()
    {
        // Limpa o banco para garantir que não há pedidos anteriores
        _context.Pedido.RemoveRange(_context.Pedido);
        await _context.SaveChangesAsync();

        // Arrange
        var pedido = new Pedido { Id = 1, NomeCliente = "Nome", EmailCliente = "Email", DataCriacao = DateTime.Now, Pago = true };

        // Act
        await _pedidoApplication.CreatePedido(pedido);

        // Assert
        Assert.Single(_context.Pedido);
        Assert.Equal(pedido.Id, _context.Pedido.First().Id);
    }

    [Fact]
    public async Task ReadPedido_ShouldThrowNotFoundException_WhenPedidoNotFound()
    {

        // Limpa o banco para garantir que não há pedidos anteriores
        _context.Pedido.RemoveRange(_context.Pedido);
        await _context.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _pedidoApplication.ReadPedido(1));
    }

    [Fact]
    public async Task ReadPedido_ShouldReturnPedidoList_WhenPedidoExists()
    {
        // Limpa o banco para garantir que não há pedidos anteriores
        _context.Pedido.RemoveRange(_context.Pedido);
        await _context.SaveChangesAsync();

        // Arrange
        var pedido = new Pedido { Id = 1, NomeCliente = "Nome", EmailCliente = "Email", DataCriacao = DateTime.Now, Pago = true };
        await _context.Pedido.AddAsync(pedido);
        await _context.SaveChangesAsync();

        // Act
        var result = await _pedidoApplication.ReadPedido(1);

        // Assert
        Assert.Single(result);
        Assert.Equal(pedido.Id, result.First().id);
    }

    [Fact]
    public async Task UpdatePedido_ShouldThrowNotFoundException_WhenPedidoNotFound()
    {
        // Arrange
        var pedido = new Pedido { Id = 1, NomeCliente = "Nome", EmailCliente = "Email", DataCriacao = DateTime.Now, Pago = true };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _pedidoApplication.UpdatePedido(pedido));
    }

    [Fact]
    public async Task UpdatePedido_ShouldUpdatePedido_WhenPedidoExists()
    {

        // Limpa o banco para garantir que não há pedidos anteriores
        _context.Pedido.RemoveRange(_context.Pedido);
        await _context.SaveChangesAsync();

        // Arrange
        var pedido = new Pedido { Id = 1, NomeCliente = "Nome", EmailCliente = "Email", DataCriacao = DateTime.Now, Pago = true };
        await _context.Pedido.AddAsync(pedido);
        await _context.SaveChangesAsync();

        // Modifica o pedido
        pedido.NomeCliente = "Pedido Atualizado";

        // Act
        await _pedidoApplication.UpdatePedido(pedido);

        // Assert
        var pedidoAtualizado = _context.Pedido.First();
        Assert.Equal("Pedido Atualizado", pedidoAtualizado.NomeCliente);
    }

    [Fact]
    public async Task DeletePedido_ShouldThrowNotFoundException_WhenPedidoNotFound()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _pedidoApplication.DeletePedido(1));
    }

    [Fact]
    public async Task DeletePedido_ShouldRemovePedido_WhenPedidoExists()
    {
        // Arrange
        var pedido = new Pedido { Id = 1, NomeCliente = "Nome", EmailCliente = "Email", DataCriacao = DateTime.Now, Pago = true };
        await _context.Pedido.AddAsync(pedido);
        await _context.SaveChangesAsync();

        // Act
        await _pedidoApplication.DeletePedido(1);

        // Assert
        Assert.Empty(_context.Pedido);
    }

    public void Dispose()
    {
        // Libera os recursos do contexto
        _context.Dispose();
    }
}
