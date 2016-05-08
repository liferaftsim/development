using System.Collections;
using System.Collections.Generic;
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
        /// The amount moved before recalculating path.
        /// </summary>
        [SerializeField]
        private float recalculatePathMovementTreshold = 0.5f;

        /// <summary>
        /// The current vehicle of the character.
        /// </summary>
        public Vehicle CurrentVehicle
        {
            get;
            set;
        }

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
        public IEnumerator SwimTo(Vector3 destination)
        {
            using (new TimelineLog(this.name, "Character.Interaction.SwimTo", "SwimTo"))
            {
                yield return this.ExitVehicle();

                this.animationController.SetIsForwardCrawling(true);

                this.navMeshAgent.ResetPath();
                yield return null;

                this.navMeshAgent.SetDestination(destination);
                yield return null; // required or IsNavigating will not always work

                while (this.navMeshAgent.IsNavigating())
                {
                    yield return null;
                }

                //Debug.Log(
                //    "haspath:" + this.navMeshAgent.hasPath +
                //    " pathpending:" + navMeshAgent.pathPending +
                //    " destresult:" + result +
                //    " status:" + navMeshAgent.pathStatus +
                //    " isstale:" + navMeshAgent.isPathStale +
                //    " dest:" + navMeshAgent.destination +
                //    " pos: " + navMeshAgent.transform.position +
                //    " corners:" + navMeshAgent.path.corners.Length +
                //    " velocity:" + navMeshAgent.velocity
                //    );

                this.animationController.SetIsForwardCrawling(false);
                yield return null;
            }
            Debug.Log("Swim to end");
        }

        public IEnumerator Navigate(Transform target)
        {
            using (new TimelineLog(this.name, "Character.Interaction.Navigate", "Navigate"))
            {
                var targetRigidbody = target.GetComponent<Rigidbody>();
                var absolutePoint = targetRigidbody.ClosestPointOnBounds(this.transform.position);
                var relativePoint = absolutePoint - target.position;
                var minimumDistance = Mathf.Abs(relativePoint.magnitude);
                var startPosition = this.transform.position;

                IEnumerator navigate = null;
                var lastTargetPosition = target.position + target.forward * recalculatePathMovementTreshold * 2.0f;
                var absoluteEndTime = Time.time + 30.0f;

                do
                {
                    var distance = Vector3.Distance(startPosition, target.position);
                    if (distance <= minimumDistance)
                    {
                        this.navMeshAgent.SetDestination(this.transform.position);
                        this.navMeshAgent.ResetPath();
                        break;
                    }

                    distance = Vector3.Distance(lastTargetPosition, target.position);
                    if (distance > this.recalculatePathMovementTreshold)
                    {
                        var targetPosition
                            = lastTargetPosition
                            = target.position
                            ;
                        var origin = this.transform.position;
                        var direction = targetPosition - origin;
                        var hits = Physics.RaycastAll(origin, direction);
                        var hit = hits.FirstOrDefault(h => h.collider.transform.parent.parent == target);
                        if (hit.collider == null)
                        {
                            Debug.LogError("Cannot raycast to target.");
                            break;
                        }

                        var destination = new Vector3(
                            hit.point.x - direction.x * 0.1f,
                            hit.point.y,
                            hit.point.z - direction.z * 0.1f
                            );

                        navigate = this.SwimTo(destination);
                        yield return null;
                    }
                    else if (navigate != null)
                    {
                        if (!navigate.MoveNext())
                        {
                            break;
                        }

                        yield return navigate.Current;
                    }
                }
                while (absoluteEndTime > Time.time);
            }
        }

        public IEnumerator ExitVehicle()
        {
            using (new TimelineLog(this.name, "Character.Interaction.ExitVehicle", "ExitVehicle"))
            {
                if (this.CurrentVehicle != null)
                {
                    yield return this.CurrentVehicle.Exit(this);
                }
            }
        }
    }
}
