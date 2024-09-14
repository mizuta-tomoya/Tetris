using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TetrisScore : MonoBehaviour
{
    private Text scoreText;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "111";
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
