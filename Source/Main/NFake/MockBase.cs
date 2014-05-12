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

using NProxy.Core;

namespace NFake
{
    /// <summary>
    /// The mock base class.
    /// </summary>
    public abstract class MockBase
    {
        /// <summary>
        /// The proxy factory.
        /// </summary>
        protected static readonly IProxyFactory ProxyFactory;

        /// <summary>
        /// Initializes the <see cref="MockBase"/> class.
        /// </summary>
        static MockBase()
        {
            ProxyFactory = new ProxyFactory(MockInterceptionFilter.Instance);
        }
    }
}