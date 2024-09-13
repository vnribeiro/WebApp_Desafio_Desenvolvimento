$(() => {

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

    $('#btnRelatorio').on('click', () => {
        var data = table.rows().data().toArray();

        $.ajax({
            url: config.contextPath + 'Departamentos/Report',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            xhrFields: {
                responseType: 'blob' // Configura a resposta como um blob
            },
            success: (response) => {
                // Cria um link temporário para baixar o PDF
                var blob = new Blob([response], { type: 'application/pdf' });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = 'rptDepartamentos.pdf';
                link.click();
            },
            error: (error) => {
                console.error('Erro ao gerar relatório:', error);
                alert("Falha ao gerar relatório");
            }
        });
    });

    $('#btnAdicionar').on('click', () => {
        window.location.href = config.contextPath + 'Departamentos/Cadastrar';
    });

    $('#btnEditar').on('click', () => {
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

    $('#btnExcluir').on('click', () => {

        let data = table.row('.selected').data();

        if (data == undefined || !data.id) {
            return;
        }

        if (data.id) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + data.assunto + " ?",
                icon: "warning",
                showCancelButton: true,
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Departamentos/Excluir/' + data.id,
                        type: 'DELETE',
                        contentType: 'application/json',
                        success: (result) => {
                            Swal.fire({
                                icon: 'success',
                                title: result.title,
                                text: result.message,
                            }).then(() => {
                                table.draw();
                            });
                        },
                        error: (result) => {
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