using UnityEngine;
using UnityEngine.SceneManagement; 

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 2f;
    public float speed = 2f;
    public float waitTime = 1f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingUp = true;
    private float timer;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        Vector3 destination = movingUp ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingUp = !movingUp;
            timer = waitTime;
        }
    }

    // تم حذف دالة OnCollisionEnter لكي لا يموت اللاعب عند اللمس
}