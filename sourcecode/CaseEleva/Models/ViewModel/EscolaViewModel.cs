namespace CaseEleva.Models.ViewModel
{
    public class EscolaViewModel : BaseViewModel
    {

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Diretor { get; set; }
        public string Estado { get; set; }
    }
}