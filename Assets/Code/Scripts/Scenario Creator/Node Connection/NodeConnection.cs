using ScenarioEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ScenarioEditor
{
    [Serializable]
    public struct NodeConnection
    {
        public AbstractNode NodeA;
        public ConnectionPoint ConnectionA;

        public AbstractNode NodeB;
        public ConnectionPoint ConnectionB;

        public NodeConnection(AbstractNode pNodeA, ConnectionPoint pConnectionA, AbstractNode pNodeB, ConnectionPoint pConnectionB)
        {
            NodeA = pNodeA;
            ConnectionA = pConnectionA;

            NodeB = pNodeB;
            ConnectionB = pConnectionB;
        }
    }
}
