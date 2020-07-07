using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCArea : MonoBehaviour
{
    private List<Transform> ActivedDestinations;// 영역안의 목적지들
    private WaitForSeconds CheckDelayTime;    
    private Coroutine CheckDestinationCoroutine;

    [SerializeField]
    [Tooltip("value 마다 체크함")]
    private float FloatCheckDelayTime;

    [Header("Search Range")]
    [Tooltip("Search Range")]
    [Range(40,150)]
    //Area 의 영역 크기
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
    //에디터 상에서 Area의 크기를 보기위함
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
#endif
}
