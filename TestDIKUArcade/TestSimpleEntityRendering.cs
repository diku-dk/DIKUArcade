// using System;
// using DIKUArcade;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using DIKUArcade.Math;
// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.GraphicsLibraryFramework;

// namespace TestDIKUArcade {
//     public class TestSimpleEntityRendering {
//         public static void MainFunction() {
//             var win = new DIKUArcade.Window("TestSimpleEntityRendering", 500, AspectRatio.R1X1);
//             win.SetClearColor(System.Drawing.Color.Brown);

//             //ImageStride imgs = new ImageStride(100,
//             //    "files_01.png", "files_02.png", "files_03.png", "files_04.png");
//             var img1 = new DIKUArcade.Graphics.Image("Taxi.png");
//             var img2 = new DIKUArcade.Graphics.Image("Taxi2.png");

//             ImageStride imgs1 = new ImageStride(250, img1, img2);
//             ImageStride imgs2 = new ImageStride(250, img1, img2);

//             Shape ent = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.5f, 0.5f));
//             Shape ent2 = new DynamicShape(new Vec2F(0.0f, 0.5f), ent.Extent.Copy());

//             Entity actor = new Entity(ent, imgs1);
//             Entity actor2 = new Entity(ent2, imgs2);

//             win.AddKeyPressEventHandler(delegate(KeyboardKeyEventArgs keyArgs) {
//                 if (keyArgs.Key == Keys.A) {
//                     win.SetClearColor(System.Drawing.Color.Red);
//                 }
//                 if (keyArgs.Key == Keys.C) {
//                     win.SetClearColor(System.Drawing.Color.DarkGoldenrod);
//                 }
//                 if (keyArgs.Key == Keys.S) {
//                     Console.WriteLine("SCreenshot:");
//                     win.SaveScreenShot();
//                 }
//                 if (keyArgs.Key == Keys.Left) {
//                     ent.Position.X -= 0.03f;
//                     ent2.Position.X -= 0.03f;
//                 }
//                 if (keyArgs.Key == Keys.Right) {
//                     ent.Position.X += 0.03f;
//                     ent2.Position.X += 0.03f;
//                 }
//                 if (keyArgs.Key == Keys.KeyPadSubtract) {
//                     ent.Scale(1.1f);
//                     ent2.Scale(1.1f);
//                 }
//                 if (keyArgs.Key == Keys.KeyPadAdd) {
//                     ent.Scale(0.9f);
//                     ent2.Scale(0.9f);
//                 }
//                 if (keyArgs.Key == Keys.U) {
//                     ent.ScaleXFromCenter(0.9f);
//                     ent2.ScaleXFromCenter(0.9f);
//                 }
//                 if (keyArgs.Key == Keys.I) {
//                     ent.ScaleXFromCenter(1.1f);
//                     ent2.ScaleXFromCenter(1.1f);
//                 }
//             });

//             while (win.IsRunning()) {
//                 win.PollEvents();
//                 win.Clear();

//                 actor.RenderEntity();
//                 actor2.RenderEntity();

//                 win.SwapBuffers();
//             }
//             Console.WriteLine("window closed now");
//         }
//     }
// }
