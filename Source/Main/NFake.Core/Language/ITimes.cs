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

namespace NFake.Core.Language
{
    /// <summary>
    /// Defines the <c>Exactly</c>, <c>AtMost</c> and <c>AtLeast</c> adjectives.
    /// </summary>
    public interface ITimes : IFluent
    {
        /// <summary>
        /// Specifies that the expected number of invocations should be exactly times.
        /// </summary>
        /// <param name="times">The number of invocations.</param>
        void Exactly(int times);

        /// <summary>
        /// Specifies that the expected number of invocations should be at most times.
        /// </summary>
        /// <param name="times">The number of invocations.</param>
        void AtMost(int times);

        /// <summary>
        /// Specifies that the expected number of invocations should be at least times.
        /// </summary>
        /// <param name="times">The number of invocations.</param>
        void AtLeast(int times);
    }
}