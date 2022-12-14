using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour, IPointerClickHandler
{

    public string GameSceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        BlackoutScript.instance.OnFadeIn += OnFadeIn;
        BlackoutScript.instance.FadeIn();
    }

    private void OnFadeIn()
    {
        BlackoutScript.instance.OnFadeIn -= OnFadeIn;
        SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }
}
