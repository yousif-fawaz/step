using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;

namespace _27_1qTweak
{
	public static class HardwareFingerprint
	{
		public static string Get()
		{
			string guid = (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography", "MachineGuid", "") ?? "");
			string disk = (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\disk\Enum", "0", "") ?? "");
			using var sha = SHA256.Create();
			return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes($"{guid}|{disk}")));
		}
	}
}