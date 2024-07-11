using System;
using System.Collections.Generic;
using System.Text;
using Valve.VR;

namespace Stealth
{
    internal class Inputs
    {
        public static bool RT() { return instance().rightControllerIndexFloat == 1f; }
        public static bool LT() { return instance().leftControllerIndexFloat == 1f; }
        public static bool RG() { return instance().rightControllerGripFloat == 1f; }
        public static bool LG() { return instance().leftControllerGripFloat == 1f; }
        public static bool X() { return instance().leftControllerPrimaryButton; }
        public static bool Y() { return instance().leftControllerSecondaryButton; }
        public static bool B() { return instance().rightControllerSecondaryButton; }
        public static bool A() { return instance().rightControllerPrimaryButton; }
        public static bool RightJoystickDown() { return SteamVR_Actions.gorillaTag_RightJoystickClick.state; }
        public static bool LeftJoyStickDown() { return SteamVR_Actions.gorillaTag_LeftJoystickClick.state; }

        private static ControllerInputPoller instance()
        {
            return ControllerInputPoller.instance;
        }

    }
}