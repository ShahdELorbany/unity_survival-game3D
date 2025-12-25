using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // 👈 1. يجب تفعيل مكتبة TMPro

public class LevelTimer : MonoBehaviour
{
    public float timeLimit = 60f; 
    
    // 👈 2. يجب إزالة تعليق // من هذا السطر
    public TMP_Text timerText; 

    private float currentTime;
    private bool timerActive = false;
    private const string TargetSceneName = "Level_4"; 

    void Start()
    {
        if (SceneManager.GetActiveScene().name == TargetSceneName)
        {
            currentTime = timeLimit;
            timerActive = true;
            Debug.Log($"⏳ Level Timer started for {TargetSceneName} with {timeLimit} seconds.");
            
            // 👈 3. إضافة استدعاء لتحديث النص عند البدء
            UpdateTimerDisplay(); 
        }
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime;

            // 👈 4. إضافة استدعاء لتحديث النص في كل إطار
            UpdateTimerDisplay(); 
            
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                timerActive = false;
                
                // منطق الخسارة
                ZombieController.SceneToReload = TargetSceneName; 
                SceneManager.LoadScene("Loose Screen");
            }
        }
    }
    
    // 👈 5. إضافة دالة عرض المؤقت
    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(currentTime); 
            timerText.text = seconds.ToString();
            
            // إضافة تحذير عند قرب انتهاء الوقت
            if (seconds <= 10)
            {
                timerText.color = Color.red; 
            }
        }
    }
}