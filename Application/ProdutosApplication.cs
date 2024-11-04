using Microsoft.EntityFrameworkCore;
using Testestefanini.Application.Interfaces;
using Testestefanini.Domain.DTO;
using Testestefanini.Infra;
using Testestefanini.Infrastructure.Exceptions;

namespace Testestefanini.Application;

public class ProdutosApplication : IProdutosApplication
{
    private readonly ApplicationDbContext _context;

    public ProdutosApplication(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateProdutos(Produto? produto)
    {
        if (produto == null)
            throw new BadRequestException("Produto Inválido.");

        await _context.Produto.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Produto>> ReadProduto(uint? idProduto)
    {
        List<Produto> produtos = new List<Produto>();

        if (idProduto is not null)
        {
            produtos = await _context.Produto
                            .Where(p => p.Id == idProduto)
                            .ToListAsync();
        }
        else
        {
            produtos = await _context.Produto
                            .ToListAsync();
        }

        if (produtos.Count <= 0)
            throw new NotFoundException("Produto não encontrado.");

        return produtos;
    }

    public async Task UpdateProduto(Produto pedido)
    {
        var pedidoToPost = await _context.Produto
                            .Where(p => p.Id == pedido.Id)
                            .FirstOrDefaultAsync();

        if (pedidoToPost != null)
        {
            pedidoToPost.SetProduto(pedido);
            _context.SaveChanges();
        }
        else
            throw new NotFoundException("Produto não encontrado.");
    }

    public async Task DeleteProduto(uint produto)
    {
        var produtoToDelete = await _context.Produto
                            .Where(p => p.Id == produto)
                            .FirstOrDefaultAsync();

        if (produtoToDelete != null)
        {
            _context.Produto.Remove(produtoToDelete);
            _context.SaveChanges();
        }
        else
            throw new NotFoundException("Produto não encontrado.");
    }
}
