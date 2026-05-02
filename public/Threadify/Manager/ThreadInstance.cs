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
using System.Linq;
using System.Threading;
using Threadify.Exceptions;
using Threadify.Languages;

namespace Threadify.Manager
{
    /// <summary>
    /// The thread to simplify the access to making new threads, starting them, and stopping them
    /// </summary>
    public class ThreadInstance
    {

        internal Thread baseThread;
        internal readonly List<ThreadInstance> childThreads = [];
        private bool isReady;
        private bool isStopping;
        private bool isFailed;
        private readonly List<Exception> threadFailures = [];
        private readonly ThreadStart? ThreadDelegate;
        private readonly ThreadStart? InitialThreadDelegate;
        private readonly ParameterizedThreadStart? ThreadDelegateParameterized;
        private readonly ParameterizedThreadStart? InitialThreadDelegateParameterized;
        private readonly bool IsParameterized;
        private readonly ThreadInstance? parentThread;

        /// <summary>
        /// The name of the thread
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Is the thread a background thread?
        /// </summary>
        public bool IsBackground { get; private set; }

        /// <summary>
        /// Is the thread alive?
        /// </summary>
        public bool IsAlive =>
            baseThread.IsAlive;

        /// <summary>
        /// Is the thread ready to start?
        /// </summary>
        public bool IsReady =>
            isReady;

        /// <summary>
        /// Is the thread stopping? Use this flag to make your thread stop all operations when <see cref="Stop()"/> is called.
        /// </summary>
        public bool IsStopping =>
            isStopping;

        /// <summary>
        /// Has this thread failed?
        /// </summary>
        public bool IsFailed =>
            isFailed;

        /// <summary>
        /// If <see cref="IsFailed"/> is true, a list of exceptions that describe the thread failure.
        /// </summary>
        public List<Exception> ThreadFailures =>
            threadFailures;

        /// <summary>
        /// Parent thread. If this thread is a parent thread, returns null. On child threads, returns a parent thread that spawned this thread.
        /// </summary>
        public ThreadInstance? ParentThread =>
            parentThread;

        /// <summary>
        /// Thread ID for this thread
        /// </summary>
        public int ThreadId =>
            baseThread.ManagedThreadId;

        /// <summary>
        /// Gets child threads count
        /// </summary>
        public int ChildThreadCount =>
            childThreads.Count;

        /// <summary>
        /// Makes a new thread
        /// </summary>
        /// <param name="threadName">The thread name</param>
        /// <param name="background">Indicates if the thread is background</param>
        /// <param name="executor">The thread delegate</param>
        public ThreadInstance(string threadName, bool background, ThreadStart executor) :
            this(threadName, background, executor, false, null)
        { }

        /// <summary>
        /// Makes a new thread
        /// </summary>
        /// <param name="threadName">The thread name</param>
        /// <param name="background">Indicates if the thread is background</param>
        /// <param name="executor">The thread delegate</param>
        /// <param name="child">Specifies whether the thread is going to be a child thread</param>
        /// <param name="parentThread">If <paramref name="child"/> is on, this should be specified to specify a thread that spawned the parent thread</param>
        private ThreadInstance(string threadName, bool background, ThreadStart executor, bool child, ThreadInstance? parentThread)
        {
            InitialThreadDelegate = executor;
            executor = () => StartInternalNormal(InitialThreadDelegate);
            baseThread = new Thread(executor) { Name = threadName, IsBackground = background };
            IsParameterized = false;
            ThreadDelegate = executor;
            Name = threadName;
            IsBackground = background;
            isReady = true;
            if (child && parentThread is null)
                child = false;
            if (!child)
                ThreadManager.threadInstances.Add(this);
            else
                this.parentThread = parentThread;
        }

        /// <summary>
        /// Makes a new thread
        /// </summary>
        /// <param name="threadName">The thread name</param>
        /// <param name="background">Indicates if the thread is background</param>
        /// <param name="executor">The thread delegate</param>
        public ThreadInstance(string threadName, bool background, ParameterizedThreadStart executor) :
            this(threadName, background, executor, false, null)
        { }

        /// <summary>
        /// Makes a new thread
        /// </summary>
        /// <param name="threadName">The thread name</param>
        /// <param name="background">Indicates if the thread is background</param>
        /// <param name="executor">The thread delegate</param>
        /// <param name="child">Specifies whether the thread is going to be a child thread</param>
        /// <param name="parentThread">If <paramref name="child"/> is on, this should be specified to specify a thread that spawned the parent thread</param>
        private ThreadInstance(string threadName, bool background, ParameterizedThreadStart executor, bool child, ThreadInstance? parentThread)
        {
            InitialThreadDelegateParameterized = executor;
            executor = (Parameter) => StartInternalParameterized(InitialThreadDelegateParameterized, Parameter);
            baseThread = new Thread(executor) { Name = threadName, IsBackground = background };
            IsParameterized = true;
            ThreadDelegateParameterized = executor;
            Name = threadName;
            IsBackground = background;
            isReady = true;
            if (child && parentThread is null)
                child = false;
            if (!child)
                ThreadManager.threadInstances.Add(this);
            else
                this.parentThread = parentThread;
        }

        /// <summary>
        /// Starts the thread
        /// </summary>
        public void Start()
        {
            if (!IsReady)
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_NOTREADY"));
            if (baseThread.ThreadState.HasFlag(ThreadState.Stopped) || IsAlive)
                return;

            // Start the parent thread
            isFailed = false;
            threadFailures.Clear();
            baseThread.Start();
            StartChildThreads(null);
        }

        /// <summary>
        /// Starts the thread
        /// </summary>
        /// <param name="parameter">The parameter class instance containing multiple parameters, or a usual single parameter</param>
        public void Start(object? parameter)
        {
            if (!IsReady)
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_NOTREADY"));
            if (baseThread.ThreadState.HasFlag(ThreadState.Stopped) || IsAlive)
                return;

            // Start the parent thread
            isFailed = false;
            threadFailures.Clear();
            baseThread.Start(parameter);
            StartChildThreads(parameter);
        }

        /// <summary>
        /// Stops the thread
        /// </summary>
        public void Stop() =>
            Stop(true);

        /// <summary>
        /// Stops the thread
        /// </summary>
        /// <param name="regen">Whether to regenerate the thread</param>
        public void Stop(bool regen)
        {
            try
            {
                isStopping = true;
                baseThread.Interrupt();
                StopChildThreads();
                if (!Wait(60000))
                isReady = false;
                if (regen)
                    Regen();
                isFailed = false;
                threadFailures.Clear();
            }
            catch (Exception ex) when (ex.GetType().Name != nameof(ThreadInterruptedException) && ex.GetType().Name != nameof(ThreadStateException))
            {
                isFailed = true;
                threadFailures.Add(ex);
            }
            isStopping = false;
        }

        /// <summary>
        /// Waits for the thread to finish
        /// </summary>
        public void Wait()
        {
            if (!baseThread.IsAlive)
                return;

            try
            {
                baseThread.Join();
                WaitForChildThreads();
            }
            catch (Exception ex) when (ex.GetType().Name != nameof(ThreadInterruptedException) && ex.GetType().Name != nameof(ThreadStateException))
            {
                isFailed = true;
                threadFailures.Add(ex);
            }
        }

        /// <summary>
        /// Waits for the thread to finish unless the waiting timed out
        /// </summary>
        public bool Wait(int timeoutMs)
        {
            if (!baseThread.IsAlive)
                return true;

            try
            {
                bool SuccessfullyWaited = true;
                if (!baseThread.Join(timeoutMs))
                    SuccessfullyWaited = false;
                if (!WaitForChildThreads(timeoutMs))
                    SuccessfullyWaited = false;
                return SuccessfullyWaited;
            }
            catch (Exception ex) when (ex.GetType().Name != nameof(ThreadInterruptedException) && ex.GetType().Name != nameof(ThreadStateException))
            {
                isFailed = true;
                threadFailures.Add(ex);
            }
            return false;
        }

        /// <summary>
        /// Regenerates the thread
        /// </summary>
        public void Regen()
        {
            // We can't regen the thread unless Stop() is called first.
            if (IsReady && baseThread.ThreadState == ThreadState.Running)
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_REGENRUNNING"));

            // Remake the thread to avoid illegal state exceptions
            if (IsParameterized && ThreadDelegateParameterized is not null)
                baseThread = new Thread(ThreadDelegateParameterized) { Name = Name, IsBackground = IsBackground };
            else if (ThreadDelegate is not null)
                baseThread = new Thread(ThreadDelegate) { Name = Name, IsBackground = IsBackground };
            else
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_CANNOTREGEN") + $" {Name}");
            isReady = true;
        }

        /// <summary>
        /// Adds the child thread to this parent thread
        /// </summary>
        /// <param name="threadName">The thread name</param>
        /// <param name="background">Indicates if the thread is background</param>
        /// <param name="executor">The thread delegate</param>
        /// <exception cref="ThreadingException"></exception>
        public void AddChild(string threadName, bool background, ThreadStart executor)
        {
            if (executor is null)
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_ACTIONCANNOTBENULL"));

            ThreadInstance target = new(threadName, background, executor, true, this);
            childThreads.Add(target);
            if (IsAlive && !IsStopping)
                target.Start();
        }

        /// <summary>
        /// Adds the child thread to this parent thread
        /// </summary>
        /// <param name="threadName">The thread name</param>
        /// <param name="background">Indicates if the thread is background</param>
        /// <param name="executor">The thread delegate</param>
        /// <exception cref="ThreadingException"></exception>
        public void AddChild(string threadName, bool background, ParameterizedThreadStart executor)
        {
            if (executor is null)
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_ACTIONCANNOTBENULL"));

            ThreadInstance target = new(threadName, background, executor, true, this);
            childThreads.Add(target);
            if (IsAlive && !IsStopping)
                target.Start();
        }

        /// <summary>
        /// Gets an individual child thread
        /// </summary>
        /// <param name="threadIdx">Thread index</param>
        /// <returns>A <see cref="ThreadInstance"/> instance of a child thread</returns>
        /// <exception cref="ThreadingException"></exception>
        public ThreadInstance GetChild(int threadIdx)
        {
            if (threadIdx < 0 || threadIdx >= childThreads.Count)
                throw new ThreadingException(LanguageTools.GetLocalized("THREADIFY_MANAGER_EXCEPTION_CHILDIDXNOTVALID") + $" {childThreads.Count} [{threadIdx + 1}]");

            return childThreads[threadIdx];
        }

        private void StartInternalNormal(ThreadStart action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                threadFailures.Add(ex);
                isFailed = true;
            }
        }

        private void StartInternalParameterized(ParameterizedThreadStart action, object? arg)
        {
            try
            {
                action(arg);
            }
            catch (Exception ex)
            {
                threadFailures.Add(ex);
                isFailed = true;
            }
        }

        private void StartChildThreads(object? parameter)
        {
            // Start the child threads
            foreach (var ChildThread in childThreads)
            {
                if (ChildThread.IsParameterized)
                    ChildThread.Start(parameter);
                else
                    ChildThread.Start();
            }
        }

        private void StopChildThreads()
        {
            // Stop the child threads
            var ActiveChildThreads = childThreads.Where((thread) => thread.IsAlive).ToArray();
            foreach (var ChildThread in ActiveChildThreads)
            {
                ChildThread.Stop();
            }
        }

        private void WaitForChildThreads()
        {
            // Stop the child threads
            var ActiveChildThreads = childThreads.Where((thread) => thread.IsAlive).ToArray();
            foreach (var ChildThread in ActiveChildThreads)
            {
                // Just to make sure
                if (!ChildThread.IsAlive)
                    continue;

                ChildThread.Wait();
            }
        }

        private bool WaitForChildThreads(int timeoutMs)
        {
            // Stop the child threads
            var ActiveChildThreads = childThreads.Where((thread) => thread.IsAlive).ToArray();
            bool SuccessfullyWaited = true;
            foreach (var ChildThread in ActiveChildThreads)
            {
                // Just to make sure
                if (!ChildThread.IsAlive)
                    continue;

                if (!ChildThread.Wait(timeoutMs))
                    SuccessfullyWaited = false;
            }
            return SuccessfullyWaited;
        }
    }
}
