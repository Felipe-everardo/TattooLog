namespace TattooLog.Models
{
    public class Ficha
    {
        public int Id { get; set; }
        public Pessoa Pessoa { get; set; } = new Pessoa();
        public TipoProcedimento TipoProcedimento { get; set; }
        public bool TemAlergia { get; set; }
        public string? ObservacoesSaude { get; set; }
        public bool AceitouTermos { get; set; }
        public DateTime DataPreenchimento { get; private set; } = DateTime.Now;

        private string _localDoCorpo = string.Empty;
        private string _descricaoProcedimento = string.Empty;

        public Ficha() { }

        public Ficha(
            int id,
            Pessoa pessoa,
            TipoProcedimento tipoProcedimento,
            string localDoCorpo,
            string descricaoProcedimento,
            bool temAlergia,
            string? observacoesSaude,
            bool aceitouTermos)
        {
            Id = id;
            Pessoa = pessoa;
            TipoProcedimento = tipoProcedimento;
            LocalDoCorpo = localDoCorpo;
            DescricaoProcedimento = descricaoProcedimento;
            TemAlergia = temAlergia;
            ObservacoesSaude = observacoesSaude;
            AceitouTermos = aceitouTermos;
        }

        public string LocalDoCorpo
        {
            get { return _localDoCorpo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O local do corpo é obrigatório.");
                }

                _localDoCorpo = value;
            }
        }

        public string DescricaoProcedimento
        {
            get { return _descricaoProcedimento; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A descrição do procedimento e obrigatória.");
                }
                
                _descricaoProcedimento = value;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Pessoa: {Pessoa.Nome}, Tel: {Pessoa.Telefone}, Email: {Pessoa.Email}, " +
                $"Procedimento: {TipoProcedimento}, Local: {LocalDoCorpo}, Descricao: {DescricaoProcedimento}, Alergia: {TemAlergia}, " +
                $"Observacoes: {ObservacoesSaude}, Aceitou Termos: {AceitouTermos}, Data: {DataPreenchimento}";
        }
    }
}
