using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    [SerializeField] private GameObject _winCutsceneActivation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && GameManager.Instance.HasCard == true)
        {
            _winCutsceneActivation.SetActive(true);

            if(GameManager.Instance.HasCard == false)
            {
                Debug.Log("You MUST have the key card.");
            }
        }
    }
}
