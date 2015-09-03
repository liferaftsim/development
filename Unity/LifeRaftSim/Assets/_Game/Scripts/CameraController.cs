using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// Tag name of the target that the ocean must surround.
        /// </summary>
        [SerializeField]
        private string targetTagName = "Player";

        /// <summary>
        /// Target that the ocean must surround.
        /// </summary>
        private Transform target;

        private Vector3 offset;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheTarget();
            this.CalculateOffset();
        }

        private void Update()
        {
            this.Move();
        }

        private void CalculateOffset()
        {
            this.offset = this.transform.position - this.target.position;
        }

        /// <summary>
        /// Finds the target and caches it for continues use.
        /// </summary>
        private void CacheTarget()
        {
            var targetGameObject = GameObject
                .FindGameObjectWithTag(this.targetTagName)
                .DisableIfNull(this, "targetGameObject")
                ;
            this.target = targetGameObject.transform;
        }

        private void Move()
        {
            var targetPosition = this.target.position;
            targetPosition.y = 0.0f;
            this.transform.position = targetPosition + this.offset;
        }
    }
}
