﻿using UnityContrib.UnityEngine;
using UnityEngine;
using UnityEngine.EventSystems;
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
        /// The menu canvas to position and add/remove menu items to.
        /// </summary>
        [SerializeField]
        private Transform canvas;

        /// <summary>
        /// The menu item prefab.
        /// </summary>
        [SerializeField]
        private RectTransform menuItemPrefab;

        [SerializeField]
        private BaseSelector characterSelector;

#pragma warning restore 0649 // reenable the "Field XYZ is never assigned to, and will always have its default value XX" compiler warning

        /// <summary>
        /// Gets the last <see cref="T:UnityEngine.RaycastHit"/>.
        /// </summary>
        public RaycastHit LastHit
        {
            get;
            private set;
        }

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Update()
        {
            this.HandleInput();
        }

        /// <summary>
        /// Handles input from player.
        /// </summary>
        private void HandleInput()
        {
            this.HandleLeftClick();
            this.HandleEscapeKey();
        }

        /// <summary>
        /// Handles escape key input from the player. 
        /// </summary>
        private void HandleEscapeKey()
        {
            if (!Input.GetKeyUp(KeyCode.Escape))
            {
                return;
            }

            this.HideCanvas();
        }

        /// <summary>
        /// Handles left click input from player.
        /// </summary>
        private void HandleLeftClick()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            // makre sure to check this as ui is not hit on raycasting
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, this.raycastMaxDistance, this.raycastLayerMask))
            {
                this.LastHit = new RaycastHit();
                this.HideCanvas();
                return;
            }
            this.LastHit = hit;

            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable == null)
            {
                this.HideCanvas();
                return;
            }

            this.canvas.position = hit.point;
            this.canvas.forward = hit.point - ray.origin;

            this.canvas.DestroyAllChildren();

            var interactions = interactable.GetInteractions(this.characterSelector.Selected.GetComponent<Character>(), hit.point);
            var index = -1;
            foreach (var interaction in interactions)
            {
                index++;

                var menuItem = GameObject.Instantiate(this.menuItemPrefab, Vector3.zero, Quaternion.identity) as RectTransform;
                menuItem.SetParent(this.canvas);
                menuItem.position = Vector3.zero;
                menuItem.rotation = Quaternion.identity;
                menuItem.localScale = Vector3.one;
                menuItem.anchoredPosition3D = new Vector3(0.0f, index * 26.0f, 0.0f);
                menuItem.localRotation = Quaternion.identity;

                var button = menuItem.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    this.HideCanvas();
                    this.StartCoroutine(interaction.Action().Recursive());
                });

                var text = menuItem.GetComponent<Text>();
                text.text = interaction.Caption;
            }

            this.ShowCanvas();
        }

        /// <summary>
        /// Makes the canvas invisible.
        /// </summary>
        private void HideCanvas()
        {
            this.canvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// Makes the canvas visible.
        /// </summary>
        private void ShowCanvas()
        {
            this.canvas.gameObject.SetActive(true);
        }
    }
}
