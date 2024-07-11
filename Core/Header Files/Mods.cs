using GorillaNetworking;
using Oculus.Platform;
using Photon.Pun;
using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.XR;
using UnityEngine;
using Mods;
using Stealth;
using Stealth.Patches;
using static Mods.Globals;
using CjLib;
using HarmonyLib;
using GorillaTagScripts;
using System.Linq;
using Stealth.Core.Header_Files;
using Fusion.Collections;
using Newtonsoft.Json.Serialization;
using GorillaExtensions;
using Fusion;
using ExitGames.Client.Photon;
using Photon.Realtime;
using GorillaTag;
using System.Reflection;
using Fusion.Analyzer;
using System.IO;
using System.Threading.Tasks;
using GorillaLocomotion.Gameplay;
using UnityEngine.Animations.Rigging;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using g3;

namespace Mods
{
    internal class Mods
    {
        public static void DisableNetworkTriggers(bool f)
        {
            if (f)
            {
                GameObject.Find("JoinRoomTriggers_Prefab").SetActive(false);
            }
            else
            {
                GameObject.Find("JoinRoomTriggers_Prefab").SetActive(true);
            }
        }


        public static void Dodge(string thing)
        {
            foreach (VRRig f in GorillaParent.instance.vrrigs)
            {
                if (f.cosmeticSet.HasItem(thing) || f.cosmetics[f.cosmetics.Length].GetComponentInParent<string>().Contains(thing))
                {
                    PhotonNetwork.Disconnect();
                    Stealth.NotifiLib.SendNotification("[<color=green>DODGE MODS</color>] SOMEONE JOINED WITH THE COSMETIC OF [" + thing + "]");
                    return;
                }
            }
        }

        public static void Slingshot1()
        {
            if (Inputs.A() || Inputs.B())
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaTagger.Instance.headCollider.transform.forward * Time.time * 15f;
            }
        }

        static GameObject BallWithTrails;

        public static void Ball()
        {
            if (Inputs.LG() || Inputs.RG())
            {
                BallWithTrails = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Rigidbody body = BallWithTrails.AddComponent<Rigidbody>();
                body.useGravity = false;
                BallWithTrails.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                BallWithTrails.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                BallWithTrails.transform.rotation = UnityEngine.Random.rotation;
                TrailRenderer renderer = BallWithTrails.AddComponent<TrailRenderer>();
                renderer.startWidth = 0.1f;
                renderer.endWidth = 0.1f;
                renderer.startColor = Color.black;
                renderer.endColor = Color.white;
                renderer.material = new Material(Shader.Find("GUI/Text Shader"));

            }
            if (Inputs.LT() || Inputs.RT())
            {
                UnityEngine.Object.Destroy(BallWithTrails);
            }
        }

        public static void Identity() //ima do color change later
        {
            int shiftthroughnames = UnityEngine.Random.Range(0, 7);
            string namestring = Globals.names[shiftthroughnames];
            PhotonNetwork.LocalPlayer.NickName = namestring;
            PhotonNetwork.NickName = namestring;
            GorillaLocomotion.Player.Instance.name = namestring;
            GorillaComputer.instance.name = namestring;
            GorillaComputer.instance.currentName = namestring;
            GorillaComputer.instance.savedName = namestring;
        }


        public static Material PlatColor = new Material(Shader.Find("GorillaTag/UberShader"));

        public static bool RightToggle;

        public static bool LeftToggle;

        public static GameObject rPlat;

        public static GameObject lPlat;

        public static Vector3 scale = new Vector3(0.0100f, 0.23f, 0.3625f);

        public static void Module()
        {
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
            left.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            left.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
            left.GetComponent<Renderer>().material = new Material(Shader.Find("GUI/Text Shader"));
            left.GetComponent<Renderer>().material = MenuColor;
            UnityEngine.Object.Destroy(left.GetComponent<Collider>());

            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
            right.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            right.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            right.GetComponent<Renderer>().material = new Material(Shader.Find("GUI/Text Shader"));
            right.GetComponent<Renderer>().material = MenuColor;
            UnityEngine.Object.Destroy(right.GetComponent<Collider>());

            UnityEngine.Object.Destroy(left, Time.deltaTime);
            UnityEngine.Object.Destroy(right, Time.deltaTime);
        }
        public static void RPC()
        {
            PhotonNetwork.RemoveBufferedRPCs();
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            PhotonNetwork.RemoveRPCsInGroup(int.MaxValue);
        }
        public static void antireport()
        {
            GorillaScoreBoard[] boards = UnityEngine.Object.FindObjectsOfType<GorillaScoreBoard>();
            for (int i = 0; i < boards.Length; i++)
            {
                foreach (GorillaPlayerScoreboardLine lines in boards[i].lines)
                {
                    if (lines.linePlayer == NetworkSystem.Instance.LocalPlayer)
                    {
                        foreach (VRRig item in GorillaParent.instance.vrrigs)
                        {
                            if (item != GorillaTagger.Instance.offlineVRRig)
                            {
                                if (Vector3.Distance(item.leftHandTransform.position, lines.reportButton.transform.position) < 0.5f || Vector3.Distance(item.rightHandTransform.position, lines.reportButton.transform.position) < 0.5f)
                                {
                                    NotifiLib.SendNotification($"<color=red>[ANTI-REPORT]</color> {item.playerName} Tried To Report You");
                                    PhotonNetwork.Disconnect();
                                }
                            }
                        }
                    }
                }
            }

        }

        public static void Horror()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
            Camera.main.farClipPlane = 7;
        }

        public static void ResetHorror()
        {
            BetterDayNightManager.instance.SetTimeOfDay(timeofday);
            Camera.main.farClipPlane = int.MaxValue;
        }

        public static void PunchMod()
        {

            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.isMyPlayer)
                {
                    Vector3 idfk = Vector3.forward;
                    float distance = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, rig.leftHandTransform.position);
                    float distance2 = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, rig.rightHandTransform.position);
                    if (distance <= 0.05f || distance2 <= 0.05f)
                    {
                        GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(100f * idfk, ForceMode.Acceleration);
                    }
                }
            }
        }
        public static void AntiMovementBreak()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                foreach (MonkeyeAI monkey2 in UnityEngine.Object.FindObjectsOfType<MonkeyeAI>())
                {
                    monkey2.transform.position = GorillaTagger.Instance.leftHandTransform.up + new Vector3(0f, 10f, 0f);
                }
            }
        }
        public static void GrabIDs()
        {

        }

        public static void BreakMovementInf()
        {
            while (true)
            {

            }
        }

        public static void BreakAllMovement()
        {
            MonkeyeAI monk = GameObject.Find("Monkeye_Prefab_Angry").GetComponent<MonkeyeAI>();
            PhotonView view = monk.GetComponent<PhotonView>();
            view.OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            view.OwnershipTransfer = OwnershipOption.Takeover;
            view.RequestOwnership();
            view.TransferOwnership(PhotonNetwork.LocalPlayer);
            view.ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            view.OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monk.beginAttackTime = 0;
            monk.attackDistance = 10000000;
            monk.transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            monk.attackDistance = float.MaxValue;
            monk.SetState(MonkeyeAI_ReplState.EStates.BeginAttack);
        }

        public static void WaterModule(Vector3 pos)
        {
            if (Time.time > timerrr + 0.45f)
            {
                timerrr = Time.time;
                GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", 0, new object[]
                {
                    pos,
                    UnityEngine.Random.rotation,
                    500f,
                    250f,
                    true,
                    true
                });
            }
        }
        public static void SplashAura()
        {
            foreach (VRRig item in GorillaParent.instance.vrrigs)
            {
                if (!item.isMyPlayer)
                {
                    var vectordis = Vector3.Distance(GorillaTagger.Instance.transform.position, item.transform.position);
                    var inposof = 12f;
                    if (vectordis > inposof || vectordis < inposof)
                    {
                        WaterModule(item.transform.position);
                    }
                }
            }
        }
        public static void DrawObjs()
        {
            if (Inputs.RG() || Inputs.LG())
            {
                Vector3 position = Inputs.RG() ? GorillaTagger.Instance.rightHandTransform.position : GorillaTagger.Instance.leftHandTransform.position;
                DrawObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DrawObj.transform.position = position;
                DrawObj.transform.rotation = Quaternion.identity;
                DrawObj.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                DrawObj.name = "drawgameobject";
                DrawObj.GetComponent<Renderer>().material = Guns.Gunonmaterial;
                GameObject.Destroy(DrawObj.GetComponent<Rigidbody>());
                GameObject.Destroy(DrawObj.GetComponent<Rigidbody2D>());
                GameObject.Destroy(DrawObj.GetComponent<SphereCollider>());
                GameObject.Destroy(DrawObj.GetComponent<MeshCollider>());
            }
            if (Inputs.RT() || Inputs.LT())
            {
                for (int i = 0; i < 100; i++)
                {
                    GameObject.Destroy(DrawObj);
                    GameObject.Destroy(DrawObj.gameObject);
                    GameObject.Find(DrawObj.name).SetActive(false);
                    GameObject.Find(DrawObj.name).SetActive(false);
                    GameObject.Find(DrawObj.name).SetActive(false);
                    GameObject.Find(DrawObj.name).SetActive(false);
                    GameObject.Find("drawgameobject").SetActive(false);
                }
            }
        }

        public static void TagAll()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.mainSkin.material.name.Contains("fected") && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = rig.transform.position;
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = rig.transform.position;
                    GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = rig.transform.position;
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
                if (!GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    Stealth.NotifiLib.SendNotification("[<color=red>ERROR</color>] YOU ARE NOT TAGGED.");
                    return;
                }
            }
        }

        public static void tpgun()
        {
            Guns.Gun(delegate
            {
                GorillaLocomotion.Player.Instance.transform.position = Guns.VrrigThatIsLocked.transform.position;
            }, false);
        }
        public static void CopyMovment()
        {
            Guns.Gun(delegate
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = Guns.VrrigThatIsLocked.rightHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = Guns.VrrigThatIsLocked.leftHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.head.headTransform.position = Guns.VrrigThatIsLocked.head.headTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.position = Guns.VrrigThatIsLocked.transform.position;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation = Guns.VrrigThatIsLocked.rightHandTransform.rotation;
                GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation = Guns.VrrigThatIsLocked.leftHandTransform.rotation;
                GorillaTagger.Instance.offlineVRRig.head.headTransform.rotation = Guns.VrrigThatIsLocked.head.headTransform.rotation;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = Guns.VrrigThatIsLocked.transform.rotation;
            }, true);
            if (!Inputs.RT() || !Inputs.LT())
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void Flip1()
        {
            GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
            GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation = GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation;
            GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position;
            GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation = GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation;
        }
        public static void LookAtGun()
        {
            Guns.Gun(delegate
            {
                GorillaTagger.Instance.headCollider.transform.LookAt(Guns.VrrigThatIsLocked.head.headTransform.position);
            }, true);
        }
        public static void UPanddown()
        {
            if (Inputs.RT())
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 100f, ForceMode.Acceleration);
            }
            if (Inputs.LT())
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * 100f, ForceMode.Acceleration);
            }
        }

        public static void SpeedBoost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 2f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 12f;
        }

        public static void MosaBoost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 2f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 12f;
        }

        public static void FixBoost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
        }



        public enum Gravitys
        {
            Low, None, High, Reverse
        }
        public static void GravChanger(Gravitys type)
        {
            if (type == Gravitys.Low)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 6f, ForceMode.Acceleration);
            }
            if (type == Gravitys.None)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 15f, ForceMode.Acceleration);
            }
            if (type == Gravitys.High)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
            }
            if (type == Gravitys.Reverse)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 50f, ForceMode.Acceleration);
            }
        }

        public static void SpiderMonke()
        {
            if (Inputs.RG())
            {
                if (lineObjectRight == null)
                {
                    LineRenderer(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward + GorillaTagger.Instance.rightHandTransform.forward, 10f, true);
                }
                else
                {
                    UpdateLinePosition(lineObjectRight, GorillaTagger.Instance.rightHandTransform.position, 5f);
                }
            }
            else if (!Inputs.RG() && lineObjectRight != null)
            {
                GameObject.Destroy(lineObjectRight);
                lineObjectRight = null;
            }
            if (Inputs.LG())
            {
                if (lineObjectLeft == null)
                {
                    LineRenderer(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.forward + GorillaTagger.Instance.leftHandTransform.forward, 10f, false);
                }
                else
                {
                    UpdateLinePosition(lineObjectLeft, GorillaTagger.Instance.leftHandTransform.position, 5f);
                }
            }
            else if (!Inputs.LG() && lineObjectLeft != null)
            {
                GameObject.Destroy(lineObjectLeft);
                lineObjectLeft = null;
            }
        }

        static void LineRenderer(Vector3 SP, Vector3 HUV, float LL, bool R)
        {
            Vector3 direction = HUV.normalized * LL;

            RaycastHit hit;
            bool hitSomething = Physics.Raycast(SP, direction, out hit);
            GameObject pointerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointerObj.AddComponent<Renderer>();
            pointerObj.GetComponent<Renderer>().material.color = Color.white;
            pointerObj.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            pointerObj.transform.position = hit.point;
            GameObject.Destroy(pointerObj, Time.deltaTime);

            if (R)
            {
                if (Inputs.RT())
                {
                    pointerObj.GetComponent<Renderer>().material.color = Color.green;
                    GameObject lineObject = new GameObject("Line");
                    LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                    lineRenderer.startWidth = 0.03f;
                    lineRenderer.endWidth = 0.03f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = Color.white;
                    lineRenderer.material = Uber;
                    lineRenderer.SetPosition(0, SP);
                    if (hitSomething)
                    {
                        lineRenderer.SetPosition(1, hit.point);
                    }
                    else
                    {
                        lineRenderer.SetPosition(1, SP + direction);
                    }
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                    lineObjectRight = lineObject;
                }
            }
            else
            {
                if (Inputs.LT())
                {
                    pointerObj.GetComponent<Renderer>().material.color = Color.green;
                    GameObject lineObject = new GameObject("Line");
                    LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                    lineRenderer.startWidth = 0.03f;
                    lineRenderer.endWidth = 0.03f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.startColor = Color.white;
                    lineRenderer.endColor = Color.white;
                    lineRenderer.material = Uber;
                    lineRenderer.SetPosition(0, SP);
                    if (hitSomething)
                    {
                        lineRenderer.SetPosition(1, hit.point);
                    }
                    else
                    {
                        lineRenderer.SetPosition(1, SP + direction);
                    }
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                    lineObjectLeft = lineObject;
                }
            }
        }
        static void UpdateLinePosition(GameObject LO, Vector3 NP, float LL)
        {
            LineRenderer LR = LO.GetComponent<LineRenderer>();
            LR.SetPosition(0, NP);
            Vector3 EP = LR.GetPosition(1);
            Vector3 D = (EP - NP).normalized * LL;
            Rigidbody PR = GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>();
            PR.AddForce(D, ForceMode.Impulse);
        }
        public static void IronMonke()
        {
            void AFAV(bool L, Vector3 D)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(15f * D, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(L, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
            }

            bool[] INPS = new bool[]
            {
                Inputs.RG(),
                Inputs.LG()
            };


            if (INPS[0])
            {
                AFAV(false, GorillaLocomotion.Player.Instance.rightControllerTransform.right);
            }

            if (INPS[1])
            {
                AFAV(true, -GorillaLocomotion.Player.Instance.leftControllerTransform.right);
            }

            if (INPS[1] || INPS[0])
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.ClampMagnitude(GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity, 50f);
            }
        }


        public static void LagRig()
        {
            if (GorillaTagger.Instance.offlineVRRig.enabled)
            {
                GhostPatch.Prefix(GorillaTagger.Instance.offlineVRRig);
            }
            else
            {
            }
            toggleTimer += Time.deltaTime;
            {
                if (toggleTimer >= 0.5 && !isRigEnabled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    isRigEnabled = true;
                    toggleTimer = 0f;
                }
                else if (toggleTimer >= 0.3f && isRigEnabled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    isRigEnabled = false;
                    toggleTimer = 0f;
                }
            }
        }

        public static void Ghost()
        {
            if (Inputs.RT())
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                // Module();
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void SizeChangerer()
        {
            if (Inputs.RT())
            {
                AddedFloat2 += 0.09f;
            }
            if (Inputs.LT())
            {
                AddedFloat2 -= 0.09f;
            }
            if (Inputs.Y())
            {
                AddedFloat2 = 0f;
            }
            GorillaLocomotion.Player.Instance.scale = 1f + AddedFloat2;
        }
        public static void SizeAbleArms()
        {
            if (Inputs.RT() && !Inputs.LT())
            {
                AddedFloat += 0.05f;
            }
            else if (!Inputs.RT() && Inputs.LT())
            {
                AddedFloat -= 0.05f;
            }
            if (Inputs.Y())
            {
                AddedFloat = 0f;
            }
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f + AddedFloat, 1f + AddedFloat, 1f + AddedFloat);
        }
        public static void ChangeArmLength(float f)
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(f, f, f);
        }
        public static void GrabRig()
        {
            if (Inputs.RG() || Inputs.LG())
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Inputs.RG() ? GorillaTagger.Instance.rightHandTransform.position : GorillaTagger.Instance.leftHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void FlickTagGun()
        {
            Guns.Gun(delegate {
                GorillaLocomotion.Player.Instance.rightControllerTransform.position = Guns.PointerObj.transform.position;
            }, true);
        }

        public static void Skibussy()
        {
            if (GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = Guns.PointerObj.transform.position;
                GorillaTagManager.instance.ReportTag(Guns.rigd, Stealth.RigHandle.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig));
            }
            else
            {
                NotifiLib.SendNotification("[<color=red>ERROR</color>] YOU ARE NOT TAGGED...");
                return;
            }
        }

        public static void CheckNearest()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                float distance = Vector3.Distance(GorillaTagger.Instance.offlineVRRig.transform.position, rig.transform.position);
                if (distance < 100 && rig.mainSkin.material.name.Contains("fected"))
                {
                    NotifiLib.SendNotification("<color=green>NEAREST PLAYER: " + distance + "m</color>");
                    return;
                }
                if (distance < 75 && rig.mainSkin.material.name.Contains("fected"))
                {
                    NotifiLib.SendNotification("<color=orange>NEAREST PLAYER: " + distance + "m</color>");
                    return;
                }
                if (distance < 50 && rig.mainSkin.material.name.Contains("fected"))
                {
                    NotifiLib.SendNotification("<color=yellow>NEAREST PLAYER: " + distance + "m</color>");
                    return;
                }
                if (distance < 25 && rig.mainSkin.material.name.Contains("fected"))
                {
                    NotifiLib.SendNotification("<color=red>NEAREST PLAYER: " + distance + "m</color>");
                    return;
                }

            }
        }

        public static void CheckSecondLook()
        {
            foreach (SecondLookSkeleton second in UnityEngine.Object.FindObjectsOfType<SecondLookSkeleton>())
            {
                float distance = Vector3.Distance(GorillaTagger.Instance.offlineVRRig.transform.position, second.transform.position);
                if (distance < 100)
                {
                    NotifiLib.SendNotification("<color=green>SECOND LOOK DISTANCE: " + distance + "m</color>");
                    return;
                }
                if (distance < 75)
                {
                    NotifiLib.SendNotification("<color=orange>SECOND LOOK DISTANCE: " + distance + "m</color>");
                    return;
                }
                if (distance < 50)
                {
                    NotifiLib.SendNotification("<color=yellow>SECOND LOOK DISTANCE: " + distance + "m</color>");
                    return;
                }
                if (distance < 25)
                {
                    NotifiLib.SendNotification("<color=red>SECOND LOOK DISTANCE: " + distance + "m</color>");
                    return;
                }

            }
        }



        public static void TagGun()
        {
            Guns.Gun(delegate
            {
                Skibussy();
            }, true);
        }
        public static void TagSelf()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig.mainSkin.material.name.Contains("fected") && !GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    GorillaTagger.Instance.offlineVRRig.transform.position = rig.transform.position;
                }
            }
        }

        static GameObject aurabox;

        public static void TagAura()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                float distance = Vector3.Distance(aurabox.transform.position, rig.transform.position);

                if (distance <= 0.5f)
                {
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = rig.transform.position;
                }
            }
        }

        public static void RigGun()
        {
            if (!Inputs.RT() || !Inputs.LT())
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
            Guns.Gun(delegate
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Guns.PointerObj.transform.position;
                // Module();
            }, false);
        }
        public static void Invis()
        {
            if (Inputs.LT())
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Vector3.zero;
                // Module();
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void SetMaster()
        {
            if (!Globals.antibandone)
            {
                NotifiLib.SendNotification("[<color=cyan>NOTIFICATION</color>] Please use antiban");
            }
            else if (Globals.antibandone)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
        }

        public static void BoxEsp()
        {
            foreach (VRRig item in GorillaParent.instance.vrrigs)
            {
                if (item != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.position = item.transform.position;
                    obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    MeshRenderer rend = obj.GetComponent<MeshRenderer>();
                    rend.material = new Material(Shader.Find("GUI/Text Shader"));
                    rend.material.color = item.mainSkin.material.name.Contains("fected") ? rend.material.color = Color.red : Color.green;
                    UnityEngine.GameObject.Destroy(obj.GetComponent<Rigidbody>());
                    UnityEngine.GameObject.Destroy(obj.GetComponent<Rigidbody2D>());
                    UnityEngine.GameObject.Destroy(obj.GetComponent<MeshCollider>());
                    UnityEngine.GameObject.Destroy(obj.GetComponent<BoxCollider>());
                    UnityEngine.GameObject.Destroy(obj, Time.deltaTime);
                }
            }
        }
        public static void HeadEsp()
        {
            foreach (VRRig item in GorillaParent.instance.vrrigs)
            {
                if (item != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    obj.transform.position = item.headMesh.transform.position;
                    obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    MeshRenderer rend = obj.GetComponent<MeshRenderer>();
                    rend.material = new Material(Shader.Find("GUI/Text Shader"));
                    rend.material.color = item.mainSkin.material.name.Contains("fected") ? rend.material.color = Color.red : Color.green;
                    UnityEngine.GameObject.Destroy(obj.GetComponent<Rigidbody>());
                    UnityEngine.GameObject.Destroy(obj.GetComponent<Rigidbody2D>());
                    UnityEngine.GameObject.Destroy(obj.GetComponent<MeshCollider>());
                    UnityEngine.GameObject.Destroy(obj.GetComponent<SphereCollider>());
                    UnityEngine.GameObject.Destroy(obj, Time.deltaTime);
                }
            }
        }

        public static void Chams()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    rig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    rig.mainSkin.material.color = rig.mainSkin.name.Contains("fected") ? Color.red : Color.green;
                }
            }
        }

        public static void NoChams()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    rig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                    rig.mainSkin.material.color = rig.playerColor;
                }
            }
        }

        public static void Tracers()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject gameobject = new GameObject("TracerObject");
                    LineRenderer renderer = gameobject.AddComponent<LineRenderer>();
                    renderer.startColor = rig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                    renderer.endColor = rig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                    renderer.material.shader = Shader.Find("GUI/Text Shader");
                    renderer.startWidth = 0.015f;
                    renderer.endWidth = 0.015f;
                    renderer.SetPosition(0, GorillaTagger.Instance.offlineVRRig.rightHandTransform.position);
                    renderer.SetPosition(1, rig.transform.position);
                    UnityEngine.Object.Destroy(renderer, Time.deltaTime);
                }
            }
        }
        public static void Skele()
        {

            if (enableboxwskeleton)
            {
                TwoDemensianalboxes();
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != GorillaTagger.Instance.offlineVRRig)
                    {
                        for (int i = 0; i < Enumerable.Count<int>(integers); i += 2)
                        {
                            LineRenderer renderer = rig.mainSkin.bones[integers[i]].gameObject.AddComponent<LineRenderer>();
                            renderer.startColor = rig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                            renderer.endColor = rig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                            renderer.material.shader = Shader.Find("GUI/Text Shader");
                            renderer.startWidth = 0.01f;
                            renderer.endWidth = 0.01f;
                            renderer.SetPosition(0, rig.mainSkin.bones[integers[i]].position);
                            renderer.SetPosition(1, rig.mainSkin.bones[integers[i + 1]].position);
                            UnityEngine.Object.Destroy(renderer, Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    for (int i = 0; i < Enumerable.Count<int>(integers); i += 2)
                    {
                        LineRenderer renderer = rig.mainSkin.bones[integers[i]].gameObject.AddComponent<LineRenderer>();
                        renderer.startColor = rig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                        renderer.endColor = rig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                        renderer.material.shader = Shader.Find("GUI/Text Shader");
                        renderer.startWidth = 0.015f;
                        renderer.endWidth = 0.015f;
                        renderer.SetPosition(0, rig.mainSkin.bones[integers[i]].position);
                        renderer.SetPosition(1, rig.mainSkin.bones[integers[i + 1]].position);
                        UnityEngine.Object.Destroy(renderer, Time.deltaTime);
                    }
                }
            }
        }

        public static void StopAll()
        {
            MonkeyeAI monkey = GameObject.Find("Monkeye_Prefab_Sleepy").GetComponent<MonkeyeAI>();
            MonkeyeAI monkey1 = GameObject.Find("Monkeye_Prefab_Keen").GetComponent<MonkeyeAI>();
            MonkeyeAI monkey2 = GameObject.Find("Monkeye_Prefab_Angry").GetComponent<MonkeyeAI>();
            MonkeyeAI monkey3 = GameObject.Find("Monkeye_Prefab_Tweaky").GetComponent<MonkeyeAI>();
            monkey.GetComponent<PhotonView>().OwnershipTransfer = OwnershipOption.Takeover;
            monkey.GetComponent<PhotonView>().RequestOwnership();
            monkey.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            monkey.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monkey.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monkey1.GetComponent<PhotonView>().OwnershipTransfer = OwnershipOption.Takeover;
            monkey1.GetComponent<PhotonView>().RequestOwnership();
            monkey1.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            monkey1.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monkey1.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber; monkey1.GetComponent<PhotonView>().OwnershipTransfer = OwnershipOption.Takeover;
            monkey2.GetComponent<PhotonView>().RequestOwnership();
            monkey2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            monkey2.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monkey2.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monkey3.GetComponent<PhotonView>().RequestOwnership();
            monkey3.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            monkey3.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            monkey3.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            {
                if (monkey.GetComponent<PhotonView>().ControllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    Vector3 main = new Vector3(0, float.MaxValue, float.MaxValue);
                    for (int i = 0; i <= 100; i++)
                    {
                        monkey.SetState(MonkeyeAI_ReplState.EStates.Sleeping);
                    }
                    monkey.transform.position = main;
                    monkey.attackDistance = float.MaxValue;
                    for (int i = 0; i <= 100; i++)
                    {
                        monkey1.SetState(MonkeyeAI_ReplState.EStates.Sleeping);
                    }
                    monkey1.transform.position = main;
                    monkey1.attackDistance = float.MaxValue;
                    for (int i = 0; i <= 100; i++)
                    {
                        monkey2.SetState(MonkeyeAI_ReplState.EStates.Sleeping);
                    }
                    monkey2.transform.position = main;
                    monkey2.attackDistance = float.MaxValue;
                    for (int i = 0; i <= 100; i++)
                    {
                        monkey3.SetState(MonkeyeAI_ReplState.EStates.Sleeping);
                    }
                    monkey3.transform.position = main;
                    monkey3.attackDistance = float.MaxValue;
                }
            }
        }

        private static void SetupLineRenderer(GameObject obj, Material material, Vector3 startPosition, Vector3 endPosition)
        {
            LineRenderer lineRenderer = obj.GetComponent<LineRenderer>() ?? obj.AddComponent<LineRenderer>();

            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.material = material;
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
        private static void DestroyLineRenderer(GameObject obj)
        {
            LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                UnityEngine.Object.Destroy(lineRenderer);
            }
        }
        public static void TwoDemensianalboxes()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig.isMyPlayer)
                    {
                        if (vrrig.gameObject.GetComponent<LineRenderer>() == null)
                        {
                            vrrig.gameObject.AddComponent<LineRenderer>();
                        }
                        LineRenderer BoxLines = vrrig.gameObject.GetComponent<LineRenderer>();
                        BoxLines.startWidth = 0.015f;
                        BoxLines.endWidth = 0.015f;
                        BoxLines.startColor = vrrig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                        BoxLines.endColor = vrrig.mainSkin.material.name.Contains("fected") ? Color.red : Color.green;
                        BoxLines.material.shader = Shader.Find("GUI/Text Shader");
                        BoxLines.numCornerVertices = 8;
                        BoxLines.positionCount = 4;
                        BoxLines.loop = true;

                        Vector3 pivotPos = vrrig.transform.position;
                        Vector3 directionToHead = GorillaTagger.Instance.headCollider.transform.position - pivotPos;
                        Vector3 rightOffset = Vector3.Cross(directionToHead.normalized, Vector3.up).normalized * 0.35f;
                        Vector3 upOffset = Vector3.up * 0.45f;

                        Vector3 playerBoxOffset0 = pivotPos - rightOffset - upOffset;
                        Vector3 playerBoxOffset1 = pivotPos + rightOffset - upOffset;
                        Vector3 playerBoxOffset2 = pivotPos + rightOffset + upOffset;
                        Vector3 playerBoxOffset3 = pivotPos - rightOffset + upOffset;

                        BoxLines.SetPosition(0, playerBoxOffset0);
                        BoxLines.SetPosition(1, playerBoxOffset1);
                        BoxLines.SetPosition(2, playerBoxOffset2);
                        BoxLines.SetPosition(3, playerBoxOffset3);
                        UnityEngine.Object.Destroy(BoxLines, Time.deltaTime);
                    }
                }
            }
        }



        public static void Platforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerGripFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerGripFloat;
            if (gripdownright == 1f && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rPlat.GetComponent<Renderer>().material.color = Color.black;
                rPlat.transform.localScale = scale;
                rPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rPlat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (gripdownleft == 1f && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lPlat.GetComponent<Renderer>().material.color = Color.black;
                lPlat.transform.localScale = scale;
                lPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                lPlat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void StickyPlatforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerGripFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerGripFloat;
            if (gripdownright == 1f && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rPlat.GetComponent<Renderer>().material.color = Color.black;
                rPlat.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                rPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rPlat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (gripdownleft == 1f && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lPlat.GetComponent<Renderer>().material.color = Color.black;
                lPlat.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                lPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                lPlat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void InvisPlatforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerGripFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerGripFloat;
            if (gripdownright == 1f && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rPlat.GetComponent<Renderer>());
                rPlat.transform.localScale = scale;
                rPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rPlat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (gripdownleft == 1f && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lPlat.GetComponent<Renderer>());
                lPlat.transform.localScale = scale;
                lPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                lPlat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void NoRotatePlatforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerGripFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerGripFloat;
            if (gripdownright == 1f && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rPlat.GetComponent<Renderer>().material.color = Color.black;
                rPlat.transform.localScale = scale;
                rPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (gripdownleft == 1f && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lPlat.GetComponent<Renderer>().material.color = Color.black;
                lPlat.transform.localScale = scale;
                lPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void TriggerPlatforms()
        {
            var gripdownright = ControllerInputPoller.instance.rightControllerIndexFloat;
            var gripdownleft = ControllerInputPoller.instance.leftControllerIndexFloat;
            if (gripdownright == 1f && RightToggle)
            {
                rPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rPlat.GetComponent<Renderer>().material.color = Color.black;
                rPlat.transform.localScale = scale;
                rPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                rPlat.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                RightToggle = false;
            }
            if (gripdownright != 1f)
            {
                GameObject.Destroy(rPlat);
                RightToggle = true;
            }
            if (gripdownleft == 1f && LeftToggle)
            {
                lPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lPlat.GetComponent<Renderer>().material.color = Color.black;
                lPlat.transform.localScale = scale;
                lPlat.transform.position = new Vector3(0f, -0.00825f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                lPlat.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                LeftToggle = false;
            }
            if (gripdownleft != 1f)
            {
                GameObject.Destroy(lPlat);
                LeftToggle = true;
            }
        }

        public static void Fly()
        {
            if (Inputs.B() || Inputs.A())
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void NoClipFly()
        {
            if (Inputs.B() || Inputs.A())
            {
                foreach (MeshCollider mesh in UnityEngine.GameObject.FindObjectsOfType<MeshCollider>())
                {
                    mesh.enabled = false;
                }
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else
            {
                foreach (MeshCollider mesh in UnityEngine.GameObject.FindObjectsOfType<MeshCollider>())
                {
                    mesh.enabled = true;
                }
            }
        }



        public static void NoClipFlyTrigger()
        {
            if (Inputs.LT() || Inputs.RT())
            {
                foreach (MeshCollider mesh in UnityEngine.GameObject.FindObjectsOfType<MeshCollider>())
                {
                    mesh.enabled = false;
                }
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else
            {
                foreach (MeshCollider mesh in UnityEngine.GameObject.FindObjectsOfType<MeshCollider>())
                {
                    mesh.enabled = true;
                }
            }
        }

        public static void RoomSettings(bool publicroom, bool privateroom)
        {
            if (publicroom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    PhotonNetwork.CurrentRoom.IsVisible = true;
                }
                else
                {
                    NotifiLib.SendNotification("[<color=red>ERROR</color>] Not master client. when master try again.");
                    return;
                }

            }
            if (privateroom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                }
                else
                {
                    NotifiLib.SendNotification("[<color=red>ERROR</color>] Not master client. when master try again.");
                    return;
                }
            }
        }

        public static void TriggerFly()
        {
            if (Inputs.RT() || Inputs.LT())
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void Noclip()
        {
            if (Inputs.RT())
            {
                foreach (MeshCollider m in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    m.enabled = false;
                }
            }
            else
            {
                foreach (MeshCollider m2 in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    m2.enabled = true;
                }
            }
        }

        public static void GrabBug()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position;
            }
        }

        public static void GrabBugR()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
            }
        }

        public static void SpazBug()
        {

            GameObject.Find("Floating Bug Holdable").transform.rotation = Quaternion.EulerAngles(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }


        public static void GrabBat()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position;
            }
        }

        public static void GrabBatR()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
            }
        }

        public static void SpazBat()
        {
            GameObject.Find("Cave Bat Holdable").transform.rotation = Quaternion.EulerAngles(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
        }

        public static void GhostMod(bool activate, bool found, bool catchp)
        {
            if (activate)
            {
                foreach (SecondLookSkeletonSynchValues secondlook in UnityEngine.Object.FindObjectsOfType<SecondLookSkeletonSynchValues>())
                {
                    foreach (SecondLookSkeleton second in UnityEngine.Object.FindObjectsOfType<SecondLookSkeleton>())
                    {
                        if (second.currentState == SecondLookSkeleton.GhostState.Unactivated)
                        {
                            secondlook.currentState = SecondLookSkeleton.GhostState.Activated;
                            second.tapped = true;
                            second.GetComponent<PhotonView>().RPC("RemoteActivateGhost", RpcTarget.All, new object[] { });
                        }
                    }
                }
            }
            if (found)
            {
                foreach (SecondLookSkeletonSynchValues secondlook in UnityEngine.Object.FindObjectsOfType<SecondLookSkeletonSynchValues>())
                {
                    foreach (SecondLookSkeleton second in UnityEngine.Object.FindObjectsOfType<SecondLookSkeleton>())
                    {
                        if (second.currentState == SecondLookSkeleton.GhostState.Unactivated)
                        {

                            secondlook.currentState = SecondLookSkeleton.GhostState.Activated;
                            second.tapped = true;
                            second.GetComponent<PhotonView>().RPC("RemoteActivateGhost", RpcTarget.All, new object[] { });
                        }
                        else
                        {
                            secondlook.currentState = SecondLookSkeleton.GhostState.Chasing;
                            second.tapped = true;
                            second.GetComponent<PhotonView>().RPC("RemotePlayerSeen", RpcTarget.All, new object[] { });
                        }
                    }
                }
            }
            if (catchp)
            {
                foreach (SecondLookSkeletonSynchValues secondlook in UnityEngine.Object.FindObjectsOfType<SecondLookSkeletonSynchValues>())
                {
                    foreach (SecondLookSkeleton second in UnityEngine.Object.FindObjectsOfType<SecondLookSkeleton>())
                    {
                        if (second.currentState == SecondLookSkeleton.GhostState.Unactivated)
                        {

                            secondlook.currentState = SecondLookSkeleton.GhostState.Activated;
                            second.tapped = true;
                            second.GetComponent<PhotonView>().RPC("RemoteActivateGhost", RpcTarget.All, new object[] { });
                        }
                        else
                        {
                            secondlook.currentState = SecondLookSkeleton.GhostState.CaughtPlayer;
                            second.tapped = true;
                            second.GetComponent<PhotonView>().RPC("RemotePlayerCaught", RpcTarget.All, new object[] { });
                        }
                    }
                }
            }
        }

        public static void SlideControl()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.slideControl = int.MaxValue;
            }
            else
            {
                GorillaLocomotion.Player.Instance.slideControl = 0f;
            }
        }

        public static void GrabMonster()
        {
            foreach (MonkeyeAI ai in UnityEngine.Object.FindObjectsOfType<MonkeyeAI>())
            {
                if (ControllerInputPoller.instance.leftGrab)
                {
                    ai.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                }
            }
        }

        public static void GrabMonsterR()
        {
            foreach (MonkeyeAI ai in UnityEngine.Object.FindObjectsOfType<MonkeyeAI>())
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    ai.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                }
            }
        }

        public static void SpazMonster()
        {
            foreach (MonkeyeAI ai in UnityEngine.Object.FindObjectsOfType<MonkeyeAI>())
            {
                ai.transform.rotation = Quaternion.EulerAngles(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
            }
        }

        public static void SnowballGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 32;
            }
        }

        public static void WaterBalloonGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 204;
            }
        }

        public static void RockGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 231;
            }
        }

        public static void GiftGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 240;
            }
        }

        public static void MentosGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 249;
            }
        }

        public static void FishFoodGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 252;
            }
        }

        public static void FixGround()
        {
            foreach (GorillaSurfaceOverride gorilla in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorilla.overrideIndex = 32;
            }
        }

        public static void AutoOpenDoorWhenClose()
        {
            foreach (GTDoor door in UnityEngine.Object.FindObjectsOfType<GTDoor>())
                foreach (GTDoorTrigger trigger in UnityEngine.Object.FindObjectsOfType<GTDoorTrigger>())
                {
                    float distance = Vector3.Distance(trigger.transform.position, GorillaTagger.Instance.offlineVRRig.transform.position);

                    if (distance <= 2f)
                    {
                        door.GetComponent<PhotonView>().RPC("ChangeDoorState", RpcTarget.Others, new object[]
                        {
                        GTDoor.DoorState.Opening
                        });
                    }
                }
        }

        public static void OpenDoor()
        {
            foreach (GTDoor door in UnityEngine.Object.FindObjectsOfType<GTDoor>())
                foreach (GTDoorTrigger trigger in UnityEngine.Object.FindObjectsOfType<GTDoorTrigger>())
                {
                    door.ChangeDoorState(GTDoor.DoorState.Open);
                    door.GetComponent<PhotonView>().RPC("ChangeDoorState", RpcTarget.Others, new object[]
                        {
                        GTDoor.DoorState.Opening
                        });
                }
        }

        public static void CloseDoor()
        {
            foreach (GTDoor door in UnityEngine.Object.FindObjectsOfType<GTDoor>())
                foreach (GTDoorTrigger trigger in UnityEngine.Object.FindObjectsOfType<GTDoorTrigger>())
                {
                    door.GetComponent<PhotonView>().RPC("ChangeDoorState", RpcTarget.Others, new object[]
                        {
                        GTDoor.DoorState.Opening
                        });
                }
        }

        public static void SpazDoor()
        {
            foreach (GTDoor door in UnityEngine.Object.FindObjectsOfType<GTDoor>())
                foreach (GTDoorTrigger trigger in UnityEngine.Object.FindObjectsOfType<GTDoorTrigger>())
                {
                    door.transform.rotation = Quaternion.EulerAngles(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
                }
        }

        public static void Stick()
        {
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/LBAAK").SetActive(true);
        }

        public static void DisableStick()
        {
            GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/Old Cosmetics Body/LBAAK").SetActive(false);
        }

        public static void BreakAudioGun()
        {

        }

        public static void BreakAllAudio()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = rig.transform.position;
                    GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.All, new object[]
                    {
                    120,
                    false,
                    99999999999f
                    });
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void StumpTeleport()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (MeshCollider mesh in UnityEngine.Object.FindObjectsOfType<MeshCollider>())
                {
                    mesh.enabled = false;
                }
                Vector3 main = new Vector3(-67.0158f, 11.7668f, -82.6018f);
                GorillaTagger.Instance.offlineVRRig.transform.position = main;
            }
            else
            {
                foreach (MeshCollider mesh in UnityEngine.Object.FindObjectsOfType<MeshCollider>())
                {
                    mesh.enabled = true;
                }
            }
        }

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

        public static void SetOwnershipBug() //CATLICKER DID IT WITH ONLY LIKE 2 LINES AND IT DIDNT WORK :SOB: IT WILL CONTROLLER AND OWNERSHIP U GOTTA DO ALL OF THIS FOR IT TO WORK 
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

        public static void SetOwnershipBat()
        {
            const bool isplayersactornumberownership = false;
            Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[PhotonNetwork.CurrentRoom.PlayerCount];
            GameObject gorilla = GameObject.Find("Cave Bat Holdable");
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


        public static void CheckOwnershipGorillaNot()
        {
            GorillaNot gorilla = new GorillaNot();
            PhotonView gorillaview = gorilla.GetComponent<PhotonView>();
            if (gorillaview.ControllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are ownership of Bug");
                return;
            }
            else
            {
                NotifiLib.SendNotification("[<color=red>ERROR</color>] You are not ownership of Bug");
                return;
            }
        }

        public static void CheckOwnershipMonkeye()
        {
            MonkeyeAI monkeye = new MonkeyeAI();
            PhotonView gmonkeyeview = monkeye.GetComponent<PhotonView>();
            if (gmonkeyeview.ControllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are ownership of Bug");
                return;
            }
            else
            {
                NotifiLib.SendNotification("[<color=red>ERROR</color>] You are not ownership of Bug");
                return;
            }
        }

        public static void CheckOwnershipBug()
        {
            GameObject bugobject = GameObject.Find("Throwable Bug");
            PhotonView bugview = bugobject.GetComponent<PhotonView>();
            if (bugview.ControllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are ownership of Bug");
                return;
            }
            else
            {
                NotifiLib.SendNotification("[<color=red>ERROR</color>] You are not ownership of Bug");
                return;
            }
        }

        public static void CheckOwnershipBat()
        {
            GameObject batobject = GameObject.Find("Cave Bat Holdable");
            PhotonView batview = batobject.GetComponent<PhotonView>();
            if (batview.ControllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                NotifiLib.SendNotification("[<color=green>SUCCESS</color>] You are ownership of Bug");
                return;
            }
            else
            {
                NotifiLib.SendNotification("[<color=red>ERROR</color>] You are not ownership of Bug");
                return;
            }
        }

        public static void AntiModCheck()
        {
            Hashtable hash = new Hashtable();
            hash.Add("mods", null);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash, null, null);
        }

        public static void FuckModCheckers()
        {
            Hashtable hash = new Hashtable();
            hash.Add("mods", "लूनर ऑन टॉप भाड़ में जाओ हाहाहा");
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash, null, null);
        }

        public static void DisableObjectPools(bool enable)
        {
            if (enable)
            {
                GameObject game = GameObject.Find("GlobalObjectPools");
                GameObject game2 = game;
                game.SetActive(false);
            }
            else
            {
                GameObject game = GameObject.Find("GlobalObjectPools");
                GameObject game2 = game;
                game.SetActive(true);
            }
        }

        public static void DestroyObjectPools()
        {
            ObjectPools objectpools = new ObjectPools();
            UnityEngine.Object.Destroy(objectpools);
        }

        static string[] array = { "88.11.182.247", "145.64.26.88", "39.209.248.3" };
        static string[] array2 = { "Maine", "Kentuky", "Oklahoma", "California", "Vermont", "Mississippi" };
        static string[] array3 = { "Tanzania", "Estonia", "Jamaica", "North Korea", "Liberia", "Croatia" };
        static string[] array4 = { "Jacob Tyler", "Matheson Wheeler", "Marin Paul", "Ava Louise", "Skibidi Gyat Rizz", "Jim Arlene", "Alessa Riney" };
        static string[] array5 = { "11", "13", "12", "16", "18", "15" };
        static string[] array6 = { "Northern", "Southern", "Western", "Eastern" };
        static string[] array7 = { "115.59629", "-40.27633", "-134.44633", "94.60089", "154.3002", "-21.06900" };
        static string[] array8 = { "-35.87620", "17.93031", "3.39578", "-50.05613", "-41.80545", "56.30833" };
        static float cooldown251;

        public static void DoxGun()
        {
            int range = UnityEngine.Random.Range(0, array.Length);
            int range2 = UnityEngine.Random.Range(0, array2.Length);
            int range3 = UnityEngine.Random.Range(0, array3.Length);
            int range4 = UnityEngine.Random.Range(0, array4.Length);
            int range5 = UnityEngine.Random.Range(0, array5.Length);
            int range6 = UnityEngine.Random.Range(0, array6.Length);
            int range7 = UnityEngine.Random.Range(0, array7.Length);
            int range8 = UnityEngine.Random.Range(0, array8.Length);
            if (ControllerInputPoller.instance.rightGrab && ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                if (Time.time > cooldown251 + 2.3f)
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=cyan>LUNAR</color><color=grey>|</color>GRABBED INFO</color><color=grey>]</color>\n<color=magenta>IP: " + array[range] + "</color>\n<color=magenta>STATE: " + array2[range2] + "</color>\n<color=magenta>COUNTRY: " + array3[range3] + "</color>\n<color=magenta>NAME: " + array4[range4] + "</color>\n<color=magenta>AGE: " + array5[range5] + "</color>\n<color=magenta>HEMISPHERE: " + array6[range6] + "</color>\n<color=magenta>LONGITUDE: " + array7[range7] + "</color>\n<color=magenta>LATITUDE: " + array8[range8] + "</color>");
                    cooldown251 = Time.time;
                    return;
                }
            }
        }

        public static void Gates(bool openall, bool closeall, bool openfrontgate)
        {
            if (openall)
            {
            }
            else if (closeall)
            {

            }
            else if (openfrontgate)
            {
                GhostLabReliableState ghost2 = new GhostLabReliableState();
                foreach (GhostLab ghost in UnityEngine.Object.FindObjectsOfType<GhostLab>())
                {
                    ghost2.GetComponent<PhotonView>().RPC("RemoteEntranceDoorState", RpcTarget.Others, ghost);
                    ghost.doorState = GhostLab.EntranceDoorsState.InnerDoorOpen;
                    ghost.UpdateDoorState(3);
                }
            }
        }

    }
}
