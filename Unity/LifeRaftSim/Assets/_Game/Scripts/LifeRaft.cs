using UnityEngine;

namespace Game
{
    /// <summary>
    /// Behaviour script for life raft.
    /// </summary>
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
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.Rotate();
            this.Move();
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
            this.transform.position += this.velocity * Time.deltaTime;
        }
    } 
}
