using LuuMod.Managers;
using MelonLoader;
using System.Collections;

[assembly: MelonInfo(typeof(LuuMod.Main), "LuuMod", "1", "Luu")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace LuuMod
{
	class Main : MelonMod
	{
		public override void OnApplicationStart()
		{
			UpdateManager.UpdateMod();
			UpdateManager.UpdateCore();
			MelonCoroutines.Start(WaitForUiManagerInit());
		}

		private IEnumerator WaitForUiManagerInit()
		{
			while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null) yield return null;
			OnUiManagerInit();
			yield break;
		}

		private void OnUiManagerInit()
		{
			LoggerInstance.Msg("Initializing UI...");
			UiManager.OnUiManagerInit();
		}
	}
}
