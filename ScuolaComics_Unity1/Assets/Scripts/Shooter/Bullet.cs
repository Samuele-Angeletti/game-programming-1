using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);

            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
