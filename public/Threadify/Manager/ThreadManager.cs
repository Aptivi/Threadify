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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

#if NKS_EXTENSIONS
using Nitrocid.Base.Kernel.Extensions;
#endif

namespace Threadify.Manager
{
    /// <summary>
    /// Thread management module
    /// </summary>
    public static class ThreadManager
    {

        internal static List<ThreadInstance> threadInstances = [];

        /// <summary>
        /// Gets the operating system threads that the current process makes use of within the host OS
        /// </summary>
        public static ProcessThreadCollection OperatingSystemThreads =>
            Process.GetCurrentProcess().Threads;

        /// <summary>
        /// Gets all the threads
        /// </summary>
        public static List<ThreadInstance> ThreadInstances =>
            threadInstances;

        /// <summary>
        /// Gets active threads
        /// </summary>
        public static List<ThreadInstance> ActiveThreads =>
            threadInstances.Where(x => x.IsAlive).ToList();

        /// <summary>
        /// Stops all threads
        /// </summary>
        internal static void StopAllThreads()
        {
            foreach (ThreadInstance ThreadInstance in ThreadInstances)
                ThreadInstance.Stop();
        }

        /// <summary>
        /// Sleeps until either the time specified, or the current thread is no longer alive.
        /// </summary>
        /// <param name="Time">Time in milliseconds</param>
        public static bool SleepNoBlock(long Time) =>
            Thread.CurrentThread.Join((int)Time);

        /// <summary>
        /// Sleeps until either the time specified, or the thread is no longer alive.
        /// </summary>
        /// <param name="Time">Time in milliseconds</param>
        /// <param name="ThreadWork">The working thread</param>
        public static bool SleepNoBlock(long Time, Thread ThreadWork) =>
            ThreadWork.Join((int)Time);

        /// <summary>
        /// Sleeps until either the time specified, or the thread is no longer alive.
        /// </summary>
        /// <param name="Time">Time in milliseconds</param>
        /// <param name="ThreadWork">The working thread</param>
        public static bool SleepNoBlock(long Time, ThreadInstance ThreadWork) =>
            ThreadWork.Wait((int)Time);

        /// <summary>
        /// Gets the actual milliseconds time from the sleep time provided
        /// </summary>
        /// <param name="Time">Sleep time</param>
        /// <returns>How many milliseconds did it really sleep?</returns>
        public static int GetActualMilliseconds(int Time)
        {
            var SleepStopwatch = new Stopwatch();
            SleepStopwatch.Start();
            Thread.Sleep(Time);
            SleepStopwatch.Stop();
            return (int)SleepStopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Gets the actual ticks from the sleep time provided
        /// </summary>
        /// <param name="Time">Sleep time</param>
        /// <returns>How many ticks did it really sleep?</returns>
        public static long GetActualTicks(int Time)
        {
            var SleepStopwatch = new Stopwatch();
            SleepStopwatch.Start();
            Thread.Sleep(Time);
            SleepStopwatch.Stop();
            return SleepStopwatch.ElapsedTicks;
        }

        /// <summary>
        /// Gets the actual time span from the sleep time provided
        /// </summary>
        /// <param name="Time">Sleep time</param>
        /// <returns>The time span which describes the actual time span from the sleep time provided</returns>
        public static TimeSpan GetActualTimeSpan(int Time)
        {
            var SleepStopwatch = new Stopwatch();
            SleepStopwatch.Start();
            Thread.Sleep(Time);
            SleepStopwatch.Stop();
            return SleepStopwatch.Elapsed;
        }
    }
}
