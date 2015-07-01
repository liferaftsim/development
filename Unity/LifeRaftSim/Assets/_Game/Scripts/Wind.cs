using UnityEngine;

namespace Game
{
    /// <summary>
    /// Wind behaviour.
    /// </summary>
    public class Wind : MonoBehaviour
    {
        /// <summary>
        /// The speed the wind is rotated on the Y axis,
        /// </summary>
        [SerializeField]
        private float ySpeed = 0.1f;

        /// <summary>
        /// The amount the wind is rotated on the Y axis.
        /// </summary>
        [SerializeField]
        private float yStrength = 50.0f;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.Rotate();
        }

        /// <summary>
        /// Rotates the wind.
        /// </summary>
        private void Rotate()
        {
            this.transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time * this.ySpeed) * this.yStrength, 67.0f, 0.0f);
        }
    }
}
