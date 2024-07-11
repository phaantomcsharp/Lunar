using HarmonyLib;
using Stealth;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;

namespace RoomHandles
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    internal class LeavePatch : MonoBehaviour
    {
        private static void Prefix(Player otherPlayer)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer && otherPlayer != a)
            {
                NotifiLib.SendNotification("[<color=red>LEFT</color>] <color=white>Player: " + otherPlayer.NickName + " has left...</color>");
                a = otherPlayer;
            }
        }

        private static Player a;
    }
}