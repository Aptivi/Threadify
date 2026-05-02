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
            [.. threadInstances.Where(x => x.IsAlive)];

        /// <summary>
        /// Sleeps until either the time specified, or the current thread is no longer alive.
        /// </summary>
        /// <param name="time">Time in milliseconds</param>
        public static bool SleepNoBlock(long time) =>
            Thread.CurrentThread.Join((int)time);

        /// <summary>
        /// Sleeps until either the time specified, or the thread is no longer alive.
        /// </summary>
        /// <param name="time">Time in milliseconds</param>
        /// <param name="threadWork">The working thread</param>
        public static bool SleepNoBlock(long time, Thread threadWork) =>
            threadWork.Join((int)time);

        /// <summary>
        /// Sleeps until either the time specified, or the thread is no longer alive.
        /// </summary>
        /// <param name="time">time in milliseconds</param>
        /// <param name="threadWork">The working thread</param>
        public static bool SleepNoBlock(long time, ThreadInstance threadWork) =>
            threadWork.Wait((int)time);

        /// <summary>
        /// Gets the actual milliseconds time from the sleep time provided
        /// </summary>
        /// <param name="time">Sleep time</param>
        /// <returns>How many milliseconds did it really sleep?</returns>
        public static int GetActualMilliseconds(int time)
        {
            var SleepStopwatch = new Stopwatch();
            SleepStopwatch.Start();
            Thread.Sleep(time);
            SleepStopwatch.Stop();
            return (int)SleepStopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Gets the actual ticks from the sleep time provided
        /// </summary>
        /// <param name="time">Sleep time</param>
        /// <returns>How many ticks did it really sleep?</returns>
        public static long GetActualTicks(int time)
        {
            var SleepStopwatch = new Stopwatch();
            SleepStopwatch.Start();
            Thread.Sleep(time);
            SleepStopwatch.Stop();
            return SleepStopwatch.ElapsedTicks;
        }

        /// <summary>
        /// Gets the actual time span from the sleep time provided
        /// </summary>
        /// <param name="time">Sleep time</param>
        /// <returns>The time span which describes the actual time span from the sleep time provided</returns>
        public static TimeSpan GetActualTimeSpan(int time)
        {
            var SleepStopwatch = new Stopwatch();
            SleepStopwatch.Start();
            Thread.Sleep(time);
            SleepStopwatch.Stop();
            return SleepStopwatch.Elapsed;
        }
    }
}
