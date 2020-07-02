using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCArea : MonoBehaviour
{
    private List<Transform> ActivedDestinations;
    private WaitForSeconds CheckDelayTime;
    private Coroutine CheckDestinationCoroutine;

    [SerializeField]
    private float FloatCheckDelayTime;

    [Header("Search Range")]
    [Tooltip("Search Range")]
    [Range(40,150)]
    public float Radius;

    private void Awake()
    {
        ActivedDestinations = new List<Transform>();
        CheckDelayTime = new WaitForSeconds(FloatCheckDelayTime);
    }

    private void CheckDestination()
    {
        Collider[] SearchedObject = Physics.OverlapSphere(transform.position, Radius);

        ActivedDestinations.Clear();

        if (SearchedObject.Length == 0) return;

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
