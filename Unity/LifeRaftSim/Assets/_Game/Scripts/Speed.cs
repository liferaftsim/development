using UnityEngine;

namespace Game
{
    /// <summary>
    /// Calculates the speed of the game object the component is attached to.
    /// </summary>
    public class Speed : MonoBehaviour
    {
        /// <summary>
        /// The current speed measured in kilometers per hour.
        /// </summary>
        public float kmh;

        /// <summary>
        /// The current speed measured in mile per hour.
        /// </summary>
        public float mph;

        /// <summary>
        /// The maximum speed measured in kilometers per hour.
        /// </summary>
        public float maxKmh;

        /// <summary>
        /// The maximum speed measured in miles per hour.
        /// </summary>
        public float maxMph;

        /// <summary>
        /// The last know position.
        /// Used to calculate the distance traveled since last update.
        /// </summary>
        private Vector3 lastKnownPosition;

        /// <summary>
        /// Stores the initial last known position.
        /// </summary>
        private void Start()
        {
            this.lastKnownPosition = this.transform.position;
        }

        /// <summary>
        /// Calculates the current and maximum speed.
        /// </summary>
        private void Update()
        {
            var distance = Vector3.Distance(this.transform.position, this.lastKnownPosition);
            this.kmh = distance * Time.deltaTime * 60.0f * 60.0f;
            this.mph = this.kmh * 0.621371f;
            this.maxKmh = Mathf.Max(this.maxKmh, this.kmh);
            this.maxMph = Mathf.Max(this.maxMph, this.mph);
            this.lastKnownPosition = this.transform.position;
        }
    }
}
