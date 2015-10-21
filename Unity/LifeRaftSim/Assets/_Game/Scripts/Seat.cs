using UnityEngine;

namespace Game
{
    public class Seat
    {
        public Transform Transform
        {
            get;
            private set;
        }

        public Character Character
        {
            get;
            set;
        }

        public Seat(Transform transform)
        {
            Debug.Assert(transform != null);
            this.Transform = transform;
        }
    }
}
