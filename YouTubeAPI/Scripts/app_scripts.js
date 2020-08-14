
/* Toast Type */
var TYPE_MSG_SUCCESS = "success";
var TYPE_MSG_INFO = "info";
var TYPE_MSG_WARNING = "warning";
var TYPE_MSG_ERROR = "error";

/**
 * Mostra um alert popup baseado no toastr.
 * @param {string} title - o titulo do alert
 * @param {string} message - a mensagem do alert
 * @param {string} type_msg - o tipo de mensagem. Tipo possíveis (TYPE_MSG_SUCCESS, TYPE_MSG_INFO, TYPE_MSG_WARNING, TYPE_MSG_ERROR)
 */
var showAlertMessage = function (title, message, type_msg) {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var strType = "";

    switch (type_msg) {
        case "success": strType = "success"; break;
        case "info": strType = "info"; break;
        case "warning": strType = "warning"; break;
        case "error": strType = "error"; break;
        default:
            strType = "error"; break;
    }

    toastr[strType](message, title);
};

