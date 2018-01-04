using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class DB : MonoBehaviour {

    public const byte total_stages = 5;
    public static List<bool> unlocked_stages = new List<bool>();

    public const byte total_balls = 8;
    public static List<bool> unlocked_ball = new List<bool>();

    public static float MasterVolume = 0f;
    public static float SFX_Volume = 0f;
    public static float BackgroundMusicVolume = -24f;

    public AudioMixer audioMixer;
    public static bool initDB = false;

    public const string filename = "progress.txt";

    private void Start () {
        if (!File.Exists(filename))
        {
            unlocked_stages.Add(true);
            for(int i = 1; i < total_stages; ++i)
            {
                unlocked_stages.Add(false);
            }

            unlocked_ball.Add(true);
            for (int i = 1; i < total_balls; ++i)
            {
                unlocked_ball.Add(false);
            }

            Refresh();
        }
        // load and update game's variables from the database
        else if (!initDB)
        {
            string[] lines = File.ReadAllLines(filename);

            uint current_line = 0;

            // selected ball
            Inv_ball.Selected_Ball = Byte.Parse(lines[current_line++]);

            // unlocked stages
            for (int i = 0; i < total_stages; ++i)
                unlocked_stages.Add(Convert.ToBoolean(lines[current_line++]));

            // unlocked balls
            for (int i = 0; i < total_balls; ++i)
                unlocked_ball.Add(Convert.ToBoolean(lines[current_line++]));

            // audio mixer's values
            MasterVolume = float.Parse(lines[current_line++]);
            audioMixer.SetFloat("MasterVol", MasterVolume);

            SFX_Volume = float.Parse(lines[current_line++]);
            audioMixer.SetFloat("SfxVol", SFX_Volume);

            BackgroundMusicVolume = float.Parse(lines[current_line++]);
            audioMixer.SetFloat("BackgroundVol", BackgroundMusicVolume);

            initDB = true;
        }
    }
	
    public static void Refresh()
    {
        using (StreamWriter sw = File.CreateText(filename))
        {
            sw.WriteLine(Inv_ball.Selected_Ball);

            for (int i = 0; i < total_stages; ++i)
                sw.WriteLine(unlocked_stages[i].ToString());

            for (int i = 0; i < total_balls; ++i)
                sw.WriteLine(unlocked_ball[i].ToString());
                
            sw.WriteLine(MasterVolume);
            sw.WriteLine(SFX_Volume);
            sw.WriteLine(BackgroundMusicVolume);
        }
    }

}
