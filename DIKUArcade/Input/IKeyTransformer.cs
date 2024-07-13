namespace DIKUArcade.Input;

public interface IKeyTransformer<Key, Action> {
    public KeyboardKey TransformKey(Key key);
    public KeyboardAction TransformAction(Action key);
}