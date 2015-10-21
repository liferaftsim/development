using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Simulates bouyancy using rigidbody physics.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyBuoyancy : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.Rigidbody"/> instance.
        /// </summary>
        private new Rigidbody rigidbody;

        /// <summary>
        /// How up force is applied.
        /// </summary>
        [SerializeField]
        [Tooltip("How up force is applied.")]
        private ForceMode upForceMode = ForceMode.Impulse;

        /// <summary>
        /// Multiplier for the up force.
        /// </summary>
        [SerializeField]
        [Tooltip("Multiplier for the up force.")]
        private float upMultiplier = 3.0f;

        /// <summary>
        /// How drag force is applied.
        /// </summary>
        [SerializeField]
        [Tooltip("How drag force is applied.")]
        private ForceMode dragForceMode = ForceMode.Impulse;

        /// <summary>
        /// Multiplier for the drag force.
        /// </summary>
        [SerializeField]
        [Tooltip("Multiplier for the drag force.")]
        private float dragMultiplier = 0.1f;

        /// <summary>
        /// Called when the component is initialized.
        /// </summary>
        /// <remarks>
        /// Invoked by Unity.
        /// </remarks>
        private void Awake()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Stores the components for easy and fast access later on.
        /// </summary>
        private void CacheComponents()
        {
            this.rigidbody = this
                .GetComponent<Rigidbody>()
                .DisableIfNull(this, "rigidbody")
                ;
        }

        /// <summary>
        /// Called during the physics update.
        /// </summary>
        /// <remarks>
        /// Invoked by Unity.
        /// </remarks>
        private void FixedUpdate()
        {
            if (this.transform.position.y > 0.0f)
            {
                return;
            }

            // bouyancy
            var force = Vector3.up * -this.transform.position.y * Time.deltaTime * this.upMultiplier;
            var position = this.transform.position;
            this.rigidbody.AddForceAtPosition(force, position, this.upForceMode);

            // drag
            var drag = -this.rigidbody.velocity * this.dragMultiplier;
            this.rigidbody.AddForceAtPosition(drag, position, this.dragForceMode);
        }
    }
}
