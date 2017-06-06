using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public Text ChangeCountLabel;

    List<GameObject> coloredLabelsGameObjects;
    List<Text> coloredLabels;

    [Header("Color Change Count Image")]
    public Sprite ColorChangesYellow;
    public Sprite ColorChangesBlue;
    public Image ColorChangeCountImage;

    void Start()
    {
        Instance = this;

        coloredLabels = new List<Text>();

        coloredLabelsGameObjects = GameObject.FindGameObjectsWithTag(ColorJumperConstants.COLORED_LABEL).ToList();

        foreach (GameObject go in coloredLabelsGameObjects)
        {
            coloredLabels.Add(go.GetComponent("Text") as Text);
        }

        UpdateChangeCount(0);
    }

    public void ApplyColorChange(ColorEnum color)
    {
        switch (color)
        {
            case ColorEnum.Blue:
                ChangeLabels(ColorJumperConstants.COLOR_BLUE_5);
                ColorChangeCountImage.sprite = ColorChangesBlue;
                break;
            case ColorEnum.Yellow:
                ChangeLabels(ColorJumperConstants.COLOR_YELLOW_5);
                ColorChangeCountImage.sprite = ColorChangesYellow;
                break;
            default:
                break;
        }
    }

    void ChangeLabels(string colorCode)
    {
        Color c;

        if (ColorUtility.TryParseHtmlString(colorCode, out c))
        {
            foreach (Text coloredLabel in coloredLabels)
            {
                coloredLabel.color = c;
            }
        }
    }

    public void UpdateChangeCount(int count)
    {
        ChangeCountLabel.text = count.ToString();
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
