using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverCutscene;
    [SerializeField] private Animator _anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MeshRenderer render = GetComponent<MeshRenderer>();
            Color color = new Color(0.596f, 0.129f, 0.129f, 100.039f);
            render.material.SetColor("_TintColor", color);

            _anim.enabled = false;
            StartCoroutine(AlertRoutine());
        }
    }

    IEnumerator AlertRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _gameOverCutscene.SetActive(true);
    }
}
