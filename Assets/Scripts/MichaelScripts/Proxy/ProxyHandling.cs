// Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyHandling : MonoBehaviour
{

    [SerializeField] private GameObject realWIN;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Instantiate(realWIN, transform.position + new Vector3(0, 5, 0), transform.rotation);

            Destroy(gameObject);
        }
    }
}
