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
            //AddStartPoint(pParent.get)
        }

        public virtual void LinkChildNode(CHILD pChild)
        {
            if ((_maxChildNodes >= 0 && _childNodes.Count > _maxChildNodes) || _childNodes.Contains(pChild))
            {
                Debug.Log("Node[" + name + "] already contains a link to Child[" + pChild.name + "]");
                return;
            }

            _childNodes.Add(pChild);
        }

        public virtual void UnlinkParentNode(PARENT pParent)
        {
            if (_parentNodes.Contains(pParent))
            {
                Debug.Log("removed Parent[" + pParent.name + "] from Node[" + name + "]");
                _parentNodes.Remove(pParent);
            }
        }

        public virtual void UnlinkChildNode(CHILD pChild)
        {
            if (_childNodes.Contains(pChild))
            {
                Debug.Log("removed Child[" + pChild.name + "] from Node[" + name + "]");
                _childNodes.Remove(pChild);
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
    }
}
