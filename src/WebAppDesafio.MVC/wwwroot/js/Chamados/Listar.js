$(() => {

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
            url: config.contextPath + 'Chamados/Report',
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
                link.download = 'rptChamados.pdf';
                link.click();
            },
            error: (error) => {
                console.error('Erro ao gerar relatório:', error);
                alert("Falha ao gerar relatório");
            }
        });
    });

    $('#btnAdicionar').on('click', () => {
        window.location.href = config.contextPath + 'Chamados/Cadastrar';
    });

    $('#btnEditar').on('click', () => {
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

    $('#btnExcluir').on('click', () => {
        var data = table.row('.selected').data();

        if (data && data.id) {
            Swal.fire({
                text: "Tem certeza de que deseja excluir " + data.assunto + " ?",
                type: "warning",
                showCancelButton: true,
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        url: config.contextPath + 'Chamados/Excluir/' + data.id,
                        type: 'DELETE',
                        contentType: 'application/json',
                        success: (result) => {
                            Swal.fire({
                                icon: 'success',
                                type: result.type,
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