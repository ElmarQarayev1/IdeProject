

/**** loader *****/
function showLoading(title) {
    return swal.fire({
        title: title,
        allowEscapeKey: false,
        allowOutsideClick: false,
        onOpen: () => {
            swal.showLoading();
        }
    });
}

function stopLoading() {
    swal.close();
}
/***************  Delete item from dataTable and Database   ********************/
$(".dataTable").on("click", ".btnDelete", function () {
    var btn = $(this);
    bootbox.confirm({
        message: "Silmək istədiyinizə əminsiz?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Bağla'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Təsdiq et'
            }
        },
        callback: function (result) {
            if (result) {
                var id = btn.data("id");
                var controller = btn.data("controller");
                $.ajax({
                    type: "Post",
                    url: "/" + controller + "/Delete/" + id,
                    success: function () {
                        btn.parent().parent().parent().remove();
                    }
                });
            }
        }
    });
});


/*****  Main Partials Call******/
function callSalesmansListPartial() {

    $.ajax({
        url: '/Home/SalesmansListPartial',
        contentType: 'application/html; charset=utf-8',
        type: 'Get',
        dataType: 'html',
        success: function (result) { $("#loadPartial").html(result); $('#SalesmansListModal').modal("show"); callPartialDataTable("SALESMAN"); },
        error: function (status) {
            $.notify("Xəta baş verdi. Təmsilçilər siyahısı yüklənə bilmədi", { position: "top center", className: "error" });
        }

    });
}

function callClientsListPartial() {

 $.ajax({
        url: '/Home/ClientsListPartial',
        contentType: 'application/html; charset=utf-8',
        type: 'Get',
        dataType: 'html',
        success: function (result) { $("#loadPartial").html(result); $('#ClientsListModal').modal("show"); callPartialDataTable("CLIENT");},
        error: function (status) {
            $.notify("Xəta baş verdi. Müştərilər siyahısı yüklənə bilmədi", { position: "top center", className: "error" });
        }

    });
}

function callSaleControlClientsListPartial() {
    
    $.ajax({
        url: '/Home/SaleControlClientsListPartial',
        contentType: 'application/html; charset=utf-8',
        type: 'Get',
        dataType: 'html',
        success: function (result) { $("#loadPartial").html(result); $('#ClientsListModal').modal("show"); callPartialDataTable("SaleControlClients"); },
        error: function (status) {
            $.notify("Xəta baş verdi. Müştərilər siyahısı yüklənə bilmədi", { position: "top center", className: "error" });
        }
    });
}

function callWarehouseManListPartial() {

    $.ajax({
        url: '/Home/WarehouseListPartial',
        contentType: 'application/html; charset=utf-8',
        type: 'Get',
        dataType: 'html',
        success: function (result) { $("#loadPartial").html(result); $('#WarehouseListModal').modal("show"); callPartialDataTable("WAREHOUSE"); },
        error: function (status) {
            $.notify("Xəta baş verdi. Təmsilçilər siyahısı yüklənə bilmədi", { position: "top center", className: "error" });
        }

    });
}




//function getAllCheckData(type,input) {
   
//    var checkedValues = $("input:checkbox:checked", "#"+type).map(function () {
//        return $(this).data("value").replace('/','');
//    }).get();

//        $('#'+input).val(checkedValues.join(','));
//        $('#'+type).modal('hide');
//}