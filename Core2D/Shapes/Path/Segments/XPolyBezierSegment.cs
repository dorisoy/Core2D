﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using Portable.Xaml.Markup;

namespace Core2D
{
    /// <summary>
    /// Poly bezier path segment.
    /// </summary>
    [ContentProperty(nameof(Points))]
    public sealed class XPolyBezierSegment : XPathSegment
    {
        /// <summary>
        /// Gets or sets points array.
        /// </summary>
        public IList<XPoint> Points { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XPolyBezierSegment"/> class.
        /// </summary>
        public XPolyBezierSegment()
            : base()
        {
            Points = new List<XPoint>();
        }

        /// <inheritdoc/>
        public override IEnumerable<XPoint> GetPoints()
        {
            return Points;
        }

        /// <summary>
        /// Creates a new <see cref="XPolyBezierSegment"/> instance.
        /// </summary>
        /// <param name="points">The points array.</param>
        /// <param name="isStroked">The flag indicating whether shape is stroked.</param>
        /// <param name="isSmoothJoin">The flag indicating whether shape is smooth join.</param>
        /// <returns>The new instance of the <see cref="XPolyBezierSegment"/> class.</returns>
        public static XPolyBezierSegment Create(IList<XPoint> points, bool isStroked, bool isSmoothJoin)
        {
            return new XPolyBezierSegment()
            {
                Points = points,
                IsStroked = isStroked,
                IsSmoothJoin = isSmoothJoin
            };
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return (Points != null) && (Points.Count >= 1) ? "C" + ToString(Points) : "";
        }
    }
}
