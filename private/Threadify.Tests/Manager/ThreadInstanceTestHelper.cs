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

using System.Threading;
using System;
using Shouldly;

namespace Threadify.Tests.Manager
{

    public static class ThreadInstanceTestHelper
    {

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHello()
        {
            try
            {
                Console.WriteLine("Hello world!");
                Console.WriteLine("- Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThread?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThread?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgument(string Name)
        {
            try
            {
                Console.WriteLine("Hello, {0}!", Name);
                Console.WriteLine("- Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThread?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThread?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloWithChild()
        {
            try
            {
                Console.WriteLine("Hello world!");
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentWithChild(string Name)
        {
            try
            {
                Console.WriteLine("Hello, {0}!", Name);
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloFromChild()
        {
            try
            {
                Console.WriteLine("- Hello world!");
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentFromChild(string Name)
        {
            try
            {
                Console.WriteLine("- Hello, {0}!", Name);
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloWithAppendingChild()
        {
            try
            {
                Console.WriteLine("Hello world!");
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithAppendingChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithAppendingChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentWithAppendingChild(string Name)
        {
            try
            {
                Console.WriteLine("Hello, {0}!", Name);
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithAppendingChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithAppendingChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloFromAppendingChild()
        {
            try
            {
                Console.WriteLine("- Hello world!");
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithAppendingChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithAppendingChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentFromAppendingChild(string Name)
        {
            try
            {
                Console.WriteLine("- Hello, {0}!", Name);
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithAppendingChild?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithAppendingChild?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloWithChildrenOfChildren()
        {
            try
            {
                Console.WriteLine("Hello world!");
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentWithChildrenOfChildren(string Name)
        {
            try
            {
                Console.WriteLine("Hello, {0}!", Name);
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloFromChildrenOfChildren()
        {
            try
            {
                Console.WriteLine("- Hello world!");
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentFromChildrenOfChildren(string Name)
        {
            try
            {
                Console.WriteLine("- Hello, {0}!", Name);
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloWithAppendingChildrenOfChildren()
        {
            try
            {
                Console.WriteLine("Hello world!");
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithAppendingChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithAppendingChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentWithAppendingChildrenOfChildren(string Name)
        {
            try
            {
                Console.WriteLine("Hello, {0}!", Name);
                Console.WriteLine("- Writing from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithAppendingChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("- Goodbye from parent thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithAppendingChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console
        /// </summary>
        public static void WriteHelloFromAppendingChildrenOfChildren()
        {
            try
            {
                Console.WriteLine("- Hello world!");
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetThreadWithAppendingChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetThreadWithAppendingChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

        /// <summary>
        /// [Thread test] Write hello to console with argument
        /// </summary>
        public static void WriteHelloWithArgumentFromAppendingChildrenOfChildren(string Name)
        {
            try
            {
                Console.WriteLine("- Hello, {0}!", Name);
                Console.WriteLine("  - Writing from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                while (!ThreadInstanceTests.TargetParameterizedThreadWithAppendingChildrenOfChildren?.IsStopping ?? true)
                    Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("  - Goodbye from thread: {0} [{1}]", Thread.CurrentThread.Name, Environment.CurrentManagedThreadId);
                ThreadInstanceTests.TargetParameterizedThreadWithAppendingChildrenOfChildren?.IsStopping.ShouldBeTrue();
            }
        }

    }
}
