namespace CaseEleva.Models.ViewModel
{
    public class TurmaViewModel : BaseViewModel
    {
        public int? Id { get; set; }
        public int? EscolaId { get; set; }
        public string Professor { get; set; }
        public int? TotalAlunos { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }

        public Escola Escola { get; set; }
    }
}