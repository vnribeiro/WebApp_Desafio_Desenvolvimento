using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels.Criar;

public class CriarChamadoViewModel
{
    [StringLength(100, MinimumLength = 5, ErrorMessage = "O assunto deve ter entre 5 e 100 caracteres.")]
    public string Assunto { get; set; } = null!;

    [StringLength(100, MinimumLength = 3, ErrorMessage = "O assunto deve ter entre 3 e 100 caracteres.")]
    public string Solicitante { get; set; } = null!;

    [Required(ErrorMessage = "O departamento é obrigatório.")]
    public CriarDepartamentoViewModel Departamento { get; set; } = null!;

    [Required(ErrorMessage = "A data de abertura é obrigatória.")]
    public DateTime DataAbertura { get; set; }
}
