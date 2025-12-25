using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public AudioClip openSound;
    
    // استخدام SerializeField يضمن أن Unity يرى المتغير بوضوح
    [SerializeField] public int keysRequired = 1; 
    
    private AudioSource audioSource;
    private bool isOpen = false;

    public static string NextLevelName = "";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        NextLevelName = ""; 
        
        // سطر للتأكد عند تشغيل اللعبة من الرقم المطلوب في هذا الليفل
        Debug.Log($"<color=blue>Target Level: {SceneManager.GetActiveScene().name} | Keys Needed for this door: {keysRequired}</color>");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            PlayerInventory playerInventory = other.GetComponentInParent<PlayerInventory>();
            
            if (playerInventory != null)
            {
                // طباعة الرقمين للمقارنة (المهم جداً مراقبة هذا السطر في الـ Console)
                Debug.Log($"Current Keys: {playerInventory.numberOfKeys} / Required: {keysRequired}");

                if (playerInventory.numberOfKeys >= keysRequired)
                {
                    isOpen = true; 
                    if (audioSource != null && openSound != null)
                        audioSource.PlayOneShot(openSound);

                    Debug.Log("<color=green>✅ Success! Opening door...</color>");
                    playerInventory.numberOfKeys = 0; 
                    Invoke(nameof(PrepareNextStep), 1.2f);
                }
                else
                {
                    Debug.Log($"<color=red>🚫 Access Denied! You need {keysRequired} keys but you only have {playerInventory.numberOfKeys}.</color>");
                }
            }
        }
    }

    void PrepareNextStep()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int currentLevelNumber = 0;

        if (currentScene == "Game") currentLevelNumber = 1;
        else if (currentScene.StartsWith("Level_") && int.TryParse(currentScene.Replace("Level_", ""), out int num))
        {
            currentLevelNumber = num;
        }

        if (currentLevelNumber >= 1 && currentLevelNumber < 10)
            NextLevelName = "Level_" + (currentLevelNumber + 1);
        else if (currentLevelNumber == 10)
            NextLevelName = "FinalWinScene";

        SceneManager.LoadScene("WinScene");
    }
}