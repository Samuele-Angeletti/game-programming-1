using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    //prefab
    //funzione di spawn 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private InputActionSystem _shootAction;

    private void Awake()
    {
        _shootAction = new InputActionSystem();
        _shootAction.PlayerShoot.Shoot.performed += ShootAction;
        _shootAction.Enable();
    }

    private void ShootAction(InputAction.CallbackContext context)
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Camera.main.transform.rotation);
    }
}
