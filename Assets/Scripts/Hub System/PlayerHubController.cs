using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerHubController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed = 5;
    private bool isLocked = false;
    private bool autoMove = false;
    private Vector2 autoMovePosition;
    public float autoStopDistance = 0.2f;
    public int backgroundLayerMask = 10;
    private Camera cam;

    private IInteractable interactTarget;
    private bool autoInteract = false;

    private IInteractor playerInteractor;

    private void Awake()
    {
        playerInteractor = transform.GetComponent<IInteractor>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector2 clickPosition = cam.ScreenToWorldPoint(mousePosition);
                SetMoveToPosition(clickPosition);
                SelectAutoTarget(clickPosition);
            }

            Vector2 inputVector = GetInputVector();

            if(inputVector.magnitude > 0)
            {
                Move(inputVector);
                autoMove = false;
                autoInteract = false;
            }
            else if (autoMove)
            {
                MoveToPosition(autoMovePosition);
            }
        }
        else
        {
            DampenMovement();
        }

        anim.SetFloat("Speed", rb.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IInteractable interaction = collider.transform.GetComponent<IInteractable>();

        if(interaction == interactTarget)
        {
            AutomaticallyInteract();
        }
    }





    private void SetMoveToPosition(Vector2 clickPosition)
    {
        autoMovePosition = clickPosition;
        autoMove = true;
    }

    private void SelectAutoTarget(Vector2 clickPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, new Vector2());

        if(hit.collider != null)
        {
            Transform targetTransform = hit.transform;
            interactTarget = targetTransform.GetComponent<IInteractable>();
            autoInteract = true;
        }
    }

    private void AutomaticallyInteract()
    {
        autoInteract = false;
        autoMove = false;
        interactTarget.Interact(playerInteractor);
    }

    private void MoveToPosition(Vector2 position)
    {
        Vector2 distanceVector = UnityUtilities.GetVectorBetween((Vector2)transform.position, position);
        if (distanceVector.magnitude > autoStopDistance)
        {
            distanceVector.Normalize();

            FaceDirection(distanceVector);
            rb.velocity = distanceVector * speed;
        }
        else
        {
            rb.velocity = new Vector3();
            autoMove = false;
        }
    }


    private void Move(Vector2 direction)
    {
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

    private Vector2 GetInputVector()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(x, y);
        direction = direction.magnitude > 1 ? direction.normalized : direction;
        return direction;
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
