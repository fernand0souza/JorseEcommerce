function capturarAtributos(contentor, defaultIfNull) {
    var objRetorno = {};

    if (typeof defaultIfNull == "undefined") {
        defaultIfNull = " ";
    }

    // OBTEM O ID DO OBJETO
    var nomeModel = arguments[1] ? arguments[1] : "idModel";
    objRetorno[nomeModel] = contentor.attr(nomeModel) ? contentor.attr(nomeModel) : 0;

    // DEFINE OS CONTROLES QUE DEVEM SER CONSIDERADOS PARA CAPTURA
    var controlesCapturar = ["input[type='text']",
        "input[type='hidden']",
        "input[type='password']",
        "input[type='hidden']",
        "input[type='checkbox']",
        "input[type='number']",
        "select",
        "textarea"]
        .join(",");

    // VARRE OS CONTROLES E CAPTURA O ATRIBUTO
    contentor.find(controlesCapturar).each(function (index, elemento) {
        elemento = $(elemento);

        // ADICIONA AO OBJETO DE CAPTURA DEFININDO COMO NOME DE PROPRIEDADE O ATRIBUTO NOME, SE TIVER, SENÃO DEFINE COM O ID
        if (elemento.attr("nome")) {
            if (elemento.attr("type") == "checkbox")
                objRetorno[elemento.attr("nome")] = elemento.is(":checked") ? elemento.val() : defaultIfNull;
            else
                objRetorno[elemento.attr("nome")] = elemento.val() != "" ? elemento.val() : defaultIfNull;
        } else {
            var idNome = elemento.attr("id");//.slice(1);

            if (elemento.attr("type") == "checkbox")
                objRetorno[idNome] = elemento.is(":checked") ? elemento.val() : defaultIfNull;
            else
                objRetorno[idNome] = elemento.val() != "" ? elemento.val() : defaultIfNull;
        }
    });

    return objRetorno;
}

function Sucesso(data) {
    if (data) {
        $('#sucesso').modal('show');
        $("#sucesso .textoMsgModal").html("<h1>"+data.out_msg+"</h1>");
    } else {
        $("#sucesso .textoMsgModal").html("Procedimento realizado com SUCESSO!");
    }
    $('#sucesso').modal('toggle');
}

function Erro(data) {
    if (data) {
        $('#Erro').modal('show');
        $("#erro .textoMsgModal").html(data.out_msg);
    } else {
        $("#erro .textoMsgModal").html("FALHA na operação. Verifique sua conexão de Internet!");
    }
    var callBack = arguments[1];

    $('#erro').modal('toggle');
}

function exibirCarregando() {
    $("#Carregando").modal();
}

function esconderCarregando() {
    $('#Carregando').modal('hide');
}
