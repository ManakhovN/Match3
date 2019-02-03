using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour {
    [SerializeField] private ScoreCounter scoreCounter;
    Text label;
    public void Start()
    {
        label = GetComponent<Text>();
        scoreCounter.OnScoreChanged += UpdateScore;
    }

    private void UpdateScore(int obj)
    {
        label.text = obj.ToString();
    }
}
