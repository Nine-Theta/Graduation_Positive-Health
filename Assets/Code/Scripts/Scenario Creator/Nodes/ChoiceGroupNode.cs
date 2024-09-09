using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceGroupNode : GenericNode<NPCResponseNode,ChoiceNode>, I_QuickSetNode<SerializedChoiceGroup>
    {
        //[HorizontalLine(color: EColor.Green)] //Sadly no parameters, no so colorbar anymore (-_-)

        public void QuickSetValues(SerializedChoiceGroup pSerializedNode)
        {
            Debug.Log("QuickSetValues for GroupNode");
            //Nothing to do yet
        }
    }
}


