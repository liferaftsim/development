using UnityContrib.UnityEngine;

namespace Game
{
    /// <summary>
    /// Character implementation for selectors.
    /// 
    /// A selector is a component that selects a <see cref="T:UnityEngine.Transform"/>.
    /// The advantage of using this component is that you don't need to write the same find game object code over and over.
    /// You can simply attach an instance of a selector to a game object and let that instance find the <see cref="T:UnityEngine.Transform"/> for you.
    /// </summary>
    public class CharacterSelector : BaseSelector<Character>
    {
    }
}
