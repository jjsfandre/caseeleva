CE.RegisterNamespace('Escola');


CE.Escola.FiltrarEscolas = function (form) {

    var params = CE.SerializeFormFilter($('#divFilterResumeEscola'));

    CE.LoadPage({
        Controller: 'Escola',
        Action: 'Index',
        Params: params.searchModel
    })
}

CE.Escola.Setup = function () {
    $('#form-escola-filter').submit(function (e) {
        e.preventDefault();
    });
}


CE.Escola.Delete = function () {
    var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-escola-grid-resume'));

    var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
    var messageError = "";

    if (selectedIds.length == 0) {
        messageError = CE.Helpers.Globalization.GetString("Select one or more schools to exclude.");
    } 
    if (messageError.length > 0) {
        CE.Alert.Error(messageTitle, messageError);
        return;
    }

    var _this = this;

    _this.params = selectedIds.map(function (el, idx) {
        return 'ids[' + idx + ']=' + el;
    }).join('&')

    callbackFunction = function () {
        CE.LoadPage({
            Controller: 'Escola',
            Action: 'Delete',
            Params: _this.params
        });
    }
    params = {
        Text: CE.Helpers.Globalization.GetString("If there are classes linked to the selected schools, they will also be excluded. Do you wish to continue?"),
        Events: {
            Confirm: callbackFunction
        }
    }

    CE.Alert.Question(params);


}


CE.Escola.Turmas = function () {

    var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-escola-grid-resume'));

    var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
    var messageError = "";

    if (selectedIds.length == 0) {
        messageError = CE.Helpers.Globalization.GetString("No schools selected.");
    } else if (selectedIds.length > 1) {
        messageError = CE.Helpers.Globalization.GetString("Select only one school to list classes.");
    }
    if (messageError.length > 0) {
        CE.Alert.Error(messageTitle, messageError);
        return;
    }

    var params = {
        Id : selectedIds[0]
    }

    CE.LoadPage({
        Controller: 'Turma',
        Action: 'Index',
        Params: params
    })
}



CE.Escola.OpenDetail = function (isEdit) {

    var params = {};
    if (isEdit) {
        var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-escola-grid-resume'));

        var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
        var messageError = "";

        if (selectedIds.length == 0) {
            messageError = CE.Helpers.Globalization.GetString("No schools selected.");
        } else if (selectedIds.length > 1) {
            messageError = CE.Helpers.Globalization.GetString("Select only one school to edit.");
        }
        if (messageError.length > 0) {
            CE.Alert.Error(messageTitle, messageError);
            return;
        }
        params.Id = selectedIds[0];
    }   

    CE.LoadPage({
        Controller: 'Escola',
        Action: 'Detail',
        Params: params
    })
}