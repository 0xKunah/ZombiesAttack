using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using NativeUI;

namespace ZombiesAttack
{
    internal class Main : Script
    {
        internal static MenuPool _menuPool;
        internal UIMenu mainMenu = new UIMenu("Zombies attack", "Select an option");
        internal UIMenuItem putOnVRButton = new UIMenuItem("Put on VR headset");
        internal UIMenuItem startGameButton = new UIMenuItem("Start a game");
        public Main()
        {
            
            mainMenu.AddItem(putOnVRButton);
            mainMenu.AddItem(startGameButton);

            mainMenu.OnItemSelect += ButtonHandler;

            _menuPool = new MenuPool();
            _menuPool.Add(mainMenu);

            Tick += (o, e) =>
            {
                _menuPool.ProcessMenus();
            };

            KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.NumPad6) mainMenu.Visible = !mainMenu.Visible;
            };
        }

        private void ButtonHandler(dynamic sender, UIMenuItem item, int index)
        {
            if (item == startGameButton) StartGame();
            else if (item == putOnVRButton) PutOnVRHeadset();
        }

        private void StartGame()
        {
            if (Function.Call<int>(Hash.GET_PED_PROP_INDEX, Function.Call<Ped>(Hash.GET_PLAYER_PED, -1), 0) == 16)
            {
                GTA.Game.Player.Character.Weapons.RemoveAll();
                GTA.Game.Player.Character.Weapons.Give(WeaponHash.SNSPistol, 24, true, true);
                Game game = new Game();
            }
            else GTA.UI.Notification.Show("Put on a ~b~VR Headset~s~ before starting a game");
        }

        private void PutOnVRHeadset()
        {
            Function.Call<Ped>(Hash.SET_PED_PROP_INDEX, Function.Call<Ped>(Hash.GET_PLAYER_PED, -1), 0, 16, 0, 0);
        }

    }
}
