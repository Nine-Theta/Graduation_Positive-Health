using System;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

namespace ScenarioEditor
{
    public struct NodeID
    {
        public readonly char NodeType;
        public readonly uint NodeNumber;

        public NodeID(char pNodeType, uint pNodeNumber)
        {
            NodeType = pNodeType;
            NodeNumber = pNodeNumber;
        }

        public string GetIDString()
        {
            return NodeType + NodeNumber.ToString();
        }
    }

    public abstract class AbstractNode : MonoBehaviour
    {
        // protected AbstractNode _parent; //What lead to this node
        // protected List<AbstractNode> _connections; //All possible Nodes that this one can flow to

        protected NodeID NodeID;

        public virtual NodeID GetNodeID() { return  NodeID; }

        /*
        public virtual AbstractNode GetParentNode()
        {
            return null;// _parent;
        }

        public virtual void SetParentNode(AbstractNode pConnection)
        {
            //_parent = null;// pConnection;
        }

        public virtual void AddConnection(AbstractNode pConnection)
        {
            //TODO
            throw new NotImplementedException();
        }

        public virtual void RemoveConnection(AbstractNode pConnection)
        {
            //TODO
            throw new NotImplementedException();
        }

        public virtual List<AbstractNode> GetConnections()
        {
            //TODO
            throw new NotImplementedException();
        }*/
    }
}
