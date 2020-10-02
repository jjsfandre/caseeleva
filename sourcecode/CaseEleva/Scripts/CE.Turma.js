CE.RegisterNamespace('Turma');


CE.Turma.FiltrarTurmas = function (form) {

    var params = CE.SerializeFormFilter($('#divFilterResumeTurma'));

    CE.LoadPage({
        Controller: 'Turma',
        Action: 'Index',
        Params: params.searchModel
    })
}

CE.Turma.Delete = function () {
    var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-turma-grid-resume'));

    var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
    var messageError = "";

    if (selectedIds.length == 0) {
        messageError = CE.Helpers.Globalization.GetString("Select one or more classes to exclude.");
    } 
    if (messageError.length > 0) {
        CE.Alert.Error(messageTitle, messageError);
        return;
    }


    var params = selectedIds.map(function (el, idx) {
        return 'ids[' + idx + ']=' + el;
    }).join('&')

    CE.LoadPage({
        Controller: 'Turma',
        Action: 'Delete',
        Params: params 
    });

}

CE.Turma.OpenDetail = function (isEdit) {

    var params = {};
    if (isEdit) {
        var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-turma-grid-resume'));

        var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
        var messageError = "";

        if (selectedIds.length == 0) {
            messageError = CE.Helpers.Globalization.GetString("No class selected.");
        } else if (selectedIds.length > 1) {
            messageError = CE.Helpers.Globalization.GetString("Select only one class to edit.");
        }
        if (messageError.length > 0) {
            CE.Alert.Error(messageTitle, messageError);
            return;
        }
        params.Id = selectedIds[0];
    }   

    CE.LoadPage({
        Controller: 'Turma',
        Action: 'Detail',
        Params: params
    })
}