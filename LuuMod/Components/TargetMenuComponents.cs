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
			_UiManager.TargetMenu.AddButton("Force Clone", "Force Clone this user's PUBLIC AVATAR.", () =>
			{
				try
				{
					var user = QuickMenuEx.SelectedUserLocal.field_Private_IUser_0;
					if (user == null) return;
					var player = PlayerManager.field_Private_Static_PlayerManager_0.GetPlayer(user.prop_String_0);
					var apiavatar = player.GetApiAvatar();
					if (apiavatar == null) return;
					if (apiavatar.releaseStatus != "public") return;
					Transform screens = GameObject.Find("UserInterface/MenuContent/Screens/").transform;
					PageAvatar PageAvatar = screens.Find("Avatar").GetComponent<PageAvatar>();
					PageAvatar.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
					{
						id = apiavatar.id
					};
					PageAvatar.ChangeToSelectedAvatar();
				}
				catch (Exception ex)
				{
					MelonLogger.Error(ex.ToString());
				}
			}, null);
		}
	}
}
