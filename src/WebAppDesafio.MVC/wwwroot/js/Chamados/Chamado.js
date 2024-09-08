$(document).ready(function () {
    $('.glyphicon-calendar').closest("div.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
    });

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

    // Validação do campo Assunto
    if (!$('#Assunto').val().trim()) {
        $('#Assunto').siblings('.text-danger').text('O campo Assunto é obrigatório.');
        isValid = false;
    }

    // Validação do campo Solicitante
    if (!$('#Solicitante').val().trim()) {
        $('#Solicitante').siblings('.text-danger').text('O campo Solicitante é obrigatório.');
        isValid = false;
    }

    // Validação do campo Departamento
    if (!$('#Departamento').val()) {
        $('#Departamento').closest('.form-group').find('.text-danger').text('Selecione um departamento.');
        isValid = false;
    }

    // Validação do campo Data de Abertura
    var dataAbertura = $('#DataAbertura').val().trim();
    if (!dataAbertura) {
        $('#DataAbertura').closest('.form-group').find('.text-danger').text('O campo Data de Abertura é obrigatório.');
        isValid = false;
    } else if (isRetroactive(dataAbertura)) {
        $('#DataAbertura').closest('.form-group').find('.text-danger').text('A Data de Abertura não pode ser retroativa.');
        isValid = false;
    }

    return isValid;
}

// Validação da data retroativa
function isRetroactive(dateString) {
    var today = moment().startOf('day');
    var selectedDate = moment(dateString, 'DD/MM/YYYY').startOf('day');
    return selectedDate.isBefore(today);
}