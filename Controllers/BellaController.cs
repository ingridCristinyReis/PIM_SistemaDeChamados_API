using Microsoft.AspNetCore.Mvc;

namespace PIM_SistemaDeChamados_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BellaController : ControllerBase
    {
        public record AnaliseRequest(string Mensagem);
        public record AnaliseResponse(string Prioridade, string Setor, string Justificativa);

        [HttpPost("analisar")]
        public ActionResult<AnaliseResponse> Analisar([FromBody] AnaliseRequest req)
        {
            var msg = (req?.Mensagem ?? "").ToLowerInvariant();

            string prioridade = "Média";
            if (msg.Contains("erro") || msg.Contains("queda") || msg.Contains("parou")) prioridade = "Alta";
            if (msg.Contains("dúvida") || msg.Contains("lento")) prioridade = "Baixa";

            string setor = "TI";
            if (msg.Contains("folha") || msg.Contains("ponto") || msg.Contains("recrutamento")) setor = "RH";
            if (msg.Contains("nota") || msg.Contains("boleto") || msg.Contains("pagamento")) setor = "Financeiro";

            return Ok(new AnaliseResponse(
                prioridade,
                setor,
                "Classificação baseada em palavras-chave (v1)."
            ));
        }
    }
}
