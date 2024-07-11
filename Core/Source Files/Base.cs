using BepInEx;
using Stealth;
using HarmonyLib;
using Stealth;
using Mods;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Stealth;
using static Mods.Globals;
using Stealth.Core.Header_Files;
using Fusion;
using Unity.Burst.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using Photon.Pun;

namespace Stealth.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Base : MonoBehaviour
    {
        public static void Prefix()
        {
            try
            {
                bool[] Openinputs = new bool[]
                {
                     rightHanded == true ? Inputs.B() : Inputs.Y(), // Menu Input
                     UnityInput.Current.GetKey(KeyCode.Q) // Pc Menu Input
                };

                if (menu == null)
                {
                    if (Openinputs[0] || Openinputs[1])
                    {
                        BuildFrame();
                        Reposmenu(rightHanded, Openinputs[1]);
                        CreateReference(rightHanded);
                    }
                }
                else
                {
                    if (Openinputs[0] || Openinputs[1])
                        Reposmenu(rightHanded, Openinputs[1]);
                    else
                        Deps.DestroyMenu();
                }
            }
            catch { }
            try { Deps.Thingy(); } catch { }
            try { Deps.UpdateColorVals(); } catch { }
            try { Deps.UpdateBoards(); } catch { }
            try
            {
                if (PhotonNetwork.CurrentRoom.IsVisible == true)
                {
                    Deps.GetIndex("Is Public: ").overlapText = "Is Public: <color=green>True</color>";
                }
                else
                {
                    Deps.GetIndex("Is Public: ").overlapText = "Is Public: <color=green>False</color>";
                }

                if (PhotonNetwork.CurrentRoom.IsVisible == false)
                {
                    Deps.GetIndex("Is Private: ").overlapText = "Is Private: <color=green>True</color>";
                }
                else
                {
                    Deps.GetIndex("Is Private: ").overlapText = "Is Private: <color=green>False</color>";
                }
            }
            catch { }
        }


        public static void BuildFrame()
        {
            // Creating Parent For Menu Object.
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.26f, 0.3825f);
            // Creating Main Menu Object.
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = new Vector3(0.1f, 0.86f, 0.86f);
            menuBackground.GetComponent<Renderer>().material = MenuColor;
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            // Creating Text Canvas.
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;
            // Adding Menu Title.
            UnityEngine.UI.Text text = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<UnityEngine.UI.Text>();
            text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text.text = "lunar v" + Stealth.PluginInfo.Version;
            text.fontSize = 1;
            text.color = Color.white;
            text.supportRichText = true;
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.19f, 0.04f);
            component.position = new Vector3(0.06f, 0f, 0.148f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            // Adding Pages.
            PageButton(">");
            PageButton("<");
            // Showing visible buttons.
            var lookforcatogory = Stealth.Buttons.categories[buttonsType];
            var realbuttons = lookforcatogory.Buttons.Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < realbuttons.Length; i++)
                AddButtons(i * 0.1f, realbuttons[i], PrimitiveType.Cube);
        }
        public static void PageButton(string content)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = (content == "<") ? new Vector3(0.09f, 0.805f, 0.08f) : new Vector3(0.09f, 0.805f, 0.08f);
            gameObject.transform.localPosition = (content == "<") ? new Vector3(0.56f, 0f, 0.28f) : new Vector3(0.56f, 0f, 0.18f);
            gameObject.GetComponent<Renderer>().material.color = new Color32(21, 21, 21, 1);
            gameObject.AddComponent<Stealth.Button>().relatedText = (content == "<") ? "PreviousPage" : "NextPage";
            UnityEngine.UI.Text text = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<UnityEngine.UI.Text>();
            text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text.text = "<";
            text.fontSize = 1;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(.064f, 0, 0.28f / 2.65f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            UnityEngine.UI.Text text2 = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<UnityEngine.UI.Text>();
            text2.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text2.text = ">";
            text2.fontSize = 1;
            text2.color = Color.white;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component2 = text2.GetComponent<RectTransform>();
            component2.localPosition = Vector3.zero;
            component2.sizeDelta = new Vector2(0.2f, 0.03f);
            component2.localPosition = new Vector3(.064f, 0, 0.18f / 2.65f);
            component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void AddButtons(float offset, buttontemplate method, PrimitiveType Shape)
        {
            GameObject gameObject = GameObject.CreatePrimitive(Shape);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.805f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.08f - offset);
            gameObject.AddComponent<Stealth.Button>().relatedText = method.Text;
            gameObject.GetComponent<Renderer>().material.color = new Color32(21, 21, 21, 1);
            UnityEngine.UI.Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<UnityEngine.UI.Text>();
            text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
            text.text = method.overlapText != null ? method.overlapText : method.Text;
            text.supportRichText = true;
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, 0.032f - offset / 2.65f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (method.enabled)
            {
                text.color = Color.green;
            }
        }



        public static void Reposmenu(bool R, bool KEYS)
        {
            if (KEYS && (TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>()) != null)
            {
                TPC.transform.position = new Vector3(-999f, -999f, -999f);
                TPC.transform.rotation = Quaternion.identity;
                menu.transform.parent = TPC.transform;
                menu.transform.position = TPC.transform.position + Vector3.Scale(TPC.transform.forward + TPC.transform.up, new Vector3(0.5f, -0.02f, 0.5f));
                menu.transform.rotation = Quaternion.Euler(TPC.transform.rotation.eulerAngles - new Vector3(90, -90, 0));
                if (reference != null && Mouse.current.leftButton.isPressed)
                {
                    Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out RaycastHit hit, 100))
                        hit.transform.gameObject.GetComponent<Stealth.Button>()?.OnTriggerEnter(buttonCollider);
                }
                else if (reference != null) reference.transform.position = new Vector3(999f, -999f, -999f);
            }
            else if (!KEYS)
            {
                var handTransform = R ? GorillaTagger.Instance.rightHandTransform : GorillaTagger.Instance.leftHandTransform;
                menu.transform.position = handTransform.position;
                menu.transform.rotation = R ? Quaternion.Euler(handTransform.rotation.eulerAngles + new Vector3(0f, 0f, 180f)) : handTransform.rotation;
            }
        }


        public static void CreateReference(bool R)
        {
            if (reference == null)
            {
                reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                if (R)
                {
                    reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
                }
                else
                {
                    reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
                }
                reference.GetComponent<Renderer>().material.color = Color.black;
                reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                buttonCollider = reference.GetComponent<SphereCollider>();
            }
        }

        public static void Toggle(string BT)
        {
            int lastPage = (Stealth.Buttons.categories[buttonsType].Buttons.Count + Globals.buttonsPerPage - 1) / Globals.buttonsPerPage - 1;

            switch (BT)
            {
                case "PreviousPage":
                    pageNumber = (pageNumber > 0) ? pageNumber - 1 : lastPage;
                    break;
                case "NextPage":
                    pageNumber = (pageNumber < lastPage) ? pageNumber + 1 : 0;
                    break;
                default:
                    var buttontemplate = Deps.GetIndex(BT);
                    if (buttontemplate != null)
                    {
                        if (buttontemplate.isTogglable)
                        {
                            buttontemplate.enabled = !buttontemplate.enabled;
                            try
                            {
                                if (buttontemplate.enabled && buttontemplate.enableMethod != null) buttontemplate.enableMethod.Invoke();
                                else if (!buttontemplate.enabled && buttontemplate.disableMethod != null) buttontemplate.disableMethod.Invoke();
                            }
                            catch { }
                        }
                        else
                        {
                            try { buttontemplate.method?.Invoke(); } catch { }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError($"{BT} does not exist");
                    }
                    break;
            }

            Deps.RecreateMenu();
        }



    }
}
