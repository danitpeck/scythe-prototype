using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 input;
    private PaintBlock nearbyPaintBlock;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                input.x = -1;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                input.x = 1;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                input.y = -1;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                input.y = 1;

            if (Keyboard.current.eKey.wasPressedThisFrame && nearbyPaintBlock != null)
            {
                Destroy(nearbyPaintBlock.gameObject);
            }
        }

        input = input.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PaintBlock paintBlock = collision.gameObject.GetComponent<PaintBlock>();
        if (paintBlock != null)
        {
            nearbyPaintBlock = paintBlock;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PaintBlock paintBlock = collision.gameObject.GetComponent<PaintBlock>();
        if (paintBlock != null && nearbyPaintBlock == paintBlock)
        {
            nearbyPaintBlock = null;
        }
    }
}
