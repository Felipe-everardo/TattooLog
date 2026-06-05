using System.Text.Encodings.Web;
using System.Text.Json;
using TattooLog.Interfaces;
using TattooLog.Models;

namespace TattooLog.Repositories
{
    public class FichaRepository : IFichaRepository
    {
        private readonly string _diretorio;
        private readonly string _arquivo = "Fichas.json";

        public FichaRepository()
        {
            _diretorio = ObterDiretorioDados();
        }

        public void Salvar(List<Ficha> fichas)
        {
            if (!Directory.Exists(_diretorio))
            {
                Directory.CreateDirectory(_diretorio);
            }

            string caminho = Path.Combine(_diretorio, _arquivo);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(fichas, options);

            File.WriteAllText(caminho, json);
        }

        public List<Ficha> Carregar()
        {
            string caminho = Path.Combine(_diretorio, _arquivo);

            if (!File.Exists(caminho))
            {
                return new List<Ficha>();
            }

            string jsonString = File.ReadAllText(caminho);

            List<Ficha>? fichas = JsonSerializer.Deserialize<List<Ficha>>(jsonString);

            return fichas ?? new List<Ficha>();
        }

        private static string ObterDiretorioDados()
        {
            DirectoryInfo? diretorioAtual = new DirectoryInfo(AppContext.BaseDirectory);

            while (diretorioAtual != null)
            {
                string caminhoProjeto = Path.Combine(diretorioAtual.FullName, "FichasDigitais.csproj");

                if (File.Exists(caminhoProjeto))
                {
                    return Path.Combine(diretorioAtual.FullName, "Data");
                }

                diretorioAtual = diretorioAtual.Parent;
            }

            return Path.Combine(AppContext.BaseDirectory, "Data");
        }
    }
}
