using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AIScenarioImporter : AbstractScenarioImporter
{
    [ShowNonSerializedField]
    private string _saveFolder = "/AIScenarios/";

    [ShowNonSerializedField]
    private string _fileExtension = ".txt";

    private void Awake()
    {
        _folder = _saveFolder;
        _extension = _fileExtension;
    }


    public override ScriptableExtractedDialogue ImportScenarioFromFile(string pFilepath)
    {

        //throw new System.NotImplementedException("Due to the way AI dialogue is currently generated it is not possible to import from a file it at this time");

        Debug.Log("Import Filepath: " + pFilepath);

        string[] aiScenario = File.ReadAllText(pFilepath).Split('\n');

        string[] aiDialogue = aiScenario[2].Split('|');

        ScenarioOverwriteFile.ClearData();

        ScenarioOverwriteFile.ScenarioName = Path.GetFileName(pFilepath);
        ScenarioOverwriteFile.TargetAudience = aiScenario[0];
        ScenarioOverwriteFile.ScenarioDescription = aiScenario[1];
        ScenarioOverwriteFile.StarterGroup = new SerializedChoiceGroup(
            NodeType.GROUP,
            0,
            new NodeID[] {
                new NodeID(NodeType.CHOICE, 1),
                new NodeID(NodeType.CHOICE, 11),
                new NodeID(NodeType.CHOICE, 21),
            }
            , Vector2.zero);

        float hortSpace = 100f;

        float vertSpace = 120f;

        for (int i = 1; i< 28; i += 2)
        {
            if(i == 9 || i == 19)
            {
                continue;
            }

            if(i == 7 || i == 17 || i == 27)
            {
                ScenarioOverwriteFile.choices.Add(new SerializedChoice(NodeType.CHOICE, i, new NodeID[] { new NodeID(NodeType.RESPONSE, i) }, new Vector2(i%10f * vertSpace, hortSpace * (1-((i/11)-2))), aiDialogue[7], new ChoiceConditions(-1, 0, -1, 0)));
                ScenarioOverwriteFile.responses.Add(new SerializedResponse(NodeType.RESPONSE, i, NodeID.Empty, new Vector2(((i%10f) + 0.667f) * vertSpace, hortSpace * (1-((i / 11) - 2))), aiDialogue[8], NPCEmotionState.NEUTRAL, true));
                continue;
            }

            ScenarioOverwriteFile.choices.Add(new SerializedChoice(NodeType.CHOICE, i, new NodeID[] { new NodeID(NodeType.RESPONSE, i) }, new Vector2(i%10 * vertSpace, hortSpace * (1-((i / 11) - 2))), aiDialogue[i], new ChoiceConditions(-1, 0, -1, 0)));
            ScenarioOverwriteFile.responses.Add(new SerializedResponse(NodeType.RESPONSE, i, new NodeID(NodeType.GROUP, i), new Vector2(((i%10f) + 0.667f) * vertSpace, hortSpace * (1-((i / 11) - 2))), aiDialogue[i + 1], NPCEmotionState.NEUTRAL));
            ScenarioOverwriteFile.groups.Add(new SerializedChoiceGroup(NodeType.GROUP, i, new NodeID[] { new NodeID(NodeType.CHOICE, i + 2) }, new Vector2(((i%10f) + 1.333f) * vertSpace, hortSpace * (1-((i / 11) - 2)))));
        }

        return ScenarioOverwriteFile;
    }
}
