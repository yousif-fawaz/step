using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace _27_1qTweak
{
	public static class HardwareFingerprint
	{
		public static string Get()
		{
			string guid = ReadHKLM(@"SOFTWARE\Microsoft\Cryptography", "MachineGuid") ?? "";
			string disk = ReadHKLM(@"SYSTEM\CurrentControlSet\Services\disk\Enum", "0") ?? "";
			using var sha = SHA256.Create();
			return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes($"{guid}|{disk}")));
		}

		private static string? ReadHKLM(string subkey, string value)
		{
			try
			{
				using (var k64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(subkey))
				{
					var v = k64?.GetValue(value)?.ToString();
					if (!string.IsNullOrEmpty(v)) return v;
				}
			}
			catch {}
			try
			{
				using (var k32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
				{
					return k32?.GetValue(value)?.ToString();
				}
			}
			catch { return null; }
		}
	}
}