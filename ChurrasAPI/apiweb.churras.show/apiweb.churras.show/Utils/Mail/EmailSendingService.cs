using apiweb.churras.show.Domains;

namespace apiweb.churras.show.Utils.Mail
{
    public class EmailSendingService
    {
        private readonly IEmailService emailService;
        public EmailSendingService(IEmailService service)
        {
            emailService = service;
        }

        public async Task SendWelcomeEmail(string email, string userName)
        {
            try
            {
                MailRequest request = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Bem vindo ao VitalHub",
                    Body = GetHtmlContent(userName)
                };
                await emailService.SendEmailAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendRecoveryPassword(string email, int codigo)
        {
            try
            {
                MailRequest request = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Recuperação de Senha - VitalHub",
                    Body = GetHtmlContentRecovery(codigo)
                };
                await emailService.SendEmailAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendEventDetailsEmail(string email, Evento evento)
        {
            try
            {
                MailRequest request = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Detalhes do Evento Cadastrado",
                    Body = GetHtmlContentEventDetails(evento)
                };
                await emailService.SendEmailAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetHtmlContent(string userName)
        {
            string response = @"
<div style=""width:100%; background-color:rgba(96, 191, 197, 1); padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color:#FFFFFF; border-radius: 10px; padding: 20px;"">
        <img src=""https://blobvitalhub.blob.core.windows.net/containervitalhub/logotipo.png"" alt="" Logotipo da Aplicação"" style="" display: block; margin: 0 auto; max-width: 200px;"" />
        <h1 style=""color: #333333; text-align: center;"">Bem-vindo ao VitalHub!</h1>
        <p style=""color: #666666; text-align: center;"">Olá <strong>" + userName + @"</strong>,</p>
        <p style=""color: #666666;text-align: center"">Estamos muito felizes por você ter se inscrito na plataforma VitalHub.</p>
        <p style=""color: #666666;text-align: center"">Explore todas as funcionalidades que oferecemos e encontre os melhores médicos.</p>
        <p style=""color: #666666;text-align: center"">Se tiver alguma dúvida ou precisar de assistência, nossa equipe de suporte está sempre pronta para ajudar.</p>
        <p style=""color: #666666;text-align: center"">Aproveite sua experiência conosco!</p>
        <p style=""color: #666666;text-align: center"">Atenciosamente,<br>Equipe VitalHub</p>
    </div>
</div>";

            return response;
        }

        private string GetHtmlContentRecovery(int codigo)
        {
            string response = @"
<div style=""width:100%; background-color:rgba(96, 191, 197, 1); padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color:#FFFFFF; border-radius: 10px; padding: 20px;"">
        <img src=""https://blobvitalhub.blob.core.windows.net/containervitalhub/logotipo.png"" alt="" Logotipo da Aplicação"" style="" display: block; margin: 0 auto; max-width: 200px;"" />
        <h1 style=""color: #333333;text-align: center;"">Recuperação de senha</h1>
        <p style=""color: #666666;font-size: 24px; text-align: center;"">Código de confirmação <strong>" + codigo + @"</strong></p>
    </div>
</div>";

            return response;
        }

        private string GetHtmlContentEventDetails(Evento evento)
        {
            string response = $@"
<div style=""width:100%; background-color:rgba(96, 191, 197, 1); padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color:#FFFFFF; border-radius: 10px; padding: 20px;"">
        <img src=""https://blobvitalhub.blob.core.windows.net/containervitalhub/logotipo.png"" alt="" Logotipo da Aplicação"" style="" display: block; margin: 0 auto; max-width: 200px;"" />
        <h1 style=""color: #333333; text-align: center;"">Detalhes do Evento</h1>
        <p style=""color: #666666; text-align: center;""><strong>Data e Hora:</strong> {evento.DataHoraEvento}</p>
        <p style=""color: #666666; text-align: center;""><strong>Quantidade de Pessoas:</strong> {evento.QuantidadePessoasEvento}</p>
        <p style=""color: #666666; text-align: center;""><strong>Duração:</strong> {evento.DuracaoEvento} horas</p>
        <p style=""color: #666666; text-align: center;""><strong>Descartáveis:</strong> {evento.Descartaveis}</p>
        <p style=""color: #666666; text-align: center;""><strong>Acompanhamentos:</strong> {evento.Acompanhamentos}</p>
        <p style=""color: #666666; text-align: center;""><strong>Confirmado:</strong> {evento.Confirmado}</p>
        <p style=""color: #666666; text-align: center;""><strong>Garçonete:</strong> {evento.Garconete}</p>
        <p style=""color: #666666; text-align: center;""><strong>Endereço:</strong> {evento.Endereco.Logradouro}, {evento.Endereco.Numero}, {evento.Endereco.Bairro}, {evento.Endereco.Cidade} - {evento.Endereco.UF}, {evento.Endereco.CEP}</p>
        <p style=""color: #666666; text-align: center;""><strong>Valor Total:</strong> R$ {evento.ValorTotal}</p>
        <p style=""color: #666666;text-align: center"">Atenciosamente,<br>Equipe VitalHub</p>
    </div>
</div>";

            return response;
        }
    }
}
