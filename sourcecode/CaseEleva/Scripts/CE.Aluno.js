CE.RegisterNamespace('Aluno');


CE.Aluno.FiltrarAlunos = function (form) {

    var params = CE.SerializeFormFilter($('#divFilterResumeAluno'));

    CE.LoadPage({
        Controller: 'Aluno',
        Action: 'Index',
        Params: params.searchModel
    })
}

CE.Aluno.Delete = function () {
    var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-aluno-grid-resume'));

    var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
    var messageError = "";

    if (selectedIds.length == 0) {
        messageError = CE.Helpers.Globalization.GetString("Select one or more classes to exclude.");
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
            Controller: 'Aluno',
            Action: 'Delete',
            Params: _this.params
        });
    }
    params = {
        Text: CE.Helpers.Globalization.GetString("If there are students linked to the selected classes, they will also be excluded. Do you wish to continue?"),
        Events: {
            Confirm: callbackFunction
        }
    }

    CE.Alert.Question(params);
}

CE.Aluno.OpenDetail = function (isEdit) {

    var params = {};
    if (isEdit) {
        var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-aluno-grid-resume'));

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
        Controller: 'Aluno',
        Action: 'Detail',
        Params: params
    })
}

CE.Aluno.Alunos = function () {

    var selectedIds = CE.GetSelectedIdsFromGrid($('#crudmodel-aluno-grid-resume'));

    var messageTitle = CE.Helpers.Globalization.GetString("Ops!");
    var messageError = "";

    if (selectedIds.length == 0) {
        messageError = CE.Helpers.Globalization.GetString("No class selected.");
    } else if (selectedIds.length > 1) {
        messageError = CE.Helpers.Globalization.GetString("Select only one class to list students.");
    }
    if (messageError.length > 0) {
        CE.Alert.Error(messageTitle, messageError);
        return;
    }

    var params = {
        AlunoId: selectedIds[0]
    }

    CE.LoadPage({
        Controller: 'Aluno',
        Action: 'Index',
        Params: params
    })
}

CE.Aluno.Setup = function () {
    $("#EscolaId").change(function (event, a, b) {
        CE.Aluno.AtualizaComboTurmas($(event.target), $('#TurmaId'));
    });
}

CE.Aluno.AtualizaComboTurmas = function (fieldEscola, fieldTurma) {

    var params = {
        Controller: "Aluno",
        Action: "GetTurmasByEscolaId",
        Params: { EscolaId: fieldEscola.val() },
        JSONResponse: true,
        Mask: {
            Title: CE.Helpers.Globalization.GetString('Attention'),
            Message: CE.Helpers.Globalization.GetString('Refreshing classes...')
        },
        Delay: 200,
        Success: function (response) {
            if (response.Success) {
                fieldTurma.children('option[typeoption="custom"]').remove();
                response.Turmas.forEach(function (turma) {
                    fieldTurma.append('<option typeoption="custom" value="' + turma.Id + '">' + turma.Codigo + '</option>');
                });

                fieldTurma.trigger("chosen:updated");
            }
        }
    };

    CE.Request(params);
}