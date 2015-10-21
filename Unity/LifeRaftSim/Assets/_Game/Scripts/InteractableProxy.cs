using System.Collections.Generic;
using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Proxy <see cref="T:Game.IInteractable"/> implementation.
    /// Good for object children who's parent is an interactable.
    /// </summary>
    public class InteractableProxy : MonoBehaviour, IInteractable
    {
#pragma warning disable 0649 // disable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        [SerializeField]
        private Transform targetInteractableTransform;

#pragma warning restore 0649 // reenable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// Cached reference to the target <see cref="T:Game.IInteractable"/> instance.
        /// </summary>
        private IInteractable targetInteractable;

        public Transform TargetInteractableTransform
        {
            get
            {
                return targetInteractableTransform;
            }

            set
            {
                this.targetInteractableTransform = value;
            }
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Stores the components for easy and fast access later on.
        /// </summary>
        private void CacheComponents()
        {
            this.targetInteractable = this.targetInteractableTransform
                .GetComponent<IInteractable>()
                .DisableIfNull(this, "targetInteractable")
                ;
        }

        /// <summary>
        /// Returns information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Interaction information.
        /// </returns>
        public IEnumerable<InteractionInfo> GetInteractions(Character character, Vector3 clickPoint)
        {
            return this.targetInteractable.GetInteractions(character, clickPoint);
        }
    }
}
