using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string GameSceneName = "";
    
    public void OnMouseDown() {
        SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }
}
