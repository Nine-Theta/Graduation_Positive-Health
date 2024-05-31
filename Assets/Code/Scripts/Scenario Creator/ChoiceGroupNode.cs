using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceGroupNode : AbstractNode
    {
        [HorizontalLine(color: EColor.Orange)]

        [SerializeField]
        private List<NPCResponseNode> _linkingResponses = new List<NPCResponseNode>();

        [SerializeField]
        private List<ChoiceNode> _choicesNodes = new List<ChoiceNode>();

        #region Node linking
        public List<NPCResponseNode> GetLinkingResponses()
        {
            return _linkingResponses;
        }

        public void LinkResponse(NPCResponseNode pResponse)
        {
            if (_linkingResponses.Contains(pResponse))
                return;

            _linkingResponses.Add(pResponse);
            pResponse.SetChoiceGroup(this);
        }

        public void UnlinkResponse(NPCResponseNode pResponse)
        {
            if (!_linkingResponses.Contains(pResponse))
                return;

            _linkingResponses.Remove(pResponse);
            pResponse.SetChoiceGroup(null);
        }


        public List<ChoiceNode> GetChoices()
        {
            return _choicesNodes;
        }

        public void AddChoice(ChoiceNode pChoice)
        {
            if (_choicesNodes.Contains(pChoice))
                return;

            _choicesNodes.Add(pChoice);
            pChoice.SetChoiceGroup(this);
        }

        public void RemoveChoice(ChoiceNode pChoice)
        {
            if (!_choicesNodes.Contains(pChoice))
                return;

            _choicesNodes.Remove(pChoice);
            pChoice.SetChoiceGroup(null);
        }
        #endregion

    }
}


