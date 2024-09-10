$(function () {
    $('#btnSalvar').on('click', function () {

        // Verifica se o formulário é válido
        if (!validarFormulario()) {
            FormularioInvalidoAlert($('#form'));
            return;
        }

        let departamento = SerielizeForm($('#form'));

        $.ajax({
            method: "PATCH",
            url: `${config.contextPath}Departamentos/Atualizar/${departamento.Id}`,
            headers: {
                "Content-Type": "application/json"
            },
            data: JSON.stringify({
                Id: departamento.Id,
                Descricao: departamento.Descricao,
            }),
            success: function (result) {
                console.log(result.message);
                Swal.fire({
                    icon: 'success',
                    type: result.type,
                    title: result.title,
                    text: result.message,
                }).then(function () {
                    window.location.href = `${config.contextPath}${result.controller}/${result.action}`;
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
});