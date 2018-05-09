using System.IO;

namespace DIKUArcade.Utilities {
    public class FileIO {

        /// <summary>
        /// Return the platform-specific path of the current project directory
        /// </summary>
        /// <returns></returns>
        public static string GetProjectPath() {
            // find base path
            var dir = new DirectoryInfo(Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location));

            while (dir.Name != "bin") {
                dir = dir.Parent;
            }
            dir = dir.Parent;

            return dir.FullName.ToString();
        }
    }
}