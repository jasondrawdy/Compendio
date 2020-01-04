/*
==============================================================================
Copyright © Jason Drawdy (CloneMerge)

All rights reserved.

The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Except as contained in this notice, the name of the above copyright holder
shall not be used in advertising or otherwise to promote the sale, use or
other dealings in this Software without prior written authorization.
==============================================================================
*/

#region Imports

using System;
using System.Collections;
using System.Collections.Generic;

#endregion
namespace Compendio.Utils
{
    /// <summary>
    /// Tools which allow the manipulation of common collection types.
    /// </summary>
    public static class Collections
    {
        #region Variables

        /// <summary>
        /// Common sortable collection types.
        /// </summary>
        public enum CollectionType { Array, List, Stack }

        #endregion
        #region Methods

        /// <summary>
        /// Allows a provided collection to be sorted given a <see cref="CollectionFormatter"/>.
        /// </summary>
        /// <typeparam name="T">Any sortable collection type.</typeparam>
        /// <param name="collection">The collection which will be sorted.</param>
        /// <returns>A sorted version of the provided collection.</returns>
        [Obsolete("This method is deprecated and should not be used; No replacement method exists. The return value is the original provided collection without any sorting.")]
        public static IEnumerable<T> SortCollection<T>(this IEnumerable<T> collection, CollectionFormatter formatter)
        {
            Type type = collection.GetType().GetGenericArguments()[0];
            if (type == typeof(Array))
                return collection;
            else if (type == typeof(List<T>))
                return collection;
            else if (type == typeof(Stack))
                return collection;
            return collection;
        }

        #endregion
    }
    #region Formatters

    /// <summary>
    /// Provides an interface to describe how a collection should be formatted.
    /// </summary>
    public class CollectionFormatter : IFormatProvider
    {
        public CollectionFormatter() { }
        public object GetFormat(Type formatType) { return null; }
    }

    #endregion
}