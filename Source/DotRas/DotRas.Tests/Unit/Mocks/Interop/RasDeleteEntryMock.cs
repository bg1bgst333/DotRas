﻿//--------------------------------------------------------------------------
// <copyright file="RasDeleteEntryMock.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      the terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace DotRas.Tests.Unit.Mocks.Interop
{
    using System;
    using TypeMock;
    using TypeMock.ArrangeActAssert;

    /// <summary>
    /// Represents a mock instance of the <see cref="UnsafeNativeMethods.DeleteEntry"/> method. This class cannot be inherited.
    /// </summary>
    internal sealed class RasDeleteEntryMock : PInvokeMock
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDeleteEntryMock"/> class.
        /// </summary>
        /// <param name="returnValue">The value to return when the method is called.</param>
        public RasDeleteEntryMock(int returnValue)
        {
            this.ReturnValue = returnValue;

            this.Initialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the mock instance.
        /// </summary>
        protected override void Initialize()
        {
            Isolate.Fake.StaticMethods<UnsafeNativeMethods>();
            Isolate.WhenCalled(() => UnsafeNativeMethods.DeleteEntry(null, null)).WillReturn(this.ReturnValue);

            base.Initialize();
        }

        #endregion
    }
}