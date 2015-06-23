using UnityEngine;

namespace Game
{
    /// <summary>
    /// Provides aset of helper methods for debugging.
    /// </summary>
    public class DebugEx
    {
        /// <summary>
        /// Draws 3 lines intersecting each other at the specified <paramref name="position"/>.
        /// </summary>
        /// <param name="position">
        /// The position where the 3 lines intersect.
        /// </param>
        /// <param name="size">
        /// The length of each line.
        /// </param>
        /// <param name="color">
        /// The color of the lines.
        /// </param>
        public static void DrawCross3D(Vector3 position, float size, Color color)
        {
            var halfSize = size * 0.5f;
            Debug.DrawLine(position - Vector3.left * halfSize, position + Vector3.left * halfSize, color);
            Debug.DrawLine(position - Vector3.up * halfSize, position + Vector3.up * halfSize, color);
            Debug.DrawLine(position - Vector3.forward * halfSize, position + Vector3.forward * halfSize, color);
        }
    }
}
