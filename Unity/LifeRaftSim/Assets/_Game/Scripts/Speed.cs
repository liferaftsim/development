using UnityEngine;

namespace Game
{
    public class Speed : MonoBehaviour
    {
        public float kmh;
        public float mph;
        public float maxKmh;
        public float maxMph;
        private Vector3 lastKnownPosition;

        private void Start()
        {
            this.lastKnownPosition = this.transform.position;
        }

        private void Update()
        {
            var distance = Vector3.Distance(this.transform.position, this.lastKnownPosition);
            this.kmh = distance * Time.deltaTime * 60.0f * 60.0f;
            this.mph = this.kmh * 0.621371f;
            this.maxKmh = Mathf.Max(this.maxKmh, this.kmh);
            this.maxMph = Mathf.Max(this.maxMph, this.mph);
            this.lastKnownPosition = this.transform.position;
        }
    }
}
