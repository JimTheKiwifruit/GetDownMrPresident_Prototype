using UnityEngine;
using System.Collections;

public class GameScore : MonoBehaviour {
    

    int duration;
    int seconds;
    int mins;
    string durationString;


    // Use this for initialization
    void Start () {
        updateDuration();
    }
	
	// Update is called once per frame
	void Update () {
        updateDuration();

        print(durationString);
    }

    void updateDuration()
    {
        duration = (int)Time.time;
        seconds = duration % 60;
        mins = (duration - mins) / 60;
        durationString = mins.ToString("00") + ":" + seconds.ToString("00");
    }
}
