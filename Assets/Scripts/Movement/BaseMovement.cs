using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    protected bool canMove = true, isMoving;
    public bool IsMoving => isMoving;
    public void SetCanMove(bool newValue) { canMove = newValue; }
    

    public float speed;

    protected Rigidbody2D rb;


    #region EnemiesRelatedMethods

        #region PlayerTransform
        protected Transform playerTransform;

        private void CheckIfPlayerTransformIsAssigned()
        {
            if (playerTransform == null)
            {
                Debug.LogError("Players transform is not assigned. Use AssignPlayerTransform() in Start() or Awake() method");
            }
        }

        public void AssignPlayerTransform()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        #endregion

        #region MoveTowardsPlayer
        protected void MoveTowardsPlayer(float distanceFromTargetToStop = 0)
        {
            CheckIfPlayerTransformIsAssigned();
            MoveTowards(playerTransform, distanceFromTargetToStop);
        }
        #endregion

        #region StayAwayFromPlayer
        protected void StayAwayFromPlayer(float preferredDistance, float preferredDistanceBuffer)
        {
            CheckIfPlayerTransformIsAssigned();
            StayAway(playerTransform, preferredDistance, preferredDistanceBuffer);
        }
        #endregion

        #region FacePlayer
        //Want to move her FacePlayer and maybe make rotateTowards function
        #endregion
    #endregion

    #region OverallMethods
    public void MoveTowards(Transform targetTransform, float distanceFromTargetToStop = 0)
    {
        if (!canMove)
            return;

        //if you leave default parameters this won't 
        if(Vector3.Distance(transform.position, targetTransform.position) <= distanceFromTargetToStop)
        {
            isMoving = false;

            rb.velocity = Vector3.zero; 
            return;
        }


        //this will move object towards
        isMoving = true;

        rb.velocity = ForwardTimesSpeed(speed);
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
    #endregion
}
