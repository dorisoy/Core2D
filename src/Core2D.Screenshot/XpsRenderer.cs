﻿using System.IO;
using Avalonia;
using Avalonia.Controls;
using SkiaSharp;

namespace Core2D.Screenshot
{
    public static class XpsRenderer
    {
        public static void Render(Control target, Size size, Stream stream, double dpi = 72, bool useDeferredRenderer = false)
        {
            using var wstream = new SKManagedWStream(stream);
            using var document = SKDocument.CreateXps(stream, (float)dpi);
            using var canvas = document.BeginPage((float)size.Width, (float)size.Height);
            target.Measure(size);
            target.Arrange(new Rect(size));
            CanvasRenderer.Render(target, canvas, dpi, useDeferredRenderer);
        }
    }
}
