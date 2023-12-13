using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private AudioClip _coinSoundEffect;

    private NavMeshAgent _agent;
    private Animator _anim;

    private Vector3 _target;
    private bool _hasTossedCoin;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                //Debug.Log("Hit : " + hitInfo.point);

                _agent.SetDestination(hitInfo.point);
                _anim.SetBool("Walk", true);
                _target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, _target);

        if(distance < 1.0f)
        {
            _anim.SetBool("Walk", false);
        }

        if (Input.GetMouseButtonDown(1) && _hasTossedCoin == false)
        {
            RaycastHit hitInfo;
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                _anim.SetTrigger("Throw");
                _hasTossedCoin = true;
                Instantiate(_coinPrefab, hitInfo.point + new Vector3(0, -1.3f, 0), Quaternion.identity);
                AudioSource.PlayClipAtPoint(_coinSoundEffect, hitInfo.point);

                SendAIToCoinSpot(hitInfo.point);
            }
        }
    }

    void SendAIToCoinSpot(Vector3 coinPos)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");

        foreach(var guard in guards)
        {
            NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentAnim = guard.GetComponent<Animator>();

            currentGuard.coinTossed = true;
            currentAgent.SetDestination(coinPos);
            currentAnim.SetBool("Walk", true);
            currentGuard.coinPos = coinPos;
        }
    }
}
