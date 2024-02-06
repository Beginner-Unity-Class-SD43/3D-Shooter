using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshProUGUI tmpro;

    float score = 0f;

    // Start is called before the first frame update
    void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpro.text = "Score: " + score;
    }

    public void AddScore(float value)
    {
        score += value; // Add score based on value
    }

}
