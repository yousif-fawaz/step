using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace _27_1qTweak
{
	public class LicenseForm : Form
	{
		private readonly string _publicPem;
		private readonly string _licensePath;
		private TextBox _jwtBox;

		public LicenseForm(string publicPem, string licensePath)
		{
			_publicPem = publicPem;
			_licensePath = licensePath;

			Text = "License Activation - 27_1q tweak";
			Width = 720; Height = 480;
			StartPosition = FormStartPosition.CenterParent;

			var fp = new TextBox { ReadOnly = true, Dock = DockStyle.Top, Height = 32, Text = "HW: " + HardwareFingerprint.Get() };
			_jwtBox = new TextBox { Multiline = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Both };
			var save = new Button { Text = "Save", Dock = DockStyle.Bottom, Height = 36 };
			save.Click += OnSave;

			Controls.Add(_jwtBox);
			Controls.Add(save);
			Controls.Add(fp);
		}

		private void OnSave(object? sender, EventArgs e)
		{
			var jwt = _jwtBox.Text.Trim();
			if (!LicenseValidator.IsValid(jwt, _publicPem))
			{
				MessageBox.Show("Invalid or mismatched license.", "License", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Directory.CreateDirectory(Path.GetDirectoryName(_licensePath)!);
			File.WriteAllText(_licensePath, jwt, Encoding.UTF8);
			MessageBox.Show("License saved.");
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}