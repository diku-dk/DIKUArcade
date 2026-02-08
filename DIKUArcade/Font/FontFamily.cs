namespace DIKUArcade.Font;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public struct FontFamily {
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();
    private static readonly string[] fonts = {
        "DIKUArcade.Font.Pixeldroid.Botic.PixeldroidBoticRegular.ttf",
        "DIKUArcade.Font.Pixeldroid.Console.PixeldroidConsoleRegular.ttf",
        "DIKUArcade.Font.Pixeldroid.Console.PixeldroidConsoleRegularMono.ttf",
        "DIKUArcade.Font.Pixeldroid.Menu.PixeldroidMenuRegular.ttf"
    };

    public static FontFamily[] DefaultFontFamilies {
        get;
    } =
        FontFamilies(
            fonts.Select(font => assembly.GetManifestResourceStream(font)!)
        ).ToArray();
    internal Lowlevel.FontFamily fontFamily;
    public FontFamily(Stream font) {
        fontFamily = Lowlevel.createFontFamilies(new[] { font }).First();
    }
    internal FontFamily(Lowlevel.FontFamily fontFamily) {
        this.fontFamily = fontFamily;
    }

    public static IEnumerable<FontFamily> FontFamilies(IEnumerable<Stream> fonts) {
        return Lowlevel.createFontFamilies(fonts).Select(font => new FontFamily(font));
    }
}
