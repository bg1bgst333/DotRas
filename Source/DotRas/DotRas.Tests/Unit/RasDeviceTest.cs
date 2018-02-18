﻿//--------------------------------------------------------------------------
// <copyright file="RasDeviceTest.cs" company="Jeff Winn">
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

namespace DotRas.Tests.Unit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using DotRas;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasDevice"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasDeviceTest : UnitTest
    {
        #region Fields

        public static readonly string ValidDeviceName = "WAN Miniport (PPTP)";
        public static readonly string InvalidDeviceName = ValidDeviceName.ToLower();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDeviceTest"/> class.
        /// </summary>
        public RasDeviceTest()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tests the Name property to ensure the property returns the same value as what was passed to the constructor.
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            string expected = "Test Device";

            RasDevice target = RasDevice.Create(expected, RasDeviceType.Generic);

            string actual;
            actual = target.Name;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DeviceType property to ensure the property returns the same value as what was passed to the constructor.
        /// </summary>
        [TestMethod]
        public void DeviceTypeTest()
        {
            string name = "Test Device";
            string expected = RasDeviceType.Generic;

            RasDevice target = RasDevice.Create(name, expected);

            string actual;
            actual = target.DeviceType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetDevicesByType method to ensure it returns the correct objects from the GetDevices method.
        /// </summary>
        [TestMethod]
        public void GetDevicesByTypeTest()
        {
            string deviceType = RasDeviceType.Vpn;

            Collection<RasDevice> tempDevices = new Collection<RasDevice>();
            foreach (RasDevice device in RasDevice.GetDevices())
            {
                if (string.Compare(device.DeviceType, deviceType, true) == 0)
                {
                    tempDevices.Add(device);
                }
            }

            ReadOnlyCollection<RasDevice> expected = new ReadOnlyCollection<RasDevice>(tempDevices);
            ReadOnlyCollection<RasDevice> actual = RasDevice.GetDevicesByType(deviceType);

            CollectionAssert.AreEqual(expected, actual, new RasDeviceComparer());
        }

        /// <summary>
        /// Tests the GetDevices method to ensure it returns all of the devices from the RasHelper.GetDevices method.
        /// </summary>
        [TestMethod]
        public void GetDevicesTest()
        {
            ReadOnlyCollection<RasDevice> expected = RasHelper.GetDevices();

            ReadOnlyCollection<RasDevice> actual;
            actual = RasDevice.GetDevices();

            CollectionAssert.AreEqual(expected, actual, new RasDeviceComparer());
        }

        /// <summary>
        /// Tests the GetDeviceByName method to ensure a null reference is returned because of improperly cased device name.
        /// </summary>
        [TestMethod]
        public void GetDeviceByNameTest()
        {
            string name = RasDeviceTest.InvalidDeviceName;
            string deviceType = RasDeviceType.Vpn;

            RasDevice target = RasDevice.GetDeviceByName(name, deviceType);

            Assert.IsNotNull(target);
        }

        /// <summary>
        /// Tests the GetDeviceByName method to ensure the object returned is the same as expected.
        /// </summary>
        [TestMethod]
        public void GetDeviceByNameCaseSensitiveTest()
        {
            string name = RasDeviceTest.ValidDeviceName;
            string deviceType = RasDeviceType.Vpn;

            RasDevice expected = RasDevice.Create(name, deviceType);
            RasDevice actual = RasDevice.GetDeviceByName(name, deviceType, true);

            RasDeviceComparer comparer = new RasDeviceComparer();
            bool target = comparer.Compare(expected, actual) == 0;

            Assert.IsTrue(target);
        }

        /// <summary>
        /// Tests the GetDeviceByName method for a null reference returned because of a bad device type.
        /// </summary>
        [TestMethod]
        public void GetDeviceByNameTestWithBadDeviceType()
        {
            string name = RasDeviceTest.ValidDeviceName;
            string deviceType = string.Empty;

            RasDevice expected = null;

            RasDevice actual;
            actual = RasDevice.GetDeviceByName(name, deviceType);

            RasDeviceComparer comparer = new RasDeviceComparer();
            bool target = comparer.Compare(expected, actual) == 0;

            Assert.IsTrue(target);
        }

        /// <summary>
        /// Tests the GetDeviceByName method to ensure it returns the same object with exact matching turned off.
        /// </summary>
        [TestMethod]
        public void GetDeviceByName1TestWithoutExactMatching()
        {
            string name = RasDeviceTest.ValidDeviceName;
            string deviceType = RasDeviceType.Vpn;
            bool exactMatchOnly = false;

            RasDevice expected = RasDevice.Create(name, deviceType);

            RasDevice actual;
            actual = RasDevice.GetDeviceByName(name, deviceType, exactMatchOnly);

            RasDeviceComparer comparer = new RasDeviceComparer();
            bool target = comparer.Compare(expected, actual) == 0;

            Assert.IsTrue(target);
        }

        /// <summary>
        /// Tests the GetDeviceByName method to ensure the same object is returned with exact matching enabled.
        /// </summary>
        [TestMethod]
        public void GetDeviceByName1TestWithExactMatchOnly()
        {
            string name = RasDeviceTest.ValidDeviceName;
            string deviceType = RasDeviceType.Vpn;
            bool exactMatchOnly = true;

            RasDevice expected = RasDevice.Create(name, deviceType);

            RasDevice actual;
            actual = RasDevice.GetDeviceByName(name, deviceType, exactMatchOnly);

            RasDeviceComparer comparer = new RasDeviceComparer();
            bool target = comparer.Compare(expected, actual) == 0;

            Assert.IsTrue(target);
        }

        /// <summary>
        /// Tests the GetDeviceByName method to ensure the same object is returned with exact matching enabled.
        /// </summary>
        [TestMethod]
        public void GetDeviceByName1TestWithBadDeviceType()
        {
            string name = RasDeviceTest.ValidDeviceName;
            string deviceType = string.Empty;
            bool exactMatchOnly = true;

            RasDevice expected = null;

            RasDevice actual;
            actual = RasDevice.GetDeviceByName(name, deviceType, exactMatchOnly);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the RasDevice constructor to ensure the properties are all being set correctly.
        /// </summary>
        [TestMethod]
        public void RasDeviceConstructorTest()
        {
            string name = string.Empty;
            string deviceType = string.Empty;

            RasDevice_Accessor target = new RasDevice_Accessor(name, deviceType);

            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(deviceType, target.DeviceType);
        }

        #endregion

        #region RasDeviceComparer Class

        /// <summary>
        /// Compares two <see cref="DotRas.RasDevice"/> objects for equivalence.
        /// </summary>
        private class RasDeviceComparer : IComparer
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RasDeviceComparer"/> class.
            /// </summary>
            public RasDeviceComparer()
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Performs the comparison of two <see cref="DotRas.RasDevice"/> objects for equivalence.
            /// </summary>
            /// <param name="objA">The first <see cref="DotRas.RasDevice"/> object to compare.</param>
            /// <param name="objB">The second <see cref="DotRas.RasDevice"/> object to compare.</param>
            /// <returns>The relative value when comparing <paramref name="objA"/> to <paramref name="objB"/>.</returns>
            public int Compare(object objA, object objB)
            {
                RasDevice deviceA = (RasDevice)objA;
                RasDevice deviceB = (RasDevice)objB;

                if (deviceA == null && deviceB == null)
                {
                    return 0;
                }
                else if (deviceA != null && deviceB == null)
                {
                    return -1;
                }
                else if (deviceA == null && deviceB != null)
                {
                    return 1;
                }

                int retval = string.Compare(deviceA.Name, deviceB.Name, false);
                if (retval == 0)
                {
                    retval = string.Compare(deviceA.DeviceType, deviceB.DeviceType, false);
                }

                return retval;
            }

            #endregion
        }

        #endregion
    }
}