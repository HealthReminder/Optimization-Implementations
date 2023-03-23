using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{

    [SerializeField]private float _circleRadius = 5;
    [SerializeField]private int _poolSize = 25;
    [SerializeField]private float _showerDuration;
    [SerializeField] private AnimationCurve _showerCurve;
    [SerializeField] private Transform _prefab;
    [SerializeField] private int _poolLayer;
    private Pooling<Transform> _pool;

    private void Awake()
    {
        _pool = new Pooling<Transform>(_prefab, _poolSize, transform, _poolLayer);
    }
   
    [ContextMenu("Spawn objects")]
    public void Spawn()
    {
        StopCoroutine(SpawnRoutine());
        StartCoroutine(SpawnRoutine());
    }
    float time = 0.0f;
    float progress = 0.0f;
    public void ReturnToPool (Transform t)
    {
        _pool.ReturnToPool(t);
    }
    IEnumerator SpawnRoutine()
    {
        time = 0.0f;
        progress = 0.0f;
        float waitTime;
        while(progress < 1)
        {
            SpawnRandom();
            progress = Mathf.InverseLerp(0, _showerDuration, time);
            waitTime = Mathf.Abs(_showerCurve.Evaluate(progress));
            time += waitTime;
            yield return new WaitForSeconds(waitTime);
        }
        yield break;
    }

    //[ContextMenu("Instantiate Lots")]
    //private void InstantiateLots()
    //{
    //    for (int i = 0; i < 30; i++)
    //        InstantiateRandom();
    //}

    /// <summary>
    /// Instantiate an object in a random position in the circle
    /// </summary>
    public void SpawnRandom()
    {
        float angleInCircle = Random.Range(0f, 360f);
        Vector3 spawnPosition = transform.position + (Quaternion.Euler(0f, angleInCircle, 0f) * Vector3.forward * _circleRadius);
        _pool.GetFromPool(spawnPosition, Quaternion.identity).gameObject.SetActive(true);
    }

    /// <summary>
    /// Draw a sphere with the circle radius 
    /// in the editor preview, when selected
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _circleRadius);
    }
}
