using UnityEngine;

namespace DefaultNamespace
{
    public class SimulatedObjectLogic : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public void Launch(Vector3 force)
        {
            GetRigidbody().velocity = Vector3.zero;
            GetRigidbody().AddForce(force);
        }

        private Rigidbody GetRigidbody()
        {
            return _rigidbody ? _rigidbody : _rigidbody = GetComponent<Rigidbody>();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}