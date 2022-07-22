$("#TipoPessoa").change(verificaOpcao);
function verificaOpcao() {
    if ($(this).val() == "0") {
        $('#nrCNPJ').val("");
        $('#campCNPJ').hide();
        $('#campCPF').show(20);
    }
    if ($(this).val() == "1") {
        $('#nrCPF').val("");
        $('#campCPF').hide();
        $('#campCNPJ').show(20);
    }
};

function Cadastro() {
    var formData = new FormData();

    var usuario = capturarAtributos($("#formularioUauario"), "");

        formData.append("Nome", usuario.Nome);
        formData.append("Telefone", usuario.Telefone);
        formData.append("Email", usuario.Email);
        formData.append("Documento", usuario.TipoPessoa === "0" ? usuario.CPF : usuario.CNPJ);
        formData.append("TipoPessoa", usuario.TipoPessoa);

        $.ajax({
            url: "/Register/GravarUser/",
            data: formData,
            type: "post",
            processData: false,
            contentType: false,
            dataType: "json",
            beforeSend: function (XMLHttpRequest) {
                exibirCarregando();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            },
            success: function (data, textStatus, XMLHttpRequest) {
                console.log(data);
               // esconderCarregando();
                if (data.Consulta) {
                    data.StatusCode = data.Consulta[0].StatusCode;
                    data.StatusMessage = data.Consulta[0].StatusMessage;
                }
                if (data.StatusCode === 0) {
                    $('#ModalCadastro').modal('hide');
                    Sucesso({ out_msg: data.StatusMessage });
                    setTimeout(function () {
                        window.location = window.location;
                    }, 3000);
                } else {
                    Erro({ out_msg: data.StatusMessage });
                }
            },
        });

}
