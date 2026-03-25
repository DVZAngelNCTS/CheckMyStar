namespace CheckMyStar.Bll.Templates
{
    public static class EmailTemplates
    {
        public static string ResetPassword(string link) => $@"
        <!DOCTYPE html>
        <html lang='fr'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Réinitialisation du mot de passe</title>
        </head>
        <body style='margin:0;padding:0;background:#f4f4f4;font-family:Arial,sans-serif;'>

            <table width='100%' cellpadding='0' cellspacing='0'>
                <tr>
                    <td align='center' style='padding:40px 0;'>

                        <table width='600' cellpadding='0' cellspacing='0' style='background:white;border-radius:8px;overflow:hidden;box-shadow:0 4px 12px rgba(0,0,0,0.1);'>
                    
                            <tr>
                                <td style='background:#007bff;padding:20px;text-align:center;color:white;font-size:24px;font-weight:bold;'>
                                    Réinitialisation du mot de passe
                                </td>
                            </tr>

                            <tr>
                                <td style='padding:30px;font-size:16px;color:#333;'>
                                    <p>Bonjour,</p>
                                    <p>Vous avez demandé la réinitialisation de votre mot de passe.</p>
                                    <p>Cliquez sur le bouton ci-dessous pour définir un nouveau mot de passe :</p>

                                    <p style='text-align:center;margin:30px 0;'>
                                        <a href='{link}' 
                                           style='background:#007bff;color:white;padding:14px 24px;
                                                  text-decoration:none;border-radius:6px;font-weight:bold;
                                                  display:inline-block;'>
                                            Réinitialiser mon mot de passe
                                        </a>
                                    </p>

                                    <p>Si vous n'êtes pas à l'origine de cette demande, vous pouvez ignorer cet email.</p>

                                    <p style='margin-top:40px;font-size:12px;color:#777;text-align:center;'>
                                        Cet email a été généré automatiquement, merci de ne pas y répondre.
                                    </p>
                                </td>
                            </tr>

                        </table>

                    </td>
                </tr>
            </table>

        </body>
        </html>";
    }

}
