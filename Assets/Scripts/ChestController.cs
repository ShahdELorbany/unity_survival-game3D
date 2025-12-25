using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject doorKeyInside; // اسحبي مفتاح الباب هنا من الـ Inspector
    private bool isOpened = false;

    private void OnTriggerEnter(Collider other) 
    {
        // التحقق من تصادم اللاعب
        if (other.CompareTag("Player") && !isOpened) 
        {
            // البحث عن سكربت الانفنتوري في اللاعب أو في "أب" اللاعب
            PlayerInventory inv = other.GetComponent<PlayerInventory>();
            if (inv == null) inv = other.GetComponentInParent<PlayerInventory>();

            if (inv != null && inv.hasChestKey) 
            {
                OpenChest();
            } 
            else 
            {
                Debug.Log("الصندوق مقفول.. محتاج مفتاح الصندوق!");
            }
        }
    }

    void OpenChest() 
    {
        isOpened = true;
        if (doorKeyInside != null) 
        {
            doorKeyInside.SetActive(true); // إظهار مفتاح الباب
        }
        
        // حركة فتح بسيطة (تأكدي أن محور الدوران صحيح لصندوقك)
        transform.Rotate(-90, 0, 0); 
        Debug.Log("تم فتح الصندوق!");
    }
}