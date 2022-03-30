using MelonLoader;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LuuMod.Managers
{
	class UpdateManager
	{
		public static void UpdateMod()
		{
			MelonLogger.Msg("Checking for updates...");
			byte[] Bytes = null;
			Bytes = File.ReadAllBytes("Mods/LuuMod.dll");
			var Wc = new WebClient
			{
				Headers =
				{
					["User-Agent"] =
						"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36 OPR/85.0.4341.18"
				}
			};
			byte[] LatestBytes = null;
			try
			{
				LatestBytes = Wc.DownloadData("https://github.com/L-uu/LuuMod/releases/latest/download/LuuMod.dll");
			}
			catch (WebException ex)
			{
				MelonLogger.Msg("Failed to download LuuMod, you might encounter issues. " + ex.ToString());
			}
			if (LatestBytes != null)
			{
				var sha256 = SHA256.Create();
				var LatestHash = ComputeHash(sha256, LatestBytes);
				var CurrentHash = ComputeHash(sha256, Bytes);
				if (LatestHash != CurrentHash)
				{
					MelonLogger.Msg("Updating LuuMod...");
					Bytes = LatestBytes;
					try
					{
						File.WriteAllBytes("Mods/LuuMod.dll", Bytes);
						DialogResult Result = MessageBox.Show("LuuMod has updated and VRChat must be restarted to apply the update. Restart now?", "LuuMod Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
						if (Result == DialogResult.Yes)
						{
							Application.Exit();
							Process.Start(System.Environment.CurrentDirectory + "\\VRChat.exe", System.Environment.CommandLine.ToString());
						}
						else if (Result == DialogResult.No)
						{
							return;
						}
					}
					catch (IOException ex)
					{
						MelonLogger.Warning("Failed to write LuuMod to disk. You might encounter errors. " + ex.ToString());
					}
				}
			}
		}

		public static void UpdateCore()
		{
			MelonLogger.Msg("Checking for ReMod.Core and updating if necessary...");
			byte[] Bytes = null;
			if (File.Exists("ReMod.Core.dll"))
			{
				Bytes = File.ReadAllBytes("ReMod.Core.dll");
			}
			var Wc = new WebClient
			{
				Headers =
				{
					["User-Agent"] =
						"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36 OPR/85.0.4341.18"
				}
			};
			byte[] LatestBytes = null;
			try
			{
				LatestBytes = Wc.DownloadData("https://github.com/RequiDev/ReMod.Core/releases/latest/download/ReMod.Core.dll");
			}
			catch (WebException ex)
			{
				MelonLogger.Msg("Failed to download ReMod.Core, you might encounter issues. " + ex.ToString());
			}
			if (Bytes == null)
			{
				if (LatestBytes == null)
				{
					MelonLogger.Error("Failed to download ReMod.Core, and file doesn't exist. The mod won't work.");
					return;
				}
				MelonLogger.Msg("ReMod.Core not found, will try and download now.");
				Bytes = LatestBytes;
				try
				{
					File.WriteAllBytes("ReMod.Core.dll", Bytes);
					DialogResult Result = MessageBox.Show("ReMod.Core has updated and VRChat must be restarted to apply the update. Restart now?", "ReMod.Core Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					if (Result == DialogResult.Yes)
					{
						Application.Exit();
						Process.Start(System.Environment.CurrentDirectory + "\\VRChat.exe", System.Environment.CommandLine.ToString());
					}
					else if (Result == DialogResult.No)
					{
						return;
					}
				}
				catch (IOException ex)
				{
					MelonLogger.Warning("Failed to write ReMod.Core to disk, you might encounter issues. " + ex.ToString());
				}
			}
			else
			{
				if (LatestBytes != null)
				{
					var sha256 = SHA256.Create();
					var LatestHash = ComputeHash(sha256, LatestBytes);
					var CurrentHash = ComputeHash(sha256, Bytes);
					if (LatestHash != CurrentHash)
					{
						MelonLogger.Msg("Updating ReMod.Core");
						Bytes = LatestBytes;
						try
						{
							File.WriteAllBytes("ReMod.Core.dll", Bytes);
							DialogResult Result = MessageBox.Show("ReMod.Core has updated and VRChat must be restarted to apply the update. Restart now?", "ReMod.Core Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
							if (Result == DialogResult.Yes)
							{
								Application.Exit();
								Process.Start(System.Environment.CurrentDirectory + "\\VRChat.exe", System.Environment.CommandLine.ToString());
							}
							else if (Result == DialogResult.No)
							{
								return;
							}
						}
						catch (IOException ex)
						{
							MelonLogger.Warning("Failed to write ReMod.Core to disk. You might encounter errors. " + ex.ToString());
						}
					}
				}
			}
		}

		private static string ComputeHash(HashAlgorithm sha256, byte[] data)
		{
			var Bytes = sha256.ComputeHash(data);
			var Sb = new StringBuilder();
			foreach (var b in Bytes)
			{
				Sb.Append(b.ToString("x2"));
			}

			return Sb.ToString();
		}
	}
}
