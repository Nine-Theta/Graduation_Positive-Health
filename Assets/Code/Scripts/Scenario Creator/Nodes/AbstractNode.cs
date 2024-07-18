using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        protected IncomingConnectionPoint _endPoint = null;
        [SerializeField]
        protected List<OutgoingConnectionPoint> _startPoints = new List<OutgoingConnectionPoint>();

        public bool HasMaxConnections()
        {
            return (_startPoints.Count >= _maxChildNodes);
        }

        #region ConnectionPoint Management

        public void AddStartPoint(OutgoingConnectionPoint pPoint)
        {
            if (_startPoints.Contains(pPoint))
            {
                Debug.LogError("_startPoints already contains this Point, returning");
                return;
            }

            if (_startPoints.Count >= _maxChildNodes)
            {
                Debug.Log("Maximum amount of connections Reached, Destorying new Point");
                Destroy(pPoint.gameObject);
                return;
            }

            _startPoints.Add(pPoint);
            ReorderStartPoints();
        }

        public void RemoveStartPoint(OutgoingConnectionPoint pPoint)
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

        public List<OutgoingConnectionPoint> GetStartPoints()
        {
            return _startPoints;
        }

        public OutgoingConnectionPoint GetLastStartPoint()
        {
            return _startPoints.Last();
        }

        public IncomingConnectionPoint GetEndPoint()
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

            Debug.Log("total points: " + _startPoints.Count);

            for (int i = 0; i < _startPoints.Count; i++)
            {
                Debug.Log("Do we already have unconnected points? " + hasUnconnected);

                OutgoingConnectionPoint p = _startPoints[i];
                if (hasUnconnected)
                {
                    Debug.Log("Previous point was unconnected, Removing next point index[" + i + "] which is connected? : " + p.IsConnected());
                    _startPoints.Remove(p);
                    Destroy(p.gameObject);
                    continue;
                }


                if (!p.IsConnected())
                    Debug.Log("Current point is Unconnected, found at index: " + i);

                hasUnconnected = !p.IsConnected();
            }

            for (int i = 0; i < _startPoints.Count; i++)
            {
                _startPoints[i].RecalculateConnection();
            }
        }

        public void RecalculateAllPoints()
        {
            _endPoint.RecalculateConnection();

            for (int i = 0; i < _startPoints.Count; i++)
            {
                _startPoints[i].RecalculateConnection();
            }
        }

        #endregion ConnectionPoint Management

        public virtual void DestroyNode()
        {
            Destroy(gameObject);
        }
    }
}
