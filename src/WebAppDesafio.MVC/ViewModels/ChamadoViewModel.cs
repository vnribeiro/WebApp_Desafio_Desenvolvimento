using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebAppDesafio.MVC.ViewModels
{
    [DataContract]
    public class ChamadoViewModel
    {
        private readonly CultureInfo _ptBr = new CultureInfo("pt-BR");

        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public Guid Id { get; set; }

        [Display(Name = "Assunto")]
        [DataMember(Name = "Assunto")]
        public string Assunto { get; set; } = null!;

        [Display(Name = "Solicitante")]
        [DataMember(Name = "Solicitante")]
        public string Solicitante { get; set; } = null!;

        [Display(Name = "Departamento")]
        [DataMember(Name = "Departamento")]
        public DepartamentoViewModel Departamento { get; set; } = null!;

        [Display(Name = "DataAbertura")]
        [DataMember(Name = "DataAbertura")]
        public DateTime DataAbertura { get; set; }

        [DataMember(Name = "DataAberturaWrapper")]
        public string DataAberturaWrapper
        {
            get
            {
                return DataAbertura.ToString("d", _ptBr);
            }
            set
            {
                DataAbertura = DateTime.Parse(value, _ptBr);
            }
        }
    }
}
