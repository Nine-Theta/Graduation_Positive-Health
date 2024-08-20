using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceGroupNode : GenericNode<NPCResponseNode,ChoiceNode>, I_QuickSetNode<SerializedChoiceGroup>
    {
        [HorizontalLine(color: EColor.Green)]

        [SerializeField]
        private List<NPCResponseNode> _linkingResponses = new List<NPCResponseNode>();

        [SerializeField]
        private List<ChoiceNode> _choicesNodes = new List<ChoiceNode>();

        public void QuickSetValues(SerializedChoiceGroup pSerializedNode)
        {
            Debug.Log("QuickSetValues for GroupNode");
            //Nothing to do yet
        }
    }
}


