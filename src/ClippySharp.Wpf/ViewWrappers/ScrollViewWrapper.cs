﻿/* 
 * FigmaImageView.cs - NSImageView which stores it's associed Figma Id
 * 
 * Author:
 *   Jose Medrano <josmed@microsoft.com>
 *
 * Copyright (C) 2018 Microsoft, Corp
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit
 * persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
 * NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
 * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClippySharp;

namespace ClippySharp.Views.Wpf
{
    public class ScrollViewWrapper : ViewWrapper, IScrollViewWrapper
    {
        readonly ScrollViewer scrollView;

        Canvas canvasContainer;
        IViewWrapper canvasContainerWrapper;

        public override IReadOnlyList<IViewWrapper> Children => canvasContainerWrapper.Children;

        public IViewWrapper ContentView
        {
            get => canvasContainerWrapper;
            set
            {
                if (value != null && value.NativeObject is Canvas canvas)
                {
                    canvasContainer = canvas;
                    canvasContainerWrapper = value;
                    scrollView.Content = canvasContainer;
                }
            }
        }

        public ScrollViewWrapper (ScrollViewer scrollView) : base(scrollView)
        {
            this.scrollView = scrollView;

            ContentView = new ViewWrapper(new Canvas());

            scrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        public override void AddChild(IViewWrapper view) => canvasContainerWrapper.AddChild(view);

        public override void RemoveChild(IViewWrapper view) => canvasContainerWrapper.RemoveChild(view);

        public void AdjustToContent()
        {
            var children = Children;
            Rectangle contentRect = Rectangle.Zero;
            for (int i = 0; i < children.Count; i++)
            {
                if (i == 0)
                {
                    
                    contentRect = new Rectangle ( children[i].X, children[i].Y, children[i].Width, children[i].Height);
                }
                else
                {
                    contentRect = UnionWith(new Rectangle(children[i].X, children[i].Y, children[i].Width, children[i].Height));
                }
            }
            SetContentSize(contentRect.Width, contentRect.Height);
        }
        public Rectangle UnionWith(Rectangle allocation)
        {
            //TODO: improve
            float xMin = Math.Min(X, allocation.X);
            float yMin = Math.Min(Y, allocation.Y);
            float xMax = Math.Max(X + Width, allocation.X + allocation.Width);
            float yMax = Math.Max(Y + Height, allocation.Y + allocation.Height);
            return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public void SetContentSize(float width, float height)
        {
            canvasContainer.Width = width;
            canvasContainer.Height = height;
        }
    }
}