//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
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