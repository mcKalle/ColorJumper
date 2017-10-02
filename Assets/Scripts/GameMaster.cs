using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public Camera MainCamera;

    public static GameMaster Instance;

    [Header("Background Colors")]
    public Color Color1;
    public Color Color2;

    [Header("Obstacle Containers")]
    public GameObject Color1Obstacles;
    public GameObject Color2Obstacles;

    [Header("Player Materials")]
    public Material Color1Material;
    public Material Color2Material;

    [Header("Label Colors")]
    public Color LabelColor1;
    public Color LabelColor2;

    GameObject player;
    Renderer playerRenderer;

    static bool isColor1 = true;
    int changeCount;

    // Use this for initialization
    void Start()
    {

        Instance = this;

        MainCamera.backgroundColor = Color1;

        changeCount = 0;

        player = GameObject.FindGameObjectWithTag(ColorJumperConstants.PLAYER);
        playerRenderer = player.GetComponent<Renderer>();
    }


    #region ColorChanging

    public void ChangeColor(int colorMode)
    {
        switch (colorMode)
        {
            case 1:
                MainCamera.backgroundColor = Color2;
                playerRenderer.material = Color2Material;
                Color1Obstacles.SetActive(false);
                Color2Obstacles.SetActive(true);
                UIManager.Instance.ApplyColorChange(LabelColor1);
                break;
            case 2:
                MainCamera.backgroundColor = Color1;
                playerRenderer.material = Color1Material;
                Color1Obstacles.SetActive(true);
                Color2Obstacles.SetActive(false);
                UIManager.Instance.ApplyColorChange(LabelColor2);
                break;
            default:
                break;
        }

        changeCount++;

        // UI handling
        UIManager.Instance.UpdateChangeCount(changeCount);

    }

    #endregion
}
