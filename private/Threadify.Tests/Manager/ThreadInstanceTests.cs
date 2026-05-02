//
// Threadify  Copyright (C) 2026  Aptivi
//
// This file is part of Threadify
//
// Threadify is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Threadify is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Threading;
using Threadify.Manager;

namespace Threadify.Tests.Manager
{

    [TestClass]
    public class ThreadInstanceTests
    {

        internal static ThreadInstance? TargetThread;
        internal static ThreadInstance? TargetParameterizedThread;
        internal static ThreadInstance? TargetThreadWithChild;
        internal static ThreadInstance? TargetParameterizedThreadWithChild;
        internal static ThreadInstance? TargetThreadWithAppendingChild;
        internal static ThreadInstance? TargetParameterizedThreadWithAppendingChild;
        internal static ThreadInstance? TargetThreadWithChildrenOfChildren;
        internal static ThreadInstance? TargetParameterizedThreadWithChildrenOfChildren;
        internal static ThreadInstance? TargetThreadWithAppendingChildrenOfChildren;
        internal static ThreadInstance? TargetParameterizedThreadWithAppendingChildrenOfChildren;

        /// <summary>
        /// Tests initializing thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeThreadInstance() =>
            TargetThread = new ThreadInstance("Unit test thread #1", true, ThreadInstanceTestHelper.WriteHello);

        /// <summary>
        /// Tests initializing kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeKernelParameterizedThread() =>
            TargetParameterizedThread = new ThreadInstance("Unit test thread #2", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgument("Hello"));

        /// <summary>
        /// Tests initializing thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeThreadInstanceWithChildThread()
        {
            TargetThreadWithChild = new ThreadInstance("Unit test thread #3", true, ThreadInstanceTestHelper.WriteHelloWithChild);
            TargetThreadWithChild.AddChild("Unit test child thread #1 for parent thread #3", true, ThreadInstanceTestHelper.WriteHelloFromChild);
            TargetThreadWithChild.AddChild("Unit test child thread #2 for parent thread #3", true, ThreadInstanceTestHelper.WriteHelloFromChild);
            TargetThreadWithChild.AddChild("Unit test child thread #3 for parent thread #3", true, ThreadInstanceTestHelper.WriteHelloFromChild);
        }

        /// <summary>
        /// Tests initializing kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeKernelParameterizedThreadWithChildThread()
        {
            TargetParameterizedThreadWithChild = new ThreadInstance("Unit test thread #4", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentWithChild("Hello"));
            TargetParameterizedThreadWithChild.AddChild("Unit test child thread #1 for parent thread #4", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChild("Hello"));
            TargetParameterizedThreadWithChild.AddChild("Unit test child thread #2 for parent thread #4", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChild("Hello"));
            TargetParameterizedThreadWithChild.AddChild("Unit test child thread #3 for parent thread #4", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChild("Hello"));
        }

        /// <summary>
        /// Tests initializing thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeThreadInstanceWithAppendingChildThread()
        {
            TargetThreadWithAppendingChild = new ThreadInstance("Unit test thread #5", true, ThreadInstanceTestHelper.WriteHelloWithAppendingChild);
            TargetThreadWithAppendingChild.AddChild("Unit test child thread #1 for parent thread #5", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChild);
            TargetThreadWithAppendingChild.AddChild("Unit test child thread #2 for parent thread #5", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChild);
            TargetThreadWithAppendingChild.AddChild("Unit test child thread #3 for parent thread #5", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChild);
        }

        /// <summary>
        /// Tests initializing kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeKernelParameterizedThreadWithAppendingChildThread()
        {
            TargetParameterizedThreadWithAppendingChild = new ThreadInstance("Unit test thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentWithAppendingChild("Hello"));
            TargetParameterizedThreadWithAppendingChild.AddChild("Unit test child thread #1 for parent thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChild("Hello"));
            TargetParameterizedThreadWithAppendingChild.AddChild("Unit test child thread #2 for parent thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChild("Hello"));
            TargetParameterizedThreadWithAppendingChild.AddChild("Unit test child thread #3 for parent thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChild("Hello"));
        }

        /// <summary>
        /// Tests initializing thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeThreadInstanceWithChildrenOfChildrenThread()
        {
            TargetThreadWithChildrenOfChildren = new ThreadInstance("Unit test thread #7", true, ThreadInstanceTestHelper.WriteHelloWithChildrenOfChildren);
            TargetThreadWithChildrenOfChildren.AddChild("Unit test child thread #1 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            TargetThreadWithChildrenOfChildren.AddChild("Unit test child thread #2 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            TargetThreadWithChildrenOfChildren.AddChild("Unit test child thread #3 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            var child1 = TargetThreadWithChildrenOfChildren.GetChild(0);
            var child2 = TargetThreadWithChildrenOfChildren.GetChild(1);
            var child3 = TargetThreadWithChildrenOfChildren.GetChild(2);
            child1.AddChild("Unit test child thread #1 of child thread #1 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child1.AddChild("Unit test child thread #2 of child thread #1 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child1.AddChild("Unit test child thread #3 of child thread #1 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child2.AddChild("Unit test child thread #1 of child thread #2 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child2.AddChild("Unit test child thread #2 of child thread #2 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child2.AddChild("Unit test child thread #3 of child thread #2 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child3.AddChild("Unit test child thread #1 of child thread #3 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child3.AddChild("Unit test child thread #2 of child thread #3 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
            child3.AddChild("Unit test child thread #3 of child thread #3 for parent thread #7", true, ThreadInstanceTestHelper.WriteHelloFromChildrenOfChildren);
        }

        /// <summary>
        /// Tests initializing kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeKernelParameterizedThreadWithChildrenOfChildrenThread()
        {
            TargetParameterizedThreadWithChildrenOfChildren = new ThreadInstance("Unit test thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentWithChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithChildrenOfChildren.AddChild("Unit test child thread #1 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithChildrenOfChildren.AddChild("Unit test child thread #2 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithChildrenOfChildren.AddChild("Unit test child thread #3 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            var child1 = TargetParameterizedThreadWithChildrenOfChildren.GetChild(0);
            var child2 = TargetParameterizedThreadWithChildrenOfChildren.GetChild(1);
            var child3 = TargetParameterizedThreadWithChildrenOfChildren.GetChild(2);
            child1.AddChild("Unit test child thread #1 of child thread #1 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child1.AddChild("Unit test child thread #2 of child thread #1 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child1.AddChild("Unit test child thread #3 of child thread #1 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child2.AddChild("Unit test child thread #1 of child thread #2 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child2.AddChild("Unit test child thread #2 of child thread #2 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child2.AddChild("Unit test child thread #3 of child thread #2 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child3.AddChild("Unit test child thread #1 of child thread #3 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child3.AddChild("Unit test child thread #2 of child thread #3 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
            child3.AddChild("Unit test child thread #3 of child thread #3 for parent thread #8", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromChildrenOfChildren("Hello"));
        }

        /// <summary>
        /// Tests initializing thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeThreadInstanceWithAppendingChildrenOfChildrenThread()
        {
            TargetThreadWithAppendingChildrenOfChildren = new ThreadInstance("Unit test thread #9", true, ThreadInstanceTestHelper.WriteHelloWithAppendingChildrenOfChildren);
            TargetThreadWithAppendingChildrenOfChildren.AddChild("Unit test child thread #1 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            TargetThreadWithAppendingChildrenOfChildren.AddChild("Unit test child thread #2 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            TargetThreadWithAppendingChildrenOfChildren.AddChild("Unit test child thread #3 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            var child1 = TargetThreadWithAppendingChildrenOfChildren.GetChild(0);
            var child2 = TargetThreadWithAppendingChildrenOfChildren.GetChild(1);
            var child3 = TargetThreadWithAppendingChildrenOfChildren.GetChild(2);
            child1.AddChild("Unit test child thread #1 of child thread #1 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child1.AddChild("Unit test child thread #2 of child thread #1 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child1.AddChild("Unit test child thread #3 of child thread #1 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child2.AddChild("Unit test child thread #1 of child thread #2 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child2.AddChild("Unit test child thread #2 of child thread #2 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child2.AddChild("Unit test child thread #3 of child thread #2 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child3.AddChild("Unit test child thread #1 of child thread #3 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child3.AddChild("Unit test child thread #2 of child thread #3 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child3.AddChild("Unit test child thread #3 of child thread #3 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
        }

        /// <summary>
        /// Tests initializing kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestInitializeKernelParameterizedThreadWithAppendingChildrenOfChildrenThread()
        {
            TargetParameterizedThreadWithAppendingChildrenOfChildren = new ThreadInstance("Unit test thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentWithAppendingChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithAppendingChildrenOfChildren.AddChild("Unit test child thread #1 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithAppendingChildrenOfChildren.AddChild("Unit test child thread #2 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithAppendingChildrenOfChildren.AddChild("Unit test child thread #3 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            var child1 = TargetParameterizedThreadWithAppendingChildrenOfChildren.GetChild(0);
            var child2 = TargetParameterizedThreadWithAppendingChildrenOfChildren.GetChild(1);
            var child3 = TargetParameterizedThreadWithAppendingChildrenOfChildren.GetChild(2);
            child1.AddChild("Unit test child thread #1 of child thread #1 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child1.AddChild("Unit test child thread #2 of child thread #1 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child1.AddChild("Unit test child thread #3 of child thread #1 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child2.AddChild("Unit test child thread #1 of child thread #2 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child2.AddChild("Unit test child thread #2 of child thread #2 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child2.AddChild("Unit test child thread #3 of child thread #2 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child3.AddChild("Unit test child thread #1 of child thread #3 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child3.AddChild("Unit test child thread #2 of child thread #3 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child3.AddChild("Unit test child thread #3 of child thread #3 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
        }

        /// <summary>
        /// Tests starting thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartThreadInstance() =>
            TargetThread?.Start();

        /// <summary>
        /// Tests starting kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartKernelParameterizedThread() =>
            TargetParameterizedThread?.Start("Agustin");

        /// <summary>
        /// Tests starting thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartThreadInstanceWithChildThread() =>
            TargetThreadWithChild?.Start();

        /// <summary>
        /// Tests starting kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartKernelParameterizedThreadWithChildThread() =>
            TargetParameterizedThreadWithChild?.Start("Agustin");

        /// <summary>
        /// Tests starting thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartThreadInstanceWithAppendingChildThread()
        {
            TargetThreadWithAppendingChild?.Start();
            Thread.Sleep(1000);
            TargetThreadWithAppendingChild?.AddChild("Unit test additional child thread #4 for parent thread #5", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChild);
            TargetThreadWithAppendingChild?.AddChild("Unit test additional child thread #5 for parent thread #5", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChild);
            TargetThreadWithAppendingChild?.AddChild("Unit test additional child thread #6 for parent thread #5", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChild);
        }

        /// <summary>
        /// Tests starting kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartKernelParameterizedThreadWithAppendingChildThread()
        {
            TargetParameterizedThreadWithAppendingChild?.Start("Agustin");
            Thread.Sleep(1000);
            TargetParameterizedThreadWithAppendingChild?.AddChild("Unit test child thread #4 for parent thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChild("Hello"));
            TargetParameterizedThreadWithAppendingChild?.AddChild("Unit test child thread #5 for parent thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChild("Hello"));
            TargetParameterizedThreadWithAppendingChild?.AddChild("Unit test child thread #6 for parent thread #6", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChild("Hello"));
        }

        /// <summary>
        /// Tests starting thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartThreadInstanceWithChildrenOfChildrenThread() =>
            TargetThreadWithChildrenOfChildren?.Start();

        /// <summary>
        /// Tests starting kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartKernelParameterizedThreadWithChildrenOfChildrenThread() =>
            TargetParameterizedThreadWithChildrenOfChildren?.Start("Agustin");

        /// <summary>
        /// Tests starting thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartThreadInstanceWithAppendingChildrenOfChildrenThread()
        {
            TargetThreadWithAppendingChildrenOfChildren?.Start();
            Thread.Sleep(1000);
            TargetThreadWithAppendingChildrenOfChildren?.AddChild("Unit test additional child thread #4 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            TargetThreadWithAppendingChildrenOfChildren?.AddChild("Unit test additional child thread #5 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            TargetThreadWithAppendingChildrenOfChildren?.AddChild("Unit test additional child thread #6 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            var child4 = TargetThreadWithAppendingChildrenOfChildren?.GetChild(3);
            var child5 = TargetThreadWithAppendingChildrenOfChildren?.GetChild(4);
            var child6 = TargetThreadWithAppendingChildrenOfChildren?.GetChild(5);
            child4?.AddChild("Unit test child thread #1 of child thread #4 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child4?.AddChild("Unit test child thread #2 of child thread #4 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child4?.AddChild("Unit test child thread #3 of child thread #4 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child5?.AddChild("Unit test child thread #1 of child thread #5 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child5?.AddChild("Unit test child thread #2 of child thread #5 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child5?.AddChild("Unit test child thread #3 of child thread #5 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child6?.AddChild("Unit test child thread #1 of child thread #6 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child6?.AddChild("Unit test child thread #2 of child thread #6 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
            child6?.AddChild("Unit test child thread #3 of child thread #6 for parent thread #9", true, ThreadInstanceTestHelper.WriteHelloFromAppendingChildrenOfChildren);
        }

        /// <summary>
        /// Tests starting kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStartKernelParameterizedThreadWithAppendingChildrenOfChildrenThread()
        {
            TargetParameterizedThreadWithAppendingChildrenOfChildren?.Start("Agustin");
            Thread.Sleep(1000);
            TargetParameterizedThreadWithAppendingChildrenOfChildren?.AddChild("Unit test child thread #4 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithAppendingChildrenOfChildren?.AddChild("Unit test child thread #5 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            TargetParameterizedThreadWithAppendingChildrenOfChildren?.AddChild("Unit test child thread #6 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            var child4 = TargetParameterizedThreadWithAppendingChildrenOfChildren?.GetChild(3);
            var child5 = TargetParameterizedThreadWithAppendingChildrenOfChildren?.GetChild(4);
            var child6 = TargetParameterizedThreadWithAppendingChildrenOfChildren?.GetChild(5);
            child4?.AddChild("Unit test child thread #1 of child thread #4 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child4?.AddChild("Unit test child thread #2 of child thread #4 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child4?.AddChild("Unit test child thread #3 of child thread #4 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child5?.AddChild("Unit test child thread #1 of child thread #5 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child5?.AddChild("Unit test child thread #2 of child thread #5 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child5?.AddChild("Unit test child thread #3 of child thread #5 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child6?.AddChild("Unit test child thread #1 of child thread #6 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child6?.AddChild("Unit test child thread #2 of child thread #6 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
            child6?.AddChild("Unit test child thread #3 of child thread #6 for parent thread #10", true, (_) => ThreadInstanceTestHelper.WriteHelloWithArgumentFromAppendingChildrenOfChildren("Hello"));
        }

        /// <summary>
        /// Tests stopping thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopThreadInstance()
        {
            Thread.Sleep(300);
            TargetThread?.Stop();
            TargetThread?.IsStopping.ShouldBeFalse();
            TargetThread?.ShouldNotBeNull();
        }

        /// <summary>
        /// Tests stopping kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopKernelParameterizedThread()
        {
            Thread.Sleep(300);
            TargetParameterizedThread?.Stop();
            TargetParameterizedThread?.IsStopping.ShouldBeFalse();
            TargetParameterizedThread?.ShouldNotBeNull();
        }

        /// <summary>
        /// Tests stopping thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopThreadInstanceWithChildThread()
        {
            if (TargetThreadWithChild is null)
                throw new Exception($"{nameof(TargetThreadWithChild)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetThreadWithChild.Stop, TimeSpan.FromSeconds(5));
            TargetThreadWithChild.IsStopping.ShouldBeFalse();
            TargetThreadWithChild.ShouldNotBeNull();
            foreach (var childThread in TargetThreadWithChild.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
            }
        }

        /// <summary>
        /// Tests stopping kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopKernelParameterizedThreadWithChildThread()
        {
            if (TargetParameterizedThreadWithChild is null)
                throw new Exception($"{nameof(TargetParameterizedThreadWithChild)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetParameterizedThreadWithChild.Stop, TimeSpan.FromSeconds(5));
            TargetParameterizedThreadWithChild.IsStopping.ShouldBeFalse();
            TargetParameterizedThreadWithChild.ShouldNotBeNull();
            foreach (var childThread in TargetParameterizedThreadWithChild.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
            }
        }

        /// <summary>
        /// Tests stopping thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopThreadInstanceWithAppendingChildThread()
        {
            if (TargetThreadWithAppendingChild is null)
                throw new Exception($"{nameof(TargetThreadWithAppendingChild)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetThreadWithAppendingChild.Stop, TimeSpan.FromSeconds(5));
            TargetThreadWithAppendingChild.IsStopping.ShouldBeFalse();
            TargetThreadWithAppendingChild.ShouldNotBeNull();
            foreach (var childThread in TargetThreadWithAppendingChild.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
            }
        }

        /// <summary>
        /// Tests stopping kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopKernelParameterizedThreadWithAppendingChildThread()
        {
            if (TargetParameterizedThreadWithAppendingChild is null)
                throw new Exception($"{nameof(TargetParameterizedThreadWithAppendingChild)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetParameterizedThreadWithAppendingChild.Stop, TimeSpan.FromSeconds(5));
            TargetParameterizedThreadWithAppendingChild.IsStopping.ShouldBeFalse();
            TargetParameterizedThreadWithAppendingChild.ShouldNotBeNull();
            foreach (var childThread in TargetParameterizedThreadWithAppendingChild.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
            }
        }

        /// <summary>
        /// Tests stopping thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopThreadInstanceWithChildrenOfChildrenThread()
        {
            if (TargetThreadWithChildrenOfChildren is null)
                throw new Exception($"{nameof(TargetThreadWithChildrenOfChildren)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetThreadWithChildrenOfChildren.Stop, TimeSpan.FromSeconds(5));
            TargetThreadWithChildrenOfChildren.IsStopping.ShouldBeFalse();
            TargetThreadWithChildrenOfChildren.ShouldNotBeNull();
            foreach (var childThread in TargetThreadWithChildrenOfChildren.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
                foreach (var childChildThread in childThread.childThreads)
                {
                    childChildThread.IsStopping.ShouldBeFalse();
                    childChildThread.ShouldNotBeNull();
                }
            }
        }

        /// <summary>
        /// Tests stopping kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopKernelParameterizedThreadWithChildrenOfChildrenThread()
        {
            if (TargetParameterizedThreadWithChildrenOfChildren is null)
                throw new Exception($"{nameof(TargetParameterizedThreadWithChildrenOfChildren)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetParameterizedThreadWithChildrenOfChildren.Stop, TimeSpan.FromSeconds(5));
            TargetParameterizedThreadWithChildrenOfChildren.IsStopping.ShouldBeFalse();
            TargetParameterizedThreadWithChildrenOfChildren.ShouldNotBeNull();
            foreach (var childThread in TargetParameterizedThreadWithChildrenOfChildren.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
                foreach (var childChildThread in childThread.childThreads)
                {
                    childChildThread.IsStopping.ShouldBeFalse();
                    childChildThread.ShouldNotBeNull();
                }
            }
        }

        /// <summary>
        /// Tests stopping thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopThreadInstanceWithAppendingChildrenOfChildrenThread()
        {
            if (TargetThreadWithAppendingChildrenOfChildren is null)
                throw new Exception($"{nameof(TargetThreadWithAppendingChildrenOfChildren)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetThreadWithAppendingChildrenOfChildren.Stop, TimeSpan.FromSeconds(5));
            TargetThreadWithAppendingChildrenOfChildren.IsStopping.ShouldBeFalse();
            TargetThreadWithAppendingChildrenOfChildren.ShouldNotBeNull();
            foreach (var childThread in TargetThreadWithAppendingChildrenOfChildren.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
                foreach (var childChildThread in childThread.childThreads)
                {
                    childChildThread.IsStopping.ShouldBeFalse();
                    childChildThread.ShouldNotBeNull();
                }
            }
        }

        /// <summary>
        /// Tests stopping kernel parameterized thread
        /// </summary>
        [TestMethod]
        [Description("Initialization")]
        public void TestStopKernelParameterizedThreadWithAppendingChildrenOfChildrenThread()
        {
            if (TargetParameterizedThreadWithAppendingChildrenOfChildren is null)
                throw new Exception($"{nameof(TargetParameterizedThreadWithAppendingChildrenOfChildren)} is null");
            Thread.Sleep(500);
            Should.CompleteIn(TargetParameterizedThreadWithAppendingChildrenOfChildren.Stop, TimeSpan.FromSeconds(5));
            TargetParameterizedThreadWithAppendingChildrenOfChildren.IsStopping.ShouldBeFalse();
            TargetParameterizedThreadWithAppendingChildrenOfChildren.ShouldNotBeNull();
            foreach (var childThread in TargetParameterizedThreadWithAppendingChildrenOfChildren.childThreads)
            {
                childThread.IsStopping.ShouldBeFalse();
                childThread.ShouldNotBeNull();
                foreach (var childChildThread in childThread.childThreads)
                {
                    childChildThread.IsStopping.ShouldBeFalse();
                    childChildThread.ShouldNotBeNull();
                }
            }
        }

        /// <summary>
        /// Tests getting actual milliseconds
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetActualMilliseconds()
        {
            int actualMilliseconds = ThreadManager.GetActualMilliseconds(1);
            actualMilliseconds.ShouldBeGreaterThanOrEqualTo(1);
        }

        /// <summary>
        /// Tests getting actual milliseconds
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetActualMillisecondsRecursive()
        {
            for (int i = 1; i <= 20; i++)
            {
                int actualMilliseconds = ThreadManager.GetActualMilliseconds(i);
                actualMilliseconds.ShouldBeGreaterThanOrEqualTo(i);
            }
        }

        /// <summary>
        /// Tests getting actual ticks
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetActualTicks()
        {
            long actualTicks = ThreadManager.GetActualTicks(1);
            actualTicks.ShouldBeGreaterThanOrEqualTo(1000);
        }

        /// <summary>
        /// Tests getting actual ticks
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetActualTicksRecursive()
        {
            for (int i = 1; i <= 20; i++)
            {
                long actualTicks = ThreadManager.GetActualTicks(i);
                actualTicks.ShouldBeGreaterThanOrEqualTo(i * 1000);
            }
        }

        /// <summary>
        /// Tests getting actual milliseconds
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetActualTimeSpan()
        {
            var actualSpan = ThreadManager.GetActualTimeSpan(1);
            actualSpan.Milliseconds.ShouldBeGreaterThanOrEqualTo(1);
            actualSpan.TotalMilliseconds.ShouldBeGreaterThanOrEqualTo(1);
            actualSpan.Ticks.ShouldBeGreaterThanOrEqualTo(1000);
        }

        /// <summary>
        /// Tests getting actual milliseconds
        /// </summary>
        [TestMethod]
        [Description("Querying")]
        public void TestGetActualTimeSpanRecursive()
        {
            for (int i = 1; i <= 20; i++)
            {
                var actualSpan = ThreadManager.GetActualTimeSpan(i);
                actualSpan.Milliseconds.ShouldBeGreaterThanOrEqualTo(i);
                actualSpan.TotalMilliseconds.ShouldBeGreaterThanOrEqualTo(i);
                actualSpan.Ticks.ShouldBeGreaterThanOrEqualTo(i * 1000);
            }
        }

    }
}
