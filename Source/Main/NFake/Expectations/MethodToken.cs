﻿//
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
using System.Text;

namespace NFake.Expectations
{
    /// <summary>
    /// Represents a value which uniquely identifies a method.
    /// </summary>
    internal struct MethodToken : IEquatable<MethodToken>
    {
        /// <summary>
        /// The declaring type.
        /// </summary>
        private readonly Type _declaringType;

        /// <summary>
        /// The metadata token.
        /// </summary>
        private readonly int _metadataToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodToken"/> struct.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        public MethodToken(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            _declaringType = methodInfo.DeclaringType;
            _metadataToken = methodInfo.MetadataToken;
        }

        #region IEquatable<MethodToken> Members

        /// <inheritdoc/>
        public bool Equals(MethodToken other)
        {
            if (other._declaringType != _declaringType)
                return false;

            return (other._metadataToken == _metadataToken);
        }

        #endregion

        #region Object Members

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _metadataToken;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return (obj is MethodToken) && Equals((MethodToken) obj);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(_declaringType);
            sb.Append(Type.Delimiter);
            sb.Append(_metadataToken);

            return sb.ToString();
        }

        #endregion
    }
}