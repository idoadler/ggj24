using UnityEngine;

public class CharScript : MonoBehaviour
{
    [Header("Char Elements")]
    Rigidbody2D body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 20.0f;
    public GameManager gameManager;
    public Kid kid;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = kid.transform.position.x < transform.position.x;
        transform.rotation = kid.running ? Quaternion.Euler(0,0,0) : 
            kid.transform.position.x < transform.position.x ? 
                Quaternion.Euler(0,0,-90) : Quaternion.Euler(0,0,90);
        
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            gameManager.ActivatePowerUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Kid"))
        {
            gameManager.IncreaseOfHappiness();
        }
    }
}
