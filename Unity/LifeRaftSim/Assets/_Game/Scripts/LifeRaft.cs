using UnityEngine;

namespace Game
{
    /// <summary>
    /// Behaviour script for life raft.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class LifeRaft : MonoBehaviour
    {
        /// <summary>
        /// The minimum amount of random rotation.
        /// A negative amount will make it rotate the other way around.
        /// </summary>
        public float minimumRandomRotation = -1.0f;

        /// <summary>
        /// The maximum amount of random rotation.
        /// A negative amount will make it rotate the other way around.
        /// </summary>
        public float maximumRandomRotation = 1.0f;

        /// <summary>
        /// The direction and speed of the raft.
        /// </summary>
        public Vector3 velocity;

        /// <summary>
        /// Cached reference to the game object's <see cref="T:UnityEngine.Rigidbody"/> component.
        /// </summary>
        private new Rigidbody rigidbody;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.Rotate();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void FixedUpdate()
        {
            this.Move();
        }

        /// <summary>
        /// Caches the components used in this script for quick use later on.
        /// </summary>
        private void CacheComponents()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Rotates the raft by a random amount each update call.
        /// </summary>
        private void Rotate()
        {
            var randomRotation = Random.Range(this.minimumRandomRotation, this.maximumRandomRotation);
            this.transform.Rotate(0.0f, randomRotation * Time.deltaTime, 0.0f);
        }

        /// <summary>
        /// Moves the raft by a constant amount each update call.
        /// </summary>
        private void Move()
        {
            this.rigidbody.AddForce(this.velocity, ForceMode.Impulse);
        }
    }
}
