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

        ]
    });
}