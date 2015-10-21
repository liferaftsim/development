using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    public abstract class Vehicle : MonoBehaviour, IInteractable
    {
        private IEnumerable<Seat> seats;

        public IEnumerable<Seat> Seats
        {
            get
            {
                return this.seats;
            }
        }

        private void Start()
        {
            this.seats = this.transform
                .DecendantsBreadthFirst()
                .Where(t => t.name.Equals("Seat"))
                .Select(t => new Seat(t))
                ;
        }

        public abstract IEnumerator Enter(Character character, Seat seat);

        public abstract IEnumerator Exit(Character character);

        /// <summary>
        /// Returns information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Interaction information.
        /// </returns>
        public abstract IEnumerable<InteractionInfo> GetInteractions(Character character, Vector3 clickPoint);
    }
}
