using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// This script is attached to the RoomController object in the scene.
// It is responsible for creating the room and handling the transition between rooms.
public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    // Array of all room prefab names:
    public string[] roomPrefabs =
    {
        "Prefabs/Room_1",
        "Prefabs/Room_2",
        "Prefabs/Room_3",
        "Prefabs/Room_4",
        "Prefabs/Room_5",
        "Prefabs/Room_6",
        "Prefabs/Room_7",
        "Prefabs/Room_8",
        "Prefabs/Room_9",
        "Prefabs/Room_10",
        "Prefabs/Room_11",
        "Prefabs/Room_12",
        "Prefabs/Room_13",
        "Prefabs/Room_14",
        "Prefabs/Room_15"
    };
    
    private List<string> unvisitedRooms = new List<string>();

    // A map of room index to room prefab name:
    private Dictionary<int, string> roomMap = new();

    // The current room index:
    private int currentRoomIndex = 0;

    private SceneSwitcher activeSwitcher = null;
    
    // The scale of the room:
    public Vector3 roomScale = new(0.3f, 0.3f, 0.3f);
    
    public Vector2 spawnPoint = new(-10, 4);

    // The current room:
    private GameObject currentRoom;

    // The player:
    public GameObject player;

    // Start handler
    private void Start()
    {
        instance = this;

        // Create the first room
        roomMap[currentRoomIndex] = roomPrefabs[0];

        for (var i = 1; i < roomPrefabs.Length; i++)
        {
            unvisitedRooms.Add(roomPrefabs[i]);
        }

        InstantiateRoom();
    }

    private void CreateRandomRoom()
    {
        roomMap[currentRoomIndex] = unvisitedRooms[Random.Range(0, unvisitedRooms.Count)];
        unvisitedRooms.Remove(roomMap[currentRoomIndex]);
    }

    private void InstantiateRoom()
    {
        // Instantiate the room:
        DestroyRoom();
        currentRoom = Instantiate(Resources.Load(roomMap[currentRoomIndex])) as GameObject;
        
        // Scale the room:
        currentRoom.transform.localScale = roomScale;
    }

    private void DestroyRoom()
    {
        if (!currentRoom) return;
        Destroy(currentRoom);
        currentRoom = null;
    }

    public void HandleSwitcherTrigger(Collider2D hitCollider, SceneSwitcher switcher)
    {
        // If the player is hitting a SceneSwitcher, then we need to transition to the next room:
        if (hitCollider.gameObject != player) return;

        hitCollider.enabled = false;
        hitCollider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        activeSwitcher = switcher;
        
        BlackoutScript.instance.OnFadeIn += OnBlackoutFadeIn;
        BlackoutScript.instance.FadeIn();
    }

    private void OnBlackoutFadeIn()
    {
        BlackoutScript.instance.OnFadeIn -= OnBlackoutFadeIn;
        
        var switchDirection = activeSwitcher.switchDirection;
        switch (switchDirection)
        {
            case SwitchDirection.Left:
                currentRoomIndex--;
                break;
            case SwitchDirection.Right:
                currentRoomIndex++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        // Create a random room for the currentRoomIndex, if it doesn't exist:
        if (!roomMap.ContainsKey(currentRoomIndex))
        {
            CreateRandomRoom();
        }
        
        // Instantiate the room:
        InstantiateRoom();
        
        // Get all SceneSwitchers in the new room and teleport the player to the one that is the opposite
        // of the one they just hit:
        
        var sceneSwitchers = currentRoom.GetComponentsInChildren<SceneSwitcher>();
        var oppositeSwitcher = sceneSwitchers.FirstOrDefault(sceneSwitcher => sceneSwitcher.switchDirection != switchDirection);

        if (!oppositeSwitcher)
        {
            // This should never happen, but just in case:
            Debug.LogError("No opposite switcher found!");
            return;
        }
        
        // Teleport the player to the opposite switcher:
        oppositeSwitcher.teleportPlayer(player);

        var cameraComponent = Camera.main.GetComponent<CameraFollow>();
        cameraComponent.teleportInstantly();
        
        // Fade out the blackout:
        BlackoutScript.instance.OnFadeOut += OnBlackoutFadeOut;
        BlackoutScript.instance.FadeOut();
    }

    private void OnBlackoutFadeOut()
    {
        BlackoutScript.instance.OnFadeOut -= OnBlackoutFadeOut;
        
        // Re-enable the collider and make the player dynamic again:
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}