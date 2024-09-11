using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public interface I_QuickSetNode<SERIALIZEDNODE> where SERIALIZEDNODE : I_SerializedNode
    {
        public void QuickSetValues(SERIALIZEDNODE pSerializedNode);
    }
}
