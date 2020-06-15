using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MNPCArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == nameof(ETag.Destination))
            other.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == nameof(ETag.Destination))
            other.gameObject.SetActive(false);
    }
}
