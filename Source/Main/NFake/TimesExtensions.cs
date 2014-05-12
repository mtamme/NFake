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