//Archivo con funciones para mostrar la información de las tablas en el portal 

function ArmarDataTableTabla(table, infoJson, cadena) {

    var json = JSON.parse(infoJson);
    var data = TransformarJson(json);
  console.log(data);
    $(document).ready(function () {
        if (!DataTable.isDataTable(table)) {
            siteTable = $(table).DataTable({
                data: data,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'pdf'
                ],
                order: [[2, 'desc']],
                processing: true,
                columns: [

                    {
                        autoWidth: false,
                        responsivePriority: 6,
                        width: "2%",
                        data: 'idTramiteInstance',

                    },
                    {
                        responsivePriority: 2,
                        width: "10%",
                        data: 'creadoPor'
                    },
                    {
                        responsivePriority: 1,
                        width: "25%",
                        data: function (data, type, row) {
                            return CrearTextoTramite(data);
                        }
                    },
                    {
                        autoWidth: false,
                        responsivePriority: 6,
                        width: "30%",
                        data: 'detalle_nr',
                        visible: false,
                    },
                    {
                        responsivePriority: 7,
                        width: "15%",
                        data: 'userResponsable'
                    },
                    {
                        responsivePriority: 3,
                        className: "columnaEstado",
                        width: "5%",
                        data: 'estado',
                        render: function (data, type, row) {
                            var color = "";
                            data = capitalizeFirstLetter(data);

                            switch (data) {
                                case 'Pendiente':
                                    color = 'text-primary';
                                    break;
                                case 'Rechazado':
                                    color = 'text-danger';
                                    break;
                                case 'Aprobado':
                                    color = 'text-success';
                                    break;
                                default:
                                    color = 'text-info';
                                    break;
                            }
                            return '<span class="' + color + '">' + data + '</span>';
                        }
                    },

                    {
                        responsivePriority: 5,
                        width: "10%",
                        data: 'fechaCreacion',
                        render: function (data, type, row) {
                            if (type === "sort") {
                                return new Date(data).getTime();
                            } else {
                                return RetornarFormatoDate(data);
                            }
                        }
                    },
                    {
                        responsivePriority: 5,
                        width: "10%",
                        data: 'fechaModificacion',
                        render: function (data, type, row) {
                            if (type === "sort") {
                                return new Date(data).getTime();
                            } else {
                                return RetornarFormatoDate(data);
                            }
                        }
                    },

                    {
                        responsivePriority: 4,
                        width: "5%",
                        data: 'url', // No necesitamos datos específicos aquí
                        render: function (data, type, row) {

                            return '<a href="' + data + '"><i class="fa fa-eye"></i></a> ';
                        }
                    },

                    {
                        className: 'dt-control',
                        orderable: false,
                        width: "5%",
                        data: null,
                        defaultContent: ''
                    }
                ],
                columnDefs: [

                ],
                lengthMenu: [30],
                "language": {
                    "decimal": "",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "",
                    "infoFiltered": "(filtrado de un total de _MAX_ entradas)",
                    "infoPostFix": "",
                    "thousands": "Se encontaron más de 1000 entradas",
                    "lengthMenu": "Mostrando _MENU_ entradas",
                    "loadingRecords": "Abriendo...",
                    "processing": "Cargando información aguarde",
                    "search": "Buscar:",
                    "zeroRecords": "No hay trámites en la bandeja.",
                    "paginate": {
                        "first": "Inicio",
                        "last": "Final",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },

                    "aria": {
                        "sortAscending": ": Ordenar por columna ascendente",
                        "sortDescending": ": Ordenar por columna descendente"
                    }
                }
            });
            $('.buttons-copy', (table + '_wrapper')).text('Copiar');
            var info = siteTable.page.info();
            var totalPages = info.pages;
            if (totalPages <= 1) {
                $('.paginate_button.previous, .paginate_button.next', siteTable.table().container()).hide();
                $('.dataTables_paginate', siteTable.table().container()).hide();
            }
            $(table + ' tbody').on('click', 'td.dt-control', function () {
                var tr = $(this).closest('tr');

                var row = siteTable.row(tr);


                if (row.child.isShown()) {

                    row.child.hide();
                }
                else {

                    row.child(format(row.data())).show();

                }
                $(".columnaDetalle").parent().parent().addClass("detalleTR");

            });

            function format(d) {
                if (d.detalle_nr == null) {
                    return "";
                } else {
                    return (
                        '<td  style="text-align: left;" class="columnaDetalle" ><strong>Detalle: &nbsp;</strong></td>' +  // Columna 1
                        '<td  >' + d.detalle_nr + '</td>'
                    );
                }
            }
        }
    });

}
