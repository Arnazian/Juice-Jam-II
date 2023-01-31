using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    protected bool canMove = true, isMoving;
    public bool IsMoving => isMoving;
    public void SetCanMove(bool newValue) { canMove = newValue; }
    

    public float speed;

    protected Rigidbody2D rb;



    public void MoveTowards(Transform targetTransform, float distanceFromTargetToStop = 0)
    {
        if (!canMove)
            return;

        //if you leave default parameters this won't 
        if(Vector3.Distance(transform.position, targetTransform.position) <= distanceFromTargetToStop)
        {
            Debug.Log("not Moved enemy");

            isMoving = false;

            rb.velocity = Vector3.zero; 
            return;
        }


        //this will move object towards
        isMoving = true;

        rb.velocity = ForwardTimesSpeed(speed);

        Debug.Log("Moved enemy");
    }

    public void StayAway(Transform targetTransform, float targetDistance = 6, float targetDistanceBuffer = 2)
    {
        if (!canMove)
            return;

        if(Vector3.Distance(transform.position, targetTransform.position) > targetDistance + targetDistanceBuffer)
        {
            MoveTowards(targetTransform);
        }
        else if(Vector3.Distance(transform.position, targetTransform.position) < targetDistance - targetDistanceBuffer)
        {
            isMoving = true;

            rb.velocity = ForwardTimesSpeed(speed) * -1;
            return;
        }
        else
        {
            isMoving = false;

            rb.velocity = Vector3.zero;
            return;
        }
    }


    public void Accelerate(Vector3 velocity, float time, Vector3 direction, float maxSpeed)
    {
        if (!canMove)
            return;

        Vector2 Acceleration = (direction * maxSpeed - velocity) / time;

        rb.velocity += Acceleration;
    }

    public void Deaccelerate(Vector3 velocity, float time)
    {
        if (!canMove)
            return;

        Vector2 Deacceleration = (Vector3.zero - velocity) / time;
        rb.velocity += Deacceleration;
    }


    private Vector2 ForwardTimesSpeed(float speed)
    {
        return transform.rotation * Vector2.up * speed;
    }
}
