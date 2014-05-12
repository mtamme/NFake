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
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core;

namespace NFake.Expectations
{
    /// <summary>
    /// Represents an expectation invocation handler.
    /// </summary>
    internal sealed class ExpectationInvocationHandler : IInvocationHandler
    {
        /// <summary>
        /// The expectations.
        /// </summary>
        private readonly Dictionary<MethodToken, ExpectationBase> _expectations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectationInvocationHandler"/> class.
        /// </summary>
        public ExpectationInvocationHandler()
        {
            _expectations = new Dictionary<MethodToken, ExpectationBase>();
        }

        /// <summary>
        /// Adds the specified expectation.
        /// </summary>
        /// <param name="expectation">The expectation.</param>
        public void AddExpectation(ExpectationBase expectation)
        {
            if (expectation == null)
                throw new ArgumentNullException("expectation");

            _expectations.Add(expectation.MethodToken, expectation);
        }

        /// <summary>
        /// Verifies all expectations.
        /// </summary>
        public void VerifyExpectations()
        {
            foreach (var expectation in _expectations.Values)
            {
                expectation.Verify();
            }
        }

        #region IInvocationHandler Members

        /// <inheritdoc/>
        public object Invoke(object target, MethodInfo methodInfo, object[] parameters)
        {
            var methodToken = new MethodToken(methodInfo);
            ExpectationBase expectation;

            if (_expectations.TryGetValue(methodToken, out expectation))
                return expectation.Invoke(methodInfo, parameters);

            throw new ExpectationException(String.Format("Unexpected invocation for '{0}'", methodInfo));
        }

        #endregion
    }
}