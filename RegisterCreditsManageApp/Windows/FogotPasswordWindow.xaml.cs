using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using RegisterCreditsManageApp.Models;
using RegisterCreditsManageApp.Windows.Alert;
using System.IO;
using System.Windows;
using System.Windows.Shapes;

namespace RegisterCreditsManageApp.Windows
{
    /// <summary>
    /// Interaction logic for FogotPasswordWindow.xaml
    /// </summary>
    public partial class FogotPasswordWindow : Window
    {
        private MailSetting mailSetting;

        public FogotPasswordWindow()
        {            
            InitializeComponent();

            // Set basePath to appsettings.json to get MailSettings
            IConfiguration configuration;
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile($"{Directory.GetCurrentDirectory()}{System.IO.Path.DirectorySeparatorChar}appsettings.json");
            configuration = configurationBuilder.Build();
            var mailSettingSection = configuration.GetSection("MailSetting");

            mailSetting = new MailSetting
            {
                Email = mailSettingSection["Email"]!,
                DisplayName = mailSettingSection["DisplayName"]!,
                Password = mailSettingSection["Password"]!,
                Host = mailSettingSection["Host"]!,
                Port = int.Parse(mailSettingSection["Port"]!)
            };
        }

        private async void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = textBoxEmail._Text;
            if(emailInput.IsNullOrEmpty())
            {
                AlertBox.Show("Vui lòng nhập email", "Thông báo", AlertButton.OK, AlertIcon.Warning);
                return;
            }
            await SendMail(emailInput);
        }


        private async Task SendMail(string emailInput)
        {
            int newPassword = new Random().Next(10000, 99999);

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailSetting.DisplayName, mailSetting.Email));
            message.To.Add(new MailboxAddress(emailInput, emailInput));
            message.Sender = new MailboxAddress(mailSetting.DisplayName, mailSetting.Email);
            message.Subject = "Reset lại mật khẩu";

            BodyBuilder bodyBuilder = new BodyBuilder();            
            bodyBuilder.HtmlBody = $"<h1>Mật khẩu của bạn là: <i>{newPassword}</i>. Vui lòng đăng nhập và thay đổi mật khẩu</h1>";
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(mailSetting.Host, mailSetting.Port);            
            await smtpClient.AuthenticateAsync(mailSetting.Email, mailSetting.Password);
            var taskSend = smtpClient.SendAsync(message);
            
            var user = AppDbContext._Context.Users.FirstOrDefault(u => u.Email == emailInput);
            if(user == null) return;
            user.Password = newPassword.ToString();            
            AppDbContext._Context.Update(user);
            await AppDbContext._Context.SaveChangesAsync();
            await taskSend;            
            AlertBox.Show("Vui lòng kiểm tra hộp thư để lấy mật khẩu!", "Đã gửi thành công", AlertButton.OK, AlertIcon.Success);
            this.Hide();
        }

        class MailSetting
        {
            public string Email { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string Host { get; set; } = string.Empty;
            public int Port { get; set; }
        }
    }
}