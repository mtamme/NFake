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

namespace NFake.Language
{
    /// <summary>
    /// Defines the <c>Throws</c> verb.
    /// </summary>
    public interface IThrows : IFluent
    {
        /// <summary>
        /// Specifies the expected exception to be thrown.
        /// </summary>
        /// <param name="exception">The exception.</param>
        ITimes Throws(Exception exception);

        /// <summary>
        /// Specifies the expected exception to be thrown.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        ITimes Throws<TException>() where TException : Exception, new();
    }
}