namespace TattooLog.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        private string _nome = string.Empty;
        public string Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O Nome é obrigatório");
                }
                _nome = value;
            }
        }

        private string _telefone = string.Empty;
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == default)
                {
                    throw new ArgumentException("Número de telefone obrigatório.");
                }
                else
                {
                    _telefone = value;
                }
            }
        }
        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains('@') || !value.Contains('.'))
                {
                    throw new ArgumentException("O email deve contar @ e .");
                }
                else
                {
                    _email = value;
                }
            }
        }

        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        public Pessoa() { }

        public Pessoa(int id, string name, string telefone, string email)
        {
            Id = id;
            Nome = name;
            Telefone = telefone;
            Email = email;
        }
    }
}
