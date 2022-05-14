using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.VRChat;
using System;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.UI;

namespace LuuMod.Components
{
	class TargetMenuComponents
	{
		public static void OnUiManagerInit(UiManager _UiManager)
		{
			//FORCE CLONE
			_UiManager.TargetMenu.AddButton("Force Clone", "Force Clone this user's PUBLIC AVATAR.", () =>
			{
				try
				{
					var User = QuickMenuEx.SelectedUserLocal.field_Private_IUser_0;
					if (User == null) return;
					var Player = PlayerManager.field_Private_Static_PlayerManager_0.GetPlayer(User.prop_String_0);
					var Apiavatar = Player.GetApiAvatar();
					if (Apiavatar == null) return;
					if (Apiavatar.releaseStatus != "public") return;
					Transform Screens = GameObject.Find("UserInterface/MenuContent/Screens/").transform;
					PageAvatar PageAvatar = Screens.Find("Avatar").GetComponent<PageAvatar>();
					PageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
					{
						id = Apiavatar.id
					};
					PageAvatar.ChangeToSelectedAvatar();
				}
				catch (Exception ex)
				{
					MelonLogger.Error(ex.ToString());
				}
			}, null);

			//LOCAL BLOCK
			_UiManager.TargetMenu.AddButton("Local Block", "Block this user client-side only.", () =>
			{
				try
				{
					var User = QuickMenuEx.SelectedUserLocal.field_Private_IUser_0;
					if (User == null) return;
					var Player = PlayerManager.field_Private_Static_PlayerManager_0.GetPlayer(User.prop_String_0);
					if (Player.gameObject.activeSelf)
					{
						Player.gameObject.SetActive(false);
					}
					else
					{
						Player.gameObject.SetActive(true);
					}
				}
				catch (Exception ex)
				{
					MelonLogger.Error(ex.ToString());
				}
			});
		}
	}
}
