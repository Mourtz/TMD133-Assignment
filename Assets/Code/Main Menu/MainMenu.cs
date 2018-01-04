using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject Prompt;
    public GameObject BackgroundMusic;

    private void Awake()
    {
        if (GameObject.Find("BackgroundMusic(Clone)") == null) Instantiate(BackgroundMusic);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("Prompt(Clone)") == null)
            {
                GameObject prompt = Instantiate(Prompt);
                Button YesBtn = prompt.transform.GetChild(2).GetComponent<Button>();
                Button NoBtn = prompt.transform.GetChild(3).GetComponent<Button>();

                YesBtn.onClick.AddListener(delegate { Application.Quit(); });
                NoBtn.onClick.AddListener(delegate { Destroy(prompt); });
            }
        }
    }

    public void LoadStage(string stage)
    {
        SceneManager.LoadScene(stage);
    }
}
