//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaseEleva.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Turma
    {
        public int Id { get; set; }
        public int EscolaId { get; set; }
        public string Professor { get; set; }
        public int TotalAlunos { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
    
        public virtual Escola Escola { get; set; }
    }
}
