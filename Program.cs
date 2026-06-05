using TattooLog.Models;
using TattooLog.Repositories;
using TattooLog.Services;

namespace FichasDigitais
{
    class Program
    {
        public static void Main()
        {
            FichaRepository fichaRepository = new FichaRepository();
            FichaService fichaService = new FichaService(fichaRepository);

            bool executando = true;
            fichaService.Carregar();

            while (executando)
            {
                Menu();

                if (!int.TryParse(Console.ReadLine(), out int opcao))
                {
                    Console.WriteLine("Opção inválida. Digite um numero.");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine();
                        try
                        {
                            Ficha ficha = CriarFicha(fichaService);
                            fichaService.Adicionar(ficha);
                            fichaService.Salvar();
                            Console.WriteLine("\nFicha cadastrada e salva com sucesso!");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"\nErro de validacao: {ex.Message}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 2:
                        Console.WriteLine("Listando fichas...");
                        var fichas = fichaService.Listar();
                        if (fichas.Any())
                        {
                            foreach (var ficha in fichas)
                            {
                                Console.WriteLine(ficha);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhuma ficha cadastrada.");
                        }
                        break;
                    case 3:
                        Console.Write("Nome da pessoa: ");
                        string nomeBusca = Console.ReadLine()!;
                        var fichasEncontradas = fichaService.BuscarPorNome(nomeBusca);

                        if (!string.IsNullOrWhiteSpace(nomeBusca))
                        {
                            foreach (var ficha in fichasEncontradas)
                            {
                                Console.WriteLine(ficha);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhuma ficha encontrada para esse nome.");
                        }
                        break;       
                    case 4:
                        Console.Write("ID da ficha: ");
                        if (!int.TryParse(Console.ReadLine(), out int fichaId))
                        {
                            Console.WriteLine("ID invalido.");
                            continue;
                        }

                        bool removida = fichaService.Remover(fichaId);
                        if (removida)
                        {
                            fichaService.Salvar();
                            Console.WriteLine("Ficha removida.");
                        }
                        else
                            Console.WriteLine("Ficha nao encontrada.");
                        break;

                    case 5:
                        Console.WriteLine("Saindo...");
                        executando = false;
                        break;

                    default:
                        Console.WriteLine("Opcao nao encontrada.");
                        break;
                }
            }
        }

        static Ficha CriarFicha(FichaService fichaService)
        {
            Console.WriteLine("#####  NOVA FICHA  #####");
            Console.WriteLine();

            int fichaId = fichaService.ObterProximoFichaId();
            Console.WriteLine($"ID da ficha gerado: {fichaId}");

            Console.Write("Nome? ");
            string nome = Console.ReadLine()!;

            int pessoaId = fichaService.ObterProximoPessoaId();
            Console.WriteLine($"ID da pessoa gerado: {pessoaId}");

            Console.Write("Telefone? ");
            string telefone = Console.ReadLine()!;

            Console.Write("Email? ");
            string email = Console.ReadLine()!;

            Pessoa pessoa = new Pessoa(pessoaId, nome, telefone, email);

            Console.WriteLine("Tipo do procedimento:");
            Console.WriteLine("1 - Tatuagem");
            Console.WriteLine("2 - Piercing");
            Console.Write("Opcao? ");
            if (!int.TryParse(Console.ReadLine(), out int tipoEscolhido) ||
                !Enum.IsDefined(typeof(TipoProcedimento), tipoEscolhido))
            {
                throw new ArgumentException("Tipo de procedimento invalido.");
            }

            TipoProcedimento tipoProcedimento = (TipoProcedimento)tipoEscolhido;

            Console.Write("Local do corpo? ");
            string localDoCorpo = Console.ReadLine()!;

            Console.Write("Descricao do procedimento? ");
            string descricaoProcedimento = Console.ReadLine()!;

            Console.Write("Tem alergia? (S/N) ");
            bool temAlergia = LerRespostaSimNao("Resposta de alergia invalida.");

            Console.Write("Observacoes de saude: ");
            string observacoesSaude = Console.ReadLine()!;

            Console.Write("Pessoa aceitou os termos? (S/N) ");
            bool aceitouTermos = LerRespostaSimNao("Resposta dos termos invalida.");

            Console.WriteLine();
            return new Ficha(
                fichaId,
                pessoa,
                tipoProcedimento,
                localDoCorpo,
                descricaoProcedimento,
                temAlergia,
                observacoesSaude,
                aceitouTermos);
        }

        static bool LerRespostaSimNao(string mensagemErro)
        {
            string? respostaTexto = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(respostaTexto))
                throw new ArgumentException(mensagemErro);

            char resposta = char.ToLower(respostaTexto[0]);

            if (resposta == 's') return true;
            if (resposta == 'n') return false;

            throw new ArgumentException(mensagemErro);
        }

        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Cadastrar ficha");
            Console.WriteLine("2 - Listar fichas");
            Console.WriteLine("3 - Buscar fichas por pessoa");
            Console.WriteLine("4 - Remover ficha");
            Console.WriteLine("5 - Sair");
            Console.WriteLine();
        }
    }
}
