using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : BaseMovement
{   
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private ParticleSystem footsteps;
    [SerializeField] private float accelerationTime;

    public InputAction playerInput;
    private float inputH;
    private float inputV;


    private void OnEnable() { playerInput.Enable(); }
    private void OnDisable() { playerInput.Disable(); }

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
        inputH = playerInput.ReadValue<Vector2>().x;
        inputV = playerInput.ReadValue<Vector2>().y;

        Vector2 direction = new Vector2(inputH, inputV);


        Accelerate(rb.velocity, accelerationTime, direction, 10 * speed);

        if (Mathf.Abs(inputH) <= 0 && Mathf.Abs(inputV) <= 0)
        {
            Deaccelerate(rb.velocity, accelerationTime * 1000);
        }
    }
}
