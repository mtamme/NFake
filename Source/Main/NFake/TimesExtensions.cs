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

using NFake.Language;

namespace NFake
{
    /// <summary>
    /// Represents extensions methods for the <see cref="ITimes"/> interface.
    /// </summary>
    public static class TimesExtensions
    {
        /// <summary>
        /// Specifies that the expected number of invocations should be one.
        /// </summary>
        /// <param name="times">The times.</param>
        public static void Once(this ITimes times)
        {
            times.Exactly(1);
        }

        /// <summary>
        /// Specifies that the expected number of invocations should be zero.
        /// </summary>
        /// <param name="times">The times.</param>
        public static void Never(this ITimes times)
        {
            times.Exactly(0);
        }

        /// <summary>
        /// Specifies that the expected number of invocations should be at most one.
        /// </summary>
        /// <param name="times">The times.</param>
        public static void AtMostOnce(this ITimes times)
        {
            times.AtMost(1);
        }

        /// <summary>
        /// Specifies that the expected number of invocations should be at least one.
        /// </summary>
        /// <param name="times">The times.</param>
        public static void AtLeastOnce(this ITimes times)
        {
            times.AtLeast(1);
        }
    }
}