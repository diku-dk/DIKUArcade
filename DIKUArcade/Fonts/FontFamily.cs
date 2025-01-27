namespace DIKUArcade.Font;

using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

public struct FontFamily {
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();
    private static readonly string[] fonts = {
        "DIKUArcade.Fonts.Pixeldroid.Botic.PixeldroidBoticRegular.ttf",
        "DIKUArcade.Fonts.Pixeldroid.Console.PixeldroidConsoleRegular.ttf",
        "DIKUArcade.Fonts.Pixeldroid.Console.PixeldroidConsoleRegularMono.ttf",
        "DIKUArcade.Fonts.Pixeldroid.Menu.PixeldroidMenuRegular.ttf"
    };

    public static FontFamily[] DefaultFontFamilies { get; } =
        FontFamilies(
            fonts.Select(font => assembly.GetManifestResourceStream(font)!)
        ).ToArray();
    internal Lowlevel.FontFamily fontFamily;
    public FontFamily(Stream font) {
        fontFamily = Lowlevel.createFontFamilies(new []{font}).First();
    }
    internal FontFamily(Lowlevel.FontFamily fontFamily) {
        this.fontFamily = fontFamily;
    }

    public static IEnumerable<FontFamily> FontFamilies(IEnumerable<Stream> fonts) {
        return Lowlevel.createFontFamilies(fonts).Select(font => new FontFamily(font));
    }
}
