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

    [SerializeField]
    private Vector2 _nodeSpacing = new Vector2(140, 100);

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
            },
            Vector2.zero);


        float thirdHoriSpace = _nodeSpacing.x/3f;


        for (int i = 1; i< 28; i += 2)
        {
            if(i == 9 || i == 19)
            {
                continue;
            }

            float x = i%10 * _nodeSpacing.x;
            float y = (1-(i / 10)) * _nodeSpacing.y +1;//+1 needed because unity's being weird about positions

            if (i == 7 || i == 17 || i == 27)
            {
                ScenarioOverwriteFile.choices.Add(new SerializedChoice(NodeType.CHOICE, i, new NodeID[] { new NodeID(NodeType.RESPONSE, i) }, new Vector2(x, y), aiDialogue[i], new ChoiceConditions(-1, 0, -1, 0)));
                ScenarioOverwriteFile.responses.Add(new SerializedResponse(NodeType.RESPONSE, i, NodeID.Empty, new Vector2(x+thirdHoriSpace*2, y), aiDialogue[i+1] + aiDialogue[i+2], NPCEmotionState.NEUTRAL, true));
                ScenarioOverwriteFile.groups.Add(new SerializedChoiceGroup(NodeType.GROUP, i/10+1, new NodeID[] { new NodeID(NodeType.CHOICE, (i/5) + 2), new NodeID(NodeType.CHOICE, (i/5) + 12), new NodeID(NodeType.CHOICE, (i / 5) + 22) }, new Vector2(((i/5+1)*_nodeSpacing.x)+thirdHoriSpace, 0)));
                continue;
            }

            ScenarioOverwriteFile.choices.Add(new SerializedChoice(NodeType.CHOICE, i, new NodeID[] { new NodeID(NodeType.RESPONSE, i) }, new Vector2(x, y), aiDialogue[i], new ChoiceConditions(-1, 0, -1, 0)));
            ScenarioOverwriteFile.responses.Add(new SerializedResponse(NodeType.RESPONSE, i, new NodeID(NodeType.GROUP, ((i%10)/2)+1), new Vector2(x + thirdHoriSpace*2, y), aiDialogue[i + 1], NPCEmotionState.NEUTRAL));
        }

        return ScenarioOverwriteFile;
    }
}
