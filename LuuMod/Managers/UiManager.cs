using LuuMod.Components;

namespace LuuMod.Managers
{
	class UiManager
	{
		private static ReMod.Core.Managers.UiManager _UiManager;
		public static void OnUiManagerInit()
		{
			_UiManager = new ReMod.Core.Managers.UiManager("LuuMod", null, true);
			TargetMenuComponents.OnUiManagerInit(_UiManager);
			QuickMenuComponents.OnUiManagerInit(_UiManager);
		}
	}
}
