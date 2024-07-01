using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    Animator _animator;
    Rigidbody _rigidbody;

    private bool _idle;
    private bool _running;
    private bool _walking;
    private bool _goingToStop;
    private float _timePassed;

    Vector3 _force;
    float _maxVelocity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _idle = true;
    }

    public void SetTriggerAnimation(string animationName)
    {
        _animator.SetTrigger(animationName);
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;

        if (_idle)
        {
            if (_timePassed >= 3) // dopo 3 secondi di idle
            {
                _idle = false;
                _walking = true;
                _timePassed = 0;

                _force = new Vector3(0, 0, 5);
                _maxVelocity = 3;

                SetTriggerAnimation("Walking");
            }
        }
        else if (_walking)
        {
            if (_timePassed >= 2) // dopo 2 secondi di walking
            {
                _walking = false;

                if (_goingToStop)
                {
                    _idle = true;
                    _force = Vector3.zero;
                    _maxVelocity = 0;
                    SetTriggerAnimation("Idle");
                }
                else
                {
                    _running = true;
                    _force = new Vector3(0, 0, 15);
                    _maxVelocity = 9;
                    SetTriggerAnimation("Running");
                }

                _goingToStop = false;
                _timePassed = 0;
            }
        }
        else if (_running)
        {
            if (_timePassed >= 4) // dopo 4 secondi di running
            {
                _running = false;
                _walking = true;
                _timePassed = 0;
                _goingToStop = true;

                _force = new Vector3(0, 0, 5);
                _maxVelocity = 3;

                SetTriggerAnimation("Walking");
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_force * 1000 * Time.fixedDeltaTime);

        _rigidbody.velocity = new Vector3(
            _rigidbody.velocity.x, 
            _rigidbody.velocity.y, 
            Mathf.Clamp(_rigidbody.velocity.z, 0, _maxVelocity));
    }
}
