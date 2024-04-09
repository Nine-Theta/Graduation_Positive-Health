using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    enum ChoiceAvailability { ALWAYS, CONDITIONAL }
    public class ChoiceNode : AbstractNode
    {
        private ChoiceGroupNode _choiceGroup;

        private string _choiceDialogue;

        private ChoiceAvailability _availability = ChoiceAvailability.ALWAYS;

        private int _conditionTreshold = 1;

        private int _conditionModifier = 1;

        private List<NPCResponseNode> _linkedResponses;



        public List<NPCResponseNode> GetResponses()
        {
            return _linkedResponses;
        }

        public void LinkResponse(NPCResponseNode pResponse)
        {
            if(_linkedResponses.Contains(pResponse))
                return;

            _linkedResponses.Add(pResponse);
            pResponse.LinkChoice(this);
        }

        public void UnlinkResponse(NPCResponseNode pResponse)
        {
            if (!_linkedResponses.Contains(pResponse))
                return;

            _linkedResponses.Remove(pResponse);
            pResponse.UnlinkChoice(this);
        }


        public ChoiceGroupNode GetChoiceGroup()
        {
            return _choiceGroup;
        }

        public void SetChoiceGroup(ChoiceGroupNode pChoiceGroup)
        {
            _choiceGroup.RemoveChoice(this);
            _choiceGroup = pChoiceGroup;
            pChoiceGroup.AddChoice(this);
        }
    }
}
