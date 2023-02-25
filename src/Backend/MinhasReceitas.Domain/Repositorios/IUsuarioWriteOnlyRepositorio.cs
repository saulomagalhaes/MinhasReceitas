using MinhasReceitas.Domain.Entidades;

namespace MinhasReceitas.Domain.Repositorios;

public interface IUsuarioWriteOnlyRepositorio
{
    Task Adicionar(Usuario usuario);
}
