// using System;
// using DIKUArcade;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using DIKUArcade.Math;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.GraphicsLibraryFramework;

// namespace TestDIKUArcade {
//     public class TestRealImageStrides {
//         public static void MainFunction() {
//             var win = new DIKUArcade.Window("hej", 500, AspectRatio.R1X1);
//             win.SetClearColor(System.Drawing.Color.Brown);

//             var images = ImageStride.CreateStrides(4, "BlueMonster.png");

//             var entities = new EntityContainer(10);
//             for (int i = 0; i < 10; i++) {
//                 var shape = new DynamicShape(new Vec2F(i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f));
//                 entities.AddDynamicEntity(shape, new ImageStride(80, images[0], images[1], images[2], images[3]));
//             }

//             win.AddKeyPressEventHandler(delegate(KeyboardKeyEventArgs keyArgs) {
//                 if (keyArgs.Key == Keys.Left) {
//                     entities.Iterate(delegate(Entity entity) {
//                         entity.Shape.MoveX(-0.05f);
//                     });
//                 }
//                 if (keyArgs.Key == Keys.Right) {
//                     entities.Iterate(delegate(Entity entity) {
//                         entity.Shape.MoveX(0.05f);
//                     });
//                 }
//                 if (keyArgs.Key == Keys.Up) {
//                     entities.Iterate(delegate(Entity entity) {
//                         entity.Shape.MoveY(0.05f);
//                     });
//                 }
//                 if (keyArgs.Key == Keys.Down) {
//                     entities.Iterate(delegate(Entity entity) {
//                         entity.Shape.MoveY(-0.05f);
//                     });
//                 }
//             });

//             while (win.IsRunning()) {
//                 win.PollEvents();
//                 win.Clear();

//                 entities.RenderEntities();

//                 win.SwapBuffers();
//             }
//             Console.WriteLine("window closed now");
//         }
//     }
// }
