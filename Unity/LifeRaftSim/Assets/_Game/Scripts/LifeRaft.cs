using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Behaviour script for life raft.
    /// </summary>
    public class LifeRaft : Vehicle
    {
        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            this.AddInteractableProxyToDecendants();
            this.MarkCollidersConvexOnDecendants();
        }

        private void MarkCollidersConvexOnDecendants()
        {
            this.transform.DecendantsDepthFirst().ForEach(d =>
            {
                var collider = d.GetComponent<MeshCollider>();
                if(collider == null)
                {
                    return;
                }

                collider.convex = true;
            });
        }

        private void AddInteractableProxyToDecendants()
        {
            this.transform.DecendantsDepthFirst().ForEach(d =>
            {
                var collider = d.GetComponent<Collider>();
                if(collider == null)
                {
                    return;
                }

                var proxy = d.gameObject.AddComponent<InteractableProxy>();
                proxy.TargetInteractableTransform = this.transform;
            });
        }

        /// <summary>
        /// Returns information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Interaction information.
        /// </returns>
        public override IEnumerable<InteractionInfo> GetInteractions(Character character, Vector3 clickPoint)
        {
            yield return new InteractionInfo("Board", () => this.Enter(character, this.Seats.FirstOrDefault(s => s.Character == null)));
        }

        public override IEnumerator Enter(Character character, Seat seat)
        {
            using (new TimelineLog(character.name, "LifeRaft.Interaction.Enter", "Enter"))
            {
                Debug.Assert(character != null);
                Debug.Assert(seat != null);

                var navMeshAgent = character.GetComponent<NavMeshAgent>();
                var rigidbody = character.GetComponent<Rigidbody>();
                var animationController = character.GetComponent<HumanAnimationController>();

                // navigate to raft
                yield return character.Navigate(this.transform);

                character.CurrentVehicle = this;

                // board animation
                navMeshAgent.enabled = false;
                rigidbody.isKinematic = true;
                character.transform.SetParent(this.transform);
                // TODO replace while loop with boarding animation code
                var end = Time.time + 2.0f;
                while (Time.time < end)
                {
                    yield return null;
                }

                animationController.IsInWater = false;
                animationController.SetIsForwardCrawling(false);
                animationController.SetIsSitting(true);
                yield return null;

                // TODO add code crawl to sit animation

                // position in raft
                character.transform.SetParent(seat.Transform);
                character.transform.localPosition = Vector3.zero;
                character.transform.localRotation = Quaternion.identity;
            }
        }

        public override IEnumerator Exit(Character character)
        {
            using (new TimelineLog(character.name, "LifeRaft.Interaction.Exit", "Exit"))
            {
                Debug.Assert(character != null);

                var navMeshAgent = character.GetComponent<NavMeshAgent>();
                var rigidbody = character.GetComponent<Rigidbody>();
                var animationController = character.GetComponent<HumanAnimationController>();

                character.transform.SetParent(this.transform);

                // TODO add code crawl to sit animation

                var end = Time.time + 2.0f;
                while (Time.time < end)
                {
                    yield return null;
                }

                navMeshAgent.enabled = true;
                rigidbody.isKinematic = false;
                character.transform.SetParent(null);

                animationController.IsInWater = true;
                animationController.SetIsForwardCrawling(false);
                animationController.SetIsSitting(false);
                yield return null;

                character.CurrentVehicle = null;
            }
        }
    }
}
