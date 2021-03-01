// using System;
// using DIKUArcade;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.GraphicsLibraryFramework;

// namespace TestDIKUArcade {
//     public class TestAnimations {
//         public static void MainFunction() {
//             var win = new DIKUArcade.Window("TestAnimations", 500, AspectRatio.R1X1);

//             Animation anim = new Animation() {Duration = 0};
//             StationaryShape shape = new StationaryShape(0.5f, 0.5f, 0.5f, 0.5f);
//             var strides = ImageStride.CreateStrides(4, "PuffOfSmoke.png");

//             win.AddKeyPressEventHandler(delegate(KeyboardKeyEventArgs keyArgs) {
//                 switch (keyArgs.Key) {
//                 case Keys.Escape:
//                     win.CloseWindow();
//                     break;
//                 case Keys.Space:
//                     // do something to test animations
//                     anim = new Animation() {Duration = 1000, Shape = shape};
//                     anim.Stride = new ImageStride(40, strides);
//                     break;
//                 }
//             });

//             while (win.IsRunning()) {
//                 win.PollEvents();
//                 win.Clear();
//                 if (anim.IsActive()) {
//                     anim.RenderAnimation();
//                 }
//                 win.SwapBuffers();
//             }
//         }
//     }
// }
