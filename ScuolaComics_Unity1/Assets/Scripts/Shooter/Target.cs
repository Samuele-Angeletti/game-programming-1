using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int vita { get; set; }
    [field: SerializeField] public int vitaMassima { get; set; }

    [SerializeField] private GameObject playerReference;
    [SerializeField] private Slider barraDellaVita;

    void Awake()
    {
        vita = vitaMassima;
    }

    void Update()
    {
        RuotaBarraVita();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Hai colpito il nemico facendo danno " + damage);

        vita -= damage;

        AggiornaBarraVita();

        if (vita <= 0)
        {
            Debug.Log("nemico Ucciso");
            Destroy(gameObject);
        }
    }

    public void AggiornaBarraVita()
    {
        var value = (float)vita / (float)vitaMassima;
        barraDellaVita.value = value;
    }

    private void RuotaBarraVita()
    {
        barraDellaVita.transform.LookAt(playerReference.transform);

        barraDellaVita.transform.localRotation = 
            Quaternion.Euler(0, barraDellaVita.transform.localRotation.eulerAngles.y, 0);
    }
    
}
