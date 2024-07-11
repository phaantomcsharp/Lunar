using HarmonyLib;
using Stealth;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Threading;
using UnityEngine;

namespace RoomHandles
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    internal class JoinPatch : MonoBehaviour
    {
        private static void Prefix(Player newPlayer)
        {
            if (newPlayer != oldnewplayer)
            {
                NotifiLib.SendNotification("[<color=green>JOIN</color>] <color=white>Player: " + newPlayer.NickName + " has joined...</color>");
                oldnewplayer = newPlayer;
            }
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                NotifiLib.SendNotification("[<color=green>MASTER</color>] <color=yellow>YOU ARE NOW MASTER CLIENT...</color>");
                return;
            }
        }

        private static Player oldnewplayer;
    }
}