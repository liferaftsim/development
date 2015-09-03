using System.Collections;
using System.Linq;
using UnityContrib.UnityEngine;
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
        private HumanAnimationController animationController;

        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.NavMeshAgent"/> instance.
        /// </summary>
        private NavMeshAgent navMeshAgent;

        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.Rigidbody"/> instance.
        /// </summary>
        private new Rigidbody rigidbody;

        /// <summary>
        /// The amount moved before recalculating path.
        /// </summary>
        [SerializeField]
        private float recalculatePathMovementTreshold = 1.0f;

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
            this.animationController = this
                .GetComponent<HumanAnimationController>()
                .DisableIfNull(this, "animator")
                ;
            this.navMeshAgent = this
                .GetComponent<NavMeshAgent>()
                .DisableIfNull(this, "navMeshAgent")
                ;
            this.rigidbody = this
                .GetComponent<Rigidbody>()
                .DisableIfNull(this, "rigidbody")
                ;
        }

        private void Update()
        {
            //var isInwater = this.transform.IsInWater();
            //this.navMeshAgent.enabled = isInwater;
            //this.rigidbody.useGravity = !isInwater;
            //this.rigidbody.constraints
            //    = isInwater
            //    ? RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY
            //    : RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ
            //    ;
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
            this.animationController.SetIsForwardCrawling(true);
            this.navMeshAgent.ResetPath();
            this.navMeshAgent.SetDestination(destination);

            while (this.navMeshAgent.IsNavigating())
            {
                yield return null;
            }

            this.animationController.SetIsForwardCrawling(false);
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
            this.animationController.SetIsForwardCrawling(true);

            var targetRigidbody = targetTransform.GetComponent<Rigidbody>();
            var absolutePoint = targetRigidbody.ClosestPointOnBounds(this.transform.position);
            var relativePoint = absolutePoint - targetTransform.position;
            var minimumDistance = Mathf.Abs(relativePoint.magnitude);
            var startPosition = this.transform.position;

            do
            {
                var distance = Vector3.Distance(startPosition, targetTransform.position);
                if (distance <= minimumDistance)
                {
                    this.navMeshAgent.ResetPath();
                    break;
                }
                else if (distance > this.recalculatePathMovementTreshold)
                {
                    var targetPosition = targetTransform.position;
                    var origin = this.transform.position;
                    var direction = targetPosition - origin;
                    var hits = Physics.RaycastAll(origin, direction);
                    var hit = hits.FirstOrDefault(h => h.collider.transform.parent.parent == targetTransform);
                    if(hit.collider == null)
                    {
                        Debug.LogError("Cannot raycast to target.");
                        break;
                    }

                    var destination = new Vector3(
                        hit.point.x - direction.x * 0.1f,
                        hit.point.y,
                        hit.point.z - direction.z * 0.1f
                        );

                    this.navMeshAgent.ResetPath();
                    var success = this.navMeshAgent.SetDestination(destination);
                    if (!success)
                    {
                        Debug.LogWarning("path not found");
                        break;
                    }
                }

                yield return null;
            }
            while (this.navMeshAgent.IsNavigating());

            this.animationController.SetIsForwardCrawling(false);
            yield return null;
        }

        /// <summary>
        /// Interaction to swim to the life raft and board it.
        /// </summary>
        /// <returns>
        /// Array of interaction information.
        /// </returns>
        public IEnumerator Board(Transform target)
        {
            // swim to the raft, by passing reference character will make sure to update the target position
            foreach (var x in this.SwimTo(target))
            {
                yield return x;
            }

            // board animation
            this.navMeshAgent.enabled = false;
            this.rigidbody.isKinematic = true;

            this.transform.SetParent(target);
            var end = Time.time + 2.0f;
            while (Time.time < end)
            {
                yield return null;
            }

            this.animationController.SetIsInWater(false);
            this.animationController.SetIsForwardCrawling(false);
            this.animationController.SetIsSitting(true);
            yield return null;

            var origin = target.position + Vector3.up * 2.0f;
            var direction = Vector3.down;
            var distance = 5.0f;
            var hits = Physics.RaycastAll(origin, direction, distance);
            var hit = hits.FirstOrDefault(h => h.collider.transform.parent.parent == target);
            var y = hit.point.y;

            origin = hit.point;
            origin.y += 0.1f;
            direction = this.transform.position - origin;
            hits = Physics.RaycastAll(origin, direction, distance);
            hit = hits.FirstOrDefault(h => h.collider.transform.parent.parent == target);

            this.transform.position = new Vector3(
                hit.point.x - direction.x * 0.15f,
                y,
                hit.point.z - direction.z * 0.15f
                );

            yield return null;
        }
    }
}
