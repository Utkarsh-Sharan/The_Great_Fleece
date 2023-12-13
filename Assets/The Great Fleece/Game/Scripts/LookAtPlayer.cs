using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_playerTransform);
    }
}
