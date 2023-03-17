using SendGrid;
using SendGrid.Helpers.Mail;
using EAccount = Domain.Entities.Account;

namespace Application.Common
{
    public class Emailer
    {
        private string? ApiKey { get; set; } = Environment.GetEnvironmentVariable("SENDGRID_KEY");

        private string FromEmail { get; set; } = "jit@mmserver.io";

        #region VERIFICATION MAIL

        public async void SendVerification(string token, EAccount user)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime Account Verification",
                HtmlContent = $"<h1>Welcome {user.FirstName},</h1>" +
                $"<h3>Use this token to verify your account!</h3>" +
                $"<p>{user.ActionToken}</p>"
            };
            msg.AddTo(new EmailAddress($"{user.Email}", $"{user.FirstName + " " + user.LastName}"));
            //await client.SendEmailAsync(msg);
        }

        #endregion VERIFICATION MAIL

        #region CHANGE PASSWORD VERIFICATION

        public async void SendChangePasswordVerification(EAccount user)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime Account",
                HtmlContent = $"<h1>Hello {user.FirstName},</h1>" +
                $"<h3>Use this token to reset your account password</h3>" +
                $"<h4>if this request was not made by you, please contact your system administrator.</h4>" +
                $"<p>{user.ActionToken}</p>"
            };
            msg.AddTo(new EmailAddress($"{user.Email}", $"{user.FirstName + " " + user.LastName}"));
            await client.SendEmailAsync(msg);
        }

        #endregion CHANGE PASSWORD VERIFICATION

        #region TERMINATION MAIL

        public async void SendTerminationEmail(EAccount user)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime Account termintaion",
                PlainTextContent = "Your account has been terminated. You will no longer be able to access connected services and all data has been erased."
            };
            msg.AddTo(new EmailAddress($"{user.Email}", $"{user.FirstName + " " + user.LastName}"));
            var response = await client.SendEmailAsync(msg);
        }

        #endregion TERMINATION MAIL

        #region DEACTIVATION TOKEN MAIL

        public async void SendDeactivationTokenEmail(EAccount user)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime Account deactivation",
                HtmlContent = "<h1>Use this token to deactivate your account. Warning: Once your account has been deactivated you will no longer be able to access connected services.</h1>" +
                "<h4>This token expires in 5 minutes<h4>" +
                $"<p>{user.ActionToken}<p/>"
            };
            msg.AddTo(new EmailAddress($"{user.Email}", $"{user.FirstName + " " + user.LastName}"));
            var response = await client.SendEmailAsync(msg);
        }

        #endregion DEACTIVATION TOKEN MAIL

        #region SEND DEACTIVATOIN EMAIL

        public async void SendDeactivationEmail(EAccount user)

        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime Account Deactivated",
                PlainTextContent = "Your account has been deactivated. You will no longer be able to access connected services."
            };
            msg.AddTo(new EmailAddress($"{user.Email}", $"{user.FirstName + " " + user.LastName}"));
            var response = await client.SendEmailAsync(msg);
        }

        #endregion SEND DEACTIVATOIN EMAIL

        #region SEND FRAUD ALERT

        public async void SendFraudAlert(EAccount account)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "Suspicious Activity Alert",
                PlainTextContent = "Your account has been deactivated. You will no longer be able to access connected services."
            };
            msg.AddTo(new EmailAddress($"{account.Email}", $"{account.FirstName + " " + account.LastName}"));
            //var response = await client.SendEmailAsync(msg);
            //await client.SendEmailAsync(msg);
        }

        #endregion SEND FRAUD ALERT

        #region SEND LOCKED ALERT

        public async void SendLockedAlert(EAccount account)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime Account",
                HtmlContent = "<h1>Your account has been locked due to suspicious activity</h1> </br>" +
                "<h3>Your accound is under review</h3>" +
                $"Dear {account.FirstName}, </br> Your account has been locked due to suspicious patterns detected by our algorithms. If you believe this was a mistake, " +
                $"please contact a administrator or manager to unlock your account"
            };
            msg.AddTo(new EmailAddress($"{account.Email}", $"{account.FirstName + " " + account.LastName}"));
            //var response = await client.SendEmailAsync(msg);
            //await client.SendEmailAsync(msg);
        }

        #endregion SEND LOCKED ALERT

        #region SEND GENERATED PASSWORD

        public async void SendNewPassowrd(string password, EAccount user)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = "JustInTime New Password",
                HtmlContent = $"<h1> Your Password Has Been Reset </h1>" +
                $"<p>Your new password: {password}</p>" +
                $"<i> Make sure to change this password </i>"
            };
            msg.AddTo(new EmailAddress($"{user.Email}", $"{user.FirstName + " " + user.LastName}"));
            await client.SendEmailAsync(msg);
        }

        #endregion SEND GENERATED PASSWORD

        #region SEND PDF REPORT OF ACCOUNT

        public async void sendMontlyReport(EAccount reciever, EAccount sender, byte[] pdfBytes)
        {
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", ApiKey);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(FromEmail, "JustInTime"),
                Subject = $"New PDF Report for {sender.FirstName} {sender.LastName}",
                HtmlContent = $"<p>Attached is a automatically generated PDF report for {sender.FirstName} {sender.LastName} for {DateTime.Now.ToString("MMMM")} " +
                $"{DateTime.Now.ToString("yyyy")}</p>"
            };

            msg.AddAttachment("test.pdf", Convert.ToBase64String(pdfBytes), "application/pdf");

            //msg.AddTo(new EmailAddress($"{reciever.Email}", $"{reciever.FirstName + " " + reciever.LastName}"));
            msg.AddTo(new EmailAddress("mateomajic01@gmail.com"));

            await client.SendEmailAsync(msg);
        }

        #endregion SEND PDF REPORT OF ACCOUNT
    }
}