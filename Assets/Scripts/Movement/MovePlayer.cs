using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{   
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private ParticleSystem footsteps;
    [SerializeField] private float speed;
    [SerializeField] private float accelerationTime;

    private Rigidbody2D rb;

    public InputAction playerInput;
    private float inputH;
    private float inputV;

    private bool canMove = true;


    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footsteps.Stop();
    }

    void FixedUpdate()
    {
        HandleMovement();

        if (inputH != 0 || inputV != 0)
        {
            if(!footsteps.isPlaying)
                footsteps.Play(true);
            if(!footstepSource.isPlaying)
                footstepSource.Play();
        }
        else
        {
            if(footsteps.isPlaying)
                footsteps.Stop(true);
            if(footstepSource.isPlaying)
                footstepSource.Stop();
        }
    }

    void HandleMovement()
    {
        if (!canMove) { return; }

        inputH = playerInput.ReadValue<Vector2>().x;
        inputV = playerInput.ReadValue<Vector2>().y;


        Vector2 direction = new Vector2(inputH, inputV);

        Vector2 movement = Accelerate(rb.velocity, accelerationTime, direction, 10 * speed);

        rb.velocity += movement;


        if (Mathf.Abs(inputH) <= 0 && Mathf.Abs(inputV) <= 0)
        {
            Vector2 Deacceleration = Deaccelerate(rb.velocity, accelerationTime * 1000);
            rb.velocity += Deacceleration;
        }
    }


    Vector3 Accelerate(Vector3 velocity, float time, Vector3 direction, float maxSpeed)
    {
        Vector3 Acceleration = (direction * maxSpeed - velocity) / time;
        return Acceleration;
    }

    Vector3 Deaccelerate(Vector3 velocity, float time)
    {
        Vector3 Deacceleration = (Vector3.zero - velocity) / time;
        return Deacceleration;
    }

    public void SetCanMove(bool newCanMove)
    {
        canMove = newCanMove;
    }
}
