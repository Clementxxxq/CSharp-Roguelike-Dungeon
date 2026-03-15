using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = Keyboard.current == null ? Vector2.zero : Vector2.zero;

        if (Keyboard.current != null)
        {
            float moveX = 0f;
            float moveY = 0f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveY = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveY = -1f;

            moveInput = new Vector2(moveX, moveY).normalized;
        }

        rb.linearVelocity = moveInput * speed;
    }
}
