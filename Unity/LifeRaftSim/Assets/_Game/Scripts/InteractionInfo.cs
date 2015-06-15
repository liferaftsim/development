using System;

namespace Game
{
    /// <summary>
    /// Information about the interaction.
    /// </summary>
    public class InteractionInfo
    {
        /// <summary>
        /// Gets the presentable title for the interaction.
        /// </summary>
        public string Caption
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the delegate to invoke to start the interaction. 
        /// </summary>
        public Action Action
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialies a new instance of the <see cref="T:Game.InteractionInfo"/> class.
        /// </summary>
        /// <param name="caption">
        /// The presentable title for the interaction.
        /// </param>
        /// <param name="action">
        /// The delegate to invoke to start the interaction. 
        /// </param>
        public InteractionInfo(string caption, Action action)
        {
            this.Caption = caption;
            this.Action = action;
        }
    }
}
