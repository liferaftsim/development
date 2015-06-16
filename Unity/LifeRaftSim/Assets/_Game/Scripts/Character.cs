using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Character component.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The character's animator component.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.NavMeshAgent"/> instance.
        /// </summary>
        private NavMeshAgent navMeshAgent;

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
            this.animator = this
                .GetComponent<Animator>()
                .DisableIfNull(this, "animator")
                ;
            this.navMeshAgent = this
                .GetComponent<NavMeshAgent>()
                .DisableIfNull(this, "navMeshAgent")
                ;
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
                this.SetIsForwardCrawling(false);
                return;
            }

            this.IsInWater(true); // TODO change to raycasting at some point
            this.SetIsForwardCrawling(true); // TODO change to reflect movement according to surface, in water swimming, on raft crawling?

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

        /// <summary>
        /// Makes the character swim to the target destination.
        /// </summary>
        /// <param name="destination">
        /// The target destination to swim to.
        /// </param>
        /// <returns>
        /// An enumerator to iterate over the process of swimming to a target.
        /// </returns>
        public IEnumerable SwimTo(Vector3 destination)
        {
            this.SetIsForwardCrawling(true);
            this.navMeshAgent.SetDestination(destination);

            while (this.navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                this.SetIsForwardCrawling(false);
                yield return null;
            }

            yield return null;
        }

        /// <summary>
        /// Sets the "IsForwardCrawling" animation controller property to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// true if forward crawling; otherwise false.
        /// </param>
        private void SetIsForwardCrawling(bool value)
        {
            this.animator.SetBool("IsForwardCrawling", value);
        }

        /// <summary>
        /// Sets the "IsInWater" animation controller property to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// true if is in water; otherwise false.
        /// </param>
        private void IsInWater(bool value)
        {
            this.animator.SetBool("IsInWater", value);
        }
    }
}
