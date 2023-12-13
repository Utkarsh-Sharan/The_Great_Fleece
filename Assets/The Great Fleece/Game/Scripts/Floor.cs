using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private AudioClip _coinSoundEffect;

    private bool _hasTossedCoin;
    private Animator _playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GameObject.Find("Player").GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && _hasTossedCoin == false)
        {
            RaycastHit hitInfo;
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                _playerAnim.SetTrigger("Throw");
                _hasTossedCoin = true;
                Instantiate(_coinPrefab, hitInfo.point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_coinSoundEffect, hitInfo.point);

                SendAIToCoinSpot(hitInfo.point);
            }
        }
    }

    void SendAIToCoinSpot(Vector3 coinPos)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");

        foreach (var guard in guards)
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
