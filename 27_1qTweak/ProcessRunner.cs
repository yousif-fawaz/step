using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace _27_1qTweak
{
	public static class ProcessRunner
	{
		public static async Task<int> RunCommandAsync(string command, Action<string>? onLine)
		{
			var psi = new ProcessStartInfo("cmd.exe", "/c " + command)
			{
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				CreateNoWindow = true,
				StandardOutputEncoding = Encoding.GetEncoding(1256),
				StandardErrorEncoding = Encoding.GetEncoding(1256)
			};

			using var proc = new Process();
			proc.StartInfo = psi;
			proc.OutputDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) onLine?.Invoke(e.Data); };
			proc.ErrorDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) onLine?.Invoke(e.Data); };
			proc.Start();
			proc.BeginOutputReadLine();
			proc.BeginErrorReadLine();
			await Task.Run(() => proc.WaitForExit());
			return proc.ExitCode;
		}
	}
}