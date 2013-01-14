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

using System.Reflection;

namespace NFake.Core.Expectations
{
    /// <summary>
    /// Represents member information extensions.
    /// </summary>
    internal static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns a value which uniquely identifies a member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The unique identifier.</returns>
        public static long GetId(this MemberInfo memberInfo)
        {
            // FIXME The default implementation of the GetHashCode method
            // does not guarantee unique return values for different objects.
            // Furthermore, the .NET Framework does not guarantee the default
            // implementation of the GetHashCode method, and the value it returns
            // will be the same between different versions of the .NET Framework.
            long hashCode = memberInfo.Module.GetHashCode();
            long metadataToken = memberInfo.MetadataToken;

            return (hashCode << 32) | (metadataToken & 0xffffffffL);
        }
    }
}