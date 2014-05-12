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

namespace NFake.Language
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