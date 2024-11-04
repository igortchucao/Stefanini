using Microsoft.EntityFrameworkCore;
using Testestefanini.Application.Interfaces;
using Testestefanini.Domain.DTO;
using Testestefanini.Infra;
using Testestefanini.Infrastructure.Exceptions;

namespace Testestefanini.Application;

public class PedidoApplication : IPedidoApplication
{
    private readonly ApplicationDbContext _context;

    public PedidoApplication(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreatePedido(Pedido? pedido)
    {
        if (pedido == null)
            throw new BadRequestException("Pedido Inválido.");

        await _context.Pedido.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<List<GetPedidoComItens>> ReadPedido(uint? idPedido)
    {
        List<GetPedidoComItens> pedidos = new List<GetPedidoComItens>();

        if (idPedido is not null)
        {
            pedidos = await _context.Pedido
                            .Include(x => x.Itens)
                            .ThenInclude(x => x.Produto)
                            .Where(p => p.Id == idPedido)
                            .Select(x => new GetPedidoComItens(x))
                            .ToListAsync();
        }
        else
        {
            pedidos = await _context.Pedido
                            .Include(x => x.Itens)
                            .ThenInclude(x => x.Produto)
                            .Select(x => new GetPedidoComItens(x))
                            .ToListAsync();
        }

        if (pedidos.Count <= 0)
            throw new NotFoundException("Pedido não encontrado.");

        return pedidos;

    }

    public async Task UpdatePedido(Pedido pedido)
    {
        var pedidoToPost = await _context.Pedido
                            .Where(p => p.Id == pedido.Id)
                            .FirstOrDefaultAsync();

        if (pedidoToPost != null)
        {
            pedidoToPost.SetPedido(pedido);
            _context.SaveChanges();
        }
        else
            throw new NotFoundException("Pedido não encontrado.");
    }

    public async Task DeletePedido(uint pedido)
    {
        var pedidoToPost = await _context.Pedido
                            .Where(p => p.Id == pedido)
                            .FirstOrDefaultAsync();

        if (pedidoToPost != null)
        {
            _context.Pedido.Remove(pedidoToPost);
            _context.SaveChanges();
        }
        else
            throw new NotFoundException("Pedido não encontrado.");
    }
}
