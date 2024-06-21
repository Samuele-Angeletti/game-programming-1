using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private float shootForce = 50f;
    [SerializeField] private int proiettiliNelCaricatore = 10;
    [SerializeField] private int capacitaMassimaCaricatore = 10;
    [SerializeField] private TextMeshProUGUI proiettiliText;

    private Coroutine _ricaricaCoroutine;

    private InputActionSystem _shootAction;


    private void Awake()
    {
        _shootAction = new InputActionSystem();
        _shootAction.PlayerShoot.Shoot.performed += ShootAction;
        _shootAction.Enable();

        AggiornaUIProiettili(proiettiliNelCaricatore.ToString());
    }

    private void ShootAction(InputAction.CallbackContext context)
    {
        if(proiettiliNelCaricatore == 0)
        {
            
            if(_ricaricaCoroutine == null) 
                _ricaricaCoroutine = StartCoroutine(Reload());

            // _ricaricaCoroutine = 
            //     _ricaricaCoroutine == null ? StartCoroutine(Reload()) : null;

            return;
        }

        var bulletInstatiate = Instantiate(bulletPrefab, bulletSpawnPoint.position, Camera.main.transform.rotation);
        bulletInstatiate.
            GetComponent<Rigidbody>().
            AddForce(   bulletInstatiate.transform.forward * shootForce, 
                        ForceMode.Impulse);
        
        proiettiliNelCaricatore--;

        AggiornaUIProiettili(proiettiliNelCaricatore.ToString());
    }

    private IEnumerator Reload()
    {
        AggiornaUIProiettili("Ricaricando...");

        yield return new WaitForSeconds(3f);

        proiettiliNelCaricatore = capacitaMassimaCaricatore;

        AggiornaUIProiettili(proiettiliNelCaricatore.ToString());

        _ricaricaCoroutine = null;
    }

    private void AggiornaUIProiettili(string testo)
    {
        proiettiliText.SetText($"{testo} / {capacitaMassimaCaricatore}");
    }
}
