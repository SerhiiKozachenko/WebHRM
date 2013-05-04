function descriptionEditor(container, options) {
    $('<textarea data-bind="value: ' + options.field + '" cols="20" rows="4" required></textarea>')
    .appendTo(container);
}
        
function edit() {
    var grid = $("#grid").data("kendoGrid");
    grid.editRow(grid.select());
}

function add() {
    var grid = $("#grid").data("kendoGrid");
    grid.addRow();
}

function rem() {
    var grid = $("#grid").data("kendoGrid");
    grid.removeRow(grid.select());
}

function detailInit(e) {
    var detailRow = e.detailRow;

    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".applications").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: { url: "JobApplication/GetGridData", dataType: "json", type: 'GET' }
            },
            schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Id: { editable: false },
                            LastName: { type: 'string', validation: { required: true} },
                            FirstName: { type: 'string', validation: { required: true} },
                            MiddleName: { type: 'string', validation: { required: true} },
                            FilingDate: { type: "date", format: "{0:dd/MM/yyyy}", validation: { required: true} },
                            DesiredSalary: { type: 'number', validation: { min: 0} }
                        }
                    },
                    data: "JobApplications",
                    total: "TotalCount"
                },
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 5,
            filter: { field: "JobId", operator: "eq", value: e.data.Id }
        },
        editable: 'inline',
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
                            { field: "LastName", title: "Last name", width: "110px" },
                            { field: "FirstName", title: "First name", width: "110px" },
                            { field: "MiddleName", title: "Middle name", width: "110px" },
                            { field: "FilingDate", title: "Filing date", width: "110px", template: '#= kendo.toString(FilingDate, "dd/MM/yyyy" ) #' },
                            { field: "DesiredSalary", title: "Desired salary" },
                            { field: "Status", title: "Status" },
                            { command: ["destroy"], title: "&nbsp;" }
                        ]
    });
}

$(function () {
$.ajax({
    url: 'Department/GetAllDepartmentsFKDropDownModel',
    success: function (departments) {

        $.ajax({
            url: 'Project/GetAllProjectsFKDropDownModel',
            success: function(projects) {

                var grid = $("#grid").kendoGrid({
                    dataSource: {
                        type: "json",
                        serverPaging: true,
                        serverSorting: true,
                        serverFiltering: true,
                        pageSize: 10,
                        transport: {
                            read: { url: "Job/GetGridData", dataType: "json", type: 'GET' },
                            update: { url: "Job/UpdateGridData", dataType: "json", type: 'POST' },
                            destroy: { url: "Job/DeleteGridData", dataType: "json", type: 'DELETE' },
                            create: { url: "Job/CreateGridData", dataType: "json", type: 'PUT', complete: function(e) { $("#grid").data("kendoGrid").dataSource.read(); } }
                        },
                        schema: {
                            model: {
                                id: "Id",
                                fields: {
                                    Id: { editable: false },
                                    Title: { type: 'string', validation: { required: true } },
                                    Description: { type: 'string', validation: { required: true } },
                                    Salary: { type: 'number', validation: { min: 0 } },
                                    Status: { type: 'number', validation: { min: 0 } },
                                    DepartmentId: { field: 'DepartmentId', type: 'number', validation: { required: true } },
                                    ProjectId: { field: 'ProjectId', type: 'number', validation: { required: true } }
                                }
                            },
                            data: "Jobs",
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
                    toolbar: [
                        { text: 'Add', template: '<button class="btn" onclick="add()"><i class="icon-file"></i> Add</button>' },
                        { text: 'Edit', template: '<button class="btn" onclick="edit()"><i class="icon-edit"></i> Edit</button>' },
                        { text: 'Remove', template: '<button class="btn" onclick="rem()"><i class="icon-trash"></i> Delete</button>' },
                        { template: $("#toolbar-dropdown").html() }
                    ],
                    selectable: true,
                    detailTemplate: kendo.template($("#detail-row").html()),
                    detailInit: detailInit,

                    columns: [
                        { field: "Title", title: "Title", width: 150 },
                        { field: "Description", title: "Description", width: 200, editor: descriptionEditor },
                        { field: "Salary", title: "Salary", width: 100, format: "{0:c}" },
                        { field: "Status", title: "Status", width: 100 },
                        { field: "DepartmentId", sortable: false, title: "Department", values: departments, width: 110 },
                        { field: "ProjectId", sortable: false, title: "Project", values: projects, width: 110 }
                    ]
                });
                var dropDown = grid.find("#department").kendoDropDownList({
                    dataTextField: "text",
                    dataValueField: "value",
                    autoBind: false,
                    optionLabel: "All",
                    dataSource: {
                        type: "json",
                        severFiltering: true,
                        transport: {
                            read: "Department/GetAllDepartmentsFKDropDownModel"
                        }
                    },
                    change: function() {
                        var value = this.value();
                        if (value) {
                            grid.data("kendoGrid").dataSource.filter({ field: "DepartmentId", operator: "eq", value: parseInt(value) });
                        } else {
                            grid.data("kendoGrid").dataSource.filter({});
                        }
                    }
                });
                
                var dropDownProjects = grid.find("#project").kendoDropDownList({
                    dataTextField: "text",
                    dataValueField: "value",
                    autoBind: false,
                    optionLabel: "All",
                    dataSource: {
                        type: "json",
                        severFiltering: true,
                        transport: {
                            read: "Project/GetAllProjectsFKDropDownModel"
                        }
                    },
                    change: function () {
                        var value = this.value();
                        if (value) {
                            grid.data("kendoGrid").dataSource.filter({ field: "ProjectId", operator: "eq", value: parseInt(value) });
                        } else {
                            grid.data("kendoGrid").dataSource.filter({});
                        }
                    }
                });
            }
        });

    }
});
});