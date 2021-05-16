var tempo = 0;
var usuario = "Beatriz";
var tempoTotalNaTela;
var telaVisualizada = $("#telaVisualizada").val();

function ContarTempoTotalNaTela() {

    // Se o tempo não for zerado
    if ((tempo - 1) >= -1) {

        // Pega a parte inteira dos minutos
        var min = parseInt(tempo / 60);
        // Calcula os segundos restantes
        var seg = tempo % 60;

        // Formata o número menor que dez, ex: 08, 07, ...
        if (min < 10) {
            min = "0" + min;
            min = min.substr(0, 2);
        }
        if (seg <= 9) {
            seg = "0" + seg;
        }

        // Cria a variável para formatar no estilo hora/cronômetro
        horaImprimivel = '00:' + min + ':' + seg;

        tempoTotalNaTela = horaImprimivel;

        // Define que a função será executada novamente em 1000ms = 1 segundo
        setTimeout('ContarTempoTotalNaTela()', 1000);

        tempo++;
    }
    else {
        $('#myAlert').show().fadeOut(5000);
    }

}

ContarTempoTotalNaTela();

window.onbeforeunload = EnviarDados;

function EnviarDados() {
    var data = {
        Usuario: usuario, TelaVisualizada: telaVisualizada,
        TempoTotalNaTela: tempoTotalNaTela
    };

    $.ajax({
        url: "/Analytic/VerificarAnalytic",
        type: "POST",
        dataType: "json",
        data: data
    })
    return data;
    //   window.close();
}

