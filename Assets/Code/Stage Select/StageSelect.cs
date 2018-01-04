using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    private void Awake()
    {
        for(int i = 1; i <= DB.total_stages; ++i)
            GameObject.Find("Stage" + i).GetComponent<Button>().interactable = DB.unlocked_stages[i-1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void LoadStage(string stage)
    {
        SceneManager.LoadScene(stage);
    }
}
