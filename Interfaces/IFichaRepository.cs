using TattooLog.Models;

namespace TattooLog.Interfaces
{
    public interface IFichaRepository
    {
        void Salvar(List<Ficha> fichas);
        List<Ficha> Carregar();
    }
}
