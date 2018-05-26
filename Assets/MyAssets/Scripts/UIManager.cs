using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ChangeCountLabel;
    public TextMeshProUGUI SplitPowerUpCountLabel;
    TextMeshProUGUI _levelLabel;

    List<GameObject> _coloredLabelsGameObjects;
    List<Text> _coloredLabels;

    [Header("Color Change Count Image")]
    public Sprite ColorChanges;
    public Image ColorChangeCountImage;


    [Header("Death Menu")]
    public GameObject deathPanel;

    void Start()
    {
        _coloredLabels = new List<Text>();

        _coloredLabelsGameObjects = GameObject.FindGameObjectsWithTag(ColorJumperConstants.COLORED_LABEL).ToList();
        GameObject labelGameObject = GameObject.FindGameObjectsWithTag(ColorJumperConstants.LEVEL_LABEL).First();
        _levelLabel = labelGameObject.GetComponent<TextMeshProUGUI>();

        _levelLabel.text = SceneManager.GetActiveScene().name;
        
        foreach (GameObject go in _coloredLabelsGameObjects)
        {
            _coloredLabels.Add(go.GetComponent("Text") as Text);
        }

        UpdateChangeCount(0);
    }

    public void ApplyColorChange(Color color, int colorMode)
    {
        ChangeLabels(color, colorMode);
    }

    void ChangeLabels(Color color, int colorMode)
    {
        //float r = color.r, g = color.g, b = color.b, a = color.a;

        //// switch colors for labels
        //foreach (Text coloredLabel in _coloredLabels)
        //{
        //    coloredLabel.color= new Color(r, g, b, a);
        //    coloredLabel.gameObject.SetActive(true);
        //}

        // set the roation depending on the colorMode (1=dark color, 2=light color) 
        // --> therefore the Y roation is either 0 or 180
        ColorChangeCountImage.transform.localEulerAngles = new Vector3(colorMode == 2 ? 0 : 180, 0, 0);
    }

    public void UpdateChangeCount(int count)
    {
        ChangeCountLabel.text = count.ToString();
    }

    public void UpdateSplitPowerUpCount(int count)
    {
        SplitPowerUpCountLabel.text = count.ToString();
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
