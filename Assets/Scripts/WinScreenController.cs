using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{
    void Start()
    {
        // إظهار مؤشر الفأرة عند دخول شاشة الفوز المؤقتة
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // عند الضغط على مفتاح المسافة (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // التحقق مما إذا كان هناك مستوى تالٍ محفوظ في سكربت الباب
            if (!string.IsNullOrEmpty(DoorController.NextLevelName))
            {
                // الانتقال إلى المستوى التالي (مثلاً من ليفل 4 إلى ليفل 5)
                string nextLevel = DoorController.NextLevelName;
                
                // إعادة تعيين الاسم لمنع التكرار غير المقصود
                DoorController.NextLevelName = ""; 

                SceneManager.LoadScene(nextLevel);
            }
            else
            {
                // إذا لم يتم العثور على مستوى (نهاية اللعبة أو خطأ)، نعود للقائمة الرئيسية
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}