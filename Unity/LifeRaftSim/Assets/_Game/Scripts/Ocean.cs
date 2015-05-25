using UnityEngine;
using System.Collections;

namespace Game
{
    public class Ocean : MonoBehaviour
    {
        /// <summary>
        /// Name of the game object that represents the ocean surface.
        /// The Y position of this game object is used as ocean surface position.
        /// </summary>
        [SerializeField]
        private string waterGameObjectName = "Water";

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
            return this.water.position.y;
        }
    } 
}
