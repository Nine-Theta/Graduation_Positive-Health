using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public abstract class AbstractNode : MonoBehaviour
    {
        protected NodeConnection _parent; //What lead to this node
        protected List<NodeConnection> _connections; //All possible Nodes that this one can flow to

        public virtual NodeConnection GetParentNode()
        {
            return _parent;
        }

        public virtual void SetParentNode(NodeConnection pConnection)
        {
            _parent = pConnection;
        }

        public virtual void AddConnection(NodeConnection pConnection)
        {
            //TODO
            throw new NotImplementedException();
        }

        public virtual void RemoveConnection(NodeConnection pConnection)
        {
            //TODO
            throw new NotImplementedException();
        }

        public virtual List<NodeConnection> GetConnections()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
