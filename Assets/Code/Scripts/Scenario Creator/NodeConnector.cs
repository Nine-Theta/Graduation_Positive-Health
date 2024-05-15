using UnityEngine;

namespace ScenarioEditor
{
    public class NodeConnector : MonoBehaviour
    {
        //prototype code, should be changed
        public ChoiceGroupNode heldGroup = null;
        public ChoiceNode heldChoice = null;
        public NPCResponseNode heldResponse = null;

        public bool HasStart = false;

        public void ChoiceGroupClicked(ChoiceGroupNode pChoiceGroup)
        {
            if (HasStart)
            {
                TryLinkGroup(pChoiceGroup);
                HasStart = false;
            }
            else
            {
                heldGroup = pChoiceGroup;
                HasStart = true;
            }
        }

        public void ChoiceClicked(ChoiceNode pChoice)
        {
            if (HasStart)
            {
                TryLinkChoice(pChoice);
                HasStart = false;
            }
            else
            {
                heldChoice = pChoice;
                HasStart = true;
            }
        }

        public void NPCResponseClicked(NPCResponseNode pResponse)
        {
            if (HasStart)
            {
                TryLinkNPCResponse(pResponse);
                HasStart = false;
            }
            else
            {
                heldResponse = pResponse;
                HasStart = true;
            }
        }

        public void TryLinkNPCResponse(NPCResponseNode pResponse)
        {
            if (heldGroup != null) {
                pResponse.SetChoiceGroup(heldGroup);
                heldGroup = null;
            }

            if (heldChoice != null)
            {
                pResponse.LinkChoice(heldChoice);
                heldChoice = null;
            }
        }

        public void TryLinkChoice(ChoiceNode pChoice)
        {
            if (heldGroup != null)
            {
                pChoice.SetChoiceGroup(heldGroup);
                heldGroup = null;
            }

            if (heldResponse != null)
            {
                pChoice.LinkResponse(heldResponse);
                heldResponse = null;
            }
        }

        public void TryLinkGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (heldChoice != null)
            {
                pChoiceGroup.AddChoice(heldChoice);
                heldChoice = null;
            }

            if (heldResponse != null)
            {
                pChoiceGroup.LinkResponse(heldResponse);
                heldResponse = null;
            }
        }

        /*
        The connected nodes
        protected AbstractNode _previousNode;
        protected AbstractNode _nextNode;

        public ChoiceNode f;

        private bool _traversable = true; //Determines if the next node can be accessed

        public bool traversable { get { return _traversable; } set { _traversable = value; } }

        public void SetParentConnection(AbstractNode pNode)
        {
            _previousNode = pNode;
        }

        public void SetChildConnection(AbstractNode pNode)
        {
            _nextNode = pNode;
        }*/
    }
}
