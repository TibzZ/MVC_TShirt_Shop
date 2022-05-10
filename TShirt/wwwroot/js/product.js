var DataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "tshirtTitle", "width": "15%" },
            { "data": "productId", "width": "15%" },
            { "data": "mainPrice", "width": "15%" },
            { "data": "designer", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function(data) {
                return `
                   <div class="w-75 btn-group" role="group">
                    <a href="/Admin/Product/Upsert?id=${data}"
                    class="btn btn-primary mx-2"> <i class="bi bi-pencil"> </i> Edit</a>
                    <a href="/Admin/Product/Delete?id=${data}"
                    class="btn btn-primary mx-2"> <i class="bi bi-x-circle"></i> Delete</a>
                   </div>
                        `
                },
                "width": "15%"
            },

        ]
    });
}