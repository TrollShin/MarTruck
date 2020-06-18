using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCArea : MonoBehaviour
{
    private List<Transform> ActivedDestinations;
    private WaitForSeconds CheckDelayTime = new WaitForSeconds(3f);
    private Coroutine CheckDestinationCoroutine;

    [Header("Search Range")]
    public float Radius;

    private void Awake()
    {
        ActivedDestinations = new List<Transform>();
    }

    private void CheckDestination()
    {
        Collider[] SearchedObject = Physics.OverlapSphere(transform.position, Radius);

        ActivedDestinations.Clear();

        for(int i = 0; i < SearchedObject.Length; i++)
        {
            if(SearchedObject[i].tag == nameof(ETag.Destination))
            {
            ActivedDestinations.Add(SearchedObject[i].transform);
            }
        }
    }

    public Transform[] GetActivedDestinations()
    {
        return ActivedDestinations.ToArray().Clone() as Transform[];
    }

    IEnumerator CCheckDestination()
    {
        CheckDestination();

        yield return CheckDelayTime;

        CheckDestinationCoroutine = StartCoroutine(CCheckDestination());
    }

    public void StartCoroutine()
    {
        CheckDestinationCoroutine = StartCoroutine(CCheckDestination());
    }

    public void StopCoroutine()
    {
        StopCoroutine(CheckDestinationCoroutine);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
#endif
}
