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
        /// Name of the game object that represents the ocean surface.
        /// The Y position of this game object is used as ocean surface position.
        /// </summary>
        [SerializeField]
        private string waterGameObjectName = "Water";

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
        /// The ocean surface game object.
        /// </summary>
        private Transform water;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheWater();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.MoveY();
        }

        /// <summary>
        /// Finds and stores the ocean surface game object for later reference.
        /// </summary>
        private void CacheWater()
        {
            var waterGameObject = GameObject.Find(this.waterGameObjectName);
            if (waterGameObject == null)
            {
                Debug.LogError(this.name + ": No game object named \"" + this.waterGameObjectName + "\" found.");
                this.enabled = false;
                return;
            }
            this.water = waterGameObject.transform;
        }

        /// <summary>
        /// Positions the game object to match the ocean surface.
        /// </summary>
        private void MoveY()
        {
            var position = this.transform.position;
            position.y = this.water.position.y + this.yOffset + Mathf.Cos(Time.time * this.yConsineSpeed) * this.yConsineMultiplier;
            this.transform.position = position;
        }
    }
}
