
CE.RegisterNamespace("Alert");

CE.Alert.CreateAlert = function (config) {

    config.SweetAlert.confirmButtonText = config.SweetAlert.confirmButtonText || CE.Helpers.Globalization.GetString("Yes");
    config.SweetAlert.cancelButtonText = config.SweetAlert.cancelButtonText || CE.Helpers.Globalization.GetString("No");
    config.SweetAlert.confirmButtonColor = config.SweetAlert.confirmButtonColor || "#16987e";
    config.SweetAlert.cancelButtonColor = config.SweetAlert.cancelButtonColor || '#aaa';


    return window.swal(config.SweetAlert);
}

CE.Alert.CloseAlert = function (config) {
    swal.close()
}


CE.Alert.Error = function (title, text) {
    return CE.Alert.CreateAlert({
        Id: 'alert-error',
        SweetAlert: {
            title: title,
            text: text,
            onSuccess: function () { },
            allowOutsideClick: false,
            type: "error",
            confirmButtonText: "OK",
            showCancelButton: false
        }
    });
}

CE.Alert.Info = function (title, text, events) {
    return CE.Alert.CreateAlert({
        Id: 'alert-info',
        Events: {
            Confirm: events ? events.Confirm : false,
            Cancel: events ? events.Cancel : false,
        },
        SweetAlert: {
            title: title,
            text: text,
            allowOutsideClick: false,
            type: "info",
            confirmButtonText: "OK",
            showCancelButton: false
        }
    });
}

CE.Alert.CurrentLoadingAlert = null;
CE.Alert.ShowLoading = function (title, message) {
    return CE.Alert.CurrentLoadingAlert = CE.Alert.CreateAlert({
        Id: 'default-loading-alert',
        SweetAlert: {
            title: title,
            html: message + '<div class="clice-loader-contents"><svg class="circle-loader progress"><circle cx="20" cy="20" r="15"></svg></div>',
            allowOutsideClick: false,
            showConfirmButton: false,
            showCancelButton: false
        }
    });
}

CE.Alert.CloseLoading = function () {
    if (CE.Alert.CurrentLoadingAlert)
        CE.Alert.CloseAlert();
}

