using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 100;
    [SerializeField] private float timeToDestroy = 5f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);

        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }

    private void Update()
    {
        // transform.Translate(Vector3.forward * force * Time.deltaTime);
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
