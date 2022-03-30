using MelonLoader;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace LuuMod
{
	class Update
	{
		public static void UpdateMod()
		{
			MelonLogger.Msg("Checking for LuuMod and updating if necessary.");
			byte[] Bytes = null;
			if (File.Exists("Mods/LuuMod.dll"))
			{
				Bytes = File.ReadAllBytes("Mods/LuuMod.dll");
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
				LatestBytes = Wc.DownloadData("https://github.com/L-uu/LuuMod/releases/latest/download/LuuMod.dll");
			}
			catch (WebException ex)
			{
				MelonLogger.Msg("Failed to download LuuMod, you might encounter issues. " + ex.ToString());
			}
			if (Bytes == null)
			{
				if (LatestBytes == null)
				{
					MelonLogger.Error("Failed to download LuuMod, and file doesn't exist. The mod won't work.");
					return;
				}
				MelonLogger.Msg("LuuMod not found, will try and download now.");
				Bytes = LatestBytes;
				try
				{
					File.WriteAllBytes("Mods/LuuMod.dll", Bytes);
				}
				catch (IOException ex)
				{
					MelonLogger.Warning("Failed to write LuuMod to disk, you might encounter issues. " + ex.ToString());
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
						MelonLogger.Msg("Updating LuuMod");
						Bytes = LatestBytes;
						try
						{
							File.WriteAllBytes("Mods/LuuMod.dll", Bytes);
						}
						catch (IOException ex)
						{
							MelonLogger.Warning("Failed to write LuuMod to disk. You might encounter errors. " + ex.ToString());
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
