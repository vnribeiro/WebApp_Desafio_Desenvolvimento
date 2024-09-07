using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels.Criar;

public class CriarDepartamentoViewModel
{
    [StringLength(100, MinimumLength = 5, ErrorMessage = "O assunto deve ter entre 5 e 100 caracteres.")]
    public string Descricao { get; set; } = null!;
}