using UnityEngine;

// public class PlayerInventory : MonoBehaviour
// {
//     // public bool hasKey = false;

//     // public void AddKey()
//     // {
//     //     hasKey = true;
//     //     Debug.Log("✅ PlayerInventory: Key added!");
//     // }
//     // تغيير من bool إلى int لعد المفاتيح
//     public int numberOfKeys = 0;

//     // دالة لإضافة مفتاح عند جمعه
//     public void AddKey()
//     {
//         numberOfKeys++;
    
//         Debug.Log("🔑 Key collected! Total keys: " + numberOfKeys);
//     }
// }



// تم حذف مكتبة SceneManagement لأننا لم نعد بحاجة للانتقال لشاشة الخسارة هنا

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Status")]
    public int numberOfKeys = 0;    
    public bool hasChestKey = false; 

    void Start()
    {
        // تصفير المخزن لضمان بداية نظيفة في كل ليفل
        numberOfKeys = 0;
        hasChestKey = false;
        Debug.Log("Inventory Reset: All keys cleared for the new level.");
    }

    // دالة جمع مفاتيح الأبواب العادية
    public void AddKey()
    {
        numberOfKeys++; 
        Debug.Log("🔑 Door Key collected! Total door keys in this level: " + numberOfKeys);
    }

    // دالة جمع مفتاح الصندوق الخاص بليفل 7
    public void AddChestKey()
    {
        hasChestKey = true;
        Debug.Log("🗝️ Chest Key collected! You can now open the locked chest.");
    }

    // تم حذف دوال التصادم (OnCollisionEnter / OnTriggerEnter) 
    // لكي لا يتأثر اللاعب عند لمس الحواجز أو أي شيء آخر
}