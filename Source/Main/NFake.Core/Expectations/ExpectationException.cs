//
// NFake is a mocking library for the .NET framework.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
using System;

namespace NFake.Core.Expectations
{
    /// <summary>
    /// Represents an expectation exception.
    /// </summary>
    public sealed class ExpectationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ExpectationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ExpectationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
