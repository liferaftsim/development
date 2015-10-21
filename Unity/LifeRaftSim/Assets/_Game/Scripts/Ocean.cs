using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Ocean component.
    /// </summary>
    public class Ocean : MonoBehaviour, IInteractable
    {
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
        /// Returns information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Interaction information.
        /// </returns>
        public IEnumerable<InteractionInfo> GetInteractions(Character character, Vector3 clickPoint)
        {
            yield return new InteractionInfo("Swim here", () => character.SwimTo(clickPoint));
        }
    }
}
