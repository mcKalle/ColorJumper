using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public Camera MainCamera;

    public static GameMaster Instance;

    [Header("Background Colors")]
    public Color YellowBackground;
    public Color BlueBackground;

    [Header("Obstacle Containers")]
    public GameObject YellowObstacles;
    public GameObject BlueObstacles;

    [Header("Player Materials")]
    public Material YellowMaterial;
    public Material BlueMaterial;

    GameObject player;
    Renderer playerRenderer;

    static bool IsYellow = true;
    int changeCount;

    // Use this for initialization
    void Start()
    {

        Instance = this;

        MainCamera.backgroundColor = YellowBackground;

        changeCount = 0;

        player = GameObject.FindGameObjectWithTag(ColorJumperConstants.PLAYER);
        playerRenderer = player.GetComponent<Renderer>();
    }


    #region ColorChanging

    public void ChangeColor(ColorEnum color)
    {
        switch (color)
        {
            case ColorEnum.Blue:
                MainCamera.backgroundColor = BlueBackground;
                playerRenderer.material = BlueMaterial;
                YellowObstacles.SetActive(false);
                BlueObstacles.SetActive(true);
                break;
            case ColorEnum.Yellow:
                MainCamera.backgroundColor = YellowBackground;
                playerRenderer.material = YellowMaterial;
                YellowObstacles.SetActive(true);
                BlueObstacles.SetActive(false);
                break;
            default:
                break;
        }

        changeCount++;

        // UI handling
        UIManager.Instance.ApplyColorChange(color);
        UIManager.Instance.UpdateChangeCount(changeCount);

    }

    #endregion
}
