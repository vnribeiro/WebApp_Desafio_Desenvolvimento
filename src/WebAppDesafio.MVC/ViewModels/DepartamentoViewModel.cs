
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace WebAppDesafio.MVC.ViewModels
{
    [DataContract]
    public class DepartamentoViewModel
    {
        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Display(Name = "Descricao")]
        [DataMember(Name = "Descricao")]
        [JsonProperty("descricao")]
        public string Descricao { get; set; } = null!;
    }
}
