using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class follow : MonoBehaviour
{

    public GameObject objectToFollow;
    public int maxTime = 300;
    public int level = 1;
    public Text text;
    public Text time;
    public Text winTime;
    public Text userOutputText;
    private bool makeLose = false;
    public float speed = 2.0f;
    private int gole = 1;
    private bool win = false;
    private bool loes = false;
    private float endX = 20;
    private float endY = 20;
    private float timerOne = 0.0f;
    private float timeOffset = 0.0f;
    private Scene scene;

    void Start()
    {
        timeOffset = Time.time;
        //scene = SceneManager.GetSceneByName("Start Screen");
        //scene = SceneManager.GetSceneByName("Main Game");

        float interpolation = 100 * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

        this.transform.position = position;
    }

    void Update()
    {
        gole = objectToFollow.GetComponent<colected>().totalNumberOfCoins;
        makeLose = objectToFollow.GetComponent<colected>().makeLose;
        if ((Time.time - timeOffset) < 1)
        {
            winTime.text = "";
            text.text = "";
            userOutputText.text = "3";
            //print("3\n");
        }
        else if ((Time.time - timeOffset) < 2)
        {
            userOutputText.text = "2";
            //print("2\n");
        }
        else if ((Time.time - timeOffset) < 3)
        {
            userOutputText.text = "1";
            //print("1\n");
        }
        else if (win == false && loes == false)
        {
            int coin = objectToFollow.GetComponent<colected>().coinCount;
            if (coin >= gole)
            {
                endY = 300;
                endX = 600;
                userOutputText.text = "";
                win = true;
                timerOne = (Time.time - timeOffset) + 5.0f;
                text.text = "";
                time.text = "";


                int bestTime = 0;
                int bestLevel = 0;
                using (StreamReader sr = new StreamReader("HighScore.txt"))
                {
                    for (int j = 0; j < 15 - 1; j++)
                    {
                        if (j == 0)
                        {
                            int.TryParse(sr.ReadLine(), out bestLevel);
                            if (bestLevel == level)
                            {
                                bestLevel += 1;
                            }
                        }
                        else if (j == level)
                        {
                            int.TryParse(sr.ReadLine(), out bestTime);
                        }
                        else
                        {
                            sr.ReadLine();
                        }
                    }
                }


                string[] line = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                using (StreamReader srTwo = new StreamReader("HighScore.txt"))
                {
                    for (int b = 0; b < 15; b++)
                    {
                        line[b] = srTwo.ReadLine();
                        if (line[b].Equals("") || line[b].Equals(" "))
                        {
                            line[b] = "0";
                        }
                    }
                }
                using (StreamWriter swTwo = new StreamWriter("HighScore.txt"))
                {
                    swTwo.WriteLine(bestLevel);

                    for (int a = 1; a < 15; a++)
                    {
                        if (a == level && bestTime > ((Time.time - timeOffset) - 3) || a == level && bestTime == 0)
                        {
                            swTwo.WriteLine((int)((Time.time - timeOffset) - 3));
                        }
                        else
                        {
                            swTwo.WriteLine(line[a]);
                        }
                    }
                }


                int min = (int)((Time.time - timeOffset) - 3) / 60;
                int sec = (int)((Time.time - timeOffset) - 3) - min * 60;
                if (sec > 9)
                {
                    winTime.text = "Your Time was: " + min.ToString() + ":" + sec.ToString();
                }
                else
                {
                    winTime.text = "Your Time was: " + min.ToString() + ":0" + sec.ToString();

                }
            }
            else if (makeLose == true || ((Time.time - timeOffset) - 3) > maxTime)
            {
                endY = 300;
                endX = 300;
                userOutputText.text = "";
                loes = true;
                timerOne = (Time.time - timeOffset) + 5.0f;
                text.text = "";
                time.text = "";
            }
            else
            {

                float interpolation = speed * Time.deltaTime;

                Vector3 position = this.transform.position;
                position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
                position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

                this.transform.position = position;
                text.text = coin.ToString() + "/" + gole.ToString();
                userOutputText.text = "";
                int min = (int)((Time.time - timeOffset) - 3) / 60;
                int sec = (int)((Time.time - timeOffset) - 3) - min * 60;
                if (sec > 9)
                {
                    time.text = min.ToString() + ":" + sec.ToString();
                }
                else
                {
                    time.text = min.ToString() + ":0" + sec.ToString();

                }

            }
        }
        else if (win == true || loes == true)
        {

            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, endY, 2.0f);
            position.x = Mathf.Lerp(this.transform.position.x, endX, 2.0f);
            this.transform.position = position;
            if (timerOne < (Time.time - timeOffset))
            {

                SceneManager.LoadScene(1);
            }
        }
    }
}

