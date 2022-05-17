using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using System.Collections;
using UnityEngine;

namespace LuuMod.Components
{
	class QuickMenuComponents
	{

		private static ReMenuToggle InfiniteJumpToggle;
		private static bool InfiniteJumpEnabled;

		public static void OnUiManagerInit(UiManager _UiManager)
		{
			InfiniteJumpToggle = _UiManager.MainMenu.AddToggle("Infinite Jump", "Enable/Disable Infinite Jump.", ToggleInfiniteJump, InfiniteJumpEnabled);
			MelonCoroutines.Start(InfiniteJump());
		}

		private static void ToggleInfiniteJump(bool Value)
		{
			InfiniteJumpEnabled = Value;
			MelonLogger.Msg($"Infinite Jump set to {Value}");
		}

		private static IEnumerator InfiniteJump()
		{
			while (true)
			{
				if (InfiniteJumpEnabled && Input.GetAxis("Jump") != 0f)
				{
					if (!VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.IsPlayerGrounded())
					{
						GameObject Plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
						Plane.GetComponent<Renderer>().enabled = false;
						Plane.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position;
						Object.Destroy(Plane, 0.5f);
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}
