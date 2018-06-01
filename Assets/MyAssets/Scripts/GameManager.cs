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
    [Header("Light Color")]
    public Color LightBackgroundColor;
    public Color LightColor2;
    public Color LightPlattformColor;
    public Color LightColor4;
    public Color LightPlayerColor;
    [Space]
    public Material LightPlatformMaterial;
    [Space]
    public Material LightPlayerMaterial;
    [Space]
    public Color[] LightColorChangerColors;
    public Material[] LightColorChangerMaterials;

    [Space]
    [Space]

    [Header("Dark Color")]
    public Color DarkBackgroundColor;
    public Color DarkColor2;
    public Color DarkPlattformColor;
    public Color DarkColor4;
    public Color DarkPlayerColor;
    [Space]
    public Material DarkPlatformMaterial;
    [Space]
    public Material DarkPlayerMaterial;
    [Space]
    public Color[] DarkColorChangerColors;
    public Material[] DarkColorChangerMaterials;

    [Space]
    [Space]

    [Header("Obstacle Containers")]
    public GameObject LightObstacles;
    public GameObject DarkObstacles;

    [Space]
    [Space]

    [Header("Start Point")]
    public Transform LevelEntryPoint;

    [Space]
    [Header("Sprites")]
    public Sprite PowerUpSprite;
    public Sprite EnemySpriteBase;
    public Sprite EnemySpriteFrame;

    [HideInInspector]
    public List<GameObject> powerUps;

    GameObject player;
    Renderer playerRenderer;

    [HideInInspector]
    public bool IsLightColor = true;

    int changeCount;

    UIManager uiManager;
    CameraFollow cameraFollow;

    Vector3 initialCameraPosition;

    // Use this for initialization
    void Start()
    {
        changeCount = 0;

        InitColors();

        player = GameObject.FindGameObjectWithTag(ColorJumperConstants.PLAYER);
        playerRenderer = player.GetComponent<Renderer>();

        // save all powerUps in the scene
        powerUps = GameObject.FindGameObjectsWithTag(ColorJumperConstants.POWER_UP).ToList();

        uiManager = FindObjectOfType<UIManager>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        initialCameraPosition = Camera.main.transform.position;

        Respawn();
    }

    private void InitColors()
    {
        LightPlayerMaterial.color = LightPlayerColor;
        LightPlatformMaterial.color = LightPlattformColor;

        DarkPlayerMaterial.color = DarkPlayerColor;
        DarkPlatformMaterial.color = DarkPlattformColor;
    }

    #region SpawnDeathFinish

    public void ApplyPlayerDeath()
    {
        playerRenderer.enabled = false;

        uiManager.deathPanel.SetActive(true);

        cameraFollow.enabled = false;
    }

    public void Respawn()
    {
        // hide death menu
        if (uiManager.deathPanel != null)
        {
            uiManager.deathPanel.SetActive(false);
        }

        // reset position
        player.transform.position = LevelEntryPoint.position;
        // reset size
        player.transform.localScale = new Vector3(1, 1, 1);

        Camera.main.transform.SetPositionAndRotation(new Vector3(initialCameraPosition.x, initialCameraPosition.y + 1, initialCameraPosition.z),
            Camera.main.transform.rotation);


        cameraFollow.enabled = true;

        RespawnPowerUps();

        ChangeColor(2);
        changeCount = 0;

        if (uiManager != null)
        {
            uiManager.UpdateChangeCount(changeCount);
            uiManager.UpdateSplitPowerUpCount(0);
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    #endregion

    #region ColorChanging

    public void ChangeColor(int colorMode)
    {
        IsLightColor = !IsLightColor;

        switch (colorMode)
        {
            case 1:
                Camera.main.backgroundColor = DarkBackgroundColor;
                playerRenderer.material = DarkPlayerMaterial;

                SwitchColorChanger(DarkColorChangerColors, DarkColorChangerMaterials);


                LightObstacles.SetActive(false);
                DarkObstacles.SetActive(true);
                if (uiManager != null)
                {
                    uiManager.ApplyColorChange(colorMode);
                }
                break;
            case 2:
                Camera.main.backgroundColor = LightBackgroundColor;
                playerRenderer.material = LightPlayerMaterial;

                SwitchColorChanger(LightColorChangerColors, LightColorChangerMaterials);

                LightObstacles.SetActive(true);
                DarkObstacles.SetActive(false);
                if (uiManager != null)
                {
                    uiManager.ApplyColorChange(colorMode);
                }
                break;
            default:
                break;
        }

        changeCount++;

        // UI handling
        if (uiManager != null)
        {
            uiManager.UpdateChangeCount(changeCount);
        }

    }

    private void SwitchColorChanger(Color[] colors, Material[] materials)
    {
        for (int i = 0; i < colors.GetLength(0); i++)
        {
            materials[i].color = colors[i];
        }
    }

    //private void ChangeSprite(Sprite sprite)
    //{
    //    Color[] spriteColors = sprite.texture.GetPixels();

    //    for (int i = 0; i < spriteColors.GetLength(0); i++)
    //    {
    //        Color pixelColor = spriteColors[i];

    //        float color = IsLightColor ? 255f : 0f;
    //        pixelColor.r = color;
    //        pixelColor.g = color;
    //        pixelColor.b = color;

    //        spriteColors[i] = pixelColor;
    //    }

    //    sprite.texture.SetPixels(spriteColors);
    //}

    #endregion

    #region PowerUps

    public IPowerUp TakePowerUp(GameObject gameObject)
    {
        IPowerUp powerUp = gameObject.GetComponent<IPowerUp>();

        if (powerUp is SplitPowerUp)
        {
            powerUp.Count++;

            // do ui stuff
            uiManager.UpdateSplitPowerUpCount(powerUp.Count);
        }

        // let it disappear
        gameObject.SetActive(false);
        // TODO: maybe with particles

        return powerUp;
    }

    #endregion
}
