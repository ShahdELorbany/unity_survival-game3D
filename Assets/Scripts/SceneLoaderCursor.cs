using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderCursor : MonoBehaviour
{
    // set true when loading game from menu
    public static bool comingFromMenu = false;

    // call this from your main menu to load the Game scene
    public static void LoadGameFromMenu(string gameSceneName = "Game")
    {
        comingFromMenu = true;
        SceneManager.LoadScene(gameSceneName);
    }

    void Awake()
    {
        // keep a single instance alive across scenes
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // start coroutine to force cursor state for a few frames (robust)
        StartCoroutine(ForceCursorForFrames(scene.name));
    }

    private IEnumerator ForceCursorForFrames(string sceneName)
    {
        // small delay to let scene initialize
        yield return null;

        // if we are on MainMenu, always unlock and show
        if (sceneName == "MainMenu")
        {
            for (int i = 0; i < 10; i++)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                yield return null;
            }
            yield break;
        }

        // if we loaded Game and we are coming from the menu, keep unlocked briefly
        if (sceneName == "Game" && comingFromMenu)
        {
            for (int i = 0; i < 10; i++)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                yield return null;
            }
            // consume the flag so subsequent loads behave normally
            comingFromMenu = false;
            yield break;
        }

        // default behavior: lock cursor for gameplay
        for (int i = 0; i < 10; i++)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            yield return null;
        }
    }
}
