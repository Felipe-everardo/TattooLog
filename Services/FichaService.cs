using TattooLog.Interfaces;
using TattooLog.Models;

namespace TattooLog.Services
{
    public class FichaService : IFichaService
    {
        private readonly IFichaRepository _fichaRepository;
        private List<Ficha> _fichas = new();

        public FichaService(IFichaRepository fichaRepository)
        {
            _fichaRepository = fichaRepository;
        }

        public void Carregar()
        {
            _fichas = _fichaRepository.Carregar();
        }

        public void Salvar()
        {
            _fichaRepository.Salvar(_fichas);
        }

        public void Adicionar(Ficha ficha)
        {
            if (_fichas.Any(f => f.Id == ficha.Id))
            {
                throw new InvalidOperationException("Ja existe ficha com esse ID.");
            }

            if (!ficha.AceitouTermos)
            {
                throw new InvalidOperationException("A ficha so pode ser cadastrada se a pessoa aceitar os termos.");
            }

            _fichas.Add(ficha);
        }

        public int ObterProximoFichaId()
        {
            if (!_fichas.Any())
                return 1;

            return _fichas.Max(f => f.Id) + 1;
        }

        public int ObterProximoPessoaId()
        {
            if (!_fichas.Any())
                return 1;

            return _fichas.Max(f => f.Pessoa.Id) + 1;
        }

        public Ficha? Buscar(int id)
        {
            return _fichas.FirstOrDefault(f => f.Id == id);
        }

        public List<Ficha> BuscarPorNome(string nome)
        {
            return _fichas.Where(f => f.Pessoa.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Ficha> Listar()
        {
            return _fichas;
        }

        public List<Ficha> ListarPorPessoa(int pessoaId)
        {
            return _fichas.Where(f => f.Pessoa.Id == pessoaId).ToList();
        }

        public bool Remover(int id)
        {
            Ficha? ficha = Buscar(id);

            if (ficha == null)
                return false;

            _fichas.Remove(ficha);
            return true;
        }
    }
}
