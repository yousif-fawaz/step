using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _27_1qTweak
{
	public class MainForm : Form
	{
		private TextBox _log;
		private FlowLayoutPanel _left;
		private readonly List<(string Label, Func<Task> Action)> _actions;

		public MainForm()
		{
			Text = "27_1q tweak";
			StartPosition = FormStartPosition.CenterScreen;
			ClientSize = new Size(1100, 700);
			Font = new Font("Segoe UI", 9f);

			_left = new FlowLayoutPanel
			{
				Dock = DockStyle.Left,
				Width = 360,
				FlowDirection = FlowDirection.TopDown,
				WrapContents = false,
				AutoScroll = true,
				Padding = new Padding(12)
			};

			var right = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
			_log = new TextBox
			{
				Multiline = true,
				Dock = DockStyle.Fill,
				ScrollBars = ScrollBars.Both,
				ReadOnly = true,
				WordWrap = false,
				Font = new Font("Consolas", 10f)
			};

			right.Controls.Add(_log);
			Controls.Add(right);
			Controls.Add(_left);

			_actions = new List<(string, Func<Task>)>
			{
				("1) FULL TWEAK - Complete System Optimizer", RunFullTweakAsync),
				("2) BOOST NOW - Immediate Performance Boost", RunBoostNowAsync),
				("3) GAME MODE - Optimize for Gaming", RunGameModeAsync),
				("4) WORK MODE - Optimize for Productivity", RunWorkModeAsync),
				("5) ULTIMATE NETWORK BOOST", RunNetworkBoostAsync),
				("6) ADVANCED SSD OPTIMIZATION", RunSsdOptimizationAsync),
				("7) EXTREME DEBLOAT (No Xbox Removal)", RunExtremeDebloatAsync),
				("8) ROLLBACK - Restore Default Settings", RunRollbackAsync),
				("9) SYSTEM REPAIR - Fix System & Disk Errors", RunSystemRepairAsync),
				("10) NETWORK RESET - Reset Network Settings", RunNetworkResetAsync),
				("11) Exit", async () => Close())
			};

			foreach (var (label, func) in _actions)
			{
				var b = new Button
				{
					Text = label,
					Width = 320,
					Height = 42,
					Margin = new Padding(0, 0, 0, 8)
				};
				b.Click += async (s, e) => await RunActionAsync(label, func);
				_left.Controls.Add(b);
			}
		}

		private void SetBusy(bool busy)
		{
			foreach (Control c in _left.Controls) c.Enabled = !busy;
		}

		private void Log(string line)
		{
			if (InvokeRequired) { BeginInvoke(new Action<string>(Log), line); return; }
			_log.AppendText(line + Environment.NewLine);
			_log.SelectionStart = _log.TextLength; _log.ScrollToCaret();
		}

		private async Task RunActionAsync(string title, Func<Task> action)
		{
			SetBusy(true);
			Log($"=== {title} ===");
			try { await action(); }
			catch (Exception ex) { Log("[ERROR] " + ex.Message); }
			finally { SetBusy(false); }
		}

		// ===== Actions (no temp .bat files) =====
		private async Task RunFullTweakAsync()
		{
			string[] cmds =
			{
				"echo Running FULL TWEAK - Complete System Optimizer...",
				"wmic computersystem set AutomaticManagedPagefile=False >nul 2>&1",
				"wmic pagefileset where name=\"C:\\pagefile.sys\" set InitialSize=4096,MaximumSize=4096 >nul 2>&1",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"DisablePagingExecutive\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"LargeSystemCache\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"SystemCacheDirtyPageThreshold\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"MenuShowDelay\" /t REG_SZ /d 0 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VisualEffects\" /v \"VisualFXSetting\" /t REG_DWORD /d 2 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Windows\\DWM\" /v \"EnableAeroPeek\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Windows\\DWM\" /v \"AlwaysHibernateThumbnails\" /t REG_DWORD /d 0 /f >nul",
				"fsutil behavior set disablelastaccess 1 >nul",
				"fsutil behavior set disable8dot3 1 >nul",
				"fsutil behavior set disablecompression 1 >nul",
				"fsutil behavior set mftzone 2 >nul",
				"fsutil behavior set memoryusage 2 >nul",
				"fsutil behavior set encryptpagingfile 0 >nul",
				"netsh int tcp set global congestionprovider=ctcp >nul",
				"netsh int tcp set global ecncapability=disabled >nul",
				"netsh int tcp set global rss=enabled >nul",
				"netsh int tcp set global chimney=enabled >nul",
				"netsh int tcp set global autotuninglevel=normal >nul",
				"netsh int tcp set global timestamps=disabled >nul",
				"netsh int tcp set global nonsackrttresiliency=disabled >nul",
				"netsh int tcp set global initialRto=2000 >nul",
				"netsh int tcp set global rsc=enabled >nul",
				"del /f /q \"%SYSTEMDRIVE%\\*.tmp\" >nul 2>&1",
				"del /f /q \"%SYSTEMDRIVE%\\*._mp\" >nul 2>&1",
				"del /f /q \"%SYSTEMDRIVE%\\*.log\" >nul 2>&1",
				"del /f /q \"%SYSTEMDRIVE%\\*.gid\" >nul 2>&1",
				"del /f /q \"%SYSTEMDRIVE%\\*.chk\" >nul 2>&1",
				"del /f /q \"%SYSTEMDRIVE%\\*.old\" >nul 2>&1",
				"del /f /q \"%WINDIR%\\*.bak\" >nul 2>&1",
				"del /f /q \"%WINDIR%\\prefetch\\*.*\" >nul 2>&1",
				"del /f /q \"%LOCALAPPDATA%\\Temp\\*.*\" >nul 2>&1",
				"del /f /q \"%USERPROFILE%\\AppData\\Local\\Microsoft\\Windows\\INetCache\\*.*\" >nul 2>&1",
				"del /f /q \"%USERPROFILE%\\AppData\\Local\\Microsoft\\Windows\\WebCache\\*.*\" >nul 2>&1",
				"echo FULL TWEAK completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunBoostNowAsync()
		{
			string[] cmds =
			{
				"echo Running BOOST NOW - Immediate Performance Boost...",
				"del /f /q \"%temp%\\*.*\" >nul 2>&1",
				"del /f /q \"%windir%\\temp\\*.*\" >nul 2>&1",
				"del /f /q \"%windir%\\prefetch\\*.*\" >nul 2>&1",
				"wevtutil cl Application >nul 2>&1",
				"wevtutil cl System >nul 2>&1",
				"wevtutil cl Security >nul 2>&1",
				"del /f /q \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\Cache\\*.*\" >nul 2>&1",
				"del /f /q \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\Cache\\*.*\" >nul 2>&1",
				"del /f /q \"%LOCALAPPDATA%\\Mozilla\\Firefox\\Profiles\\*\\cache2\\*.*\" >nul 2>&1",
				"net stop wuauserv >nul 2>&1",
				"net stop bits >nul 2>&1",
				"rmdir /s /q \"%WINDIR%\\SoftwareDistribution\\Download\" >nul 2>&1",
				"mkdir \"%WINDIR%\\SoftwareDistribution\\Download\" >nul 2>&1",
				"net start bits >nul 2>&1",
				"net start wuauserv >nul 2>&1",
				"net stop DiagTrack >nul 2>&1",
				"net stop dmwappushservice >nul 2>&1",
				"net stop SysMain >nul 2>&1",
				"echo BOOST NOW completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunGameModeAsync()
		{
			string[] cmds =
			{
				"echo Running GAME MODE - Optimize for Gaming...",
				"reg add \"HKCU\\Software\\Microsoft\\GameBar\" /v \"AutoGameModeEnabled\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\GameBar\" /v \"AllowAutoGameMode\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKCU\\System\\GameConfigStore\" /v \"GameDVR_FSEBehaviorMode\" /t REG_DWORD /d 2 /f >nul",
				"reg add \"HKCU\\System\\GameConfigStore\" /v \"GameDVR_FSEBehavior\" /t REG_DWORD /d 2 /f >nul",
				"reg add \"HKCU\\System\\GameConfigStore\" /v \"GameDVR_HonorUserFSEBehaviorMode\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKCU\\System\\GameConfigStore\" /v \"GameDVR_DXGIHonorFSEWindowsCompatible\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKCU\\System\\GameConfigStore\" /v \"GameDVR_EFSEFeatureFlags\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKCU\\Control Panel\\Mouse\" /v \"MouseSensitivity\" /t REG_SZ /d 10 /f >nul",
				"reg add \"HKCU\\Control Panel\\Mouse\" /v \"SmoothMouseYCurve\" /t REG_BINARY /d 0000000000000000C0CC0C0000000000809919000000000040662600000000000033330000000000 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"SystemResponsiveness\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\" /v \"NetworkThrottlingIndex\" /t REG_DWORD /d 4294967295 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Affinity\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Background Only\" /t REG_SZ /d False /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Clock Rate\" /t REG_DWORD /d 10000 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"GPU Priority\" /t REG_DWORD /d 8 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Priority\" /t REG_DWORD /d 6 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"Scheduling Category\" /t REG_SZ /d High /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Tasks\\Games\" /v \"SFIO Priority\" /t REG_SZ /d High /f >nul",
				"powercfg -duplicatescheme 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c >nul",
				"powercfg /setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c >nul",
				"powercfg /change -monitor-timeout-ac 0 >nul",
				"powercfg /change -disk-timeout-ac 0 >nul",
				"powercfg /change -standby-timeout-ac 0 >nul",
				"powercfg /change -hibernate-timeout-ac 0 >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VisualEffects\" /v \"VisualFXSetting\" /t REG_DWORD /d 3 /f >nul",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_BINARY /d 9012078010000000 /f >nul",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"DragFullWindows\" /t REG_SZ /d 0 /f >nul",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"FontSmoothing\" /t REG_SZ /d 0 /f >nul",
				"echo GAME MODE completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunWorkModeAsync()
		{
			string[] cmds =
			{
				"echo Running WORK MODE - Optimize for Productivity...",
				"net start WSearch >nul 2>&1",
				"net start SysMain >nul 2>&1",
				"net start wuauserv >nul 2>&1",
				"net start bits >nul 2>&1",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"DragFullWindows\" /t REG_SZ /d 1 /f >nul",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"FontSmoothing\" /t REG_SZ /d 2 /f >nul",
				"reg add \"HKCU\\Control Panel\\Desktop\" /v \"UserPreferencesMask\" /t REG_BINARY /d 9E3E078012000000 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v \"MultiTaskingAltTabFilter\" /t REG_DWORD /d 3 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v \"TaskbarGlomLevel\" /t REG_DWORD /d 0 /f >nul",
				"powercfg -duplicatescheme 381b4222-f694-41f0-9685-ff5bb260df2e >nul",
				"powercfg /setactive 381b4222-f694-41f0-9685-ff5bb260df2e >nul",
				"powercfg /change -monitor-timeout-ac 15 >nul",
				"powercfg /change -disk-timeout-ac 20 >nul",
				"powercfg /change -standby-timeout-ac 30 >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Office\\16.0\\Common\\Graphics\" /v \"DisableHardwareAcceleration\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Office\\16.0\\Common\\Graphics\" /v \"EnableSubpixelPositioning\" /t REG_DWORD /d 1 /f >nul",
				"reg add \"HKCU\\Software\\Microsoft\\Office\\16.0\\Common\" /v \"DisableBootToOfficeStart\" /t REG_DWORD /d 1 /f >nul",
				"echo WORK MODE completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunNetworkBoostAsync()
		{
			string[] cmds =
			{
				"echo Running ULTIMATE NETWORK BOOST...",
				"netsh int tcp set global rss=enabled >nul",
				"netsh int tcp set global chimney=enabled >nul",
				"netsh int tcp set global autotuninglevel=normal >nul",
				"netsh int tcp set global ecncapability=disabled >nul",
				"netsh int tcp set global timestamps=disabled >nul",
				"netsh int tcp set global initialRto=2000 >nul",
				"netsh int tcp set global rsc=enabled >nul",
				"netsh int tcp set global fastopen=enabled >nul",
				"netsh int tcp set global pacingprofile=off >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\" /v \"TcpNumConnections\" /t REG_DWORD /d 16777214 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\" /v \"MaxUserPort\" /t REG_DWORD /d 65534 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\" /v \"TcpTimedWaitDelay\" /t REG_DWORD /d 30 /f >nul",
				"ipconfig /flushdns >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\" /v \"MaxCacheEntryTtlLimit\" /t REG_DWORD /d 86400 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\" /v \"MaxSOACacheEntryTtlLimit\" /t REG_DWORD /d 300 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\" /v \"NegativeCacheTime\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\" /v \"NetFailureCacheTime\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\" /v \"NegativeSOACacheTime\" /t REG_DWORD /d 0 /f >nul",
				"netsh wlan set autoconfig enabled=yes interface=\"Wi-Fi\" >nul 2>&1",
				"powershell -command \"Set-NetAdapterAdvancedProperty -Name 'Wi-Fi' -DisplayName 'Roaming Aggressiveness' -DisplayValue 'Lowest' -ErrorAction SilentlyContinue\" >nul 2>&1",
				"powershell -command \"Set-NetAdapterAdvancedProperty -Name 'Wi-Fi' -DisplayName 'Preferred Band' -DisplayValue '5GHz' -ErrorAction SilentlyContinue\" >nul 2>&1",
				"powershell -command \"Set-NetAdapterAdvancedProperty -Name 'Wi-Fi' -DisplayName 'Power Save Mode' -DisplayValue 'Maximum Performance' -ErrorAction SilentlyContinue\" >nul 2>&1",
				"ipconfig /release >nul",
				"ipconfig /renew >nul",
				"ipconfig /flushdns >nul",
				"netsh winsock reset >nul",
				"echo ULTIMATE NETWORK BOOST completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunSsdOptimizationAsync()
		{
			string[] cmds =
			{
				"echo Running ADVANCED SSD OPTIMIZATION...",
				"fsutil behavior set DisableDeleteNotify 0 >nul",
				"fsutil behavior set DisableLastAccess 1 >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"ClearPageFileAtShutdown\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\\PrefetchParameters\" /v \"EnablePrefetcher\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\\PrefetchParameters\" /v \"EnableSuperfetch\" /t REG_DWORD /d 0 /f >nul",
				"sc config \"WSearch\" start= disabled >nul 2>&1",
				"sc stop \"WSearch\" >nul 2>&1",
				"powercfg -h off >nul 2>&1",
				"fsutil resource setautoreset true C:\\ >nul",
				"fsutil behavior set disablelastaccess 1 >nul",
				"fsutil behavior set mftzone 2 >nul",
				"schtasks /create /tn \"SSD TRIM Weekly\" /tr \"fsutil behavior set disabledeletenotify 0\" /sc weekly /d SAT /st 12:00 /ru \"SYSTEM\" /f >nul 2>&1",
				"echo ADVANCED SSD OPTIMIZATION completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunExtremeDebloatAsync()
		{
			string[] cmds =
			{
				"echo Running EXTREME DEBLOAT (Keeping Xbox Features)...",
				"PowerShell -Command \"Get-AppxPackage *bing* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *zune* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *3d* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *phone* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *soundrecorder* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *commsphone* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *windowsphone* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *maps* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *feedback* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *messaging* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *solitaire* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *connectivitystore* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"PowerShell -Command \"Get-AppxPackage *Microsoft.YourPhone* | Remove-AppxPackage -ErrorAction SilentlyContinue\"",
				"sc config \"DiagTrack\" start= disabled >nul",
				"sc config \"dmwappushservice\" start= disabled >nul",
				"sc config \"RetailDemo\" start= disabled >nul",
				"sc config \"diagnosticshub.standardcollector.service\" start= disabled >nul",
				"sc config \"WMPNetworkSvc\" start= disabled >nul",
				"sc config \"WerSvc\" start= disabled >nul",
				"sc config \"ndu\" start= disabled >nul",
				"sc config \"WbioSrvc\" start= disabled >nul",
				"sc config \"PcaSvc\" start= disabled >nul",
				"sc config \"HomeGroupListener\" start= disabled >nul",
				"sc config \"HomeGroupProvider\" start= disabled >nul",
				"schtasks /Change /TN \"Microsoft\\Windows\\Application Experience\\Microsoft Compatibility Appraiser\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Application Experience\\ProgramDataUpdater\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Customer Experience Improvement Program\\Consolidator\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Customer Experience Improvement Program\\UsbCeip\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\DiskDiagnostic\\Microsoft-Windows-DiskDiagnosticDataCollector\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Power Efficiency Diagnostics\\AnalyzeSystem\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Shell\\FamilySafetyMonitor\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Shell\\FamilySafetyRefresh\" /DISABLE >nul 2>&1",
				"schtasks /Change /TN \"Microsoft\\Windows\\Windows Error Reporting\\QueueReporting\" /DISABLE >nul 2>&1",
				"reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection\" /v \"AllowTelemetry\" /t REG_DWORD /d 0 /f >nul",
				"reg add \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection\" /v \"AllowTelemetry\" /t REG_DWORD /d 0 /f >nul",
				"echo EXTREME DEBLOAT completed successfully!"
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunRollbackAsync()
		{
			string[] cmds =
			{
				"echo Running ROLLBACK - Restoring Default Settings...",
				"wmic computersystem set AutomaticManagedPagefile=True >nul 2>&1",
				"sc config \"WSearch\" start= delayed-auto >nul 2>&1",
				"net start \"WSearch\" >nul 2>&1",
				"powercfg /setactive 381b4222-f694-41f0-9685-ff5bb260df2e >nul 2>&1",
				"powercfg -h on >nul 2>&1",
				"echo ROLLBACK finished."
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunSystemRepairAsync()
		{
			string[] cmds =
			{
				"echo Running SYSTEM REPAIR - Fix System & Disk Errors...",
				"DISM /Online /Cleanup-Image /CheckHealth",
				"DISM /Online /Cleanup-Image /ScanHealth",
				"DISM /Online /Cleanup-Image /RestoreHealth",
				"sfc /scannow",
				"echo Y | chkdsk C: /f /r",
				"echo SYSTEM REPAIR completed! If prompted, please restart your computer."
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}

		private async Task RunNetworkResetAsync()
		{
			string[] cmds =
			{
				"echo Running NETWORK RESET - Resetting Network Settings...",
				"netsh int ip reset",
				"netsh winsock reset",
				"ipconfig /flushdns",
				"echo NETWORK RESET completed! Please restart your computer."
			};
			foreach (var c in cmds) await ProcessRunner.RunCommandAsync(c, Log);
		}
	}
}