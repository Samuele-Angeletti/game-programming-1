using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private float shootForce = 50f;

    private InputActionSystem _shootAction;

    private void Awake()
    {
        _shootAction = new InputActionSystem();
        _shootAction.PlayerShoot.Shoot.performed += ShootAction;
        _shootAction.Enable();
    }

    private void ShootAction(InputAction.CallbackContext context)
    {
        var bulletInstatiate = Instantiate(bulletPrefab, bulletSpawnPoint.position, Camera.main.transform.rotation);
        bulletInstatiate.
            GetComponent<Rigidbody>().
            AddForce(   bulletInstatiate.transform.forward * shootForce, 
                        ForceMode.Impulse);
    }
}
