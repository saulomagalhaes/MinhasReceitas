using Microsoft.EntityFrameworkCore;
using MinhasReceitas.Domain.Entidades;
using MinhasReceitas.Domain.Repositorios;

namespace MinhasReceitas.Infrastructure.AcessoRepositorio.Repositorio;

public class UsuarioRepositorio : IUsuarioReadOnlyRepositorio, IUsuarioWriteOnlyRepositorio
{
    private readonly MinhasReceitasContext _contexto;

    public UsuarioRepositorio(MinhasReceitasContext contexto)
    {
        _contexto = contexto;
    }

    public async Task Adicionar(Usuario usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> ExisteUsuarioComEmail(string email)
    {
        return await _contexto.Usuarios.AnyAsync(u => u.Email == email);
    }
}
