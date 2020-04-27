﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Core2D.Containers
{
    /// <summary>
    /// Named items library.
    /// </summary>
    public class Library<T> : ObservableObject, ILibrary<T>
    {
        private ImmutableArray<T> _items;
        private T _selected;

        /// <inheritdoc/>
        public ImmutableArray<T> Items
        {
            get => _items;
            set => Update(ref _items, value);
        }

        /// <inheritdoc/>
        public T Selected
        {
            get => _selected;
            set => Update(ref _selected, value);
        }

        /// <inheritdoc/>
        public void SetSelected(T item) => Selected = item;

        /// <inheritdoc/>
        public override object Copy(IDictionary<object, object> shared)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check whether the <see cref="Items"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeItems() => true;

        /// <summary>
        /// Check whether the <see cref="Selected"/> property has changed from its default value.
        /// </summary>
        /// <returns>Returns true if the property has changed; otherwise, returns false.</returns>
        public virtual bool ShouldSerializeSelected() => _selected != null;
    }
}