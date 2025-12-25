// using UnityEngine;

// public class KeyController : MonoBehaviour
// {
//     [Header("Floating & Rotation Settings")]
//     public float rotationSpeed = 50f;
//     public float floatSpeed = 1f;
//     public float floatHeight = 0.25f;

//     [Header("Audio")]
//     public AudioClip pickupSound;
//     private AudioSource audioSource;

//     private Vector3 startPos;

//     void Start()
//     {
//         startPos = transform.position;

//         // **التعديل 1: نضمن وجود AudioSource بغض النظر عن طريقة إنشاء الكائن**
//         audioSource = GetComponent<AudioSource>();
//         if (audioSource == null)
//         {
//             audioSource = gameObject.AddComponent<AudioSource>();
//         }
//         audioSource.playOnAwake = false;
//         // ملاحظة: إذا كان الـ AudioSource مُرفَقاً بالكائن، فلن يُنشئ واحداً جديداً.
//     }

//     void Update()
//     {
//         // Float + rotate
//         transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
//         float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
//         transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         Debug.Log($"Key collided with: {other.name} (tag: {other.tag})");

//         // look for PlayerInventory anywhere on the player or its children
//         if (other.CompareTag("Player"))
//         {
//             // نستخدم GetComponentInParent<PlayerInventory>() لضمان العثور عليه
//             var inv = other.GetComponentInParent<PlayerInventory>();

//             if (inv != null)
//             {
//                 inv.AddKey();
//                 Debug.Log("🔑 Key collected! Player now has the key.");

//                 // **التعديل 2: نتحقق من audioSource قبل استخدامه لتجنب الـ NullReferenceException**
//                 if (pickupSound != null && audioSource != null)
//                 {
//                     audioSource.PlayOneShot(pickupSound);
//                 }

//                 // destroy key after short delay (so sound finishes)
//                 Destroy(gameObject, 0.3f);
//             }
//             else
//             {
//                 Debug.LogError("❌ PlayerInventory script not found on player or children! (Check if PlayerInventory script is attached to the player object itself)");
//             }
//         }
//     }
// }

using UnityEngine;

public class KeyController : MonoBehaviour
{
    [Header("Floating & Rotation Settings")]
    public float rotationSpeed = 50f;
    public float floatSpeed = 1f;
    public float floatHeight = 0.25f;

    [Header("Audio")]
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private Vector3 startPos;
    private bool isCollected = false; // لمنع الجمع المتكرر لنفس المفتاح قبل حذفه

    void Start()
    {
        startPos = transform.position;

        // التأكد من وجود AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // جعل الصوت 3D
    }

    void Update()
    {
        // دوران وتحريك المفتاح للأعلى وللأسفل (تأثير بصري)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // التحقق من اصطدام اللاعب ولم يتم جمع هذا المفتاح بعد
        if (other.CompareTag("Player") && !isCollected)
        {
            var inv = other.GetComponentInParent<PlayerInventory>();

            if (inv != null)
            {
                isCollected = true; // نضع علامة أنه جُمع فوراً
                
                // استدعاء دالة زيادة عداد المفاتيح في اللاعب
                inv.AddKey(); 
                Debug.Log($"🔑 Key collected! Current Keys: {inv.numberOfKeys}");

                // تشغيل صوت الالتقاط
                if (pickupSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                // إخفاء شكل المفتاح فوراً لكي لا يلمسه اللاعب مرة أخرى
                // مع إبقاء الكائن حياً لنصف ثانية حتى ينتهي الصوت
                if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = false;
                foreach (Renderer r in GetComponentsInChildren<Renderer>()) r.enabled = false;

                Destroy(gameObject, 0.5f);
            }
            else
            {
                Debug.LogError("❌ PlayerInventory script not found on player! Ensure numberOfKeys is an int.");
            }
        }
    }
}