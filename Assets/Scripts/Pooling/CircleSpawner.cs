using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{

    public float CircleRadius = 5;
    public int PoolSize = 25;
    public float ShowerDuration;
    public AnimationCurve ShowerCurve;
    public Transform Prefab;
    public int PoolLayer;
    private Pooling<Transform> _pool;

    private void Awake()
    {
        _pool = new Pooling<Transform>(Prefab, PoolSize, transform, PoolLayer);
    }
   
    [ContextMenu("Do Shower")]
    public void InstantiateTimed()
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
            InstantiateRandom();
            progress = Mathf.InverseLerp(0, ShowerDuration, time);
            waitTime = Mathf.Abs(ShowerCurve.Evaluate(progress));
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
    public void InstantiateRandom()
    {
        float angleInCircle = Random.Range(0f, 360f);
        Vector3 spawnPosition = transform.position + (Quaternion.Euler(0f, angleInCircle, 0f) * Vector3.forward * CircleRadius);
        _pool.GetFromPool(spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Draw a sphere with the circle radius 
    /// in the editor preview, when selected
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CircleRadius);
    }
}
