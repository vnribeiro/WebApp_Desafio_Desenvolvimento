$(document).ready(function () {
    $('#btnCancelar').click(function () {
        Swal.fire({
            html: "Deseja cancelar essa operação? O registro não será salvo.",
            type: "warning",
            showCancelButton: true,
        }).then(function (result) {
            if (result.value) {
                history.back();
            } else {
                console.log("Cancelou a inclusão.");
            }
        });
    });
});

// Validação do formulário
function validarFormulario() {
    let isValid = true;

    // Limpar mensagens de erro anteriores
    $('.text-danger').text('');

    // Validação do campo Departamento
    if (!$('#Descricao').val().trim()) {
        $('#Descricao').siblings('.text-danger').text('O campo Departamento é obrigatório.');
        isValid = false;
    }

    return isValid;
}