using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class DialogueNode : AbstractNode
    {
        private string _text; //Dialogue 

        public void SetText(string pText)
        {
            _text = pText;
        }
    }
}
