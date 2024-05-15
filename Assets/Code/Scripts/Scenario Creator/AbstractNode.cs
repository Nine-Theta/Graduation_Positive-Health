using System;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

namespace ScenarioEditor
{
    [Serializable]
    public abstract class AbstractNode : MonoBehaviour
    {
        // protected AbstractNode _parent; //What lead to this node
        // protected List<AbstractNode> _connections; //All possible Nodes that this one can flow to


       // protected NodeType Type;

        //protected string Dialogue;

       // protected AbstractNode ConnectedNodes;

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
