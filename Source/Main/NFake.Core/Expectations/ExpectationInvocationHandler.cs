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

using System;
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core;

namespace NFake.Core.Expectations
{
    /// <summary>
    /// Represents an expectation invocation handler.
    /// </summary>
    internal sealed class ExpectationInvocationHandler : IInvocationHandler
    {
        private readonly Dictionary<MethodToken, ExpectationBase> _expectations;

        public ExpectationInvocationHandler()
        {
            _expectations = new Dictionary<MethodToken, ExpectationBase>();
        }

        public void AddExpectation(ExpectationBase expectation)
        {
            if (expectation == null)
                throw new ArgumentNullException("expectation");

            _expectations.Add(expectation.MethodToken, expectation);
        }

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
            ExpectationBase expectation;

            if (_expectations.TryGetValue(methodInfo.GetToken(), out expectation))
                return expectation.Verify(methodInfo, parameters);

            throw new ExpectationException(String.Format("Unexpected invocation for '{0}'", methodInfo));
        }

        #endregion
    }
}