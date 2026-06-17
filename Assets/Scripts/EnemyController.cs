using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    [Header("Settings")]
    public float chaseSpeed = 3.5f;
    public float patrolSpeed = 1.5f;
    public float detectionRadius = 6f;
    public float patrolZone = 4f;
    public RPGStats rpgStats;
    public GameObject GUI;

    private CombatUI combatUI;
    private Vector2 startPosition;
    private Vector2 nextPatrolPoint;
    private Transform player;
    private float waitTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        SetNewPatrolPoint();
        combatUI = GUI.GetComponent<CombatUI>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            FlipSprite(player.position.x);
        }
        else
        {
            PatrolLogic();
        }
        MoveAnimation();
    }

    void PatrolLogic()
    {
        // Если мы ждем на точке — просто уменьшаем таймер
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                SetNewPatrolPoint();
            }
            return; // Не двигаемся, пока тикает таймер
        }

        // Если таймер кончился — идем к точке
        transform.position = Vector2.MoveTowards(transform.position, nextPatrolPoint, patrolSpeed * Time.deltaTime);
        FlipSprite(nextPatrolPoint.x);

        // Как только дошли — запускаем таймер ожидания
        if (Vector2.Distance(transform.position, nextPatrolPoint) < 0.2f)
        {
            waitTimer = Random.Range(1f, 3f);
        }
    }


    void SetNewPatrolPoint()
    {
        nextPatrolPoint = startPosition + Random.insideUnitCircle * patrolZone;
        waitTimer = Random.Range(1f, 3f);
    }

    void FlipSprite(float targetX)
    {
        if (targetX > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, 1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Vector3 center = Application.isPlaying ? (Vector3)startPosition : transform.position;
        Gizmos.DrawWireSphere(center, patrolZone);
    }
    private void MoveAnimation()
    {
        bool isMoving = Vector2.Distance(transform.position, nextPatrolPoint) > 0.2f;

        if (isMoving)
        {
            float angle = Mathf.Sin(Time.time * 15f) * 5f;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RPGStats playerStats = collision.gameObject.GetComponent<PlayerController>().playerStats;
            Debug.Log("Collided via Trigger!");
            if (GUI != null) { GUI.SetActive(true); Debug.Log("GUI was ACTIVATED!"); }
            if (combatUI != null) { combatUI.Reload(rpgStats, playerStats); Debug.Log("RPGStats were loaded!"); };
        }
    }

}
