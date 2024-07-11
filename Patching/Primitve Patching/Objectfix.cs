﻿using System;
using HarmonyLib;
using UnityEngine;

namespace Patching.ObjectPatching
{
    [HarmonyPatch(typeof(GameObject))]
    [HarmonyPatch("CreatePrimitive", 0)]
    internal class ShaderFix : MonoBehaviour
    {
        private static void Postfix(GameObject __result)
        {
            __result.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            __result.GetComponent<Renderer>().material.color = new Color32(255, 128, 0, 128);
        }
    }
}