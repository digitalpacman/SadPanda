using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Net.Mail;
using OpenPOP.POP3;

namespace Starcraft2ReplayShare
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SqlHelper.File.FlatFile.CreateTable(@"C:\test.txt", "EmailAddress");

			DirectoryInfo starcraftDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\StarCraft II");
			List<FileInfo> replays = GetAccountReplays(starcraftDirectory);
			localReplayListBox.ItemsSource = replays;

			emailAddressListBox.ItemsSource = GetSavedEmailAddresses();
		}

		private List<string> GetSavedEmailAddresses()
		{
			List<string> emailAddresses = new List<string>();
			DataTable data = SqlHelper.File.FlatFile.GetTable(@"C:\test.txt", true);
			foreach (DataRow row in data.Rows)
				emailAddresses.Add(row["EmailAddress"].ToString());
			return emailAddresses;
		}

		private List<FileInfo> GetAccountReplays(DirectoryInfo account)
		{
			List<FileInfo> replays = new List<FileInfo>();
			account.GetDirectories().All(d =>
			{
				replays = replays.Concat(GetAccountReplays(d)).ToList();
				return true;
			});

			account.GetFiles().All(f =>
			{
				if (f.Extension.CompareTo(".SC2Replay") == 0)
				{
					replays.Add(f);
				}
				return true;
			});
			return replays;
		}

		private void addEmailAddressButton_Click(object sender, RoutedEventArgs e)
		{
			string newEmailAddress = emailAddressTextBox.Text;

			DataTable data = SqlHelper.File.FlatFile.GetTable(@"C:\test.txt", true);
			if (data.Select("EmailAddress = '" + newEmailAddress + "'").Count() != 0) return;
			data.Rows.Add(newEmailAddress);
			SqlHelper.File.FlatFile.SaveTable(@"C:\test.txt", data);
			emailAddressListBox.ItemsSource = GetSavedEmailAddresses();
		}

		private void emailAddressListBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Delete) return;

			int index = emailAddressListBox.SelectedIndex;
			if (index == -1) return;

			DataTable data = SqlHelper.File.FlatFile.GetTable(@"C:\test.txt", true);
			data.Rows[index].Delete();
			SqlHelper.File.FlatFile.SaveTable(@"C:\test.txt", data);
			emailAddressListBox.ItemsSource = GetSavedEmailAddresses();
		}

		private void sendReplayButton_Click(object sender, RoutedEventArgs e)
		{
			FileInfo selectedReplay = (FileInfo)localReplayListBox.SelectedItem;
			if (selectedReplay == null) return;
			string selectedEmailAddress = (string)emailAddressListBox.SelectedItem;
			if (string.IsNullOrEmpty(selectedEmailAddress)) return;
	
			string smtpUsername = smtpUsernameTextBox.Text;
			string smtpPassword = smtpPasswordTextBox.Text;
			string smtpUrl = smtpUrlTextBox.Text;
			string emailFrom = smtpUsername;

			if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword) || string.IsNullOrEmpty(smtpUrl))
				return;

			if (smtpUsername.IndexOf("@") == -1)
				emailFrom += "@sc2replays.com";

			SmtpClient mailClient = new SmtpClient(smtpUrl);
			mailClient.EnableSsl = true;
			mailClient.Port = 587;
			mailClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
			MailMessage message = new MailMessage(emailFrom, selectedEmailAddress,
				"Replay " + selectedReplay.Name, selectedReplay.Name);
			message.Attachments.Add(new Attachment(selectedReplay.FullName));
			mailClient.Send(message);





		}

		private void downloadReplaysButton_Click(object sender, RoutedEventArgs e)
		{
			string smtpUsername = smtpUsernameTextBox.Text;
			string smtpPassword = smtpPasswordTextBox.Text;
			string pop3Url = smtpUrlTextBox.Text;

			smtpUsername = "digitalpacman@hotmail.com";
			smtpPassword = "iloveashley";
			pop3Url = "pop3.live.com";

			if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword) || string.IsNullOrEmpty(pop3Url))
				return;

			POPClient client = new POPClient();
			client.Connect(pop3Url, 995, true);
			client.Authenticate(smtpUsername, smtpPassword);
			if (!client.Connected) return;

			int totalEmails = client.GetMessageCount();

			for (int i = 83; i < totalEmails; i++)
			{
				OpenPOP.MIME.Header.MessageHeader header = client.GetMessageHeaders(i);
				if (header.Subject.IndexOf("SC2Replay", StringComparison.InvariantCultureIgnoreCase) == -1) continue;

				OpenPOP.MIME.Message message = client.GetMessage(i);

				if (message == null) continue;
				if (message.Attachments.Count != 1) continue;

				OpenPOP.MIME.Attachment attachment = message.Attachments[0];
				if (attachment.IsMIMEMailFile()) continue;

				if (attachment.Headers.ContentType.Name == null) continue;
				if (attachment.Headers.ContentType.Name.ToLower() != "sc2replay")
					continue;

				attachment.SaveToFile(@"C:\waka.sc2replay");
			}
			client.Disconnect();
		}
	}
}
