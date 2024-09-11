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
                _heldChoice.LinkParentNode(pChoiceGroup);
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
                _heldResponse.LinkParentNode(pChoice);
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
                _heldGroup.LinkParentNode(pResponse);
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
                _heldResponse.LinkChildNode(pChoiceGroup);
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
                _heldGroup.LinkChildNode(pChoice);
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
                _heldChoice.LinkChildNode(pResponse);
                ClearSelections();
            }
            else
            {
                ClearSelections();
                _heldResponse = pResponse;
                _hasEnd = true;
            }
        }

        //Should probably be done in a better way
        public void DestroyAllNodes()
        {
            foreach(AbstractNode node in FindObjectsOfType(typeof(AbstractNode)))
            {
                if (node.name.Contains("(Clone)"))
                {
                    Destroy(node.gameObject);
                }
            }
        }


        public void ClearConnection()
        {
            //TODO
            Debug.LogWarning("Clearing Connections is not implemented yet");
        }
    }
}
