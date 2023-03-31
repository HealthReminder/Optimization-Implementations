using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HingeRope : MonoBehaviour
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
        _lineRenderer.positionCount = _segmentCount + 1;
        InstantiateSegments();

    }

    private void Update()
    {
        for (int i = 0; i < _segmentCount; i++)
            _lineRenderer.SetPosition(i, _segments[i].transform.position);
        _lineRenderer.SetPosition(_segmentCount, _endTransform.position);

    }
    /// <summary>
    /// Directions of the hinge limits
    /// </summary>
    Vector3[] directions = new Vector3[] {
        new Vector3(1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 1, 0)};
    private void InstantiateSegments()
    {
        _segments = new List<GameObject>();
        int dir = 0;
        for (int i = 0; i < _segmentCount; i++)
        {
            _segments.Add(Instantiate(_segmentPrefab, transform));
            GameObject segment = _segments[i];
            HingeJoint joint = segment.GetComponent<HingeJoint>();
            segment.SetActive(true);
            segment.transform.position = Vector3.Lerp(_startTransform.position, _endTransform.position, (float)i / (float)_segmentCount);
            //joint.anchor = segment.transform.position;
            if (i == 0)
            //{
                joint.connectedBody = _startTransform.GetComponent<Rigidbody>();
            //    joint.connectedAnchor = _startTransform.position;

            //}
            else
            //{
                joint.connectedBody = _segments[i - 1].GetComponent<Rigidbody>();
                //joint.connectedAnchor = _segments[i - 1].transform.position;
            //}

            joint.axis = directions[dir];


            dir++;
            dir = (dir >= directions.Length) ? 0 : dir;

        }
        _endTransform.GetComponent<HingeJoint>().connectedBody = _segments[_segmentCount - 1].GetComponent<Rigidbody>();

    }
}