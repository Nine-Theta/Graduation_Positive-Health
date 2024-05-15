using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceNode : AbstractNode
    {
        [SerializeField]
        private ChoiceGroupNode _choiceGroup;

        [SerializeField]
        private string _choiceDialogue;

        [SerializeField]
        private ChoiceAvailability _availability = ChoiceAvailability.ALWAYS;

        [SerializeField]
        private int _unlockIndex = -1;
        [SerializeField]
        private int _unlockTreshold = 0;

        [SerializeField]
        private int _conditionIndex = -1;
        [SerializeField]
        private int _conditionModifier = 0;

        [SerializeField]
        private List<NPCResponseNode> _linkedResponses = new List<NPCResponseNode>();

        public string GetDialogue()
        {
            return _choiceDialogue;
        }

        public void SetDialogue(string pDialogue)
        {
            _choiceDialogue = pDialogue;
        }

        public bool IsChoiceConditional
        {
            get
            {
                if (_availability == ChoiceAvailability.ALWAYS)
                    return false;
                else
                    return true;
            }

            set { _availability = value ? ChoiceAvailability.CONDITIONAL : ChoiceAvailability.ALWAYS; }
        }

        public ChoiceAvailability GetChoiceAvailability()
        {
            return _availability;
        }

        public int ConditionThreshold
        {
            get { return _unlockTreshold; }
            set { _unlockTreshold = value; }
        }

        public int ConditionModifier
        {
            get { return _conditionModifier; }
            set { _conditionModifier = value; }
        }

        #region Node linking
        public List<NPCResponseNode> GetResponses()
        {
            return _linkedResponses;
        }

        public void LinkResponse(NPCResponseNode pResponse)
        {
            if (_linkedResponses.Contains(pResponse))
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
            if (pChoiceGroup == null)
            {
                _choiceGroup = null;
                return;
            }

            Debug.Log("is GC null? " + (_choiceGroup == null).ToString());
            Debug.Log("is pGC null? " + (pChoiceGroup == null).ToString());
            Debug.Log("is C null? " + (this == null).ToString());

            if (_choiceGroup != null)
            {
                _choiceGroup.RemoveChoice(this);
            }

            _choiceGroup = pChoiceGroup;
            pChoiceGroup.AddChoice(this);

        }
        #endregion
    }
}
