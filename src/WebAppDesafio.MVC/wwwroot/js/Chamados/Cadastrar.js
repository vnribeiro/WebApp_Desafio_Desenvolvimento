$(() => {
    $('#btnSalvar').on('click', () => {
        // Verifica se o formulário é válido
        if (!validarFormulario() || !validarDataAbertura()) {
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
            success: (result) => {
                Swal.fire({
                    icon: 'success',
                    type: result.type,
                    title: result.title,
                    text: result.message,
                }).then(() => {
                    window.location.href = config.contextPath + result.controller + '/' + result.action;
                });
            },
            error: (result) => {
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
        source: (request, response) => {
            $.ajax({
                url: config.contextPath + "Chamados/Solicitantes",
                method: "GET",
                data: {
                    Solicitante: request.term
                },
                success: (data) => {
                    response(data);
                },
                error: (error) => {
                    console.error("Erro ao buscar dados", error);
                }
            });
        },
        minLength: 2
    });
});