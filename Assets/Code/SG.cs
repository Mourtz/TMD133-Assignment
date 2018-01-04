using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SG : MonoBehaviour {

    public GameObject[] Balls;
    public GameObject Prompt;

    public Text TimeCounter;
    private static float initTime;

    private static readonly float[] timeToBeat = { 3.0f, 5.0f, 6.0f, 4.0f, 2.0f };

    private static Color red_col = new Color(1f, 0f, 0f);
    private static Color green_col = new Color(0f, 1f, 0f);

    private static string currentScene;
    private static int currentSceneIndex;

    private static string nextScene = "MainMenu";
    private static int nextSceneIndex;

    private GameObject actor;

    private void initTimeCounter()
    {
        initTime = Time.time;
        TimeCounter.enabled = true;
    }

    private void Awake ()
    {
        TimeCounter.enabled = false;
        DrawLine.OnStart += initTimeCounter;

        actor = Instantiate(Balls[Inv_ball.Selected_Ball]);
        currentScene = SceneManager.GetActiveScene().name;
        currentSceneIndex = int.Parse(currentScene.Substring(currentScene.Length - 1));
        
        switch (currentScene)
        {
            case "stage0":
                actor.transform.position = new Vector3(-6.026f, 3.302f, 0f);
                nextScene = "stage1";
                break;
            case "stage1":
                actor.transform.position = new Vector3(-7.8f, 3.8f, 0f);
                nextScene = "stage2";
                break;
            case "stage2":
                actor.transform.position = new Vector3(0f, 4.0f, 0f);
                nextScene = "stage3";
                break;
            case "stage3":
                actor.transform.position = new Vector3(-7f, 3.75f, 0f);
                nextScene = "stage4";
                break;
            case "stage4":
                actor.transform.position = new Vector3(-7f, 3.75f, 0f);
                break;
            default:
                break;
        }

        if (!int.TryParse(nextScene.Substring(nextScene.Length - 1), out nextSceneIndex))
        {
            nextSceneIndex = -1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DrawLine.CanDraw = false;

            if (GameObject.Find("Prompt(Clone)") == null)
            {
                GameObject prompt = Instantiate(Prompt);
                Button YesBtn = prompt.transform.GetChild(2).GetComponent<Button>();
                Button NoBtn = prompt.transform.GetChild(3).GetComponent<Button>();

                YesBtn.onClick.AddListener(delegate { SceneManager.LoadScene("MainMenu"); });
                NoBtn.onClick.AddListener(delegate { Destroy(prompt); });
            }
        }
    }

    private void LateUpdate()
    {
        float t = Time.time - initTime;

        if(t > timeToBeat[currentSceneIndex])
            TimeCounter.color = red_col;
        else
            TimeCounter.color = green_col;

        TimeCounter.text = string.Format("{0:0.00}", t);
    }

    private void OnDestroy()
    {
        DrawLine.OnStart -= initTimeCounter;
    }

    public static void Finish()
    {
        if(nextSceneIndex != -1)
        {
            if(Time.time - initTime <= timeToBeat[currentSceneIndex])
            {
                DB.unlocked_ball[currentSceneIndex + 1] = true;
            }

            DB.unlocked_stages[nextSceneIndex] = true;
            DB.Refresh();
        }

        SceneManager.LoadScene(nextScene);
    }

    public void Restart(float t)
    {
        StartCoroutine(c_restart(t));
    }

    private IEnumerator c_restart(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
