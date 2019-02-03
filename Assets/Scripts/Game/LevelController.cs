using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    [SerializeField]
    private LevelInfo levelInfo;
    [SerializeField]
    private RectTransform field;

    public LevelInfo LevelInfo
    {
        get
        {
            return levelInfo;
        }

        set
        {
            levelInfo = value;
        }
    }

    public RectTransform Field
    {
        get
        {
            return field;
        }

        set
        {
            field = value;
        }
    }

    private FieldProcessor fieldProcessor;
    public FieldProcessor FieldProcessor
    {
        get
        {
            return fieldProcessor;
        }

        set
        {
            fieldProcessor = value;
        }
    }
    public Action OnScoreChanged = delegate { };


    public void Awake()
    {
        var levelGenerator = new LevelGenerator(LevelInfo, Field);
        FieldProcessor = new FieldProcessor(levelInfo.FieldInfo, LevelInfo.ItemSet.ItemsCount);
        var fieldUIController = new FieldUIController(levelGenerator, FieldProcessor);
        AnimationsController.Instance.OnAnimationsFinished += FieldProcessor.CheckFields;
        AnimationsController.Instance.OnAnimationsFinished += FieldProcessor.CheckForFreeSpaces;
    }

    public void OnDestroy()
    {
        if (AnimationsController.Instance == null)
            return;
        AnimationsController.Instance.OnAnimationsFinished -= FieldProcessor.CheckFields;
        AnimationsController.Instance.OnAnimationsFinished -= FieldProcessor.CheckForFreeSpaces;
    }
}
