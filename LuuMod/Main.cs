using MelonLoader;
using ReMod.Core.Managers;
using System.Collections;

[assembly: MelonInfo(typeof(LuuMod.Main), "LuuMod", "1", "Luu")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace LuuMod
{
	class Main : MelonMod
	{
		private static UiManager _UiManager;
		public override void OnApplicationStart()
		{
			Update.UpdateMod();
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
			_UiManager = new UiManager("LuuMod", null, true);
		}
	}
}
