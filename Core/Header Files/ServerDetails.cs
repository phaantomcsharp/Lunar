using Photon.Pun;
using Stealth;
using System;
using System.Collections.Generic;
using System.Text;

namespace stealth.Core.Header_Files
{
    internal class ServerDetails
    {
        public static void ServerAddress()
        {
            NotifiLib.SendNotification("[<color=green>SERVER</color>] Server Address is: " + PhotonNetwork.ServerAddress);
            return;
        }

        public static void ServerPlayerCount()
        {
            NotifiLib.SendNotification("[<color=green>SERVER</color>] Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount + "/ " + PhotonNetwork.CurrentRoom.MaxPlayers);
            return;
        }
        public static void ServerMaxPlayerCount()
        {
            NotifiLib.SendNotification("[<color=green>SERVER</color>] Max Player Count: " + PhotonNetwork.CurrentRoom.MaxPlayers);
            return;
        }

        public static void ServerName()
        {
            NotifiLib.SendNotification("[<color=green>SERVER</color>] Room Name: " + PhotonNetwork.CurrentRoom.Name);
            return;
        }

        public static void IsPublic()
        {
            if (PhotonNetwork.CurrentRoom.IsVisible == true)
            {
                NotifiLib.SendNotification("[<color=green>SERVER</color>] Is Public: " + PhotonNetwork.CurrentRoom.IsVisible);
                return;
            }
        }

        public static void IsPrivate()
        {
            if (PhotonNetwork.CurrentRoom.IsVisible == false)
            {
                NotifiLib.SendNotification("[<color=green>SERVER</color>] Is Private: " + PhotonNetwork.CurrentRoom.IsVisible);
                return;
            }
        }
    }
}
