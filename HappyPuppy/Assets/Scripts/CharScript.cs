using UnityEngine;

public class CharScript : MonoBehaviour
{
    public static CharScript Instance; 
    
    [Header("Char Elements")]
    Rigidbody2D body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 20.0f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = Kid.Instance.transform.position.x < transform.position.x;
        GetComponent<Animator>().SetBool("play",!Kid.Instance.running);
        
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        bool isIdle = body.velocity.sqrMagnitude <= 0.1f;
        GetComponent<Animator>().SetBool("idle",isIdle);
        GetComponent<AudioSource>().volume = isIdle ? 0.3f : 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.IncreaseOfHappiness();
    }
}
