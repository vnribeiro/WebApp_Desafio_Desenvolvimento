$(function () {

    var table = $('#dataTables-Departamentos').DataTable({
        paging: false,
        ordering: false,
        info: false,
        searching: false,
        processing: true,
        serverSide: true,
        ajax: {
            url: config.contextPath + 'Departamentos/Datatable',
            type: 'GET'
        },
        columns: [
            { data: 'id', title: 'ID' },
            { data: 'descricao', title: 'Descrição' },
        ],
    });

    $('#dataTables-Departamentos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#btnRelatorio').on('click', function () {
        window.location.href = config.contextPath + 'Departamentos/Report';
    });

    $('#btnAdicionar').on('click', function () {
        window.location.href = config.contextPath + 'Departamentos/Cadastrar';
    });

    $('#btnEditar').on('click', function () {
        var data = table.row('.selected').data();

        if (data != undefined) {
            var queryString = $.param({
                id: data.id,
                descricao: data.descricao
            });

            window.location.href = config.contextPath + 'Departamentos/Editar?' + queryString;
        }
    });

    $('#dataTables-Departamentos tbody').on('dblclick', 'tr', function () {
        var data = table.row(this).data();

        if (data != undefined) {
            var queryString = $.param({
                id: data.id,
                descricao: data.descricao
            });

            window.location.href = config.contextPath + 'Departamentos/Editar?' + queryString;
        }
    });

    $('#btnExcluir').on('click', function () {

        let data = table.row('.selected').data();

        if (data == undefined || !data.id) {
            return;
        }

        if (data.id) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + data.assunto + " ?",
                icon: "warning",
                showCancelButton: true,
            }).then(function (result) {
                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Departamentos/Excluir/' + data.id,
                        type: 'DELETE',
                        contentType: 'application/json',
                        success: function (result) {
                            Swal.fire({
                                icon: 'success',
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
                                title: result.responseJSON.title,
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