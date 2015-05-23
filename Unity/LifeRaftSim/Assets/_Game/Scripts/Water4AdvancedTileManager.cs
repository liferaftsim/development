using UnityEngine;
using System.Linq;
using System.Collections;
using UnityStandardAssets.Water;
using System;

namespace Game
{
    /// <summary>
    /// Instantiates and moves tiles around to ensure there is always visible ocean in sight.
    /// </summary>
    public class Water4AdvancedTileManager : MonoBehaviour
    {
        /// <summary>
        /// Tile prefab to instantiate.
        /// </summary>
        [SerializeField]
        private WaterTile tilePrefab;

        /// <summary>
        /// The number of tiles on the x and z axis.
        /// Example if tileCount is 3 there will be 3 tiles on the x axis and 3 on the z axis and 9 tiles in total.
        /// </summary>
        [SerializeField]
        private int tileCount = 3;

        /// <summary>
        /// Tag name of the target that the ocean must surround.
        /// </summary>
        [SerializeField]
        private string targetTagName = "Player";

        /// <summary>
        /// Target that the ocean must surround.
        /// </summary>
        private Transform target;

        /// <summary>
        /// The tiles surrounding the target.
        /// These are the tiles we're going to move around.
        /// </summary>
        private WaterTile[] tiles;

        /// <summary>
        /// The size of the tile.
        /// </summary>
        private Vector3 tileSize;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Awake()
        {
            this.CacheTarget();
            this.CreateAndCacheTiles();
            this.CalculateTileSize();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.MoveTiles();
        }

        /// <summary>
        /// Finds the target and caches it for continues use.
        /// </summary>
        private void CacheTarget()
        {
            var targetGameObject = GameObject.FindGameObjectWithTag(this.targetTagName);
            if (targetGameObject == null)
            {
                Debug.LogError(this.name + ": No game object tagged \"" + this.targetTagName + "\" found.");
                this.enabled = false;
                return;
            }
            this.target = targetGameObject.transform;
        }

        /// <summary>
        /// Finds the existing tiles, creates the missing and caches them for moving them around during game play.
        /// </summary>
        private void CreateAndCacheTiles()
        {
            var tiles = this.transform.GetComponentsInChildren<WaterTile>();
            if (tiles.Length < this.tileCount * this.tileCount)
            {
                Array.Resize(ref tiles, this.tileCount * this.tileCount);
                for (var index = 0; index < tiles.Length; index++)
                {
                    var tile = tiles[index];
                    if (tile != null)
                    {
                        continue;
                    }

                    tiles[index] = tile = GameObject.Instantiate(this.tilePrefab, Vector3.zero, Quaternion.identity) as WaterTile;
                    tile.transform.SetParent(this.transform);

                }
            }
            this.tiles = tiles;
        }

        private void CalculateTileSize()
        {
            var filter = this.tiles.First().GetComponent<MeshFilter>();
            var mesh = filter.mesh;
            this.tileSize = mesh.bounds.size * 0.5f;
        }

        /// <summary>
        /// Move the tiles to keep the ocean in sight.
        /// </summary>
        private void MoveTiles()
        {
            var position = this.target.position;
            var roundedPosition = new Vector3(
                Mathf.Round(position.x / this.tileSize.x) * this.tileSize.x - this.tileCount * this.tileSize.x * 0.5f,
                Mathf.Round(position.y / this.tileSize.y) * this.tileSize.y,
                Mathf.Round(position.z / this.tileSize.z) * this.tileSize.z - this.tileCount * this.tileSize.z * 0.5f
                );

            for (var index = 0; index < this.tiles.Length; index++)
            {
                var x = index / this.tileCount;
                var z = index % this.tileCount;
                var tile = this.tiles[index];
                var offset = new Vector3(
                    x * this.tileSize.x,
                    0.0f,
                    z * this.tileSize.z
                    );
                tile.transform.position = roundedPosition + offset;
            }
        }
    }
}
