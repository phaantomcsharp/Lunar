using Stealth.Core.Header_Files;
using Stealth;
using System.IO;
using UnityEngine;
using static Stealth.Menu.Base;
using static MB3_MeshBakerRoot.ZSortObjects;
using static Mods.Globals;
using System.Collections;
namespace Mods
{
    internal class SettingsMods
    {
        public static void JoinTab(int e)
        {
            buttonsType = e;
            pageNumber = 0;
        }
        public static void RightHand()
        {
            rightHanded = true;
        }

        public static void LeftHand()
        {
            rightHanded = false;
        }

        public static int index = 0;

        public static void TimeOfDay()
        {
            BetterDayNightManager.WeatherType weather;
            index++;
            if (index == 2)
            {
                index = 0;
            }
            if (index == 0)
            {
                weather = BetterDayNightManager.WeatherType.None;
                Deps.GetIndex("Change Weather: Raining").Text = "Change Weather: None";
            }
            if (index == 1)
            {
                weather = BetterDayNightManager.WeatherType.Raining;
                Deps.GetIndex("Change Weather: None").Text = "Change Weather: Raining";
            }
        }

        public static int index2 = 0;
        public static void ThemeChange()
        {
            index2++;
            if (index2 == 2)
            {
                index2 = 0;
            }
            if (index2 == 0)
            {
                MenuColor.color = Color.black;
                Deps.GetIndex("Change Weather: Red").Text = "Change Weather: Lunar";
            }
            if (index2 == 1)
            {
                MenuColor.color = new Color32(140, 8, 8, 1);
                Deps.GetIndex("Change Weather: Lunar").Text = "Change Weather: Red";
            }
        }

        public static void ArrayList()
        {
            arraylist = true;
        }

        public static void NoArrayList()
        {
            arraylist = false;
        }

        public static void EnableBoxWithSkeleton()
        {
            enableboxwskeleton = true;
        }

        public static void DisableBoxWithSkeleton()
        {
            enableboxwskeleton = false;
        }
    }
}
