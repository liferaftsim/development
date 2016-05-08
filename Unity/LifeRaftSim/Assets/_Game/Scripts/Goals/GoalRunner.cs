using UnityEngine;

namespace Game.Goals
{
    public class GoalRunner : MonoBehaviour
    {
        private BaseGoal root;
        private bool loop;

        private void Update()
        {
            var root = this.root;
            var loop = this.loop;

            if (root == null)
            {
                return;
            }

            var result = root.Tick();
            if (!loop && result != GoalResult.Running)
            {
                this.root.Terminate();
                this.root = null;
            }
        }

        public void Once(BaseGoal root)
        {
            this.root = root;
            this.loop = false;

            this.root.Initialize();
        }

        public void Loop(BaseGoal root)
        {
            this.root = root;
            this.loop = true;

            this.root.Initialize();
        }
    }

}