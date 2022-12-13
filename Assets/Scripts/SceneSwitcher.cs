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
        var bottomPoint = switcherTransform.position + switcherTransform.up * -0.5f;
        
        // Move the point to the middle of the player
        var playerTransform = player.transform;
        var playerBottomPoint = bottomPoint + playerTransform.up * -0.5f;
        
        // Move the player to the bottom point
        playerTransform.position = playerBottomPoint;
        
        // Ignore the next trigger event
        ignoreNextTriggerEnter = true;
    }
}
