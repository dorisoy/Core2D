﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Immutable;

namespace Dock.Model
{
    /// <summary>
    /// Views layout.
    /// </summary>
    public class ViewsLayout : ObservableObject, IViewsLayout
    {
        private ImmutableArray<IViewsPanel> _panels;
        private IView _currentView;

        /// <inheritdoc/>
        public ImmutableArray<IViewsPanel> Panels
        {
            get => _panels;
            set => Update(ref _panels, value);
        }

        /// <inheritdoc/>
        public IView CurrentView
        {
            get => _currentView;
            set => Update(ref _currentView, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewsLayout"/> class.
        /// </summary>
        public ViewsLayout()
        {
            _panels = ImmutableArray<IViewsPanel>.Empty;
        }

        /// <inheritdoc/>
        public void RemoveView(IViewsPanel panel, int index)
        {
            var item = panel.Views[index];
            var builder = panel.Views.ToBuilder();
            builder.RemoveAt(index);
            panel.Views = builder.ToImmutable();

            if (panel.Views.Length > 0)
            {
                panel.CurrentView = panel.Views[index > 0 ? index - 1 : 0];
            }
        }

        /// <inheritdoc/>
        public void MoveView(IViewsPanel panel, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                var item = panel.Views[sourceIndex];
                var builder = panel.Views.ToBuilder();
                builder.Insert(targetIndex + 1, item);
                builder.RemoveAt(sourceIndex);

                panel.Views = builder.ToImmutable();
            }
            else
            {
                int removeIndex = sourceIndex + 1;
                if (panel.Views.Length + 1 > removeIndex)
                {
                    var item = panel.Views[sourceIndex];
                    var builder = panel.Views.ToBuilder();
                    builder.Insert(targetIndex, item);
                    builder.RemoveAt(removeIndex);

                    panel.Views = builder.ToImmutable();
                }
            }
        }

        /// <inheritdoc/>
        public void SwapView(IViewsPanel panel, int sourceIndex, int targetIndex)
        {
            var item1 = panel.Views[sourceIndex];
            var item2 = panel.Views[targetIndex];
            var builder = panel.Views.ToBuilder();
            builder[targetIndex] = item1;
            builder[sourceIndex] = item2;

            panel.Views = builder.ToImmutable();
        }

        /// <inheritdoc/>
        public void MoveView(IViewsPanel sourcePanel, IViewsPanel targetPanel, int sourceIndex, int targetIndex)
        {
            var item = sourcePanel.Views[sourceIndex];
            var sourceBuilder = sourcePanel.Views.ToBuilder();
            var targetBuilder = targetPanel.Views.ToBuilder();
            sourceBuilder.RemoveAt(sourceIndex);
            targetBuilder.Insert(targetIndex, item);

            sourcePanel.Views = sourceBuilder.ToImmutable();
            if (sourcePanel.Views.Length > 0)
            {
                sourcePanel.CurrentView = sourcePanel.Views[sourceIndex > 0 ? sourceIndex - 1 : 0];
            }

            targetPanel.Views = targetBuilder.ToImmutable();
            if (targetPanel.Views.Length > 0)
            {
                targetPanel.CurrentView = targetPanel.Views[targetIndex];
            }
        }

        /// <inheritdoc/>
        public void SwapView(IViewsPanel sourcePanel, IViewsPanel targetPanel, int sourceIndex, int targetIndex)
        {
            var item1 = sourcePanel.Views[sourceIndex];
            var item2 = targetPanel.Views[targetIndex];
            var sourceBuilder = sourcePanel.Views.ToBuilder();
            var targetBuilder = targetPanel.Views.ToBuilder();
            sourceBuilder[sourceIndex] = item2;
            targetBuilder[targetIndex] = item1;

            sourcePanel.Views = sourceBuilder.ToImmutable();
            targetPanel.Views = targetBuilder.ToImmutable();
            sourcePanel.CurrentView = item2;
            targetPanel.CurrentView = item1;
        }

        /// <inheritdoc/>
        public void OnChangeCurrentView(IView view)
        {
            if (view != null && CurrentView != view)
            {
                CurrentView = view;
            }
        }

        /// <summary>
        /// Check whether the <see cref="Panels"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeLeftPanels() => _panels.IsEmpty == false;

        /// <summary>
        /// Check whether the <see cref="CurrentView"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeCurrentView() => _currentView != null;
    }
}
