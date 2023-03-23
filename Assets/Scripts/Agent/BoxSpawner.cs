using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public int PositionRange;
    public int PoolSize = 25;
    public AnimationCurve ShowerCurve;
    public Transform Prefab;
    public int PoolLayer;
    private Pooling<Transform> _pool;

    private void Awake()
    {
        _pool = new Pooling<Transform>(Prefab, PoolSize, transform, PoolLayer);
    }
    [ContextMenu("Do")]
    public void Test()
    {
        SpawnBoxes(100);
    }
    public void SpawnBoxes(int quantity)
    {
        StopCoroutine(SpawnRoutine(0));
        StartCoroutine(SpawnRoutine(quantity));
    }

    int objCount = 0;
    float progress = 0.0f;
    
    IEnumerator SpawnRoutine(int quantity)
    {
        objCount = 0;
        progress = 0;
        float waitTime;
        while (progress < 1)
        {
            progress = Mathf.InverseLerp(0, quantity, objCount);
            waitTime = Mathf.Abs(ShowerCurve.Evaluate(progress));

            _pool.GetFromPool(transform.position + 
                new Vector3(Random.Range((float)-PositionRange, (float)PositionRange),
                Random.Range(-PositionRange, PositionRange),
                Random.Range(-PositionRange, PositionRange)), 
                Quaternion.identity).gameObject.SetActive(true);
            objCount += 1;
            yield return new WaitForSeconds(waitTime);
        }
        yield break;
    }

    public void ReturnToPool(Transform t)
    {
        _pool.ReturnToPool(t);
    }
    /// <summary>
    /// Draw a sphere with the position range 
    /// in the editor preview, when selected
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PositionRange);
    }
}
