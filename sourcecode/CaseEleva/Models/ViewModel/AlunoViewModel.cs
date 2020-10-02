namespace CaseEleva.Models.ViewModel
{
    public class AlunoViewModel : BaseViewModel
    {
        public int? Id { get; set; }
        public int? TurmaId { get; set; }
        public int? EscolaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Turma Turma { get; set; }
    }
}