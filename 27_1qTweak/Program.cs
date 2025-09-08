using System;
using System.IO;
using System.Windows.Forms;

namespace _27_1qTweak
{
	static class Program
	{
		private const string PublicKeyPem = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1WILUIpvE2TOEA+wRnxU
3HlaXyJka2E9b93gTAWVja5sZTO+KpcDyaC3b5k8+kE7TVW7L+bHaG+m8syMGbme
JkrUzp1GKjXfBdiP7fm9i8Rtej2uDc0QuZVDwKt13aW4eSWpobxFCxF2yRoghKBa
Ut7SheVWVFYEfW+vJORYDLftIm8+U2dRPCVLkDIn4gMxlQtWVJ3Sj+pMN10YC1me
HnJg6bGB2FTT5f1+pNj8SK6BkggrFE5MXZYLaIlXDx3vDT874aMF9rHHoI5gPyW7
Wvv2Y8UiwFef9fz/zZtJvgfYLoQp+vfmXOQnNGq0gj7BQHDR8ecB9N8HUCfJetmU
AwIDAQAB
-----END PUBLIC KEY-----";
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
				// Debug info to help diagnose mismatch quickly
				try
				{
					string currentHw = HardwareFingerprint.Get();
					string licHw = "(no license or invalid)";
					if (!string.IsNullOrEmpty(lic))
					{
						var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
						var t = handler.ReadJwtToken(lic);
						licHw = t.Payload.TryGetValue("hw", out var v) ? v?.ToString() ?? "(null)" : "(no hw claim)";
					}
					MessageBox.Show($"License mismatch.\nCurrent HW:\n{currentHw}\n\nHW in license:\n{licHw}", "License", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				catch { }

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