﻿// <copyright file="Resize.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Benchmarks
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using BenchmarkDotNet.Attributes;

    using SixLabors.ImageSharp.PixelFormats;

    using CoreSize = SixLabors.Primitives.Size;

    public class Resize : BenchmarkBase
    {
        [Benchmark(Baseline = true, Description = "System.Drawing Resize")]
        public Size ResizeSystemDrawing()
        {
            using (Bitmap source = new Bitmap(2000, 2000))
            {
                using (Bitmap destination = new Bitmap(400, 400))
                {
                    using (Graphics graphics = Graphics.FromImage(destination))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.DrawImage(source, 0, 0, 400, 400);
                    }

                    return destination.Size;
                }
            }
        }

        [Benchmark(Description = "ImageSharp Resize")]
        public CoreSize ResizeCore()
        {
            using (Image<Rgba32> image = new Image<Rgba32>(2000, 2000))
            {
                image.Mutate(x => x.Resize(400, 400));
                return new CoreSize(image.Width, image.Height);
            }
        }

        //[Benchmark(Description = "ImageSharp Vector Resize")]
        //public CoreSize ResizeCoreVector()
        //{
        //    using (Image<RgbaVector> image = new Image<RgbaVector>(2000, 2000))
        //    {
        //        image.Resize(400, 400);
        //        return new CoreSize(image.Width, image.Height);
        //    }
        //}

        //[Benchmark(Description = "ImageSharp Compand Resize")]
        //public CoreSize ResizeCoreCompand()
        //{
        //    using (Image<Rgba32> image = new Image<Rgba32>(2000, 2000))
        //    {
        //        image.Resize(400, 400, true);
        //        return new CoreSize(image.Width, image.Height);
        //    }
        //}

        //[Benchmark(Description = "ImageSharp Vector Compand Resize")]
        //public CoreSize ResizeCoreVectorCompand()
        //{
        //    using (Image<RgbaVector> image = new Image<RgbaVector>(2000, 2000))
        //    {
        //        image.Resize(400, 400, true);
        //        return new CoreSize(image.Width, image.Height);
        //    }
        //}
    }
}
