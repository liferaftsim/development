using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Behaviour script for life raft.
    /// </summary>
    public class LifeRaft : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Cached reference to the <see cref="T:Game.Character"/> instance.
        /// </summary>
        private Character character;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Caches the components used in this script for quick use later on.
        /// </summary>
        private void CacheComponents()
        {
            this.character = GameObject
                .FindObjectOfType<Character>()
                .DisableIfNull(this, "character")
                ;
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
