using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneLoaderCursor.LoadGameFromMenu("Game");
        }
    }
}
