using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// Handles local player input.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
#pragma warning disable 0649 // disable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// Defines which layers to include when raycasting.
        /// </summary>
        [SerializeField]
        private LayerMask raycastLayerMask = -1;

        /// <summary>
        /// The maximum distance the raycast ray may travel.
        /// </summary>
        [SerializeField]
        private float raycastMaxDistance = 20.0f;

        /// <summary>
        /// Name of the game object that represents the water when ray hits it.
        /// </summary>
        [SerializeField]
        private string waterGameObjectName = "Water";

        /// <summary>
        /// The menu canvas to position and add/remove menu items to.
        /// </summary>
        [SerializeField]
        private Transform canvas;

        /// <summary>
        /// Name of the tag representing the player.
        /// </summary>
        [SerializeField]
        private string playerTagName = "Player";

        /// <summary>
        /// The menu item prefab.
        /// </summary>
        [SerializeField]
        private RectTransform menuItemPrefab;

#pragma warning restore 0649 // reenable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// The character component.
        /// </summary>
        private Character character;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Start()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.HandleInput();
        }

        /// <summary>
        /// Caches the component dependencies for quick access later.
        /// </summary>
        private void CacheComponents()
        {
            var player = GameObject
                .FindGameObjectWithTag(this.playerTagName)
                .DisableIfNull(this, "player")
                ;

            this.character = player
                .GetComponent<Character>()
                .DisableIfNull(this, "character");
                ;
        }

        /// <summary>
        /// Handle input from user.
        /// </summary>
        private void HandleInput()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            if(this.canvas.gameObject.activeSelf)
            {
                this.canvas.gameObject.SetActive(false);
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(!Physics.Raycast(ray, out hit, this.raycastMaxDistance, this.raycastLayerMask))
            {
                return;
            }

            if (hit.collider.name != this.waterGameObjectName)
            {
                return;
            }

            this.canvas.position = hit.point;
            this.canvas.forward = hit.point - ray.origin;

            if (this.canvas.childCount > 0)
            {
                GameObject.Destroy(this.canvas.GetChild(0).gameObject);
            }

            var menuItem = GameObject.Instantiate(this.menuItemPrefab, Vector3.zero, Quaternion.identity) as RectTransform;
            menuItem.SetParent(this.canvas);
            menuItem.position = Vector3.zero;
            menuItem.rotation = Quaternion.identity;
            menuItem.localScale = Vector3.one;
            menuItem.anchoredPosition3D = Vector3.zero;
            menuItem.localRotation = Quaternion.identity;

            var button = menuItem.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError("Button not found.");
                return;
            }

            button.onClick.AddListener(() => this.character.SetTargetDestination(hit.point));

            this.canvas.gameObject.SetActive(true);
        }
    }
}
