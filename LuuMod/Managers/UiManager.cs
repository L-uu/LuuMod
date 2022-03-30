using LuuMod.Components;
using MelonLoader;

namespace LuuMod.Managers
{
	class UiManager
	{
		private static ReMod.Core.Managers.UiManager _UiManager;
		public static void OnUiManagerInit()
		{
			MelonLogger.Msg("Initializing UI...");
			_UiManager = new ReMod.Core.Managers.UiManager("LuuMod", null, true);
			TargetMenuComponents.OnUiManagerInit(_UiManager);
		}
	}
}
