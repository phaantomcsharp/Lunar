using BepInEx;
using Stealth;
using Stealth.Patches;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static Mods.Globals;
using static Stealth.Menu.Base;
using System.Linq;

using Oculus.Platform;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using GorillaNetworking;
namespace Stealth.Core.Header_Files
{
    internal class Deps
    {
        public static void Thingy()
        {
            foreach (var category in Stealth.Buttons.categories)
            {
                foreach (var button in category.Buttons)
                {
                    if (button.enabled)
                    {
                        if (button.method != null)
                        {
                            try
                            {
                                button.method.Invoke();
                            }
                            catch (Exception e)
                            {
                                Debug.LogError($"Error executing button {button.Text}: {e.Message}");
                            }
                        }
                    }
                }
            }
        }
        public static void UpdateBoards()
        {
            Material material = new Material(Shader.Find("GorillaTag/UberShader"));
            material.color = MenuColor.color;
            const string lunartext = "Lunar";
            if (PlayFabAuthenticator.instance.loginFailed)
            {
                Stealth.Boards.Setcoc("<color=white>" + lunartext + "</color>".ToUpper(), "<color=white>Made by phaantom & azora \nBan Length: idfk i didint add yet</color>".ToUpper(), Color.black);
            }
            else
            {
                Stealth.Boards.Setcoc("<color=white>" + lunartext + "</color>".ToUpper(), "<color=white>Made by phaantom & azora</color>".ToUpper(), Color.black);
            }
            Stealth.Boards.SetMOTD("<color=white>Lunar</color>".ToUpper());
            GameObject.Find("wallmonitorforest").GetComponent<Renderer>().material = material;
            GameObject.Find("monitorScreen").GetComponent<MeshRenderer>().material = material;
            GameObject.Find("modtext").GetComponent<Text>().text = "hi this is phaantom, the creator of lunar. have fun be safe and i dont take responsibility for ur bans. have fun".ToUpper();
             
        }

        public static buttontemplate GetIndex(string buttonText)
        {
            return Stealth.Buttons.categories
                .SelectMany(category => category.Buttons)
                .FirstOrDefault(button => button.Text == buttonText);
        }


        public static void DestroyMenu()
        {
            UnityEngine.Object.Destroy(menu);
            menu = null;

            UnityEngine.Object.Destroy(reference);
            reference = null;
        }
        public static void UpdateColorVals()
        {
            MenuColor.color = Color.black;
            Guns.Gunonmaterial.color = Color.Lerp(Color.black, Color.black, Mathf.PingPong(Time.time, 1f));
            Guns.Gunoffmaterial.color = Color.black;
        }
        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                BuildFrame();
                Reposmenu(rightHanded, UnityInput.Current.GetKey(KeyCode.Q));
            }
        }

    }
}
