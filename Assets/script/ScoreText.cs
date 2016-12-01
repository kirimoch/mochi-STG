using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class ScoreText : MonoBehaviour
{

    public static float totalScore = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "SCORE:" + totalScore.ToString();
    }
}
