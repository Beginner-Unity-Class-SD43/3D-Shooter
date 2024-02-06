using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera; // Main camera

    // Movement variables
    public float walkSpeed = 6f; // Player walk speed
    public float runMultiplier = 2f; // Multiply the walk speed when the player runs
    public float jumpPower = 7f; // How high the player will jump
    public float gravity = 10f; // Force of gravity acting on the player

    Vector3 moveDirection = Vector3.zero; // The direction the player will move in
    float rotationX = 0; // How far the player rotates
    float curSpeedX; // Player's current vertical speed
    float curSpeedY; // Player's current horizontal speed

    // Camera variables
    public float lookSpeed = 2f; // Camera turn sensitivity
    public float lookXLimit = 45f; // How far you can look up or down

    CharacterController characterController;
    bool canMove = true;

    [SerializeField] Image healthBar; // Health bar image
    [SerializeField] float maxHealth = 100f; // Max player health
    float health; // Current player health

    [SerializeField] GameObject deathScreen; // Death Screen

    Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Set current health to max health at the start of the game
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathScreen.SetActive(false);
        gun = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); // Getting our forward facing vector
        Vector3 right = transform.TransformDirection(Vector3.right); // Getting our right facing vector

        // Hold Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (canMove)
        {
            curSpeedX = walkSpeed * Input.GetAxis("Vertical"); // W and S keys (up and down)
            curSpeedY = walkSpeed * Input.GetAxis("Horizontal"); // A and D keys (left and right)
        }
        else
        {
            // Reset speed to 0 if no inputs are detected
            curSpeedX = 0;
            curSpeedY = 0;
        }
        if (isRunning)
        {
            // Multiply current speed by run multiplier
            curSpeedX *= runMultiplier;
            curSpeedY *= runMultiplier;
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY); // Set our move direction vector

        // Jump
        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower; // If we jump, then add jump power to our y direction
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime; // Add gravity to the player when they are in the air
        }

        characterController.Move(moveDirection * Time.deltaTime); // Move the character in our movement direction

        // Move the camera
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed; // Get the camera rotation up and down
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); // Lock the camera between -45 and 45 degrees up and down
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Actually rotate the camera up and down
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0); // Rotates our character left and right
        }
    }

    public void TakeDamage(float damage) // Take damage
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;

        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        deathScreen.SetActive(true); // Turn on the death screen
        Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
        Cursor.visible = true; // Lets us see the cursor again
        canMove = false;
        gun.canShoot = false;
    }

}
