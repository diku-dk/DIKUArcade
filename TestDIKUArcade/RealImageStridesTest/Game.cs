namespace TestDIKUArcade.RealImageStridesTest;

using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.Numerics;
using System.Reflection;

public class Game : DIKUGame {

    private EntityContainer entities;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        var images = ImageStride.CreateStrides(4, "TestDIKUArcade.Assets.BlueMonster.png");

        entities = new EntityContainer(10);
        for (int i = 0; i < 10; i++) {
            var shape = new DynamicShape(new Vector2(i * 0.1f, 0.9f), new Vector2(0.1f, 0.1f));
            var stride = new ImageStride(80, images[0], images[1], images[2], images[3]);
            entities.AddDynamicEntity(shape, stride);
        }
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Left:
                entities.Iterate(entity  => entity.Shape.MoveX(-0.05f));
                break;
            case KeyboardKey.Right:
                entities.Iterate(entity  => entity.Shape.MoveX(0.05f));
                break;
            case KeyboardKey.Up:
                entities.Iterate(entity  => entity.Shape.MoveY(0.05f));
                break;
            case KeyboardKey.Down:
                entities.Iterate(entity  => entity.Shape.MoveY(-0.05f));
                break;
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render(WindowContext context) {
        entities.RenderEntities(context);
    }

    public override void Update() { }
}