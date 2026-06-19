namespace GPUInstancing
{
    using UnityEngine;

    public class DemoRotate : MonoBehaviour
    {
        [SerializeField] private float _speed;
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, _speed * Time.deltaTime, 0);
        }
    }
}
