var plugin = {
    getToast(title, body, position, type) {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": position,
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "8000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr[`${type}`](body, title)
    }
}

const ToastrPosition = {
    topRight: "toast-top-right",
    topCenter: "toast-top-center",
    topLeft: "toast-top-left",
    bottomRight: "toast-bottom-right",
    bottomCenter: "toast-bottom-center",
    bottomLeft: "toast-bottom-left",
}
const ToastrType = {
    success: "success",
    warning: "warning",
    info: "info",
    error: "error"
}