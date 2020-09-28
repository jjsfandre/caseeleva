CE.RegisterNamespace('Helpers.Globalization');

CE.Helpers.Globalization._config = null;

CE.Helpers.Globalization.GetString = function (id) {
    var error = "[Undefined " + id + " on " + CE.Helpers.Globalization._config.CuntureName + "]";
    if (CE.Helpers.Globalization._config.Strings != null) {
        var string = CE.Helpers.Globalization._config.Strings[id];
        return string != null ? string : error;
    }
    return error;
}

CE.Helpers.Globalization.AddStrings = function (strings) {
    $.map(strings, function (v, i) {
        if (CE.Helpers.Globalization._config.Strings[i] == null) {
            CE.Helpers.Globalization._config.Strings[i] = v;
        }
    });
}

CE.Helpers.Globalization.SetConfig = function (cfg) {
    CE.Helpers.Globalization._config = cfg;
    CE.Helpers.Globalization.Date.SetTranslations();
    if (cfg.Culture.Name == "pt-BR")
        CE.Helpers.Globalization._config.DateTime.DateFormat = "dd/mm/yyyy";
}

CE.Helpers.Globalization.GetCultureName = function () {
    return CE.Helpers.Globalization._config.Culture.Name;
}

CE.Helpers.Globalization.GetCurrencySymbol = function () {
    return CE.Helpers.Globalization._config.Currency.Symbol;
}

CE.Helpers.Globalization.GetISOCurrencySymbol = function () {
    return CE.Helpers.Globalization._config.Currency.ISO;
}

CE.Helpers.Globalization.GetDecimalSeparator = function () {
    return CE.Helpers.Globalization._config.Number.DecimalSeparator;
}

CE.Helpers.Globalization.GetThousandsSeparator = function () {
    return CE.Helpers.Globalization._config.Number.ThousandsSeparator;
}

CE.Helpers.Globalization.GetDateFormat = function () {
    return CE.Helpers.Globalization._config.DateTime.DateFormat;
}

CE.Helpers.Globalization.GetTimeFormat = function () {
    return CE.Helpers.Globalization._config.DateTime.TimeFormat;
}

/* - Decimal - */
CE.RegisterNamespace('Helpers.Globalization.Decimal');
CE.Helpers.Globalization.Decimal.Format = function (value, cfg) {
    var defaultConfig = {
        ThousandLength: 3,
        DecimalLength: 2,
        ThousandsSeparator: CE.Helpers.Globalization.GetThousandsSeparator(),
        DecimalSeparator: CE.Helpers.Globalization.GetDecimalSeparator()
    };

    cfg = CE.Helpers.MergeObject(defaultConfig, cfg);

    var re = '\\d(?=(\\d{' + (cfg.ThousandLength || 3) + '})+' + (cfg.DecimalLength > 0 ? '\\D' : '$') + ')',
        num = value.toFixed(Math.max(0, ~~cfg.DecimalLength));
    return (cfg.DecimalSeparator ? num.replace('.', cfg.DecimalSeparator) : num).replace(new RegExp(re, 'g'), '$&' + (cfg.ThousandsSeparator));
}

CE.Helpers.Globalization.Decimal.UnFormat = function (value, cfg) {
    var defaultConfig = {
        ThousandsSeparator: CE.Helpers.Globalization.GetThousandsSeparator(),
        DecimalSeparator: CE.Helpers.Globalization.GetDecimalSeparator()
    };
    cfg = CE.Helpers.MergeObject(defaultConfig, cfg);
    var integerNumber = value.split(cfg.DecimalSeparator)[0];
    var decimalNumber = value.split(cfg.DecimalSeparator)[1];
    return parseFloat(integerNumber.split(cfg.ThousandsSeparator).join("") + "." + decimalNumber)
}

/* - Currency - */
CE.RegisterNamespace('Helpers.Globalization.Currency');
CE.Helpers.Globalization.Currency.Format = function (value, cfg) {
    return CE.Helpers.Globalization.GetCurrencySymbol() + "" + CE.Helpers.Globalization.Decimal.Format(value);
}

/* - Time - */
CE.RegisterNamespace('Helpers.Globalization.Time');
CE.Helpers.Globalization.Time.Format = function (value, cfg) {
    var days = parseInt(value / (60 * 60 * 24));
    var aux = value % (60 * 60 * 24);
    var hours = parseInt(aux / (60 * 60));
    aux = aux % (60 * 60);
    var minutes = parseInt(aux / (60));
    var seconds = aux % (60);
    return hours.padLeft(2, '0') + ':' + minutes.padLeft(2, '0') + ':' + seconds.padLeft(2, '0')
}

CE.Helpers.Globalization.Time.UnFormat = function (value, cfg) {
    var hours = parseInt(value.split(":")[0]);
    if (hours > 23)
        return 0;
    var minutes = parseInt(value.split(":")[1]);
    if (minutes > 59)
        return 0;
    var seconds = parseInt(value.split(":")[2]);
    if (seconds > 59)
        return 0;
    return hours * 60 * 60 + minutes * 60 + seconds;
}

/* - DateTime - */
CE.RegisterNamespace('Helpers.Globalization.DateTime');
CE.Helpers.Globalization.DateTime.JsonToDate = function (value) {
    return new Date(parseInt(/-?\d+/.exec(value)[0]));
}

CE.Helpers.Globalization.DateTime.Format = function (value) {
    if (typeof value != "object" || typeof value.getDate == "undefined")
        throw "The value is not a valid Date object";

    return DateFormat.format.date(value, CE.Helpers.Globalization.GetDateFormat() + " " + CE.Helpers.Globalization.GetTimeFormat());
}

/* - Date - */
CE.RegisterNamespace('Helpers.Globalization.Date');
CE.Helpers.Globalization.Date.Format = function (value, cfg) {
    if (!value)
        return "";
    if (typeof value != "object" || typeof value.getDate == "undefined")
        throw "The value is not a valid Date object";

    var format = (cfg != null && cfg.Format) || CE.Helpers.Globalization.GetDateFormat();

    if (format == "UTC")
        return value.toISOString();
    else
        return DateFormat.format.date(value, format);
}

CE.Helpers.Globalization.Date.UnFormat = function (value, cfg) {
    var format = (cfg != null && cfg.Format) || CE.Helpers.Globalization.GetDateFormat();

    if (format == "UTC")
        return new Date(value);
}

CE.Helpers.Globalization.Date.SetTranslations = function(){
    $.fn.datepicker.dates['pt-BR'] = {
        days: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"],
        daysShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
        daysMin: ["Do", "Se", "Te", "Qu", "Qu", "Se", "Sa"],
        months: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        monthsShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
        today: "Hoje",
        monthsTitle: "Meses",
        clear: "Limpar",
        format: "dd/mm/yyyy"
    };
}