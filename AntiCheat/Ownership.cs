using Photon.Pun;
using Stealth;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace stealth.Core
{
    internal class Ownership
    {
        public static void SetOwnership()
        {
            const bool isplayersactornumberownership = false;
            Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[PhotonNetwork.CurrentRoom.PlayerCount];
            GorillaNot gorilla = new GorillaNot();
            PhotonView gorillaview = gorilla.GetComponent<PhotonView>();
            gorillaview.TransferOwnership(PhotonNetwork.LocalPlayer);
            gorillaview.RequestOwnership();
            gorillaview.OwnershipTransfer = OwnershipOption.Takeover;
            gorillaview.ControllerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            gorillaview.OwnerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            if (gorillaview.OwnerActorNr != PhotonNetwork.LocalPlayer.ActorNumber || gorillaview.ControllerActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                gorillaview.TransferOwnership(PhotonNetwork.LocalPlayer);
                gorillaview.RequestOwnership();
                gorillaview.OwnershipTransfer = OwnershipOption.Takeover;
                gorillaview.ControllerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
                gorillaview.OwnerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            }
            else
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are now ownership.");
                return;
            }
        }

        public static void SetOwnershipMonsters()
        {
            const bool isplayersactornumberownership = false;
            Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[PhotonNetwork.CurrentRoom.PlayerCount];
            MonkeyeAI gorilla = new MonkeyeAI();
            PhotonView gorillaview = gorilla.GetComponent<PhotonView>();
            gorillaview.TransferOwnership(PhotonNetwork.LocalPlayer);
            gorillaview.RequestOwnership();
            gorillaview.OwnershipTransfer = OwnershipOption.Takeover;
            gorillaview.ControllerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            gorillaview.OwnerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            if (gorillaview.OwnerActorNr != PhotonNetwork.LocalPlayer.ActorNumber || gorillaview.ControllerActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                gorillaview.TransferOwnership(PhotonNetwork.LocalPlayer);
                gorillaview.RequestOwnership();
                gorillaview.OwnershipTransfer = OwnershipOption.Takeover;
                gorillaview.ControllerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
                gorillaview.OwnerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            }
            else
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are now ownership.");
                return;
            }
        }

        public static void SetOwnershipBug()
        {
            const bool isplayersactornumberownership = false;
            Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[PhotonNetwork.CurrentRoom.PlayerCount];
            GameObject gorilla = GameObject.Find("Throwable Bug");
            PhotonView gorillaview = gorilla.GetComponent<PhotonView>();
            gorillaview.TransferOwnership(PhotonNetwork.LocalPlayer);
            gorillaview.RequestOwnership();
            gorillaview.OwnershipTransfer = OwnershipOption.Takeover;
            gorillaview.ControllerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            gorillaview.OwnerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            if (gorillaview.OwnerActorNr != PhotonNetwork.LocalPlayer.ActorNumber || gorillaview.ControllerActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                gorillaview.TransferOwnership(PhotonNetwork.LocalPlayer);
                gorillaview.RequestOwnership();
                gorillaview.OwnershipTransfer = OwnershipOption.Takeover;
                gorillaview.ControllerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
                gorillaview.OwnerActorNr = isplayersactornumberownership ? PhotonNetwork.LocalPlayer.ActorNumber : player.ActorNumber;
            }
            else
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are now ownership.");
                return;
            }
        }
    }
}
