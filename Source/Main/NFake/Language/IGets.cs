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
    /// Defines the <c>Gets</c> verb.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface IGets<in TValue> : IFluent
    {
        /// <summary>
        /// Specifies the expected value to be get.
        /// </summary>
        /// <param name="value">The value.</param>
        ITimes Gets(TValue value);
    }
}