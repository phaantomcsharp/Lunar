using Stealth;
using Mods;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Stealth.Core.Header_Files
{
    internal class Projectiles /* IIDK GAVE ME THESE */
    {
        public static string[] fullProjectileNames = new string[]
        {
            "Snowball", //actual gameobjects
            "WaterBalloon",
            "LavaRock",
            "ThrowableGift",
            "ScienceCandy",
            "FishFood"
        };
        static float floatlol = 0f;
        public static void Base(string projectileName, Vector3 position, Vector3 velocity, Color color, bool noDelay = false)
        {
            
            ControllerInputPoller.instance.leftControllerGripFloat = 1f;
            GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(lhelp, 0.1f);
            lhelp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            lhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            lhelp.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            int[] overrides = new int[]
            {
                32, //overide index for object
                204,
                231,
                240,
                249,
                252
            };
            lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = overrides[Array.IndexOf(fullProjectileNames, projectileName)];
            lhelp.GetComponent<Renderer>().enabled = false;
            if (Time.time > 0.1f)
            {
                try
                {
                    Vector3 startpos = position;
                    Vector3 charvel = velocity;

                    Vector3 oldVel = GorillaTagger.Instance.GetComponent<Rigidbody>().velocity;

                    string[] name2 = new string[]
                    {
                        "LMACE.",
                        "LMAEX.",
                        "LMAGD.",
                        "LMAHQ.",
                        "LMAIE.",
                        "LMAIO.",
                        ""
                    };
                    SnowballThrowable throwable = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/" + fullProjectileNames[System.Array.IndexOf(fullProjectileNames, projectileName)] + "LeftAnchor").transform.Find(name2[System.Array.IndexOf(fullProjectileNames, projectileName)]).GetComponent<SnowballThrowable>();
                    throwable.randomizeColor = true;
                    throwable.transform.position = position;
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = charvel;
                    GorillaTagger.Instance.offlineVRRig.SetThrowableProjectileColor(true, color);
                    GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/EquipmentInteractor").GetComponent<EquipmentInteractor>().ReleaseLeftHand();
                    Mods.Mods.RPC();
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = oldVel;
                }
                catch { }
                if (0.1f > 0f && !noDelay)
                {
                     floatlol = Time.time + 0.1f;
                }
                PhotonNetwork.NetworkingClient.OpRaiseEvent(3, null, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
                PhotonNetwork.NetworkingClient.OpRaiseEvent(3, null, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendUnreliable);
            }
        }

        public static void Launch(string projectilename, string trailname, Vector3 position, Vector3 velocity, Color color, Color color2,bool oranget, bool noDelay = false)
        {
            Base(projectilename, position, velocity, Color.Lerp(color, color2, Mathf.PingPong(Time.time, 1f)), noDelay);
        }


        public static void Snowball()
        {
            if (Inputs.LG())
            {

                Vector3 position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position + new Vector3(0f, -0.05f, 0.25f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("Snowball", "none", position, velocity, Color.red, Color.blue ,false, false);
            }
            
        }

        public static void WaterBalloon()
        {
            if (Inputs.LG())
            {
                Vector3 position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position + new Vector3(0f, -0.05f, 0.25f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("WaterBalloon", "none", position, velocity, Color.red, Color.blue, false, false);
            }
        }

        public static void LavaRock()
        {
            if (Inputs.LG())
            {
                Vector3 position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position + new Vector3(0f, -0.05f, 0.25f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("LavaRock", "none", position, velocity, Color.red, Color.blue, false, false);
            }

        }

        public static void ThrowableGift()
        {
            if (Inputs.LG())
            {
                Vector3 position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position + new Vector3(0f, -0.05f, 0.25f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("ThrowableGift", "none", position, velocity, Color.red, Color.blue, false, false);
            }

        }

        public static void FishFood()
        {
            if (Inputs.LG())
            {
                Vector3 position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position + new Vector3(0f, -0.05f, 0.25f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("FishFood", "none", position, velocity, Color.red, Color.blue, false, false);
            }

        }

        public static void Mentos()
        {
            if (Inputs.LG())
            {
                Vector3 position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position + new Vector3(0f, -0.05f, 0.25f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("ScienceCandy", "none", position, velocity, Color.red, Color.blue, false, false);
            }

        }

        public static void Pee()
        {
            if (Inputs.LG())
            {
                Vector3 position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, -0.15f, 0f);
                Vector3 velocity = GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f;

                Launch("Snowball", "none", position, velocity, Color.yellow, Color.yellow, false, false);
            }

        }

        

    }
}
