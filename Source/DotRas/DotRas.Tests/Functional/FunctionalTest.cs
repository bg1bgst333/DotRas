﻿//--------------------------------------------------------------------------
// <copyright file="FunctionalTest.cs" company="Jeff Winn">
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

namespace DotRas.Tests.Functional
{
    using System;

    /// <summary>
    /// Provides the base class for all functional (system) tests. This class must be inherited.
    /// </summary>
    public abstract class FunctionalTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionalTest"/> class.
        /// </summary>
        protected FunctionalTest()
        {
        }

        #endregion
    }
}