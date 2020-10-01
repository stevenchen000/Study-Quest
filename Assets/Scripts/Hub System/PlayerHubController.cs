using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHubController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed = 5;
    private bool isLocked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            Move();
        }
        else
        {
            DampenMovement();
        }

        anim.SetFloat("Speed", rb.velocity.magnitude);
    }


    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(x, y);
        direction = direction.magnitude > 1 ? direction.normalized : direction;
        FaceDirection(direction);

        rb.velocity = direction * speed;
    }

    private void FaceDirection(Vector2 direction)
    {
        float xScale = transform.localScale.x;
        float yScale = transform.localScale.y;
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(xScale), yScale, 1);
        }else if(direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(xScale), yScale, 1);
        }
    }

    private void DampenMovement()
    {
        rb.velocity /= 2;
    }



    public void LockMovement()
    {
        isLocked = true;
    }

    public void UnlockMovement()
    {
        isLocked = false;
    }
}
