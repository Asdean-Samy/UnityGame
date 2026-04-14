using UnityEngine;

public class ChataigneMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    float inputX = 0f;
    float inputY = 0f;

    float lastMessageTime;

    bool isGrounded = true;
    float verticalVelocity = 0f;
    float gravity = -9.81f;

    void Update()
    {
        // STOP AUTOMATIQUE si plus de signal
        if (Time.time - lastMessageTime > 0.2f)
        {
            inputX = 0f;
            inputY = 0f;
        }

        // ROTATION (gauche / droite)
        if (inputX > 0.1f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (inputX < -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        // JUMP (déclenchement unique)
        if (inputY > 0.5f && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        // GRAVITÉ
        verticalVelocity += gravity * Time.deltaTime;

        // MOUVEMENT FINAL
        Vector3 move = new Vector3(inputX, verticalVelocity, 0f);
        transform.Translate(move * speed * Time.deltaTime);

        // RESET SOL (simple)
        if (transform.position.y <= 0f)
        {
            isGrounded = true;
            verticalVelocity = 0f;

            Vector3 pos = transform.position;
            pos.y = 0f;
            transform.position = pos;
        }
    }

    public void OnMoveX(float value)
    {
        inputX = Mathf.Clamp(value, -1f, 1f);
        lastMessageTime = Time.time;
    }

    public void OnMoveY(float value)
    {
        inputY = Mathf.Clamp(value, -1f, 1f);
        lastMessageTime = Time.time;
    }

    public void Move(Vector2 direction)
    {
        OnMoveX(direction.x);
        OnMoveY(direction.y);
    }
}