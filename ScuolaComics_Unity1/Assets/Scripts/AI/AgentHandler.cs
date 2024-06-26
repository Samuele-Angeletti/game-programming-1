using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentHandler : MonoBehaviour
{
    [SerializeField] List<Transform> destinations;
    [SerializeField] float offsetDistance = 0.1f;

    private NavMeshAgent _agent;
    private int _index = -1;
    private Vector3 _nextDestination;
    private Player _target;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.SetDestination(GetNextDestination());
    }

    // la posizione successiva da raggiungere
    private Vector3 GetNextDestination()
    {
        if (_index + 1 >= destinations.Count) // se index è più lungo della lista
        {
            // _index è più lungo della lista quindi torna a zero
            _index = 0;
        }
        else // altrimenti (index è minore della lunghezza della lista) index++
        {
            _index++;
        }

        _nextDestination = destinations[_index].position;
        return _nextDestination;
    }

    private void Update()
    {
        // se la distanza tra la mia posizione e quella della destinazione è minore di offset,
        // allora cambia destinazione

        if (_target != null)
        {
            _agent.SetDestination(_target.transform.position);
            return;
        }

        if (Vector3.Distance(transform.position, _nextDestination) <= offsetDistance)
        {
            _agent.SetDestination(GetNextDestination());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // questa funzione restituisce un bool se ha trovato il componente,
        // nell'out abbiamo il componente trovato
        if (other.TryGetComponent(out Player player)) 
        {
            _target = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _agent.SetDestination(_nextDestination);
            _target = null;
        }
    }
}
