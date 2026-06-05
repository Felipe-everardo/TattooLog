using TattooLog.Models;

namespace TattooLog.Interfaces
{
    public interface IFichaService
    {
        void Adicionar(Ficha ficha);
        List<Ficha> Listar();
        List<Ficha> ListarPorPessoa(int pessoaId);
        Ficha? Buscar(int id);
        List<Ficha> BuscarPorNome(string nome);
        bool Remover(int id);
        int ObterProximoFichaId();
        int ObterProximoPessoaId();
        void Carregar();
        void Salvar();
    }
}
