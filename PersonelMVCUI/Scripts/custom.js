function ValidDateTime(obj) {
    var value = $(obj).val();
    if (value == "") {
        $(obj).valid("false");

    }
    else {
        $(obj).valid();
    }

}
$(document).ready(function () {
    $(".deleted-true").on("click", ".btn-delete", function () {
        var url = $(this).closest(".deleted-true").data("url");
        var row = $(this).closest("tr");
        var id = $(this).data("id");
        bootbox.confirm("Silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                $.ajax({
                    method: "GET",
                    url: url + id,
                    success: function () {
                        row.remove();
                    },
                    error: function () {
                        bootbox.alert("Silme işlemi başarısız..");
                    }
                });
            }
        });
    });
    $(".table-datatable").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.25/i18n/Turkish.json"
        }
    });

});
