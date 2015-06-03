using UnityEngine;

namespace Game
{
    /// <summary>
    /// Character component.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The character's animator component.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// The target position.
        /// </summary>
        private Vector3 target;

        /// <summary>
        /// Speed multiplier for when character is crawling.
        /// </summary>
        private float forwardCrawlSpeed = 1.0f;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Awake()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.Move();
        }

        /// <summary>
        /// Stores the components for easy and fast access later on.
        /// </summary>
        private void CacheComponents()
        {
            this.animator = this.GetComponent<Animator>();
        }

        /// <summary>
        /// Positions the character.
        /// </summary>
        private void Move()
        {
            var currentPosition = this.transform.position;
            var targetPosition = this.target;

            currentPosition.y
                = target.y
                = 0.0f;

            var distance = Vector3.Distance(currentPosition, targetPosition);
            if (distance < 2.0f)
            {
                this.animator.SetBool("IsForwardCrawling", false);
                return;
            }

            this.animator.SetBool("IsInWater", true); // TODO change to raycasting at some point
            this.animator.SetBool("IsForwardCrawling", true); // TODO change to reflect movement according to surface, in water swimming, on raft crawling?

            this.transform.forward = this.target - this.transform.position;
            this.transform.position += this.transform.forward * this.forwardCrawlSpeed * Time.deltaTime;
        }

        /// <summary>
        /// Assign a new target position.
        /// </summary>
        /// <param name="position">
        /// The new target position.
        /// </param>
        public void SetTargetDestination(Vector3 position)
        {
            this.target = position;
        }
    }
}
