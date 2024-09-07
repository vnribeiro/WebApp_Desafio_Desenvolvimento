using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebAppDesafio.MVC.ViewModels
{
    [DataContract]
    public class DepartamentoViewModel
    {
        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Descricao")]
        [DataMember(Name = "Descricao")]
        public string Descricao { get; set; } = null!;
    }
}
