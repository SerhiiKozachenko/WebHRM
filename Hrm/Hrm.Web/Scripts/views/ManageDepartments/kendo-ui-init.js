function descriptionEditor(container, options) {
    $('<textarea data-bind="value: ' + options.field + '" cols="20" rows="4" required></textarea>')
        .appendTo(container);
}

$(function () {
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 10,
            transport: {
                read: { url: "Department/GetGridData", dataType: "json", type: 'GET' },
                update: { url: "Department/UpdateGridData", dataType: "json", type: 'POST' },
                destroy: { url: "Department/DeleteGridData", dataType: "json", type: 'DELETE' },
                create: { url: "Department/CreateGridData", dataType: "json", type: 'PUT' }
            },
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false },
                        Title: {
                            type: 'string',
                            validation: { required: true }
                        },
                        Description: { type: 'string', validation: { required: true} }
                    }
                },
                data: "Departments",
                total: "TotalCount"
            }
        },
        height: '500',

        pageable: {
            refresh: true,
            pageSizes: true
        },
        sortable: true,
        filterable: true,

        editable: 'popup',
        toolbar: ['create'],
        selectable: true,
        columns: [
                    { field: "Title", title: "Title", width: 150 },
                    { field: "Description", title: "Description", editor: descriptionEditor },
                    { command: ["edit", "destroy"], title: "&nbsp;", width: "172px" }
                ]
    });
});