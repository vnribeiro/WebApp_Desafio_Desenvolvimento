$(function () {

    var table = $('#dataTables-Chamados').DataTable({
        paging: false,
        ordering: false,
        info: false,
        searching: false,
        processing: true,
        serverSide: true,
        ajax: {
            url: config.contextPath + 'Chamados/Datatable',
            type: 'GET'
        },
        columns: [
            { data: 'id', title: 'ID' },
            { data: 'assunto', title: 'Assunto' },
            { data: 'solicitante', title: 'Solicitante' },
            { data: 'departamento.descricao', title: 'Departamento' },
            { data: 'dataAberturaWrapper', title: 'Data Abertura' },
        ],
    });

    $('#dataTables-Chamados tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

    $('#btnRelatorio').on('click', function () {
        window.location.href = config.contextPath + 'Chamados/Report';
    });

    $('#btnAdicionar').on('click', function () {
        window.location.href = config.contextPath + 'Chamados/Cadastrar';
    });

    $('#btnEditar').on('click', function () {
        var data = table.row('.selected').data();

        if (data) {
            var queryString = $.param({
                id: data.id,
                assunto: data.assunto,
                solicitante: data.solicitante,
                departamentoId: data.departamento.id,
                dataAbertura: data.dataAbertura
            });

            window.location.href = config.contextPath + 'Chamados/Editar?' + queryString;
        }
    });

    $('#dataTables-Chamados tbody').on('dblclick', 'tr', function () {
        var data = table.row(this).data();

        if (data) {
            var queryString = $.param({
                id: data.id,
                assunto: data.assunto,
                solicitante: data.solicitante,
                departamentoId: data.departamento.id,
                dataAbertura: data.dataAbertura
            });

            window.location.href = config.contextPath + 'Chamados/Editar?' + queryString;
        }
    });

    $('#btnExcluir').on('click', function () {
        var data = table.row('.selected').data();

        if (data && data.id) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + data.assunto + " ?",
                type: "warning",
                showCancelButton: true,
            }).then(function (result) {
                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Chamados/Excluir/' + data.id,
                        type: 'DELETE',
                        contentType: 'application/json',
                        success: function (result) {
                            Swal.fire({
                                icon: 'success',
                                type: result.type,
                                title: result.title,
                                text: result.message,
                            }).then(function () {
                                table.draw();
                            });
                        },
                        error: function (result) {
                            Swal.fire({
                                icon: 'error',
                                confirmButtonText: 'OK',
                                type: result.responseJSON.type,
                                text: result.responseJSON.message,
                            });
                        }
                    });
                } else {
                    console.log("Cancelou a exclusão.");
                }
            });
        }
    });
});