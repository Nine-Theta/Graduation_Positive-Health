using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScenarioEditor
{
    public class SavedScenarioUITile : MonoBehaviour
    {  
        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI;

        [ShowNonSerializedField]
        private string _filename = "";
        [ShowNonSerializedField]
        private string _filepath = "";
        [ShowNonSerializedField]
        private SavedScenarioTileManager _tileManager;

        public string Filepath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        public string GetFilename()
        { 
            return _filename;
        }

        public void SetFilename(string pFilename)
        {
            _filename = pFilename;
            _textMeshProUGUI.text = _filename.Split('.')[0];
        }

        public void SetTileManager(SavedScenarioTileManager pTileManager)
        {
            _tileManager = pTileManager;
        }

        public void LoadThisScenario()
        {
            _tileManager.ImportScenario(this);
        }
    }
}
