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
    public abstract class AbstractNode : MonoBehaviour
    {
        //TODO: keep track of available connections, probably limit connections as well
        [Header("Base Class"), Space(5)]

        [SerializeField]
        private int _maxOutgoingConnections = 5;


        [SerializeField]
        protected ConnectionPoint _endPoint = null;
        [SerializeField]
        protected List<ConnectionPoint> _startPoints = new List<ConnectionPoint>();

        public bool HasMaxConnections()
        {
            return (_startPoints.Count >= _maxOutgoingConnections);
        }

        public void AddStartPoint(ConnectionPoint pPoint)
        {
            if (_startPoints.Contains(pPoint))
            {
                Debug.LogError("_startPoints already contains this Point, returning");
                return;
            }

            if(_startPoints.Count >= _maxOutgoingConnections)
            {
                Debug.Log("Maximum amount of connections Reached, Destorying new Point");
                Destroy(pPoint.gameObject);
                return;
            }

            _startPoints.Add(pPoint);
            ReorderStartPoints();
        }

        public void RemoveStartPoint(ConnectionPoint pPoint)
        {
            if (!_startPoints.Contains(pPoint))
            {
                Debug.LogError("_startPoints does not contain this Point, returning");
                return;
            }

            _startPoints.Remove(pPoint);
            Destroy(pPoint.gameObject);
            ReorderStartPoints();
        }

        public List<ConnectionPoint> GetStartPoints()
        {
            return _startPoints;
        }

        public ConnectionPoint GetLastStartPoint()
        {
            return _startPoints.Last();
        }

        public ConnectionPoint GetEndPoint()
        {
            return _endPoint;
        }

        private void ReorderStartPoints()
        {
            if (_startPoints.Count == 1)
            {
                _startPoints.Last().RecalculateConnection();
                return;
            }

            _startPoints.Sort();
            bool hasUnconnected = false;

            Debug.Log("total points: "+_startPoints.Count);

            for (int i = 0; i < _startPoints.Count; i++)
            {
                ConnectionPoint p = _startPoints[i];
                if (hasUnconnected)
                {
                    Debug.Log("Empty connection found, Removing next point index["+i+"] which is connected? : " + p.IsConnected());
                    _startPoints.Remove(p);
                    Destroy(p.gameObject);
                    continue;
                }

                Debug.Log("No connecton found");
                hasUnconnected = !p.IsConnected();

                if (hasUnconnected)
                    Debug.Log("Unconnected Point found at index: "+i);
            }

            for (int i = 0; i < _startPoints.Count; i++)
            {
                _startPoints[i].RecalculateConnection();
            }
        }



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
