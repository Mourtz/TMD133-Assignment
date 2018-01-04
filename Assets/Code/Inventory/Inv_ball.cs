using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inv_ball : MonoBehaviour {

    public static byte Selected_Ball = 0;
    public static GameObject[] Inv_balls = new GameObject[8];

    private byte BallPtr = 0;
    private Button button;

    private static bool initialized_defaultColorBlock = false;
    private static ColorBlock defaultColorBlock = new ColorBlock();
    private static bool initialized_pressedColorBlock = false;

    private static ColorBlock pressedColorBlock = new ColorBlock();

    private void Start()
    {
        BallPtr = byte.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
        BallPtr -= 1;

        Inv_balls[BallPtr] = gameObject;

        bool Unlocked = DB.unlocked_ball[BallPtr];

        button = GetComponent<Button>();
        button.interactable = Unlocked;

        if (!initialized_defaultColorBlock)
        {
            defaultColorBlock = new ColorBlock();
            defaultColorBlock.normalColor = button.colors.normalColor;
            defaultColorBlock.highlightedColor = button.colors.highlightedColor;
            defaultColorBlock.pressedColor = button.colors.pressedColor;
            defaultColorBlock.disabledColor = button.colors.disabledColor;
            defaultColorBlock.colorMultiplier = 1f;

            initialized_defaultColorBlock = true;
        }

        if (!initialized_pressedColorBlock)
        {
            pressedColorBlock = new ColorBlock();
            pressedColorBlock.normalColor = new Color(0.4f, 0.6f, 0.5f);
            pressedColorBlock.highlightedColor = new Color(0.4f, 0.6f, 0.5f);
            pressedColorBlock.pressedColor = new Color(0.4f, 0.6f, 0.5f);
            pressedColorBlock.disabledColor = new Color(0.4f, 0.6f, 0.5f);
            pressedColorBlock.colorMultiplier = 1f;

            initialized_pressedColorBlock = true;
        }

        if (Selected_Ball == BallPtr)
        {
            button.interactable = false;
            button.colors = pressedColorBlock;
        }

        if (Unlocked) button.onClick.AddListener(onClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void onClick() {
        if (Selected_Ball != BallPtr)
        {
            button.colors = pressedColorBlock;
            button.interactable = false;

            Inv_balls[Selected_Ball].GetComponent<Button>().colors = defaultColorBlock;
            Inv_balls[Selected_Ball].GetComponent<Button>().interactable = true;

            Selected_Ball = BallPtr;
            DB.Refresh();
        }
    }
}
