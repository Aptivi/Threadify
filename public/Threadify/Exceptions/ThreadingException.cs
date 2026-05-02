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

namespace Threadify.Exceptions
{
    /// <summary>
    /// An error occurred when performing a threading operation
    /// </summary>
    public class ThreadingException : Exception
    {
        /// <summary>
        /// Makes a new threading exception instance
        /// </summary>
        public ThreadingException()
        { }

        /// <summary>
        /// Makes a new threading exception instance
        /// </summary>
        /// <param name="message">Exception message</param>
        public ThreadingException(string message) :
            base(message)
        { }

        /// <summary>
        /// Makes a new threading exception instance
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception to use</param>
        public ThreadingException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
