using UnityContrib.UnityEngine;

namespace Game.Goals
{
    public abstract class BaseGoal<T>
    {
        private TimelineLog log;
        private T context;

        public BaseGoal(TimelineLog log, T context)
        {
            this.log = log;
            this.context = context;
        }

        public GoalResult Initialize()
        {
            return this.InnerInitialize();
        }

        protected virtual GoalResult InnerInitialize()
        {
            return GoalResult.Success;
        }

        public GoalResult Tick()
        {
            return this.InnerTick();
        }

        protected virtual GoalResult InnerTick()
        {
            return GoalResult.Success;
        }

        public GoalResult Terminate()
        {
            this.log.Dispose();
            return this.InnerTerminate();
        }

        protected virtual GoalResult InnerTerminate()
        {
            return GoalResult.Success;
        }
    }

}