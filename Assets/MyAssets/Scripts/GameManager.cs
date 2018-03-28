using Assets.MyAssets.Scripts.PowerUps;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public Camera MainCamera;

    public static GameManager Instance;

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

    [HideInInspector]
    public List<GameObject> powerUps;


    [Header("Start Point")]
    public Transform LevelEntryPoint;

    GameObject player;
    Renderer playerRenderer;

    [HideInInspector]
    public bool IsColor1 = true;

    int changeCount;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        changeCount = 0;

        player = GameObject.FindGameObjectWithTag(ColorJumperConstants.PLAYER);
        playerRenderer = player.GetComponent<Renderer>();

        // save all powerUps in the scene
        powerUps = GameObject.FindGameObjectsWithTag(ColorJumperConstants.POWER_UP).ToList();

        //foreach (var item in allPowerUps)
        //{
        //    IPowerUp powerUp = item.GetComponent<IPowerUp>();

        //    //// set all power ups to active
        //    //powerUp.Placed = true;
        //    //// save parent
        //    //powerUp.Parent = item.transform.parent;
        //    //// save position
        //    //powerUp.XPos = item.transform.position.x;
        //    //powerUp.YPos = item.transform.position.y;

        //    powerUps.Add(powerUp);
        //}

        Respawn();
    }

    #region SpawnDeathFinish

    public void ApplyPlayerDeath()
    {
        MainCamera.backgroundColor = Color.black;

        playerRenderer.enabled = false;

        Respawn();
    }

    private void Respawn()
    {
        // reset position
        player.transform.position = LevelEntryPoint.position;
        // reset size
        player.transform.localScale = new Vector3(1, 1, 1);

        RespawnPowerUps();

        ChangeColor(2);
        changeCount = 0;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateChangeCount(changeCount);
            UIManager.Instance.UpdateSplitPowerUpCount(0);
        }

        playerRenderer.enabled = true;

        player.GetComponent<BoxCollider2D>().transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y + 5), player.transform.rotation);
    }

    private void RespawnPowerUps()
    {
        foreach (GameObject powerUp in powerUps)
        {
            // only enable powerUp if its current color container is enabled
            if (powerUp.transform.parent.gameObject.activeInHierarchy)
            {
                powerUp.SetActive(true);
            }
        }
    }

    public void Finish()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string lastChar = currentScene[currentScene.Length - 1].ToString();

        int level = Convert.ToInt32(lastChar);

        SceneManager.LoadScene("Level " + (level + 1));
    }

    #endregion

    #region ColorChanging

    public void ChangeColor(int colorMode)
    {
        IsColor1 = !IsColor1;

        switch (colorMode)
        {
            case 1:
                MainCamera.backgroundColor = Color2;
                playerRenderer.material = Color2Material;
                Color1Obstacles.SetActive(false);
                Color2Obstacles.SetActive(true);
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.ApplyColorChange(LabelColor2, colorMode);
                }
                break;
            case 2:
                MainCamera.backgroundColor = Color1;
                playerRenderer.material = Color1Material;
                Color1Obstacles.SetActive(true);
                Color2Obstacles.SetActive(false);
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.ApplyColorChange(LabelColor1, colorMode);
                }
                break;
            default:
                break;
        }

        changeCount++;

        // UI handling
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateChangeCount(changeCount);
        }

    }

    #endregion

    #region PowerUps

    public IPowerUp TakePowerUp(GameObject gameObject)
    {
        IPowerUp powerUp = gameObject.GetComponent<IPowerUp>();

        if (powerUp is SplitPowerUp)
        {
            powerUp.Count++;

            // do ui stuff
            UIManager.Instance.UpdateSplitPowerUpCount(powerUp.Count);
        }

        // let it disappear
        gameObject.SetActive(false);
        // TODO: maybe with particles

        return powerUp;
    }

    #endregion
}
