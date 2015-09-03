using UnityEngine;

namespace Game
{
    public class FollowXZPosition : MonoBehaviour
    {
#pragma warning disable 0649 // disable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        [SerializeField]
        private Transform targetTransform;

#pragma warning restore 0649 // reenable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        private void Update()
        {
            if (this.targetTransform == null)
            {
                Debug.LogError("targetTransform is null");
                return;
            }

            var position = this.targetTransform.position;
            position.y = this.transform.position.y;
            this.transform.position = position;
        }
    }
}
