using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider MasterVolume;
    public Slider SFX_Volume;
    public Slider BackgroundMusicVolume;

    public GameObject Prompt;

    public AudioMixer audioMixer;

    private void Awake()
    {
        MasterVolume.value = DB.MasterVolume;
        SFX_Volume.value = DB.SFX_Volume;
        BackgroundMusicVolume.value = DB.BackgroundMusicVolume;
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

                YesBtn.onClick.AddListener(delegate {
                    DB.MasterVolume = MasterVolume.value;
                    DB.SFX_Volume = SFX_Volume.value;
                    DB.BackgroundMusicVolume = BackgroundMusicVolume.value;

                    DB.Refresh();

                    SceneManager.LoadScene("MainMenu");
                });
                NoBtn.onClick.AddListener(delegate { Destroy(prompt); });
            }
        }
    }

    public void OnChange()
    {
        audioMixer.SetFloat("MasterVol", MasterVolume.value);
        audioMixer.SetFloat("SfxVol", SFX_Volume.value);
        audioMixer.SetFloat("BackgroundVol", BackgroundMusicVolume.value);
    }
}
