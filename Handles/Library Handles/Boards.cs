using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Stealth
{
    internal class Boards
    {
        public static void Setcoc(string COCTOP, string COCBOTTOM, Color col)
        {
            GameObject[] Objects = new GameObject[]
            {
                GameObject.Find("CodeOfConduct"), // Top
                GameObject.Find("COC Text"), // Bottom
                GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/StaticUnlit/screen") // bg
            };
            Objects[0].GetComponent<Text>().text = COCTOP;
            Objects[1].GetComponent<Text>().text = COCBOTTOM;
        }
        public static void SetMOTD(string Top)
        {
            GameObject MOTD = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/motd");
            MOTD.GetComponent<Text>().text = Top;
        }

    }
}
