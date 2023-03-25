using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FixedRope : MonoBehaviour
{

    [SerializeField] private int _segmentCount = 10;
    [SerializeField] private GameObject _segmentPrefab;
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _endTransform;
    //[SerializeField] 
    private List<GameObject> _segments;
    private LineRenderer _lineRenderer;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount+1;
        InstantiateSegments();

    }

    private void Update()
    {
        for (int i = 0; i < _segmentCount; i++)
            _lineRenderer.SetPosition(i, _segments[i].transform.position);
        _lineRenderer.SetPosition(_segmentCount, _endTransform.position);

    }
    private void InstantiateSegments()
    {
        _segments = new List<GameObject>();
        for (int i = 0; i < _segmentCount; i++)
        {
            _segments.Add(Instantiate(_segmentPrefab,transform));
            _segments[i].SetActive(true);
            _segments[i].transform.position = Vector3.Lerp(_startTransform.position, _endTransform.position, (float)i / (float)_segmentCount);
            
            if(i == 0)
                _segments[i].GetComponent<FixedJoint>().connectedBody = _startTransform.GetComponent<Rigidbody>(); 
            //else if (i == _segmentCount-1)
                //_segments[i].GetComponent<SpringJoint>().connectedBody = _endTransform.GetComponent<Rigidbody>();
            else
                _segments[i].GetComponent<FixedJoint>().connectedBody = _segments[i-1].GetComponent<Rigidbody>();



        }
        _endTransform.GetComponent<FixedJoint>().connectedBody = _segments[_segmentCount-1].GetComponent<Rigidbody>();

    }
}