using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModels.Atualizar;

public class AtualizarDepartamentoViewModel
{
    [Required(ErrorMessage = "O ID é obrigatório.")]
    public Guid Id { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "O assunto deve ter entre 5 e 100 caracteres.")]
    public string Descricao { get; set; } = null!;
}