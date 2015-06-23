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
        /// Cached reference to the <see cref="T:UnityEngine.Animator"/> instance.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.NavMeshAgent"/> instance.
        /// </summary>
        private NavMeshAgent navMeshAgent;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Awake()
        {
            this.CacheComponents();
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

            while (this.navMeshAgent.IsNavigating())
            {
                yield return null;
            }

            this.SetIsForwardCrawling(false);
            yield return null;
        }

        /// <summary>
        /// Makes the character swim to the target <see cref="T:UnityEngine.Transform"/>.
        /// </summary>
        /// <param name="targetTransform">
        /// The target destination to swim to.
        /// </param>
        /// <returns>
        /// An enumerator to iterate over the process of swimming to a target.
        /// </returns>
        public IEnumerable SwimTo(Transform targetTransform)
        {
            this.SetIsForwardCrawling(true);

            var nextPathReplan = 0.0f;

            var targetRigidbody = targetTransform.GetComponent<Rigidbody>();
            var absolutePoint = targetRigidbody.ClosestPointOnBounds(this.transform.position);
            var relativePoint = absolutePoint - targetTransform.position;
            var minimumDistance = Mathf.Abs(relativePoint.magnitude);

            do
            {
                if (nextPathReplan < Time.time)
                {
                    var targetPosition = targetTransform.position;
                    var direction = targetPosition - this.transform.position;
                    direction.Normalize();

                    var distance = Vector3.Distance(this.transform.position, targetPosition);
                    if (distance <= minimumDistance)
                    {
                        this.navMeshAgent.ResetPath();
                        break;
                    }

                    var success = this.navMeshAgent.SetDestination(targetPosition - direction);
                    if (!success)
                    {
                        Debug.LogWarning("path not found");
                        break;
                    }

                    nextPathReplan = Time.time + 2.0f;
                }

                yield return null;
            }
            while (this.navMeshAgent.IsNavigating());

            this.SetIsForwardCrawling(false);
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
