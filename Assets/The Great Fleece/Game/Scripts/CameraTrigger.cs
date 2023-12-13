using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform _cameraUpdatePos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Trigger Activated");
            Camera.main.transform.position = _cameraUpdatePos.transform.position;
            Camera.main.transform.rotation = _cameraUpdatePos.transform.rotation;
        }
    }
}
