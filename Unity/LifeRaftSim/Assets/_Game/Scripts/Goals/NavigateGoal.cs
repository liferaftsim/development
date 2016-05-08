using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityContrib.UnityEngine;
using UnityEngine;

namespace Game.Goals
{
    public class NavigateGoal : BaseGoal<Character>
    {
        public NavigateGoal(TimelineLog log, Character context, Transform target) : base(log, context)
        {
            // do nothing
        }
    }
}