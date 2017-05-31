using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {


    public Color YellowBackground;

    public Color BlueBackground;

    public Camera MainCamera;

    public static GameMaster Instance;

    static bool IsYellow = true;

    // Use this for initialization
    void Start () {

        Instance = this;

        MainCamera.backgroundColor = YellowBackground;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region ColorChanging

    public void ChangeColor(ColorEnum color)
    {
        switch (color)
        {
            case ColorEnum.Blue:
                MainCamera.backgroundColor = BlueBackground;
                break;
            case ColorEnum.Yellow:
                MainCamera.backgroundColor = YellowBackground;
                break;
            default:
                break;
        }
    }

    #endregion
}
