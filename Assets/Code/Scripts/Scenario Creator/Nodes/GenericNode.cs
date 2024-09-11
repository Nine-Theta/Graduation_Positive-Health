using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.Rendering;

namespace ScenarioEditor
{
    [Serializable]
    public class GenericNode<PARENT, CHILD> : AbstractNode where PARENT : AbstractNode where CHILD : AbstractNode 
    {
        [Header("Base Class"), Space(5)]

        [SerializeField, ReadOnly]
        protected List<PARENT> _parentNodes = new List<PARENT>();
        [SerializeField, ReadOnly]
        protected List<CHILD> _childNodes = new List<CHILD>();


        public virtual void LinkParentNode(PARENT pParent)
        {
            if ((_maxParentNodes >= 0 && _parentNodes.Count > _maxParentNodes) || _parentNodes.Contains(pParent))
            {
                Debug.Log("Node[" + name + "] already contains a link to Parent[" + pParent.name + "]");
                return;
            }

            _parentNodes.Add(pParent);
            pParent.OnDisconnectNode.AddListener(UnlinkParentNode);
        }

        public virtual void LinkChildNode(CHILD pChild)
        {
            if ((_maxChildNodes >= 0 && _childNodes.Count > _maxChildNodes) || _childNodes.Contains(pChild))
            {
                Debug.Log("Node[" + name + "] already contains a link to Child[" + pChild.name + "]");
                return;
            }

            _childNodes.Add(pChild);
            GetLastOutgoingPoint().MakeConnection(pChild.GetIncomingPoint());

            pChild.OnDisconnectNode.AddListener(UnlinkChildNode);            
        }

        private void UnlinkParentNode(AbstractNode pNode)
        {
            if (pNode is PARENT)
                UnlinkParentNode(pNode as PARENT);
        }

        public virtual void UnlinkParentNode(PARENT pParent)
        {
            if (_parentNodes.Contains(pParent))
            {
                Debug.Log("removed Parent[" + pParent.name + "] from Node[" + name + "]");
                _parentNodes.Remove(pParent);
            }
        }

        private void UnlinkChildNode(AbstractNode pNode)
        {
            if (pNode is CHILD)
                UnlinkChildNode(pNode as CHILD);
        }

        public virtual void UnlinkChildNode(CHILD pChild)
        {
            if (_childNodes.Contains(pChild))
            {
                Debug.Log("removed Child[" + pChild.name + "] from Node[" + name + "]");
                _childNodes.Remove(pChild);
                ClearConnectionToInPoint(pChild.GetIncomingPoint());
            }
        }

        public PARENT[] GetParentNodes()
        {
            return _parentNodes.ToArray();
        }

        public CHILD[] GetChildNodes()
        {
            return _childNodes.ToArray();
        }
        public virtual Vector2 GetNodePosition()
        {
            return (Vector2)(transform.position);
        }

        public override void DisconnectNode()
        {
            for (int i = 0; i < _parentNodes.Count; i++)
            {
                Debug.Log("Disconnecting parent: " + _parentNodes[i]);
                UnlinkParentNode(_parentNodes[i]);
            }

            for (int i = 0; i < _childNodes.Count; i++)
            {
                Debug.Log("Disconnecting child: " + _childNodes[i]);
                UnlinkChildNode(_childNodes[i]);
            }

            base.DisconnectNode();
        }

        public override void DestroyNode()
        {
            DisconnectNode();
            base.DestroyNode();
        }        
    }
}
