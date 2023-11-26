using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    TMP_Text scoreText;
    
    public static int scoreCount = 0;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        scoreText.text = "Score: " + scoreCount;
    }
}
