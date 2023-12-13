using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;

    [SerializeField] private int _currentTarget;
    private bool _reverse = false;
    private bool _targetReached = false;

    private NavMeshAgent _agent;
    private Animator _anim;

    public bool coinTossed;
    public Vector3 coinPos;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_wayPoints.Count > 0 && _wayPoints[_currentTarget] != null && coinTossed == false)
        {
            _agent.SetDestination(_wayPoints[_currentTarget].position);
            float distance = Vector3.Distance(transform.position, _wayPoints[_currentTarget].position);

            if(distance < 1 && (_currentTarget == 0 || _currentTarget == _wayPoints.Count - 1))
            {
                _anim.SetBool("Walk", false);
            }
            else
            {
                _anim.SetBool("Walk", true);
            }

            if(distance < 1.0f && _targetReached == false)
            {
                if(_wayPoints.Count < 2)
                {
                    return;
                }

                if(_currentTarget == 0 || _currentTarget == _wayPoints.Count - 1)
                {
                    _targetReached = true;
                    StartCoroutine(WaitBeforeMoving());
                }
                else
                {
                    if (_reverse == true)
                    {
                        _currentTarget--;
                        if(_currentTarget <= 0)
                        {
                            _reverse = false;
                            _currentTarget = 0;
                        }
                    }
                    else
                    {
                        _currentTarget++;
                    }
                }
            }           
        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPos);
            if(distance < 4.0f)
            {
                _anim.SetBool("Walk", false);
            }
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        if(_currentTarget == 0)
        {
            yield return new WaitForSeconds(2.0f);
        }
        else if(_currentTarget == _wayPoints.Count - 1)
        {
            yield return new WaitForSeconds(2.0f);
        }

        if (_reverse == true)
        {
            _currentTarget--;
            if (_currentTarget == 0)
            {
                _reverse = false;
                _currentTarget = 0;
            }
        }
        else if(_reverse == false)
        {
            _currentTarget++;
            if (_currentTarget == _wayPoints.Count)
            {
                _reverse = true;
                _currentTarget--;
            }
        }

        _targetReached = false;
    }
}
