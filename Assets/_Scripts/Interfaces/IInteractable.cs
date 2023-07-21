namespace Boxfriend
{
    /// <summary>
    /// Base interactable interface. 
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// This method is used to interact with an interactable object.
        /// </summary>
        /// <returns>True if interact was successful</returns>
        public bool Interact ();
    }

    /// <summary>
    /// Not all objects will be interactable by players, most will only be interacted with by enemies.
    /// This interface is for objects that the player may interact with
    /// </summary>
    public interface IPlayerInteractable : IInteractable { }
}
