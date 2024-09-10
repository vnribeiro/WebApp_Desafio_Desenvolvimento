$(function () {
    $('#btnSalvar').on('click', function () {
        // Verifica se o formulário é válido
        if (!validarFormulario()) {
            FormularioInvalidoAlert($('#form'));
            return;
        }

        let chamado = SerielizeForm($('#form'));
        let url = $('#form').attr('action');

        $.ajax({
            method: "POST",
            url: url,
            data: {
                Assunto: chamado.Assunto,
                Solicitante: chamado.Solicitante,
                Departamento: {
                    Id: $('#Departamento').val()
                },
                DataAbertura: chamado.DataAbertura,
            },
            success: function (result) {
                Swal.fire({
                    icon: 'success',
                    type: result.type,
                    title: result.title,
                    text: result.message,
                }).then(function () {
                    window.location.href = config.contextPath + result.controller + '/' + result.action;
                });
            },
            error: function (result) {
                Swal.fire({
                    icon: 'error',
                    confirmButtonText: 'OK',
                    type: result.responseJSON.type,
                    text: result.responseJSON.message,
                });
            },
        });
    });

    $("#Solicitante").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: config.contextPath + "Chamados/Solicitantes",
                method: "GET",
                data: {
                    Solicitante: request.term
                },
                success: function (data) {
                    response(data);
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching autocomplete data:", error);
                }
            });
        },
        minLength: 3
    });
});