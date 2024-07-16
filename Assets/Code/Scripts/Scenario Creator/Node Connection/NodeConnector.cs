using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
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
        private bool _hasEnd = false;

        public void SetStartGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (_hasEnd && _heldChoice != null)
            {
                pChoiceGroup.LinkChildNode(_heldChoice);
                pChoiceGroup.GetLastStartPoint().MakeConnection(_heldChoice.GetEndPoint());
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldGroup = pChoiceGroup;
                _hasStart = true;
            }
        }

        public void SetStartChoice(ChoiceNode pChoice)
        {
            if (_hasEnd && _heldResponse != null)
            {
                pChoice.LinkChildNode(_heldResponse);
                pChoice.GetLastStartPoint().MakeConnection(_heldResponse.GetEndPoint());
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldChoice = pChoice;
                _hasStart = true;
            }
        }

        public void SetStartResponse(NPCResponseNode pResponse)
        {
            if (_hasEnd && _heldGroup != null)
            {
                pResponse.LinkChildNode(_heldGroup);
                pResponse.GetLastStartPoint().MakeConnection(_heldGroup.GetEndPoint());
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldResponse = pResponse;
                _hasStart = true;
            }
        }

        public void ClearSelections()
        {
            _heldGroup = null;
            _heldChoice = null;
            _heldResponse = null;

            _hasStart = false;
            _hasEnd = false;
        }

        public void SetEndGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (_hasStart && _heldResponse != null)
            {
                pChoiceGroup.LinkParentNode(_heldResponse);
                _heldResponse.GetLastStartPoint().MakeConnection(pChoiceGroup.GetEndPoint());
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldGroup = pChoiceGroup;
                _hasEnd = true;
            }
        }

        public void SetEndChoice(ChoiceNode pChoice)
        {
            if (_hasStart && _heldGroup != null)
            {
                pChoice.LinkParentNode(_heldGroup);
                _heldGroup.GetLastStartPoint().MakeConnection(pChoice.GetEndPoint());
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldChoice = pChoice;
                _hasEnd = true;
            }           
        }

        public void SetEndResponse(NPCResponseNode pResponse)
        {
            if (_hasStart && _heldChoice != null)
            {
                pResponse.LinkParentNode(_heldChoice);
                _heldChoice.GetLastStartPoint().MakeConnection(pResponse.GetEndPoint());
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldResponse = pResponse;
                _hasEnd = true;
            }
        }


        public void ClearConnection()
        {
            //TODO
            Debug.LogWarning("Clearing Connections is not implemented yet");
        }
    }
}
