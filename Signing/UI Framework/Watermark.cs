using BepInEx;
using Mods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace stealth.Signing.UI_Framework
{
    [BepInPlugin("arraylist", "phaantom", "1.0")]
    internal class Watermark : BaseUnityPlugin
    {
        public static string[] buttonNames = new string[1000];
        public static string labelText = "";
        public static float updateCooldown;
        public static bool didUpdate = false;
        public void OnGUI()
        {
            if (Globals.arraylist)
            {
                if (Time.time > updateCooldown + 0.05f)
                {
                    labelText = "";
                    int i = 0;
                    for (int l = 0; l < buttonNames.Count(); l++)
                    {
                        buttonNames[i] = "";
                        i++;
                    }
                    didUpdate = false;
                    updateCooldown = Time.time;
                }
                if (!didUpdate)
                {
                    int i = 0;
                    foreach (var category in Stealth.Buttons.categories)
                    {
                        foreach (var button in category.Buttons)
                        {
                            if (button.enabled)
                            {
                                buttonNames[i] += "<color=green>| " + button.Text + "</color>\n";
                                i += 1;
                            }
                        }
                    }
                    foreach (string s in buttonNames)
                    {
                        labelText += s;
                    }
                    didUpdate = true;
                }
                GUI.skin.label.fontSize = 20;
                GUI.Label(new Rect(0, 0, 300, Screen.height), labelText);
            }
        }
    }
}
