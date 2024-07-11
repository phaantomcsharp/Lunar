using Stealth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stealth.Handles.Button_Handles
{
    public class categorytemplate
    {
        public string Name;
        public List<buttontemplate> Buttons;

        public categorytemplate(string name)
        {
            Name = name;
            Buttons = new List<buttontemplate>();
        }

      
    }
}
