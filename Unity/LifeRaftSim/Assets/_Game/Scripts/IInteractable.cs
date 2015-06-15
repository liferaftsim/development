
namespace Game
{
    /// <summary>
    /// Defines the interface for interactable objects.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Returns an array of information about interactions possible with the object.
        /// </summary>
        /// <returns>
        /// Array of interaction information.
        /// </returns>
        InteractionInfo[] GetInteractions();
    }
}
