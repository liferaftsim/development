﻿using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game
{
    public class HumanAnimationController : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the <see cref="T:UnityEngine.Animator"/> instance.
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Called by Unity.
        /// </summary>
        private void Awake()
        {
            this.CacheComponents();
        }

        /// <summary>
        /// Stores the components for easy and fast access later on.
        /// </summary>
        private void CacheComponents()
        {
            this.animator = this
                .GetComponent<Animator>()
                .DisableIfNull(this, "animator")
                ;
        }

        /// <summary>
        /// Sets the "IsForwardCrawling" animation controller property to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// true if forward crawling; otherwise false.
        /// </param>
        public void SetIsForwardCrawling(bool value)
        {
            //Debug.Log(Time.time + " SetIsForwardCrawling=" + value);
            this.animator.SetBool("IsForwardCrawling", value);
        }

        /// <summary>
        /// Sets the "IsInWater" animation controller property to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// true if is in water; otherwise false.
        /// </param>
        public void SetIsInWater(bool value)
        {
            this.animator.SetBool("IsInWater", value);
        }

        /// <summary>
        /// Sets the "IsSitting" animation controller property to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// true if is sitting; otherwise false.
        /// </param>
        public void SetIsSitting(bool value)
        {
            this.animator.SetBool("IsSitting", value);
        }
    }
}