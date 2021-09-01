$(document).ready(function () {
    $("viewMenuTale").DataTable({
        /*Configurations*/
        "ajax": {
            "url": "/Admin/ViewMenu",
            "type": "GET",
            "datatype": "json",
        },
        "columns": [{ "data": "Item_Code" },
            { "data": "Item_Name" },
            { "data": "Description" },
            { "data": "Price" },
            { "data": "Category" },
            { "data": "Picture" },
        ]
    });
})