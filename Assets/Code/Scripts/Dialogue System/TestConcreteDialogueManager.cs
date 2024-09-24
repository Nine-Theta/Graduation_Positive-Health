using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestConcreteDialogueManager : AbstractDialogueManager
{
    [SerializeField]
    private string _scenarioFilename;

    [SerializeField]
    private TextMeshProUGUI _buttonTextOne;
    [SerializeField]
    private TextMeshProUGUI _buttonTextTwo;
    [SerializeField]
    private TextMeshProUGUI _buttonTextThree;

    [SerializeField]
    private TextMeshProUGUI _patientText;

    [SerializeField]
    private TextMeshProUGUI _clipboardText;

    private void Start()
    {
        ImportScenario(_scenarioFilename);       
    }

    public void populateChoices()
    {
        string[] choices = GetNextChoices();

        _buttonTextOne.text = choices[0];
        _buttonTextTwo.text = choices[1];
        _buttonTextThree.text = choices[2];
    }

    public void populatePatientText(int pChoiceMade)
    {
        _patientText.text = GetNextResponse(pChoiceMade);
    }

    public override void ImportScenario(string pFilename)
    {
        base.ImportScenario(pFilename);

        _clipboardText.text = GetScenarioDescription();

        string[] choices = GetStartChoices();
        _buttonTextOne.text = choices[0];
        _buttonTextTwo.text = choices[1];
        _buttonTextThree.text = choices[2];

        _patientText.text = "";
    }

}
