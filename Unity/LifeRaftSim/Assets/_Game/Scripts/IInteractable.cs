using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Defines the interface for interactable objects.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Returns information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Interaction information.
        /// </returns>
        IEnumerable<InteractionInfo> GetInteractions(Character character, Vector3 clickPoint);
    }
}
