using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseScreenController : MonoBehaviour
{
    void Start()
    {
        // عرض مؤشر الفأرة (Mouse Cursor) عند ظهور شاشة الخسارة
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // عند الضغط على أي مفتاح
        if (Input.anyKeyDown)
        {
            // **التحقق من اسم المشهد المحفوظ**
            // نستخدم المتغير الثابت SceneToReload الذي تم تعيينه في سكربت ZombieController
            
            string sceneToLoad = ZombieController.SceneToReload;

            // 1. التحقق مما إذا كان هناك اسم مشهد صالح محفوظ (أي أننا جئنا من مستوى لعب)
            if (!string.IsNullOrEmpty(sceneToLoad) && sceneToLoad != "MainMenu")
            {
                Debug.Log($"⏪ Loading previous scene: {sceneToLoad}");
                
                // إعادة تعيين المتغير الثابت لمنع حدوث مشاكل في المستقبل
                ZombieController.SceneToReload = ""; 

                // تحميل المشهد الذي خسر فيه اللاعب
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                // 2. إذا لم يكن هناك مشهد محفوظ أو إذا كان الاسم المحفوظ هو "MainMenu" (كحماية إضافية)
                Debug.Log("Returning to Main Menu.");
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}