using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Controls the camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
#pragma warning disable 0649 // disable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        [SerializeField]
        private BaseSelector targetSelector;

#pragma warning restore 0649 // disable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// Target that the ocean must surround.
        /// </summary>
        private Transform target;

        /// <summary>
        /// The offset between the player and the camera.
        /// </summary>
        private Vector3 offset;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.target = this.targetSelector.Selected;
            this.CalculateOffset();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.target = this.targetSelector.Selected;
            this.Move();
        }

        /// <summary>
        /// Calculates the offset between the player and the camera.
        /// </summary>
        private void CalculateOffset()
        {
            this.offset = this.transform.position - this.target.position;
        }

        /// <summary>
        /// Updates the position of the camera relative to the position of the player.
        /// </summary>
        private void Move()
        {
            var targetPosition = this.target.position;
            targetPosition.y = 0.0f;
            this.transform.position = targetPosition + this.offset;
        }
    }
}
