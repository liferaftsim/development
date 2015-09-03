using System.Collections;
using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Ocean component.
    /// </summary>
    public class Ocean : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Cached reference to the <see cref="T:Game.Character"/> instance.
        /// </summary>
        private Character character;

        /// <summary>
        /// Cached reference to the <see cref="T:Game.PlayerInput"/> instance.
        /// </summary>
        private PlayerInput playerInput;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Caches the component dependencies for quick access later.
        /// </summary>
        private void CacheComponents()
        {
            this.character = GameObject
                .FindObjectOfType<Character>()
                .DisableIfNull(this, "character")
                ;
            this.playerInput = GameObject
                .FindObjectOfType<PlayerInput>()
                .DisableIfNull(this, "playerInput")
                ;
        }

        /// <summary>
        /// Returns the ocean surface y position at the specified <paramref name="position"/>.
        /// </summary>
        /// <param name="position">
        /// Where on the ocean to get the surface height from.
        /// The Y component is not used.
        /// </param>
        /// <returns>
        /// The height (y position) of the ocean surface.
        /// </returns>
        public float GetHeight(Vector3 position)
        {
            return this.transform.position.y;
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
                new InteractionInfo("Swim here", this.SwimHere),
            };
        }

        /// <summary>
        /// Interaction to swim to the location.
        /// </summary>
        private IEnumerator SwimHere()
        {
            foreach (var x in this.character.SwimTo(this.playerInput.LastHit.point))
            {
                yield return x;
            }
        }
    }
}
