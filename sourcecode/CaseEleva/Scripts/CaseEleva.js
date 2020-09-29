window.CaseEleva = new Object();
window.CE = CaseEleva;

CE.RegisterNamespace = function (className, parentClass) {
    var classNameSplit = className.split('.');
    var currentObject = parentClass || CE;
    classNameSplit.forEach(function (clsName, clsIndex) {
        if (currentObject[clsName] == null)
            currentObject[clsName] = new Object();
        currentObject = currentObject[clsName];
    });
}


CE.LoadPage = function (config) {
    var url = "";
    if (config.Url != null) {
        var url = config.Url;
    } else {
        var url = config.Area != null ? config.Area + "/" : "";
        url = config.Controller != null ? url + config.Controller + "/" : url;
        url = config.Action != null ? url + config.Action + "/" : url;
        url = location.protocol + "//" + location.host + "/" + url;
    }
    location.href = url;
}

CE.Request = function (cfg) {
    var area = typeof (cfg.Area) == "string" ? cfg.Area + '/' : "";
    var controller = cfg.Controller;
    var action = cfg.Action;
    var params = cfg.Params;
    var method = cfg.Method || 'GET';
    var JSONResponse = cfg.JSONResponse == false ? false : true;
    var success = cfg.Success;
    var failure = cfg.Failure;
    var mask = cfg.Mask == null ? {
        Title: CE.Helpers.Globalization.GetString('Attention'),
        Message: CE.Helpers.Globalization.GetString('Loading...')
    } : (cfg.Mask == false ? false : cfg.Mask);
    var delay = cfg.Delay || 0;
    var showErrorMessage = cfg.ShowErrorMessage == false ? false : true;

    if (mask)
        CE.Alert.ShowLoading(mask.Title, mask.Message);

    if (method == "POST")
        params = JSON.stringify(params);

    var send = function () {
        $.ajax({
            method: method,
            dataType: JSONResponse ? 'json' : 'html',
            contentType: "application/json; charset=utf-8",
            url: location.protocol + "//" + location.host + "/" + area + controller + '/' + action,
            data: params,
            success: function (resp, textStatus, jqXHR) {
                if (mask)
                    CE.Alert.CloseLoading();

                if (typeof success == 'function')
                    success(resp);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (mask)
                    CE.Alert.CloseLoading();

                if (showErrorMessage) {
                    function errorMessage(msg) {
                        CE.Alert.CreateAlert({
                            Id: 'alert-handler-delete',
                            SweetAlert: {
                                title: CE.Helpers.Globalization.GetString('There was an error processing'),
                                html: msg + '</br>' + CE.Helpers.Globalization.GetString('Contact your system administrator to troubleshoot the issue.'),
                                closeOnConfirm: true,
                                allowOutsideClick: false,
                                type: "error",
                                confirmButtonText: "OK",
                                showCancelButton: false
                            }
                        });
                    }

                    if (jqXHR.status == 404) {
                        errorMessage(CE.Helpers.Globalization.GetString('Appeal not found or invalid address. Error code: HTTP 404.'));
                    } else if (jqXHR.status == 403) {
                        errorMessage(CE.Helpers.Globalization.GetString('You do not have permission to access this resource.Error code: HTTP 403.'));
                    } else if (jqXHR.status == 401) {
                        internalMessage = CE.Helpers.Globalization.GetString('You are not authorized to access this feature. Please try logging again. Error code: HTTP 401.')
                        CE.Alert.CreateAlert({
                            Id: 'alert-handler-delete',
                            SweetAlert: {
                                title: CE.Helpers.Globalization.GetString('Ops!'),
                                html: CE.Helpers.Globalization.GetString('Your session appears to be no longer active or you do not have privileges to access this feature.Do you want to reload the page ?'),
                                closeOnConfirm: true,
                                allowOutsideClick: false,
                                type: "warning",
                                confirmButtonText: CE.Helpers.Globalization.GetString('Yes'),
                                cancelButtonText: CE.Helpers.Globalization.GetString('No'),
                                showCancelButton: true
                            }, Events: {
                                Confirm: function () {
                                    setTimeout(function () {
                                        location.reload();
                                    }, 500);
                                }
                            }

                        });
                    } else if (jqXHR.status == 500) {
                        errorMessage(CE.Helpers.Globalization.GetString('There was an internal error while processing the request. Error code: HTTP 500.'));
                    } else if (jqXHR.status == 200) {
                        //When the error is on send the request
                        errorMessage(CE.Helpers.Globalization.GetString('There was an error submitting the request. Details:') + errorThrown.message + '.');
                    }
                }

                if (typeof failure == 'function')
                    failure();
            }
        });
    }

    if (delay > 0)
        setTimeout(send, delay);
    else
        send();
}

CE.ShowMessageForgotPassword = function () {
    var title = CE.Helpers.Globalization.GetString("Attention");
    var text = CE.Helpers.Globalization.GetString("There is no need for authentication data at this time. Just click \"Login\"!");
    CE.Alert.Info(title, text);
}

CE.LoadPageMenu = function (cfg) {
    CE.Request({
        Controller: cfg.Controller,
        Action: cfg.Action,
        Method: 'GET',
        Delay: 200,
        Mask: {
            Title: CE.Helpers.Globalization.GetString('Wait'),
            Message: CE.Helpers.Globalization.GetString('Loading page...')
        },
        JSONResponse: false,
        Params: cfg.Params,
        Success: function (resp) {
            $('#contents-page').html(resp)
        }
    })
}

