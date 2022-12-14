using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SwitchDirection
{
    Left = 1,
    Right = -1
}

public class SceneSwitcher : MonoBehaviour
{
    public SwitchDirection switchDirection = SwitchDirection.Right;
    private bool ignoreNextTriggerEnter;

    private void Start()
    {
        // Hide the sprite and add portal particle effect
        GetComponent<SpriteRenderer>().enabled = false;
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ignoreNextTriggerEnter)
        {
            ignoreNextTriggerEnter = false;
            return;
        }
        RoomController.instance.HandleSwitcherTrigger(other, this);
    }

    public void teleportPlayer(GameObject player)
    {
        // Teleport the player to the bottom of the switcher
        // Find the bottom point of the opposite switcher:
        var switcherTransform = transform;
        // var bottomPoint = switcherTransform.position - switcherTransform.up * switcherTransform.localScale.y / 2;
        
        // Move the point to the middle of the player
        var playerTransform = player.transform;
        // var playerBottomPoint = bottomPoint + playerTransform.up * playerTransform.localScale.y / 2;
        
        // Move the player to the bottom point
        playerTransform.position = switcherTransform.transform.position;
        
        // Ignore the next trigger event
        ignoreNextTriggerEnter = true;
    }
}
