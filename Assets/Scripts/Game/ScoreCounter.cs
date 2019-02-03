using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
    private int score = 0;
    public Action<int> OnScoreChanged = delegate { };
    [SerializeField]
    LevelController levelController;
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            OnScoreChanged.Invoke(score);
        }
    }

    public void Start()
    {
        levelController.FieldProcessor.OnItemRemove += IncreaseScore;
    }

    private void IncreaseScore(int arg1, int arg2)
    {
        Score++;
    }
}
