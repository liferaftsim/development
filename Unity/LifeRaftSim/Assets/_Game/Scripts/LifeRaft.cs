using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Behaviour script for life raft.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class LifeRaft : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Cached reference to the <see cref="T:Game.Character"/> instance.
        /// </summary>
        private Character character;

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
            this.rigidbody = this
                .GetComponent<Rigidbody>()
                .DisableIfNull(this, "rigidbody")
                ;
            this.character = GameObject
                .FindObjectOfType<Character>()
                .DisableIfNull(this, "character")
                ;
        }

        /// <summary>
        /// Moves the raft by a constant amount each update call.
        /// </summary>
        private void Move()
        {
            this.rigidbody.AddForceAtPosition(this.velocity, this.transform.position - this.velocity, ForceMode.Impulse);
        }

        /// <summary>
        /// Returns an array of information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Array of interaction information.
        /// </returns>
        public InteractionInfo[] GetInteractions()
        {
            return new[]
            {
                new InteractionInfo("Board", this.Board),
            };
        }

        /// <summary>
        /// Interaction to swim to the life raft and board it.
        /// </summary>
        /// <returns>
        /// Array of interaction information.
        /// </returns>
        private IEnumerator Board()
        {
            // swim to the raft, by passing reference character will make sure to update the target position
            foreach (var x in this.character.SwimTo(this.transform))
            {
                yield return x;
            }

            // board animation
            //this.character.Board(this.transform);
        }
    }
}
