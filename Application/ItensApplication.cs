using Microsoft.EntityFrameworkCore;
using Testestefanini.Application.Interfaces;
using Testestefanini.Infra;
using Testestefanini.Infrastructure.Exceptions;

namespace Testestefanini.Application;

public class ItensApplication : IItensApplication
{
    private readonly ApplicationDbContext _context;

    public ItensApplication(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateItens(ItensPedido? item)
    {
        if (item == null)
            throw new BadRequestException("Item Inválido.");

        await _context.ItensPedido.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ItensPedido>> ReadItem(uint idPedido, uint? idItem)
    {
        List<ItensPedido> produtos = new List<ItensPedido>();

        if (idItem is not null)
        {
            produtos = await _context.ItensPedido
                            .Where(p => p.Id == idItem && p.IdPedido == idPedido)
                            .ToListAsync();
        }
        else
        {
            produtos = await _context.ItensPedido
                            .Where(p => p.IdPedido == idPedido)
                            .ToListAsync();
        }

        if (produtos.Count <= 0)
            throw new NotFoundException("Item não encontrado.");

        return produtos;
    }

    public async Task UpdateItem(ItensPedido idItem)
    {
        var pedidoToPost = await _context.ItensPedido
                            .Where(p => p.Id == idItem.Id)
                            .FirstOrDefaultAsync();

        if (pedidoToPost != null)
        {
            pedidoToPost.SetItem(idItem);
            _context.SaveChanges();
        }
        else
            throw new NotFoundException("Item não encontrado.");
    }

    public async Task DeleteItem(uint idItem)
    {
        var produtoToDelete = await _context.ItensPedido
                            .Where(p => p.Id == idItem)
                            .FirstOrDefaultAsync();

        if (produtoToDelete != null)
        {
            _context.ItensPedido.Remove(produtoToDelete);
            _context.SaveChanges();
        }
        else
            throw new NotFoundException("Item não encontrado.");
    }
}
