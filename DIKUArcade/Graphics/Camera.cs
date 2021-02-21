using DIKUArcade.Math;
using OpenTK.Graphics.OpenGL;

namespace DIKUArcade.Graphics {

    public abstract class Camera {
        public Vec2F Offset;
        public float Scale;

        public void ScaleBy(float scalar) {
            Scale *= scalar;
            setZoom();
        }
        
        public void OffsetBy(Vec2F offsetBy) {
            Offset += offsetBy;
        }

        private void setZoom() {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
            GL.Ortho(0.0, 1.0 * Scale,0.0,1.0 * Scale, 0.0, 4.0);
        }
    }
}