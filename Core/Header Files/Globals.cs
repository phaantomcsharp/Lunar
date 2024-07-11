using UnityEngine;
using static Stealth.Menu.Base;

namespace Mods
{
    internal class Globals
    {
        public static void ReturnHome()
        {
            buttonsType = 0;
        }

        public static float timer;
        public static string SavedRoom;

        public static int[] integers = new int[]
        {
             4, 3, 5, 4, 19, 18, 20, 19, 3, 18, 21, 20, 22, 21, 25, 21, 29, 21, 31, 29, 27, 25, 24, 22, 6, 5, 7, 6, 10, 6, 14, 6, 16, 14, 12, 10, 9, 7

        };
        public static string[] names = new string[]
        {
            "SIGMA",
            "FORTNITE",
            "COD",
            "MYSTIC",
            "CHEVY",
            "GTAG",
            "2024"
        };

        public static bool isRigEnabled = false;
        public static bool enableboxwskeleton = false;
        public static bool arraylist = false;
        public static float toggleTimer;
        public static bool rightHanded = false;
        public static GameObject MenuObj;
        public static bool antibandone = false;
        public static GameObject canvasObj = null;
        public static GameObject DrawObj;
        public static int timeofday = 0;
        public static GameObject reference = null;
        public static int framePressCooldown = 0;
        public static int btnCooldown;
        public static int Tabindex = 0;
        public static int buttonsPerPage = 5;
        public static int pageNumber = 0;
        public static float AddedFloat = 0;
        public static bool sliphand = GorillaTagger.Instance.offlineVRRig.rightHandTransform;
        public static float AddedFloat2 = 0;
        public static float timerrr = 0;
        public static Material MenuColor = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material BtnDisabledColor = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material BtnEnabledColor = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material Next = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material Previous = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material MenuPointer = new Material(Shader.Find("GUI/Text Shader"));
        public static Material ColorChanger = new Material(Shader.Find("GorillaTag/UberShader"));
        public static Material Uber = new Material(Shader.Find("GorillaTag/UberShader"));
        public static bool ShowPointer = true;
        public static bool isColorChangerActive = true;
        public static bool Tpthreshold = false;
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject lineObjectRight;
        public static GameObject lineObjectLeft;
        public static GameObject canvasObject;
        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static int buttonsType = 0;
    }
}
