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
using System.Reflection;
using NProxy.Core;

namespace NFake
{
    /// <summary>
    /// Represents a mock interception filter.
    /// </summary>
    internal sealed class MockInterceptionFilter : IInterceptionFilter
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static readonly MockInterceptionFilter Instance = new MockInterceptionFilter();

        /// <summary>
        /// The name of the destructor method.
        /// </summary>
        private const string DestructorMethodName = "Finalize";

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptionFilter"/> class.
        /// </summary>
        private MockInterceptionFilter()
        {
        }

        #region IInterceptionFilter Members

        /// <inheritdoc/>
        public bool AcceptEvent(EventInfo eventInfo)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool AcceptProperty(PropertyInfo propertyInfo)
        {
            return true;
        }

        /// <inheritdoc/>
        public bool AcceptMethod(MethodInfo methodInfo)
        {
            // Don't intercept destructor method.
            if (methodInfo.DeclaringType != typeof (object))
                return true;

            return !String.Equals(methodInfo.Name, DestructorMethodName);
        }

        #endregion
    }
}