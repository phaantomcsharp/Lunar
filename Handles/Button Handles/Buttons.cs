using Mods;
using static Mods.SettingsMods;
using static Mods.Globals;
using static Mods.Mods;
using Photon.Pun;
using UnityEngine;
using GorillaNetworking;
using TMPro;
using Fusion;
using UnityEngine.InputSystem;
using Stealth.Handles.Button_Handles;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Voice;
using Stealth.Core.Header_Files;
using Patching.rigpatching;
using stealth.Core.Header_Files;
using stealth.Core;
using System;


namespace Stealth
{
    internal class Buttons
    {
        public static List<categorytemplate> categories = new List<categorytemplate>
        {
            new categorytemplate("List")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Disable Slide Audio", method =() => GhostRig.DisableSlideAudio(), isTogglable = true },
                    new buttontemplate { Text = "Settings", method =() => JoinTab(1), isTogglable = false },
                    new buttontemplate { Text = "Room", method =() => JoinTab(2), isTogglable = false },
                    new buttontemplate { Text = "Movement", method =() => JoinTab(3), isTogglable = false },
                    new buttontemplate { Text = "Player", method =() => JoinTab(4), isTogglable = false },
                    new buttontemplate { Text = "Overpowered", method =() => JoinTab(5), isTogglable = false },
                    new buttontemplate { Text = "Visual", method =() => JoinTab(6), isTogglable = false },
                    new buttontemplate { Text = "Safety", method =() => JoinTab(7), isTogglable = false },
                    new buttontemplate { Text = "Projectiles", method =() => JoinTab(8), isTogglable = false },
                    new buttontemplate { Text = "Maps & Gamemodes", method =() => JoinTab(9), isTogglable = false },
                    new buttontemplate { Text = "Cosmetics", method =() => JoinTab(10), isTogglable = false },
                    new buttontemplate { Text = "Server Details", method =() => JoinTab(11), isTogglable = false },
                    new buttontemplate { Text = "PC Details", method =() => JoinTab(12), isTogglable = false },
                    new buttontemplate { Text = "Misc", method =() => JoinTab(14), isTogglable = false },
                }
            },
            new categorytemplate("Settings")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Right Hand", enableMethod =() => RightHand(), disableMethod =() => LeftHand() },
                    new buttontemplate { Text = "Change Weather: None", method =() => TimeOfDay(), isTogglable = false},
                    new buttontemplate { Text = "Array List", method =() => ArrayList(), disableMethod =() => NoArrayList(),isTogglable = true},
                    new buttontemplate { Text = "ESP Settings", method =() => JoinTab(13),isTogglable = false},
                    new buttontemplate { Text = "Change Theme: Lunar", method =() => ThemeChange(),isTogglable = false},
                }
            },
            new categorytemplate("Room")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Disconnect", method =() => PhotonNetwork.Disconnect(), isTogglable = false },
                    new buttontemplate { Text = "Join Random", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false },
                    new buttontemplate { Text = "Quit", method =() => Application.Quit(), isTogglable = false },
                    new buttontemplate { Text = "Set Room Public", method =() => RoomSettings(true, false), isTogglable = false },
                    new buttontemplate { Text = "Set Room Private", method =() => RoomSettings(false, true), isTogglable = false },
                    new buttontemplate { Text = "Join Forest", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false},
                    new buttontemplate { Text = "Join Canyons", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false},
                    new buttontemplate { Text = "Join City", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false},
                    new buttontemplate { Text = "Join Caves", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Cave").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false},
                    new buttontemplate { Text = "Join Basement", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Basement for Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false},
                    new buttontemplate { Text = "Join Beach", method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Beach from Forest").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered(), isTogglable = false},
                    new buttontemplate { Text = "Disable Network Triggers", method =() => DisableNetworkTriggers(true), isTogglable = true, disableMethod =() => DisableNetworkTriggers(false) },
                }
            },
            new categorytemplate("Movement")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Platforms", method =() => Platforms(), isTogglable = true },
                    new buttontemplate { Text = "Trigger Platforms", method =() => TriggerPlatforms(), isTogglable = true },
                    new buttontemplate { Text = "Invis Platforms", method =() => InvisPlatforms(), isTogglable = true },
                    new buttontemplate { Text = "No Rotate Platforms", method =() => NoRotatePlatforms(), isTogglable = true },
                    new buttontemplate { Text = "Sticky Platforms", method =() => StickyPlatforms(), isTogglable = true },
                    new buttontemplate { Text = "No Clip", method =() => Noclip(), isTogglable = true },
                    new buttontemplate { Text = "Fly", method =() => Fly(), isTogglable = true },
                    new buttontemplate { Text = "Trigger Fly", method =() => TriggerFly(), isTogglable = true },
                    new buttontemplate { Text = "Slingshot Fly", method =() => Slingshot1(), isTogglable = true},
                    new buttontemplate { Text = "Noclip Fly", method =() => NoClipFly(), isTogglable = true},
                    new buttontemplate { Text = "Noclip Trigger Fly", method =() => NoClipFlyTrigger(), isTogglable = true},
                    new buttontemplate { Text = "Iron Monke", method =() => IronMonke(), isTogglable = true },
                    new buttontemplate { Text = "Spider Monke", method =() => SpiderMonke(), isTogglable = true },
                    new buttontemplate { Text = "Tp Gun", method =() => tpgun(), isTogglable = true },
                    new buttontemplate { Text = "Up And Down", method =() => UPanddown(), isTogglable = true },
                    new buttontemplate { Text = "Low Grav", method =() => GravChanger(Gravitys.Low), isTogglable = true },
                    new buttontemplate { Text = "High Grav", method =() => GravChanger(Gravitys.High), isTogglable = true },
                    new buttontemplate { Text = "No Grav", method =() => GravChanger(Gravitys.None), isTogglable = true },
                    new buttontemplate { Text = "Reverse Grav", method =() => GravChanger(Gravitys.Reverse), isTogglable = true },
                    new buttontemplate { Text = "Speed Boost", method =() => SpeedBoost(), isTogglable = true, disableMethod =() => FixBoost()},
                    new buttontemplate { Text = "Mosa Boost", method =() => MosaBoost(), isTogglable = true, disableMethod =() => FixBoost()},
                    new buttontemplate { Text = "Punch Mod", method =() => PunchMod()},
                    new buttontemplate { Text = "Teleport To Stump [<color=red>RG</color>]", method =() => StumpTeleport()},
                }
            },
            new categorytemplate("Player")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Ghost", method =() => Ghost(), isTogglable = true },
                    new buttontemplate { Text = "Invis", method =() => Invis(), isTogglable = true },
                    new buttontemplate { Text = "Rig Gun", method =() => RigGun(), isTogglable = true },
                    new buttontemplate { Text = "Grab Rig", method =() => GrabRig(), isTogglable = true },
                    new buttontemplate { Text = "Long Arms", method =() => ChangeArmLength(1.3f), isTogglable = true, disableMethod =() => ChangeArmLength(1f) },
                    new buttontemplate { Text = "Sizeable Arms", method =() => SizeAbleArms(), isTogglable = true },
                    new buttontemplate { Text = "Size Changer", method =() => SizeChangerer(), isTogglable = true },
                    new buttontemplate { Text = "Flick Tag Gun", method =() => FlickTagGun(), isTogglable = true },
                    new buttontemplate { Text = "Lag Rig", method =() => LagRig(), isTogglable = true, disableMethod =() => GorillaTagger.Instance.offlineVRRig.enabled = true },
                    new buttontemplate { Text = "Tag All", method =() => TagAll(), isTogglable = true },
                    new buttontemplate { Text = "Tag Gun", method =() => TagGun(), isTogglable = true },
                    new buttontemplate { Text = "Tag Self", method =() => TagSelf(), isTogglable = true },
                    new buttontemplate { Text = "Tag Aura", method =() => TagAura(), isTogglable = true },
                    new buttontemplate { Text = "Copy Movment Gun", method =() => CopyMovment(), isTogglable = true },
                    new buttontemplate { Text = "Look At Gun", method =() => LookAtGun(), isTogglable = true },
                }
            },
            new categorytemplate("Overpowered")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Set Ownership Gorilla Not", method =() => SetOwnership(), isTogglable = false},
                    new buttontemplate { Text = "Set Ownership Monsters", method =() => SetOwnershipMonsters(), isTogglable = false},
                    new buttontemplate { Text = "Set Ownership Bug", method =() => SetOwnershipBug(), isTogglable = false},
                    new buttontemplate { Text = "Set Ownership Bat", method =() => SetOwnershipBat(), isTogglable = false},
                    new buttontemplate { Text = "Check Ownership Gorilla Not", method =() => CheckOwnershipGorillaNot(), isTogglable = false},
                    new buttontemplate { Text = "Check Ownership Monsters", method =() => CheckOwnershipMonkeye(), isTogglable = false},
                    new buttontemplate { Text = "Check Ownership Bug", method =() => CheckOwnershipBug(), isTogglable = false},
                    new buttontemplate { Text = "Check Ownership Bat", method =() => CheckOwnershipBat(), isTogglable = false},
                    new buttontemplate { Text = "Break All Audio", method =() => BreakAllAudio()},
                    new buttontemplate { Text = "Break Movement [<color=green>DELAY BAN!</color>]", method =() => StopAll()},
                    new buttontemplate { Text = "Dox Gun [<color=green>UD</color>] [<color=yellow>NOT REAL!</color>]", method =() => DoxGun()},
                }
            },
            new categorytemplate("Visual")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Box ESP", method =() => BoxEsp(), isTogglable = true },
                    new buttontemplate { Text = "Head ESP", method =() => HeadEsp(), isTogglable = true },
                    new buttontemplate { Text = "Chams", enableMethod =() => Chams(), disableMethod =() => NoChams(), isTogglable = true },
                    new buttontemplate { Text = "2D Box ESP", method =() => TwoDemensianalboxes(), isTogglable = true },
                    new buttontemplate { Text = "Skeleton", method =() => Skele(), isTogglable = true },
                    new buttontemplate { Text = "Tracers", method =() => Tracers(), isTogglable = true },
                    new buttontemplate { Text = "FPS Booster", method =() => QualitySettings.masterTextureLimit = 10, isTogglable = true, disableMethod =()=>QualitySettings.masterTextureLimit = 0},
                    new buttontemplate { Text = "Horror Mode", enableMethod =() => Horror(), disableMethod =() => ResetHorror(), isTogglable = true },
                    new buttontemplate { Text = "Draw", method =() => DrawObjs(), isTogglable = true },
                    new buttontemplate { Text = "Balls With Trails", method =() => Ball(), isTogglable = true}
                }
            },
            new categorytemplate("Safety")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Anti Report [<color=green>FIX BUT LAG</color>]", method =()=> antireport(), isTogglable = true },
                    new buttontemplate { Text = "Delete Report Calls", method =() => RPC(), isTogglable = false },
                    new buttontemplate { Text = "Dodge Moderators", method =() => Dodge("LBAAK."), isTogglable = true },
                    new buttontemplate { Text = "Grab Id's", method =() => GrabIDs(), isTogglable = true },
                    new buttontemplate { Text = "Anti Mod Check", method =() => AntiModCheck(), isTogglable = true},
                    new buttontemplate { Text = "Fuck Mod Check", method =() => FuckModCheckers(), isTogglable = true},
                    new buttontemplate { Text = "Disable Object Pool", method =() => DisableObjectPools(true), disableMethod =() => DisableObjectPools(false) ,isTogglable = true},
                    new buttontemplate { Text = "Destroy Object Pool", method =() => DestroyObjectPools(), isTogglable = true},
                }
            },
            new categorytemplate("Projectiles")
            {
                Buttons = new List<buttontemplate>
                {
                     new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                     new buttontemplate { Text = "THANKS IIDK FOR PROJECTILES. [cs projectiles btw]", method =() => ReturnHome(), isTogglable = false },
                     new buttontemplate { Text = "Snowball Spam", method =() => Projectiles.Snowball(), isTogglable = true},
                     new buttontemplate { Text = "Waterballoon Spam", method =() => Projectiles.WaterBalloon(), isTogglable = true},
                     new buttontemplate { Text = "Lava Rock Spam", method =() => Projectiles.LavaRock(), isTogglable = true},
                     new buttontemplate { Text = "Fish Food Spam", method =() => Projectiles.FishFood(), isTogglable = true},
                     new buttontemplate { Text = "Candy Spam", method =() => Projectiles.Mentos(), isTogglable = true},
                     new buttontemplate { Text = "Gift Spam", method =() => Projectiles.ThrowableGift(), isTogglable = true},
                     new buttontemplate { Text = "Pee", method =() => Projectiles.Pee(), isTogglable = true}

                }
            },
            new categorytemplate("MapsGamemodes")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Grab Bug [<color=red>LG</color>]", method =() => GrabBug()},
                    new buttontemplate { Text = "Grab Bug [<color=red>RG</color>]", method =() => GrabBugR()},
                    new buttontemplate { Text = "Spaz Bug", method =() => SpazBug()},
                    new buttontemplate { Text = "Grab Bat [<color=red>LG</color>]", method =() => GrabBat()},
                    new buttontemplate { Text = "Grab Bat [<color=red>RG</color>]", method =() => GrabBatR()},
                    new buttontemplate { Text = "Spaz Bat", method =() => SpazBat()},
                    new buttontemplate { Text = "SecondLook State Activate", method =() => GhostMod(true, false, false)},
                    new buttontemplate { Text = "SecondLook State Found", method =() => GhostMod(false, true, false)},
                    new buttontemplate { Text = "SecondLook State Caught", method =() => GhostMod(false, false, true)},
                    new buttontemplate { Text = "Slide Control", method =() => SlideControl()},
                    new buttontemplate { Text = "Grab AI [<color=red>LG</color>]", method =() => GrabMonster()},
                    new buttontemplate { Text = "Grab AI [<color=red>RG</color>]", method =() => GrabMonsterR()},
                    new buttontemplate { Text = "Spaz AI", method =() => SpazMonster()},
                    new buttontemplate { Text = "Open Basement Door", method =() => OpenDoor()},
                    new buttontemplate { Text = "Close Basement Door", method =() => CloseDoor()},
                    new buttontemplate { Text = "Spaz Basement Door", method =() => SpazDoor()},
                    new buttontemplate { Text = "Splash Left", method =() => WaterModule(GorillaTagger.Instance.leftHandTransform.position), isTogglable = true },
                    new buttontemplate { Text = "Splash Right", method =() => WaterModule(GorillaTagger.Instance.rightHandTransform.position), isTogglable = true },
                    new buttontemplate { Text = "Splash Aura", method =() => SplashAura(), isTogglable = true },
                }
            },
            new categorytemplate("Cosmetics")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Bongos", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/BONGOS").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/BONGOS").SetActive(false)},
                    new buttontemplate { Text = "Drums", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/DRUM SET").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/DRUM SET").SetActive(false)},
                    new buttontemplate { Text = "Blue Bunny", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/POCKET GORILLA BUN BLUE").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/POCKET GORILLA BUN BLUE").SetActive(false)},
                    new buttontemplate { Text = "Pink Bunny", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/POCKET GORILLA BUN PINK").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/POCKET GORILLA BUN PINK").SetActive(false)},
                    new buttontemplate { Text = "Yellow Bunny", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/POCKET GORILLA BUN YELLOW").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/POCKET GORILLA BUN YELLOW").SetActive(false)},
                    new buttontemplate { Text = "Rain Scarf", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/YELLOW RAIN SHAWL").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/YELLOW RAIN SHAWL").SetActive(false)},
                    new buttontemplate { Text = "Mountain Pin", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/MOUNTAIN PIN").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/MOUNTAIN PIN").SetActive(false)},
                    new buttontemplate { Text = "GT1 Badge", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/GT1 BADGE").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/GT1 BADGE").SetActive(false)},
                    new buttontemplate { Text = "Unadded Cosmetic", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/OVERRIDDEN").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/OVERRIDDEN").SetActive(false)},
                    new buttontemplate { Text = "Admin Badge", enableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/ADMINISTRATOR BADGE").SetActive(true), disableMethod =() => GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/ADMINISTRATOR BADGE").SetActive(false)},
                }
            },
            new categorytemplate("ServerInfo")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Show Server Ip", method =() => ServerDetails.ServerAddress(), isTogglable = false},
                    new buttontemplate { Text = "Show Player Count", method =() => ServerDetails.ServerPlayerCount(), isTogglable = false},
                    new buttontemplate { Text = "Show Max Player Count", method =() => ServerDetails.ServerMaxPlayerCount(), isTogglable = false},
                    new buttontemplate { Text = "Show Room Name", method =() => ServerDetails.ServerName(), isTogglable = false},
                    new buttontemplate { Text = "Is Public: ", method =() => ServerDetails.IsPublic(), isTogglable = false},
                    new buttontemplate { Text = "Is Private: ", method =() => ServerDetails.IsPrivate(), isTogglable = false},
                }
            },
            new categorytemplate("PcDetails")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Desktop Name: ", method =() => Environment.MachineName.ToString(), isTogglable = false},
                }
            },

            new categorytemplate("ESPSettings")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Add Box When Skeleton ESP Is On    ", enableMethod =() => EnableBoxWithSkeleton(), disableMethod =() => DisableBoxWithSkeleton()},
                }
            },
            new categorytemplate("Misc")
            {
                Buttons = new List<buttontemplate>
                {
                    new buttontemplate { Text = "Back", method =() => ReturnHome(), isTogglable = false },
                    new buttontemplate { Text = "Snowball Surface", enableMethod =() => SnowballGround(), disableMethod =() => FixGround()},
                    new buttontemplate { Text = "Waterballoon Surface", enableMethod =() => WaterBalloonGround(), disableMethod =() => FixGround()},
                    new buttontemplate { Text = "Rock Surface", enableMethod =() => RockGround(), disableMethod =() => FixGround()},
                    new buttontemplate { Text = "Gift Surface", enableMethod =() => GiftGround(), disableMethod =() => FixGround()},
                    new buttontemplate { Text = "Mentos Surface", enableMethod =() => MentosGround(), disableMethod =() => FixGround()},
                    new buttontemplate { Text = "Fishfood Surface", enableMethod =() => FishFoodGround(), disableMethod =() => FixGround()},
                }
            },
        };

    }
}
    