using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour {

    public GameObject Prompt;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
}
