using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TMPro;
using UnityEngine;

namespace ScenarioEditor
{
    public class SavedScenarioTileManager : MonoBehaviour
    {
        [SerializeField]
        private ImportedScenarioConstructor _scenarioImporter;

        [SerializeField]
        private GameObject _tilePrefab;

        [SerializeField]
        private string _savedScenariosPath = "/JsonScenarios";

        [SerializeField]
        private SavedScenarioUIManager _uiManager;

        private void Start()
        {
            DirectoryInfo DirInfo = new DirectoryInfo(GetFullSavePath());

            IEnumerable<FileInfo> files = DirInfo.EnumerateFiles();

            foreach (FileInfo file in files)
            {
                Debug.Log("Existing savefile found: " + file.Name + " : " + file.FullName);
                SavedScenarioUITile uiTile = Instantiate(_tilePrefab, transform).GetComponent<SavedScenarioUITile>();
                uiTile.SetFilename(file.Name);
                uiTile.Filepath = file.FullName;
                uiTile.SetTileManager(this);
            }
        }

        public string GetFullSavePath()
        {
            return Application.persistentDataPath + _savedScenariosPath;
        }

        public void ImportScenario(SavedScenarioUITile pTile)
        {
            _uiManager.CloseScenarioUI();
            _scenarioImporter.ImportScenarioFromFile(pTile.GetFilename());
        }
    }
}
