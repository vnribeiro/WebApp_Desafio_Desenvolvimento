$(() => {
    $('#btnSalvar').on('click', () => {

        // Verifica se o formulário é válido
        if (!validarFormulario()) {
            FormularioInvalidoAlert($('#form'));
            return;
        }

        let data = SerielizeForm($('#form'));
        let url = $('#form').attr('action');

        $.ajax({
            method: "POST",
            url: url,
            data: {
                Descricao: data.Descricao
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
});