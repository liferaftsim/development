using UnityEngine;

namespace Game
{
    /// <summary>
    /// Applies the X and Z components of the target position to the game object this component is attached to.
    /// </summary>
    public class FollowXZPosition : MonoBehaviour
    {
#pragma warning disable 0649 // disable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// The target to follow.
        /// </summary>
        [SerializeField]
        private Transform targetTransform;

#pragma warning restore 0649 // reenable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// Updates the position relative to the target.
        /// </summary>
        /// <remarks>
        /// This method is called by Unity.
        /// </remarks>
        private void Update()
        {
            if (this.targetTransform == null)
            {
                Debug.LogError("targetTransform is null");
                return;
            }

            var position = this.targetTransform.position;
            position.y = this.transform.position.y;
            this.transform.position = position;
        }
    }
}
