using UnityEngine;

public class AddChestKey : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip pickupSound; // اسحبي ملف الصوت هنا في الـ Inspector

    private bool isCollected = false; // لمنع تنفيذ الكود أكثر من مرة في نفس اللحظة

    private void OnTriggerEnter(Collider other)
    {
        // 1. التحقق من أن الذي لمس المفتاح هو اللاعب ولم يُجمع بعد
        if (other.CompareTag("Player") && !isCollected)
        {
            // 2. الوصول لسكربت المخزن (PlayerInventory)
            PlayerInventory inv = other.GetComponent<PlayerInventory>();
            if (inv == null) inv = other.GetComponentInParent<PlayerInventory>();

            if (inv != null)
            {
                isCollected = true; // تفعيل القفل فوراً

                // 3. تحديث حالة المفتاح في مخزن اللاعب
                inv.hasChestKey = true; 
                Debug.Log("<color=yellow>chestkey collected</color>");

                // 4. تشغيل الصوت (PlayClipAtPoint يضمن سماع الصوت حتى بعد حذف المفتاح)
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                // 5. حذف المفتاح من الأرض
                Destroy(gameObject); 
            }
            else
            {
                Debug.LogError("❌ لم يتم العثور على سكربت PlayerInventory على اللاعب!");
            }
        }
    }
}