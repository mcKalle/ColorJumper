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

    List<GameObject> _coloredLabelsGameObjects;
    List<Text> _coloredLabels;

    [Header("Color Change Count Image")]
    public Sprite ColorChangesYellow;
    public Sprite ColorChangesBlue;
    public Image ColorChangeCountImage;

    void Start()
    {
        Instance = this;

        _coloredLabels = new List<Text>();

        _coloredLabelsGameObjects = GameObject.FindGameObjectsWithTag(ColorJumperConstants.COLORED_LABEL).ToList();

        foreach (GameObject go in _coloredLabelsGameObjects)
        {
            _coloredLabels.Add(go.GetComponent("Text") as Text);
        }

        UpdateChangeCount(0);
    }

    public void ApplyColorChange(Color color)
    {
        ChangeLabels(color);
    }

    void ChangeLabels(Color color)
    {
        //float r = color.r, g = color.g, b = color.b, a = color.a;

        //// switch colors for labels
        //foreach (Text coloredLabel in _coloredLabels)
        //{
        //    coloredLabel.color= new Color(r, g, b, a);
        //    coloredLabel.gameObject.SetActive(true);
        //}

        // rotate count icon
        ColorChangeCountImage.transform.Rotate(new Vector3(0,180,0));
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
