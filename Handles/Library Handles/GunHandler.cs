using BepInEx;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.GUI;
using UnityEngine.InputSystem;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Reflection;
using UnityEngine.Animations.Rigging;

namespace Stealth
{
    internal class Guns
    {
        public static bool line = true;
        public static VRRig VrrigThatIsLocked;
        public static Photon.Realtime.Player rigd;
        public static GameObject PointerObj;
        public static Material Gunonmaterial = new Material(Shader.Find("GUI/Text Shader"));
        public static Material Gunoffmaterial = new Material(Shader.Find("GUI/Text Shader"));
        public static void LineRendererModule(Vector3 FirstPos, Vector3 LastPos)
        {
            GameObject renderer = new GameObject("LineRenderer");
            LineRenderer render = renderer.AddComponent<LineRenderer>();
            render.material.shader = Shader.Find("GUI/Text Shader");
            render.startColor = Inputs.RT() ? Gunonmaterial.color : Gunoffmaterial.color;
            render.endColor = Inputs.RT() ? Gunonmaterial.color : Gunoffmaterial.color;
            render.positionCount = 2;
            render.useWorldSpace = true;
            render.startWidth = 0.013f;
            render.endWidth = 0.013f;
            render.SetPosition(0, FirstPos);
            render.SetPosition(1, LastPos);
            UnityEngine.Object.Destroy(renderer, Time.deltaTime);
        }
        public static void Gun(Action act, bool LockOn)
        {
            bool pc = false;

            if (ControllerInputPoller.instance.rightControllerGripFloat == 1f || Mouse.current.rightButton.isPressed)
            {
                RaycastHit raycastHit;
                Vector3 rayStart = Vector3.zero;
                Vector3 rayDirection = Vector3.zero;

                if (ControllerInputPoller.instance.rightControllerGripFloat == 1f)
                {
                    pc = false;
                    rayStart = GorillaTagger.Instance.rightHandTransform.position;
                    rayDirection = -GorillaTagger.Instance.rightHandTransform.up;
                }
                if (Mouse.current.rightButton.isPressed)
                {
                    pc = true;
                    rayStart = pc ? Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).origin : Camera.main.transform.position;
                    rayDirection = pc ? Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).direction : Camera.main.transform.forward;
                }

                if (Physics.Raycast(rayStart, rayDirection, out raycastHit))
                {
                    PointerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    PointerObj.AddComponent<Renderer>();
                    PointerObj.GetComponent<Renderer>().material = Gunoffmaterial;
                    PointerObj.gameObject.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
                    PointerObj.gameObject.transform.position = raycastHit.point;
                    GameObject.Destroy(PointerObj, Time.deltaTime);
                    if (line && pc)
                        LineRendererModule(GorillaTagger.Instance.headCollider.transform.position, PointerObj.transform.position);
                    else if (line && !pc)
                        LineRendererModule(GorillaTagger.Instance.rightHandTransform.position, PointerObj.transform.position);


                    if (ControllerInputPoller.instance.rightControllerIndexFloat == 1f || Mouse.current.leftButton.isPressed)
                    {
                        PointerObj.GetComponent<Renderer>().material = Gunonmaterial;
                        if (/*!pc &&*/ LockOn)
                        {
                            if (raycastHit.collider.GetComponentInParent<VRRig>() != null)
                            {
                                VrrigThatIsLocked = raycastHit.collider.GetComponentInParent<VRRig>();
                                
                            }

                            if (VrrigThatIsLocked != null)
                            {
                                PointerObj.transform.position = VrrigThatIsLocked.transform.position;
                                act();
                            }
                            else
                            {
                                act();
                            }
                            rigd = RigHandle.GetPlayerFromVRRig(VrrigThatIsLocked);
                        }
                        if (/*pc || */!LockOn)
                        {
                            act();
                        }
                    }
                }
            }

            if (ControllerInputPoller.instance.leftControllerGripFloat == 1f || Mouse.current.leftButton.isPressed)
            {
                RaycastHit raycastHit;
                Vector3 rayStart = Vector3.zero;
                Vector3 rayDirection = Vector3.zero;

                if (ControllerInputPoller.instance.leftControllerGripFloat == 1f)
                {
                    pc = false;
                    rayStart = GorillaTagger.Instance.leftHandTransform.position;
                    rayDirection = -GorillaTagger.Instance.leftHandTransform.up;
                }
                if (Mouse.current.leftButton.isPressed)
                {
                    pc = true;
                    rayStart = pc ? Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).origin : Camera.main.transform.position;
                    rayDirection = pc ? Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).direction : Camera.main.transform.forward;
                }

                if (Physics.Raycast(rayStart, rayDirection, out raycastHit))
                {
                    PointerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    PointerObj.AddComponent<Renderer>();
                    PointerObj.GetComponent<Renderer>().material = Gunoffmaterial;
                    PointerObj.gameObject.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
                    PointerObj.gameObject.transform.position = raycastHit.point;
                    GameObject.Destroy(PointerObj, Time.deltaTime);

                    if (line && pc)
                        LineRendererModule(GorillaTagger.Instance.headCollider.transform.position, PointerObj.transform.position);
                    else if (line && !pc)
                        LineRendererModule(GorillaTagger.Instance.leftHandTransform.position, PointerObj.transform.position);
                    //Photon.Realtime.Player rigder;
                    if (ControllerInputPoller.instance.leftControllerIndexFloat == 1f || Mouse.current.leftButton.isPressed)
                    {
                        PointerObj.GetComponent<Renderer>().material = Gunonmaterial;
                        if (/*!pc &&*/ LockOn)
                        {
                            if (raycastHit.collider.GetComponentInParent<VRRig>() != null)
                            {
                                VrrigThatIsLocked = raycastHit.collider.GetComponentInParent<VRRig>();
                            }

                            if (VrrigThatIsLocked != null)
                            {
                                PointerObj.transform.position = VrrigThatIsLocked.transform.position;
                                act();
                            }
                            else
                            {
                                act();
                            }
                            rigd = RigHandle.GetPlayerFromVRRig(VrrigThatIsLocked);
                        }
                        if (/*pc ||*/ !LockOn)
                        {
                            act();
                        }
                    }
                }
            }
        }
    }
}