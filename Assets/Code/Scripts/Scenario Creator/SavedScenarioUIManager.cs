using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScenarioEditor
{
    public class SavedScenarioUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _fileLocationBar;
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private SavedScenarioTileManager[] _tabPages;


        private bool _isOpened = false;

        private void Start()
        {
            _fileLocationBar.text = _tabPages[0].GetFullSavePath();
            _tabPages[0].gameObject.SetActive(true);
        }

        public void SelectTab(SavedScenarioTileManager pActiveTab)
        {
            for (int i = 0; i < _tabPages.Length; i++)
            {
                _tabPages[i].gameObject.SetActive(false);
            }
            pActiveTab.gameObject.SetActive(true);

            _fileLocationBar.text = pActiveTab.GetFullSavePath();
        }

        public void OpenScenarioUI()
        {
            if(_isOpened) { return; }
            _animator.SetTrigger("Toggle");
            _isOpened = true;
        }

        public void CloseScenarioUI()
        {
            if(!_isOpened) { return; }
            _animator.SetTrigger("Toggle");
            _isOpened = false;
        }
    }
}
