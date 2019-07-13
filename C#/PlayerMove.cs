using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float dushSpeed;
    [SerializeField] private float jumpMultiplier;

    [SerializeField] private AnimationCurve jumpFallOff;

    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode dushKey;

    public static KeyCode DushKey;

    private CharacterController characterController;

    private bool isJumping;

    private float speed;

    private Vector3 vector;

    float mouseX, mouseY;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        DushKey = dushKey;
    }
	
	void Update ()
    {
        PlayerMoveMent();
        JumpInput();
        Dush();
    }

    private void Dush()
    {
        if (Input.GetKey(dushKey))
        {
            speed = dushSpeed * 0.1f;
        } else
        {
            speed = defaultSpeed * 0.1f;
        }
    }

    private void PlayerMoveMent()
    {
        Vector3 w = transform.forward * speed;
        Vector3 s = transform.forward * -speed;
        Vector3 d = transform.right * speed;
        Vector3 a = transform.right * -speed;

        characterController.SimpleMove(transform.up);

        if (Input.GetKey(KeyCode.W)) /* Forward */
        {
            characterController.Move(w);
        }

        if (Input.GetKey(KeyCode.S))/* Back */
        {
            characterController.Move(s);
        }

        if (Input.GetKey(KeyCode.D)) /* Right */
        {
            characterController.Move(d);
        }

        if (Input.GetKey(KeyCode.A)) /* Left */
        {
            characterController.Move(a);
        }
    }

    public void RotateX(float value)
    {
        float current = transform.rotation.y;

        transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0, current + value, 0),
                0.1f);

        print("pressed D");
        print(current);
    }

    private void JumpInput()
    {
        if (Input.GetKey(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    public IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;
        characterController.slopeLimit = 90.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            characterController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!characterController.isGrounded && characterController.collisionFlags != CollisionFlags.Above);

        characterController.slopeLimit = 90.0f;

        isJumping = false;
    }
}
