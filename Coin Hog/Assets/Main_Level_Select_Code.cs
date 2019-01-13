using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Main_Level_Select_Code : MonoBehaviour
{
    public GameObject levels;
    public GameObject images;
    private int numberOfLevels = 0;
    private int[] bestTime = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    private int playableLevels = 0;
    // Use this for initialization
    void Start()
    {
        GameObject currentObject;
        numberOfLevels = levels.transform.childCount;
        using (StreamReader sr = new StreamReader("HighScore.txt"))
        {
            int.TryParse(sr.ReadLine(), out playableLevels);
            if (playableLevels > numberOfLevels)
            {
                playableLevels = numberOfLevels;
            }

            if (playableLevels < 1)
            {
                playableLevels = 1;
            }
            for (int i = 0; i < playableLevels; i += 1)
            {
                int.TryParse(sr.ReadLine(), out bestTime[i]);
                int min = (int)bestTime[i] / 60;
                int sec = (int)bestTime[i] - min * 60;
                currentObject = levels.transform.GetChild(i).gameObject;
                currentObject.transform.GetChild(1).GetComponent<Image>().sprite = images.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
                currentObject = currentObject.transform.GetChild(0).gameObject;
                if (sec > 9)
                {
                    currentObject.GetComponent<UnityEngine.UI.Text>().text = "Best Time\n" + min.ToString() + ":" + sec.ToString();
                }
                else
                {
                    currentObject.GetComponent<UnityEngine.UI.Text>().text = "Best Time\n" + min.ToString() + ":0" + sec.ToString();

                }
            }

            for (int loopPlayableLevels = playableLevels; loopPlayableLevels < numberOfLevels; loopPlayableLevels += 1)
            {
                currentObject = levels.transform.GetChild(loopPlayableLevels).gameObject;
                currentObject.transform.GetChild(1).GetComponent<Image>().sprite = images.transform.GetChild(loopPlayableLevels + (images.transform.childCount / 2)).gameObject.GetComponent<SpriteRenderer>().sprite;
                currentObject = currentObject.transform.GetChild(0).gameObject;
                currentObject.GetComponent<UnityEngine.UI.Text>().text = "Best Time\n0:00";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int selectedLevel = -1;
        for (int i = 0; i < playableLevels; i++)
        {
            GameObject checkPlay = levels.transform.GetChild(i).gameObject;
            if (checkPlay.GetComponent<Selected>().clicked == true)
            {
                selectedLevel = i;
            }
        }
        if (selectedLevel != -1)
        {
            SceneManager.LoadScene(selectedLevel + 2);
        }
    }
}
