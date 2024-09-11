using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ScenarioEditor
{
    [Serializable]
    public abstract class AbstractNode : MonoBehaviour
    {
        [SerializeField, Min(-1), Tooltip("-1 will be treated as limitless")]
        protected int _maxParentNodes = -1;

        [SerializeField, Min(-1), Tooltip("-1 will be treated as limitless")]
        protected int _maxChildNodes = 5;

        [SerializeField]
        protected IncomingConnectionPoint _inPoint = null; //Our point for incoming connections
        [SerializeField]
        protected List<OutgoingConnectionPoint> _outPoints = new List<OutgoingConnectionPoint>(); //Our points for outgoing connections

        public UnityEvent<AbstractNode> OnDisconnectNode = new UnityEvent<AbstractNode>();

        public bool HasMaxConnections()
        {
            return (_outPoints.Count >= _maxChildNodes);
        }

        #region ConnectionPoint Management

        public void AddOutgoingPoint(OutgoingConnectionPoint pPoint)
        {
            if (_outPoints.Contains(pPoint))
            {
                Debug.LogError("_outPoints already contains this Point, returning");
                return;
            }

            if (_outPoints.Count >= _maxChildNodes)
            {
                Debug.Log("Maximum amount of connections Reached, Destorying new Point");
                Destroy(pPoint.gameObject);
                return;
            }

            _outPoints.Add(pPoint);
            ReorderOutgoingPoints();
        }

        public void RemoveOutgoingPoint(OutgoingConnectionPoint pPoint)
        {
            if (!_outPoints.Contains(pPoint))
            {
                Debug.LogError("_outPoints does not contain this Point, returning");
                return;
            }

            _outPoints.Remove(pPoint);
            Destroy(pPoint.gameObject);
            ReorderOutgoingPoints();
        }

        public void ClearConnectionToInPoint(IncomingConnectionPoint pPoint)
        {
            for (int i = 0; i < _outPoints.Count; i++)
            {
                if (_outPoints[i].GetConnectedPoint() == pPoint)
                {
                    _outPoints[i].ClearConnection();
                    ReorderOutgoingPoints();
                    return;
                }
            }
        }

        public List<OutgoingConnectionPoint> GetOutgoingPoints()
        {
            return _outPoints;
        }

        public OutgoingConnectionPoint GetLastOutgoingPoint()
        {
            return _outPoints.Last();
        }

        public IncomingConnectionPoint GetIncomingPoint()
        {
            return _inPoint;
        }

        private void ReorderOutgoingPoints()
        {
            if (_outPoints.Count == 1)
            {
                _outPoints.Last().RecalculateConnection();
                return;
            }

            _outPoints.Sort();
            bool hasUnconnected = false;

            for (int i = 0; i < _outPoints.Count; i++)
            {
                OutgoingConnectionPoint p = _outPoints[i];
                if (hasUnconnected)
                {
                    _outPoints.Remove(p);
                    Destroy(p.gameObject);
                    continue;
                }

                hasUnconnected = !p.IsConnected();
            }

            for (int i = 0; i < _outPoints.Count; i++)
            {
                _outPoints[i].RecalculateConnection();
            }
        }

        public void RecalculateAllPoints()
        {
            if(_inPoint != null)
                _inPoint.RecalculateConnection();

            for (int i = 0; i < _outPoints.Count; i++)
            {
                _outPoints[i].RecalculateConnection();
            }
        }

        #endregion ConnectionPoint Management

        public virtual void DisconnectNode()
        {
           for(int i = 0; i < _outPoints.Count; i++)
            {
                _outPoints[i].ClearConnection();
            }

            OnDisconnectNode?.Invoke(this);
            OnDisconnectNode.RemoveAllListeners();
        }

        public virtual void DestroyNode()
        {
            DisconnectNode();
            Destroy(gameObject);
        }
    }
}
