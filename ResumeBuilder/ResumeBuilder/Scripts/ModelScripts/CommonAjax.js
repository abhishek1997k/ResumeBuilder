﻿function commonAjax(params) {

    var url = params['url'];
    var requestType = params['requestType'];
    var contentType = params['contentType'];
    var dataType = params['dataType'];
    var data = params['data'];
    var beforeSendCallbackFunction = params['beforeSendCallbackFunction'];
    var successCallbackFunction = params['successCallbackFunction'];
    var completeCallbackFunction = params['completeCallbackFunction'];
    var errorCallBackFunction = params['errorCallBackFunction'];

    //make sure that url ends with '/'
    /*if(!url.endsWith("/")){
     url = url + "/";
    }*/

    $.ajax({
        url: url,
        crossDomain: true,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {
            if (typeof beforeSendCallbackFunction === "function") {
                beforeSendCallbackFunction();
            }
        },
        success: function ( textStatus, jqXHR) {
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction();
                removeBackdrop();
                var url = $("#ajaxEditForm").data('url');
                $.get(url, function (data) {
                    $('#pageContent').html(data);
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (typeof errorCallBackFunction === "function") {
                errorCallBackFunction(errorThrown);
            }

        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}