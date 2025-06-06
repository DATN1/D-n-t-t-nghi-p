using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Tốc độ và khoảng cách")]
    public float moveSpeed = 3.5f;
    public float stopDistance = 1.2f; // Khoảng cách dừng lại

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = stopDistance;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            agent.isStopped = true; // ⛔ Dừng mọi chuyển động
            return;
        }
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist > stopDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;
                // Gọi attack hoặc trigger animation tại đây nếu cần
            }
        }
    }
}
