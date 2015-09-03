using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game
{
    public static class World
    {
        public static bool IsInWater(this Transform transform)
        {
            var origin = transform.position + Vector3.up;
            var direction = Vector3.down;
            var distance = 5.0f;
            RaycastHit hit;
            if (!Physics.Raycast(origin, direction, out hit, distance))
            {
                return false;
            }

            var ocean = GameObject.FindObjectOfType<Ocean>();

            if (hit.collider.transform == ocean.transform)
            {
                return true;
            }

            Debug.Log(hit.collider.name);
            return false;
        }
    }
}
