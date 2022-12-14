using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutScript : MonoBehaviour
{
    public static BlackoutScript instance;

    public Image image;
    
    public float fadeSpeed = 1f;
    public bool isFading = false;
    public bool isBlack = false;
    
    private float _alpha = 0.0f;
    private float Alpha
    {
        get => _alpha;
        set
        {
            _alpha = value;
            if (image)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, _alpha);   
            }
        }
    }

    // Event of the fade in/out
    public event Action OnFadeIn;
    public event Action OnFadeOut;

    public bool fadeoutOnStart = true;

    private void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
        Alpha = Alpha;
    }

    private void Start()
    {
        if (fadeoutOnStart)
        {
            FadeOut();
        }
    }

    private void Update()
    {
        if (!isFading) return;
        if (isBlack)
        {
            Alpha -= fadeSpeed * Time.deltaTime;
            if (Alpha > 0) return;

            Alpha = 0;
            image.enabled = false;
            isFading = false;
            isBlack = false;
            OnFadeOut?.Invoke();
        }
        else
        {
            Alpha += fadeSpeed * Time.deltaTime;
            if (Alpha < 1) return;

            Alpha = 1;
            isFading = false;
            isBlack = true;
            OnFadeIn?.Invoke();
        }
    }
    
    public void FadeIn()
    {
        Alpha = 0;
        isBlack = false;
        image.enabled = true;
        isFading = true;
    }
    
    public void FadeOut()
    {
        Alpha = 1;
        isBlack = true;
        isFading = true;
        image.enabled = true;
    }

    public void FadeInImmediate()
    {
        OnFadeIn?.Invoke();
        isFading = false;
        Alpha = 1;
        isBlack = true;
    }
    
    public void FadeOutImmediate()
    {
        image.enabled = false;
        OnFadeOut?.Invoke();
        Alpha = 1;
        isBlack = false;
        isFading = false;
    }
}
