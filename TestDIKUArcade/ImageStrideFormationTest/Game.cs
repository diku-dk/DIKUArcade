namespace TestDIKUArcade.ImageStrideFormationTest;

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
        var img1 = new Image("TestDIKUArcade.Assets.Taxi.png");
        var img2 = new Image("TestDIKUArcade.Assets.Taxi2.png");

        var maxEntities = 10;

        entities = new EntityContainer(maxEntities);

        for (int i = 0; i < maxEntities; i++) {
            var shape = new DynamicShape(new Vector2(i * 0.1f, 0.9f), new Vector2(0.1f, 0.1f));
            entities.AddDynamicEntity(shape, new ImageStride(250, img1, img2));
        }
    }

    public override void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }

        switch (key) {
            case KeyboardKey.Left:
                entities.Iterate(entity => entity.Shape.MoveX(-0.05f));
                break;
            case KeyboardKey.Right:
                entities.Iterate(entity => entity.Shape.MoveX(0.05f));
                break;
            case KeyboardKey.Up:
                entities.Iterate(entity => entity.Shape.MoveY(0.05f));
                break;
            case KeyboardKey.Down:
                entities.Iterate(entity => entity.Shape.MoveY(-0.05f));
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