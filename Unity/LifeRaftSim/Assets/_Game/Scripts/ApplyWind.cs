using UnityEngine;

namespace Game
{
    /// <summary>
    /// Applies wind force to the current game object.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ApplyWind : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.WindZone"/> instance.
        /// </summary>
        private WindZone windZone;

        /// <summary>
        /// Cached reference to the game object's <see cref="T:UnityEngine.Rigidbody"/> component.
        /// </summary>
        private new Rigidbody rigidbody;

        /// <summary>
        /// Cached reference to the game object's <see cref="T:UnityEngine.Renderer"/> component.
        /// </summary>
        private new Renderer renderer;

        /// <summary>
        /// The kind of force applied to the rigidbody.
        /// </summary>
        [SerializeField]
        private ForceMode forceMode = ForceMode.Impulse;

        /// <summary>
        /// Multiplier for wind force.
        /// </summary>
        [SerializeField]
        private float multiplier = 1.0f;

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
            this.AddForce();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void CacheComponents()
        {
            this.windZone = GameObject
                .FindObjectOfType<WindZone>()
                .DisableIfNull(this, "windZone")
                ;
            this.rigidbody = this
                .GetComponent<Rigidbody>()
                .DisableIfNull(this, "rigidbody")
                ;
        }

        /// <summary>
        /// Applies the force to the game object's <see cref="T:UnityEngine.Rigidbody"/>.
        /// </summary>
        private void AddForce()
        {
            var velocity = this.windZone.transform.forward * this.multiplier;
            this.rigidbody.AddForceAtPosition(velocity, this.transform.position - velocity, this.forceMode);
        }
    }
}
