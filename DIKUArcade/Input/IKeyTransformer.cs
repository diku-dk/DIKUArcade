namespace DIKUArcade.Input;

/// <summary>
/// Defines a contract for transforming key inputs into standardized keyboard actions and keys.
/// </summary>
/// <typeparam name="Key">The type of the key input to be transformed.</typeparam>
/// <typeparam name="Action">The type of the action associated with the key input.</typeparam>
public interface IKeyTransformer<Key, Action> {
    /// <summary>
    /// Transforms a key input of type <typeparamref name="Key"/> into a standardized <see cref="KeyboardKey"/>.
    /// </summary>
    /// <param name="key">The key input to transform.</param>
    /// <returns>A <see cref="KeyboardKey"/> representation of the provided key input.</returns>
    public KeyboardKey TransformKey(Key key);

    /// <summary>
    /// Transforms an action of type <typeparamref name="Action"/> into a standardized <see cref="KeyboardAction"/>.
    /// </summary>
    /// <param name="action">The action to transform.</param>
    /// <returns>A <see cref="KeyboardAction"/> representation of the provided action.</returns>
    public KeyboardAction TransformAction(Action action);
}
