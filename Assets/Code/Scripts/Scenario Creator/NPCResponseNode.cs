using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class NPCResponseNode : AbstractNode
    {

        [SerializeField]
        private string _responseDialogue;

        private NPCEmotionState _emotionState;

        [SerializeField, ReadOnly]
        private List<ChoiceNode> _linkingChoices = new List<ChoiceNode>();
        [SerializeField]
        private ChoiceGroupNode _linkedChoiceGroup;

        public string GetDialogue()
        {
            return _responseDialogue;
        }

        #region Node linking
        public void SetDialogue(string pDialogue)
        {
            _responseDialogue = pDialogue;
        }

        public ChoiceGroupNode GetChoiceGroup()
        {
            return _linkedChoiceGroup;
        }

        public void SetChoiceGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (_linkedChoiceGroup != null)
            {
                _linkedChoiceGroup.UnlinkResponse(this);
            }
            _linkedChoiceGroup = pChoiceGroup;
            pChoiceGroup.LinkResponse(this);
        }

        public List<ChoiceNode> GetChoices()
        {
            return _linkingChoices;
        }

        public void LinkChoice(ChoiceNode pChoice)
        {
            if (_linkingChoices.Contains(pChoice))
                return;

            _linkingChoices.Add(pChoice);
            pChoice.LinkResponse(this);
        }

        public void UnlinkChoice(ChoiceNode pChoice)
        {
            if (!_linkingChoices.Contains(pChoice))
                return;

            _linkingChoices.Remove(pChoice);
            pChoice.UnlinkResponse(this);
        }
        #endregion

       
    }
}
