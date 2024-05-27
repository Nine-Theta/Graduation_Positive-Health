using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    [RequireComponent(typeof(LineRenderer))]
    public class NodeConnector : MonoBehaviour
    {
        [SerializeField]
        private ChoiceGroupNode _heldGroup = null;
        [SerializeField]
        private ChoiceNode _heldChoice = null;
        [SerializeField]
        private NPCResponseNode _heldResponse = null;

        [SerializeField]
        private bool _hasStart = false;

        [SerializeField]
        private ConnectionPoint _heldConnectionA = null;
        [SerializeField]
        private ConnectionPoint _heldConnectionB = null;

        [SerializeField]
        private List<NodeConnection> _nodeConnections = new List<NodeConnection>();

        private LineRenderer _lineRenderer;

        public void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void StartGroupLink(ChoiceGroupNode pChoiceGroup)
        {
            _heldGroup = pChoiceGroup;
            _heldChoice = null;
            _heldResponse = null;

            _hasStart = true;
        }

        public void StartChoiceLink(ChoiceNode pChoice)
        {
            _heldGroup = null;
            _heldChoice = pChoice;
            _heldResponse = null;

            _hasStart = true;
        }

        public void StartResponseLink(NPCResponseNode pResponse)
        {
            _heldGroup = null;
            _heldChoice = null;
            _heldResponse = pResponse;

            _hasStart = true;
        }

        public void ClearSelections()
        {
            _heldGroup = null;
            _heldChoice = null;
            _heldResponse = null;

            _hasStart = false;
        }

        public void OnClick()
        {
            Debug.Log("clicked");
        }

        public void TryLinkResponse(NPCResponseNode pResponse)
        {
            if (!_hasStart) return;

            if (_heldGroup != null) {
                pResponse.SetChoiceGroup(_heldGroup);
                _heldGroup = null;
            }

            if (_heldChoice != null)
            {
                pResponse.LinkChoice(_heldChoice);
                _heldChoice = null;
            }

            _hasStart = false;
        }

        public void TryLinkChoice(ChoiceNode pChoice)
        {
            if (_heldGroup != null)
            {
                pChoice.SetChoiceGroup(_heldGroup);
                _heldGroup = null;
            }

            if (_heldResponse != null)
            {
                pChoice.LinkResponse(_heldResponse);
                _heldResponse = null;
            }
        }

        public void TryLinkGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (_heldChoice != null)
            {
                pChoiceGroup.AddChoice(_heldChoice);
                _heldChoice = null;
            }

            if (_heldResponse != null)
            {
                pChoiceGroup.LinkResponse(_heldResponse);
                _heldResponse = null;
            }
        }

        public void SetConnectionPoint(ConnectionPoint pConnectionPoint)
        {
            if (_heldConnectionA == null)
            {
                _heldConnectionA = pConnectionPoint;
            }
            else if (_heldConnectionB == null)
            {
                _heldConnectionB = pConnectionPoint;
            }
        }
    }
}
