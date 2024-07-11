using BepInEx;
using GorillaLocomotion;
using Mods;
using UnityEngine;

namespace Patching.rigpatching
{
    [BepInPlugin("nulltrrt", "ghostrigger", "1.0.0")]
    internal class GhostRig : BaseUnityPlugin
    {
        private VRRig ghostRig;

        public static GhostRig instance;

        public static bool hasInstance;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                hasInstance = true;
            }
        }

        public void LateUpdate()
        {
            var VR = GorillaTagger.Instance.offlineVRRig;
            if (ghostRig == null && VR != null)
            {
                InitializeGhostRig();
            }

            if (ghostRig == null) return;

            
            if (ghostRig.enabled && VR.enabled)
            {
                ResetGhostRig();
            }
            if (!VR.enabled || VR.headBodyOffset.x == 180)
            {
                EnableGhostRig();
            }
        }

        private void InitializeGhostRig()
        {
            var rigOB = Instantiate(GorillaTagger.Instance.offlineVRRig.gameObject);
            ghostRig = rigOB.GetComponent<VRRig>();
            Destroy(rigOB.GetComponent<Rigidbody>());
            var material = ghostRig.mainSkin.material;
            material.shader = Shader.Find("GUI/Text Shader");
            material = Globals.MenuColor;
            ghostRig.transform.position = Vector3.zero;
            ghostRig.enabled = false;
            DisableSlideAudio();
        }

        private void ResetGhostRig()
        {
            ghostRig.transform.position = Vector3.zero;
            ghostRig.enabled = false;
        }

        private void EnableGhostRig()
        {
            ghostRig.enabled = true;
            var material = ghostRig.mainSkin.material;
            material.shader = Globals.MenuColor.shader;
            material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
            DisableSlideAudio();
            Mods.Mods.Module();
        }

        public static void DisableSlideAudio()
        {
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
            GameObject.Find("SlideAudio").SetActive(false);
        }
    }
}
