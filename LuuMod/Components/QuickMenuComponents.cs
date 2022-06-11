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
				if (InfiniteJumpEnabled)
				{
					if (VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Boolean_2
						&& RoomManager.field_Internal_Static_ApiWorld_0 != null
						&& !VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.IsPlayerGrounded())
					{
						try
						{
							Vector3 PlayerVelocity = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetVelocity();
							PlayerVelocity.y = 3f;
							VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.SetVelocity(PlayerVelocity);
						}
						catch
						{
						}
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}
