using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnMouseDown() {
        Application.Quit ();
        Debug.Log("Game should be closed");
    }
}
