using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions; // مكتبة لاستخراج الأرقام بسهولة

public class LevelUI : MonoBehaviour
{
    private TMP_Text levelText;

    void Start()
    {
        levelText = GetComponent<TMP_Text>();

        // 1. الحصول على اسم المشهد الحالي
        string currentScene = SceneManager.GetActiveScene().name;

        // 2. إذا كان المشهد الأول اسمه "Game"، اعتبريه ليفل 1
        if (currentScene == "Game")
        {
            levelText.text = "LEVEL 1";
        }
        else
        {
            // 3. استخراج الرقم من اسم المشهد (سواء كان Level2 أو Level_2 أو Level 8)
            // هذا السطر يبحث عن أي أرقام داخل الاسم ويضعها بجانب كلمة LEVEL
            string result = Regex.Match(currentScene, @"\d+").Value;

            if (!string.IsNullOrEmpty(result))
            {
                levelText.text = "LEVEL " + result;
            }
            else
            {
                // إذا لم يجد رقماً، سيعرض اسم المشهد كبيراً (مثل WIN SCENE)
                levelText.text = currentScene.ToUpper();
            }
        }
    }
}