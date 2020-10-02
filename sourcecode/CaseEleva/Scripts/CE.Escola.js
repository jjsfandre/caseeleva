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

