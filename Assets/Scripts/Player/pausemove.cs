using UnityEngine;

public class pausemove : MonoBehaviour
{
    public float PlayerSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 MoveInput;
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");
        MoveAnimation();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = MoveInput.normalized * PlayerSpeed;
    }
    private void MoveAnimation()
    {
        if (MoveInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Sin(Time.time * Speed) * 5f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10f);
        }
    }
}
