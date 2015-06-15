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
        /// Moves the raft by a constant amount each update call.
        /// </summary>
        private void Move()
        {
            this.rigidbody.AddForceAtPosition(this.velocity, this.transform.position - this.velocity, ForceMode.Impulse);
        }
    }
}
