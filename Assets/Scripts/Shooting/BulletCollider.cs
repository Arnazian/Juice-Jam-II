using UnityEngine;

public enum OwnerOfBulletType
{
    Enemy,
    Player
}

public class BulletCollider : MonoBehaviour
{
    [SerializeField] private GameObject collisionParticle;
    [SerializeField] private OwnerOfBulletType bulletColliderType;

    [HideInInspector] public float damage = 100;

    /*
    void OnCollisionEnter2D(Collision2D other)
    {
        
        var normal = other.contacts[0].normal;
        var point = other.contacts[0].point;
        var particleRotation = Quaternion.LookRotation(normal);
        Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal, Color.red, 200000f);

        Instantiate(collisionParticle, point, particleRotation);
        
        if(other.transform.CompareTag(bulletColliderType == OwnerOfBulletType.Enemy ? "Player" : "Enemy"))
            other.transform.GetComponent<IDamageable>().Damage(damage);
        
        Destroy(gameObject);
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BulletCollider>() != null) 
        {
            Instantiate(collisionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (!other.transform.CompareTag(bulletColliderType == OwnerOfBulletType.Enemy ? "Player" : "Enemy"))
        {
            return;
        }
        other.transform.GetComponent<IDamageable>().Damage(damage);
        Instantiate(collisionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);

        //var normal = other.contacts[0].normal;
        //var point = other.contacts[0].point;
        //var particleRotation = Quaternion.LookRotation(normal);
        //Debug.DrawRay(other.contacts[0].point, other.contacts[0].normal, Color.red, 200000f);
    }
}
