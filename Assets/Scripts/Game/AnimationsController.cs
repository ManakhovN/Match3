using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour {
    private static AnimationsController instance;
    public static AnimationsController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<AnimationsController>();
            return instance;
        }        
    }

    public int AnimationsCount
    {
        get
        {
            return animationsCount;
        }

        set
        {
            animationsCount = value;
            if (animationsCount == 0)
                OnAnimationsFinished.Invoke();
        }
    }

    int animationsCount = 0;
    public Action OnAnimationsFinished = delegate { };

    public void Awake()
    {
        instance = this;
    }

    public void OnDestroy()
    {
        instance = null;
    }

    public void MoveAnchoredPositionTo(RectTransform transform, Vector2 position, float moveSpeed)
    {
        AnimationsCount++;
        StartCoroutine(MoveAnchoredPositionToCoroutine(transform, position, moveSpeed));
    }

    public void ScaleTo(Transform transform, Vector3 scale, float scaleSpeed) {
        AnimationsCount++;
        StartCoroutine(ScaleToCoroutine(transform, scale, scaleSpeed));
    }

    private IEnumerator MoveAnchoredPositionToCoroutine(RectTransform transform, Vector2 position, float moveSpeed)
    {
        while (transform!=null && transform.anchoredPosition != position)
        {
            transform.anchoredPosition = Vector2.MoveTowards(transform.anchoredPosition, position, Time.deltaTime * moveSpeed);
            yield return new WaitForEndOfFrame();
        }
        AnimationsCount--;
    }

    private IEnumerator ScaleToCoroutine(Transform transform, Vector3 scale, float scaleSpeed)
    {
        while (transform!=null && transform.localScale != scale)
        {
            transform.localScale = Vector2.MoveTowards(transform.localScale, scale, Time.deltaTime * scaleSpeed);
            yield return new WaitForEndOfFrame();
        }
        AnimationsCount--;
    }
}
