using UnityEngine;

namespace Game
{
    /// <summary>
    /// Very simple non-physics based buoyancy behaviour used for hero, items and debris.
    /// </summary>
    public class Buoyancy : MonoBehaviour
    {
        /// <summary>
        /// Additional height offset relative to the ocean surface.
        /// </summary>
        [SerializeField]
        private float yOffset = 0.0f;

        /// <summary>
        /// Speed of the cosine function.
        /// The cosine is used to add noise to the buoyancy.
        /// This affects how fast the noise is moving the game object up and down.
        /// </summary>
        [SerializeField]
        private float yConsineSpeed = 1.0f;

        /// <summary>
        /// Multiplier of the cosine function result.
        /// The cosine is used to add noise to the buoyancy.
        /// This affects the min and max of the added y position.
        /// </summary>
        [SerializeField]
        private float yConsineMultiplier = 1.0f;

        /// <summary>
        /// Ocean component used for getting the Y position of the ocean surface.
        /// </summary>
        private Ocean ocean;

        /// <summary>
        /// Gets or sets the additional height offset relative to the ocean surface.
        /// </summary>
        public float YOffset
        {
            get
            {
                return this.yOffset;
            }
            set
            {
                this.yOffset = value;
            }
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheOcean();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.MoveY();
        }

        /// <summary>
        /// Finds and stores the ocean behaviour for later reference.
        /// </summary>
        private void CacheOcean()
        {
            this.ocean = GameObject
                .FindObjectOfType<Ocean>()
                .DisableIfNull(this, "ocean")
                ;
        }

        /// <summary>
        /// Positions the game object to match the ocean surface.
        /// </summary>
        private void MoveY()
        {
            var oceanSurfaceHeight = this.ocean.GetHeight(this.transform.position);
            var position = this.transform.position;
            position.y = oceanSurfaceHeight + this.yOffset + Mathf.Cos(Time.time * this.yConsineSpeed) * this.yConsineMultiplier;
            this.transform.position = position;
        }
    }
}
