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
        protected EndConnectionPoint _endPoint = null;
        [SerializeField]
        protected List<StartConnectionPoint> _startPoints = new List<StartConnectionPoint>();

        public bool HasMaxConnections()
        {
            return (_startPoints.Count >= _maxOutgoingConnections);
        }

        public void AddStartPoint(StartConnectionPoint pPoint)
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

        public void RemoveStartPoint(StartConnectionPoint pPoint)
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

        public List<StartConnectionPoint> GetStartPoints()
        {
            return _startPoints;
        }

        public StartConnectionPoint GetLastStartPoint()
        {
            return _startPoints.Last();
        }

        public EndConnectionPoint GetEndPoint()
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
                Debug.Log("Do we already have unconnected points? " + hasUnconnected);

                StartConnectionPoint p = _startPoints[i];
                if (hasUnconnected)
                {
                    Debug.Log("Previous point was unconnected, Removing next point index["+i+"] which is connected? : " + p.IsConnected());
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

            for(int i = 0; i < _startPoints.Count; i++)
            {
                _startPoints[i].RecalculateConnection();
            }
        }

        public void DestroyNode()
        {
            Destroy(gameObject);
        }
    }
}
