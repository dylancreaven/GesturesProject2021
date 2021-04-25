using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bowling : MonoBehaviour
{

    public static int pinKnockedDownCount;

    public Text scoreText;
    private bool isOnReload = false;
    int pointPerPin = 5;
    public static int score;

    int count = 0;
    private void Awake()
    {
        pinKnockedDownCount=0;
        scoreText.text = "0";
    }

    private void Update()
    {
        if (pinKnockedDownCount != 0)
        {
            Invoke("TurnEnded", 3.0f);
        }
    }
    public void testButton()
    {
        Debug.Log("Button works");
    }
    private void TurnEnded()
    {

        if (count == 0)
        {
            Debug.Log("Pins Knocked down => " + pinKnockedDownCount);
            score +=(pinKnockedDownCount * pointPerPin);
            scoreText.text = score.ToString();
            count++;
        }


    }
    public void ReloadLevel()
    {
        Debug.Log("Reloading Scene");
        SceneManager.LoadScene(0);

    }


}
