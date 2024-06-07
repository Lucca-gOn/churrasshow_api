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
                    Subject = "Bem vindo ao Churras Show",
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
                    Subject = "Recuperação de Senha - Churras Show",
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
                    Body = GetHtmlContentOrcamento(evento)
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
<div style=""width:100%; background-color:#ba9c57; padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color:#FFFFFF; border-radius: 10px; padding: 20px;"">
        <img src=""https://fhjaviu.stripocdn.email/content/guids/CABINET_69be903d57665267711c255052aac881bf7c711592b94930f6f360ee050ff600/images/churrasshow_1.png"" alt="" Logotipo da Aplicação"" style="" display: block; margin: 0 auto; max-width: 200px;"" />
        <h1 style=""color: #333333; text-align: center;"">Bem-vindo ao Churras Show!</h1>
        <p style=""color: #666666; text-align: center;"">Olá <strong>" + userName + @"</strong>,</p>
        <p style=""color: #666666;text-align: center"">Estamos muito felizes por você ter se inscrito na plataforma Churras Show.</p>
        <p style=""color: #666666;text-align: center"">Explore todas funcionalidades que temos, e venha marcar o seu evento.</p>
        <p style=""color: #666666;text-align: center"">Se tiver alguma dúvida ou precisar de assistência, entre em contato e não deixe para amanhã.</p>
        <p style=""color: #666666;text-align: center"">Aproveite sua experiência conosco e venha viver esse momento com sua família!</p>
        <p style=""color: #666666;text-align: center"">Atenciosamente,<br>Equipe Churras Show</p>
    </div>
</div>";

            return response;
        }

        private string GetHtmlContentRecovery(int codigo)
        {
            string response = @"
<div style=""width:100%; background-color:#ba9c57; padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color:#FFFFFF; border-radius: 10px; padding: 20px;"">
        <img src=""https://fhjaviu.stripocdn.email/content/guids/CABINET_69be903d57665267711c255052aac881bf7c711592b94930f6f360ee050ff600/images/churrasshow_1.png"" alt="" Logotipo da Aplicação"" style="" display: block; margin: 0 auto; max-width: 200px;"" />
        <h1 style=""color: #333333;text-align: center;"">Recuperação de senha</h1>
        <p style=""color: #666666;font-size: 24px; text-align: center;"">Código de confirmação <strong>" + codigo + @"</strong></p>
    </div>
</div>";

            return response;
        }

        private string GetHtmlContentOrcamento(Evento evento)
        {
            string response = $@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html dir=""ltr"" xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
    <meta name=""x-apple-disable-message-reformatting"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta content=""telephone=no"" name=""format-detection"">
    <title>Orçamento</title>
    <!--[if (mso 16)]>
    <style type=""text/css"">
    a {{text-decoration: none;}}
    </style>
    <![endif]-->
    <!--[if gte mso 9]>
    <style>sup {{ font-size: 100% !important; }}</style>
    <![endif]-->
    <!--[if gte mso 9]>
    <xml>
        <o:OfficeDocumentSettings>
        <o:AllowPNG></o:AllowPNG>
        <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
    </xml>
    <![endif]-->
    <style type=""text/css"">
        #outlook a {{ padding:0; }}
        .es-button {{ mso-style-priority:100!important; text-decoration:none!important; }}
        a[x-apple-data-detectors] {{ color:inherit!important; text-decoration:none!important; font-size:inherit!important; font-family:inherit!important; font-weight:inherit!important; line-height:inherit!important; }}
        .es-desk-hidden {{ display:none; float:left; overflow:hidden; width:0; max-height:0; line-height:0; mso-hide:all; }}
        @media only screen and (max-width:600px) {{
            p, ul li, ol li, a {{ line-height:150%!important }}
            h1, h2, h3, h1 a, h2 a, h3 a {{ line-height:120%!important }}
            h1 {{ font-size:36px!important; text-align:left }}
            h2 {{ font-size:26px!important; text-align:left }}
            h3 {{ font-size:20px!important; text-align:left }}
            .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:36px!important; text-align:left }}
            .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:26px!important; text-align:left }}
            .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important; text-align:left }}
            .es-menu td a {{ font-size:12px!important }}
            .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {{ font-size:14px!important }}
            .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {{ font-size:14px!important }}
            .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {{ font-size:14px!important }}
            .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {{ font-size:12px!important }}
            *[class=""gmail-fix""] {{ display:none!important }}
            .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {{ text-align:center!important }}
            .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {{ text-align:right!important }}
            .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {{ text-align:left!important }}
            .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }}
            .es-button-border {{ display:inline-block!important }}
            a.es-button, button.es-button {{ font-size:20px!important; display:inline-block!important }}
            .es-adaptive table, .es-left, .es-right {{ width:100%!important }}
            .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }}
            .es-adapt-td {{ display:block!important; width:100%!important }}
            .adapt-img {{ width:100%!important; height:auto!important }}
            .es-m-p0 {{ padding:0!important }}
            .es-m-p0r {{ padding-right:0!important }}
            .es-m-p0l {{ padding-left:0!important }}
            .es-m-p0t {{ padding-top:0!important }}
            .es-m-p0b {{ padding-bottom:0!important }}
            .es-m-p20b {{ padding-bottom:20px!important }}
            .es-mobile-hidden, .es-hidden {{ display:none!important }}
            tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }}
            tr.es-desk-hidden {{ display:table-row!important }}
            table.es-desk-hidden {{ display:table!important }}
            td.es-desk-menu-hidden {{ display:table-cell!important }}
            .es-menu td {{ width:1%!important }}
            table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }}
            table.es-social {{ display:inline-block!important }}
            table.es-social td {{ display:inline-block!important }}
            .es-m-p5 {{ padding:5px!important }}
            .es-m-p5t {{ padding-top:5px!important }}
            .es-m-p5b {{ padding-bottom:5px!important }}
            .es-m-p5r {{ padding-right:5px!important }}
            .es-m-p5l {{ padding-left:5px!important }}
            .es-m-p10 {{ padding:10px!important }}
            .es-m-p10t {{ padding-top:10px!important }}
            .es-m-p10b {{ padding-bottom:10px!important }}
            .es-m-p10r {{ padding-right:10px!important }}
            .es-m-p10l {{ padding-left:10px!important }}
            .es-m-p15 {{ padding:15px!important }}
            .es-m-p15t {{ padding-top:15px!important }}
            .es-m-p15b {{ padding-bottom:15px!important }}
            .es-m-p15r {{ padding-right:15px!important }}
            .es-m-p15l {{ padding-left:15px!important }}
            .es-m-p20 {{ padding:20px!important }}
            .es-m-p20t {{ padding-top:20px!important }}
            .es-m-p20r {{ padding-right:20px!important }}
            .es-m-p20l {{ padding-left:20px!important }}
            .es-m-p25 {{ padding:25px!important }}
            .es-m-p25t {{ padding-top:25px!important }}
            .es-m-p25b {{ padding-bottom:25px!important }}
            .es-m-p25r {{ padding-right:25px!important }}
            .es-m-p25l {{ padding-left:25px!important }}
            .es-m-p30 {{ padding:30px!important }}
            .es-m-p30t {{ padding-top:30px!important }}
            .es-m-p30b {{ padding-bottom:30px!important }}
            .es-m-p30r {{ padding-right:30px!important }}
            .es-m-p30l {{ padding-left:30px!important }}
            .es-m-p35 {{ padding:35px!important }}
            .es-m-p35t {{ padding-top:35px!important }}
            .es-m-p35b {{ padding-bottom:35px!important }}
            .es-m-p35r {{ padding-right:35px!important }}
            .es-m-p35l {{ padding-left:35px!important }}
            .es-m-p40 {{ padding:40px!important }}
            .es-m-p40t {{ padding-top:40px!important }}
            .es-m-p40b {{ padding-bottom:40px!important }}
            .es-m-p40r {{ padding-right:40px!important }}
            .es-m-p40l {{ padding-left:40px!important }}
            button.es-button {{ width:100% }}
            .es-desk-hidden {{ display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important }}
        }}
        @media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}
    </style>
</head>
<body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
    <div dir=""ltr"" class=""es-wrapper-color"" lang=""en"" style=""background-color:#FAFAFA"">
        <!--[if gte mso 9]>
        <v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
            <v:fill type=""tile"" color=""#fafafa""></v:fill>
        </v:background>
        <![endif]-->
        <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FAFAFA"">
            <tr>
                <td valign=""top"" style=""padding:0;Margin:0"">
                    <table class=""es-header"" cellspacing=""0"" cellpadding=""0"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                        <tr>
                            <td align=""center"" style=""padding:0;Margin:0"">
                                <table class=""es-header-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"">
                                    <tr>
                                        <td align=""left"" style=""Margin:0;padding-top:10px;padding-bottom:10px;padding-left:20px;padding-right:20px"">
                                            <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                <tr>
                                                    <td class=""es-m-p0r"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:560px"">
                                                        <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                            <tr>
                                                                <td style=""padding:0;Margin:0;padding-bottom:20px;font-size:0px"" align=""center""><img src=""https://fhjaviu.stripocdn.email/content/guids/CABINET_69be903d57665267711c255052aac881bf7c711592b94930f6f360ee050ff600/images/churrasshow_1.png"" alt=""Logo"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;font-size:12px"" title=""Logo"" width=""200""></td>
                                                            </tr>
                                                            <tr>
                                                                <td style=""padding:0;Margin:0"">
                                                                    <table class=""es-menu"" width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                        <tr class=""links"">
                                                                            <td style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:15px;padding-bottom:15px;border:0"" width=""50%"" valign=""top"" align=""center""><a target=""_blank"" href="""" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#666666;font-size:14px"">+55 11 4002-8922</a></td>
                                                                            <td style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:15px;padding-bottom:15px;border:0"" width=""50%"" valign=""top"" align=""center""><a target=""_blank"" href="""" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#666666;font-size:14px"">churrasshow@gmail.com</a></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                        <tr>
                            <td align=""center"" style=""padding:0;Margin:0"">
                                <table class=""es-content-body"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;border-top:10px solid #BA9C57;width:600px;border-bottom:10px solid #BA9C57"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" role=""none"">
                                    <tr>
                                        <td align=""left"" style=""padding:0;Margin:0;padding-left:20px;padding-right:20px;padding-top:30px"">
                                            <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                <tr>
                                                    <td valign=""top"" align=""center"" style=""padding:0;Margin:0;width:560px"">
                                                        <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=""padding-bottom:1px;Margin:0;background-image:url(https://fhjaviu.stripocdn.email/content/guids/CABINET_69be903d57665267711c255052aac881bf7c711592b94930f6f360ee050ff600/images/2.png);background-repeat:no-repeat;background-position:center 80px"" background=""https://fhjaviu.stripocdn.email/content/guids/CABINET_69be903d57665267711c255052aac881bf7c711592b94930f6f360ee050ff600/images/2.png"" align=""left"">
                                            <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                <tr>
                                                    <td valign=""top"" align=""center"" style=""padding:0;Margin:0;width:560px"">
                                                        <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                            <tr>
                                                                <td class=""es-m-txt-l"" align=""left"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:10px""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#3c2e1e"">Orçamento</h3></td>
                                                            </tr>
                                                            <tr>
                                                                <td align=""left"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:10px"">
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Pacote:</strong> {evento.Pacotes.NomePacote} R${evento.Pacotes.ValorPorPessoa} por pessoa</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Descrição do pacote:</strong>{evento.Pacotes.DescricaoPacote}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Data e Hora:</strong> {evento.DataHoraEvento}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Quantidade de Pessoas:</strong> {evento.QuantidadePessoasEvento}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Duração:</strong> {evento.DuracaoEvento} Horas</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Descartáveis:</strong> {(evento.Descartaveis.HasValue ? (evento.Descartaveis.Value ? "Sim" : "Não") : "Indefinido")}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Acompanhamentos:</strong> {(evento.Acompanhamentos.HasValue ? (evento.Acompanhamentos.Value ? "Sim" : "Não") : "Indefinido")}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Garçom:</strong> {evento.Garconete}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Endereço:</strong> Rua: {evento.Endereco.Logradouro}, Nº{evento.Endereco.Numero}, Bairro:{evento.Endereco.Bairro}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Cidade/CEP:</strong>{evento.Endereco.Cidade} - {evento.Endereco.UF}, CEP: {evento.Endereco.CEP}</p>
                                                                    <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><strong>Valor Total:</strong> R$ {evento.ValorTotal}</p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                        <tr>
                            <td align=""center"" style=""padding:0;Margin:0"">
                                <table class=""es-content-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
                                    <tr>
                                        <td align=""left"" style=""padding:20px;Margin:0"">
                                            <!--[if mso]>
                                            <table style=""width:560px"" cellpadding=""0"" cellspacing=""0"">
                                                <tr>
                                                    <td style=""width:115px"" valign=""top"">
                                            <![endif]-->
                                            <table class=""es-left"" cellspacing=""0"" cellpadding=""0"" align=""left"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                                <tr>
                                                    <td align=""left"" style=""padding:0;Margin:0;width:115px"">
                                                        <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                            <tr>
                                                                <td class=""es-m-txt-c"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:5px;font-size:0px"" align=""center""><img src=""https://fhjaviu.stripocdn.email/content/guids/CABINET_69be903d57665267711c255052aac881bf7c711592b94930f6f360ee050ff600/images/1698279105045_1_uhY.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""115""></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--[if mso]>
                                                    </td><td style=""width:20px""></td><td style=""width:425px"" valign=""top"">
                                            <![endif]-->
                                            <table class=""es-right"" cellspacing=""0"" cellpadding=""0"" align=""right"" role=""none"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                                <tr>
                                                    <td align=""left"" style=""padding:0;Margin:0;width:425px"">
                                                        <table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                            <tr>
                                                                <td class=""es-m-txt-c"" align=""left"" style=""padding:0;Margin:0""><h3 style=""Margin:0;line-height:30px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#2a2a2a"">Everton Araujo</h3><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#2a2a2a;font-size:14px"">CEO ""Churras Show""</p><span style=""font-size:14px"">+55 11 4002-8922</span><br><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#2a2a2a;font-size:14px"">email<a target=""_blank"" href=""mailto:aaronparker@email.com"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:14px;line-height:21px"">@email.com</a></p></td>
                                                            </tr>
                                                            <tr>
                                                                <td class=""es-m-txt-c"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:5px;font-size:0"" align=""left"">
                                                                    <table class=""es-table-not-adapt es-social"" cellspacing=""0"" cellpadding=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                                        <tr>
                                                                            <td valign=""top"" align=""center"" style=""padding:0;Margin:0""><a target=""_blank"" href=""https://www.instagram.com/churras.show/?hl=am-et"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:14px""><img title=""Instagram"" src=""https://fhjaviu.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png"" alt=""Inst"" width=""24"" height=""24"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--[if mso]>
                                                </td></tr></table>
                                            <![endif]-->
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>";

            return response;
        }

    }
}
