using System;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;

namespace ElectronicStamp
{
    public class ImageProcessing
    {
        private const string title = "БА САРРАЁСАТИ КОНСУЛИИ ВАЗОРАТИ КОРҲОИ ХОРИҶИИ ҶУМҲУРИИ ТОҶИКИСТОН";
        private const int padding = 20;

        public static void Start()
        {
            System.IO.Directory.CreateDirectory("output");
            using (Image<Rgba32> img = new Image<Rgba32>(800, 1000))
            {
                
                var mainRectSize = new SizeF(260, 180);
                var mainRectPoint = new PointF(img.Width - mainRectSize.Width - padding, 
                    img.Height - mainRectSize.Height - padding);
                var mainRect = new RectangleF(mainRectPoint, mainRectSize);
                
                
                var centerRectSize = new SizeF(140, 60);
                var centerRectPoint = new PointF(
                    mainRect.X + (mainRectSize.Width / 2) - (centerRectSize.Width / 2),
                    mainRect.Y + (mainRectSize.Height / 2) - (centerRectSize.Height / 2));
                var centerRect = new RectangleF(centerRectPoint, centerRectSize);
                
                
                var font = SystemFonts.CreateFont("Arial", 39, FontStyle.Regular);
                var titleOptions = new TextGraphicsOptions()
                {
                    WrapTextWidth = mainRect.Width,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                var titleFont = SystemFonts.CreateFont("Arial", 14, FontStyle.Regular);
                var titlePoint = new PointF(mainRectPoint.X, mainRectPoint.Y + 10);

                var dateOptions = new TextGraphicsOptions()
                {
                    WrapTextWidth = centerRectSize.Width,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                var datePoint = new PointF(centerRectPoint.X, centerRectPoint.Y + centerRectSize.Height / 2);
                var dateFont = SystemFonts.CreateFont("Arial", 20, FontStyle.Regular);
                
                
                string text = "Hello World Hello World Hello World Hello World Hello World";
                var textGraphicsOptions = new TextGraphicsOptions(true) // draw the text along the path wrapping at the end of the line
                {
                };

                // lets generate the text as a set of vectors drawn along the path

                var glyphs = SixLabors.Shapes.TextBuilder.GenerateGlyphs(text, new RendererOptions(font, textGraphicsOptions.DpiX, textGraphicsOptions.DpiY)
                {
                    HorizontalAlignment = textGraphicsOptions.HorizontalAlignment,
                    TabWidth = textGraphicsOptions.TabWidth,
                    VerticalAlignment = textGraphicsOptions.VerticalAlignment,
                    WrappingWidth = textGraphicsOptions.WrapTextWidth,
                    ApplyKerning = textGraphicsOptions.ApplyKerning
                });

                img.Mutate(ctx => ctx
                    .Fill(Rgba32.White)
                    .Draw(Rgba32.Red, 1, mainRect)
                    .Draw(Rgba32.Red, 1, centerRect)
                    .DrawText( titleOptions, title, titleFont, Rgba32.Red, titlePoint)
                    .DrawText(dateOptions, "11 МАЙ 2019", dateFont, Rgba32.Red, datePoint)
                    .Fill((GraphicsOptions)textGraphicsOptions, Rgba32.Black, glyphs));

                img.Save("output/Rect.png");
            }
        }
        
    }
}