using System;
using System.IO;
using System.Windows.Forms;

namespace _27_1qTweak
{
	static class Program
	{
		private const string PublicKeyPem = "-----BEGIN PUBLIC KEY-----\nREPLACE_WITH_YOUR_PUBLIC_KEY\n-----END PUBLIC KEY-----";
		private const string LicensePath = @"C:\\ProgramData\\27_1qTweak\\license.jwt";

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Directory.CreateDirectory(Path.GetDirectoryName(LicensePath)!);
			var lic = LicenseValidator.Load(LicensePath);
			if (string.IsNullOrEmpty(lic) || !LicenseValidator.IsValid(lic!, PublicKeyPem))
			{
				using (var lf = new LicenseForm(PublicKeyPem, LicensePath))
				{
					var res = lf.ShowDialog();
				}
				lic = LicenseValidator.Load(LicensePath);
				if (string.IsNullOrEmpty(lic) || !LicenseValidator.IsValid(lic!, PublicKeyPem))
				{
					MessageBox.Show("License is missing or invalid. Exiting.", "License", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			Application.Run(new MainForm());
		}
	}
}