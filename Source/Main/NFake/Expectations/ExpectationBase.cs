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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NFake.Language;

namespace NFake.Expectations
{
    /// <summary>
    /// Represents an expectation base class.
    /// </summary>
    internal abstract class ExpectationBase : IThrows, ITimes
    {
        /// <summary>
        /// The constraints.
        /// </summary>
        private readonly List<IConstraint> _constraints;

        /// <summary>
        /// The exception.
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// The invocation count.
        /// </summary>
        private int _invocationCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpectationBase"/> class.
        /// </summary>
        protected ExpectationBase()
        {
            _constraints = new List<IConstraint>();

            _exception = null;
            _invocationCount = 0;
        }

        /// <summary>
        /// Returns the method token.
        /// </summary>
        public MethodToken MethodToken
        {
            get { return new MethodToken(Method); }
        }

        /// <summary>
        /// Returns the method.
        /// </summary>
        protected abstract MethodInfo Method { get; }

        /// <summary>
        /// Returns the arguments.
        /// </summary>
        protected abstract ICollection<Expression> Arguments { get; }

        /// <summary>
        /// Returns the return value.
        /// </summary>
        protected abstract object ReturnValue { get; }

        /// <summary>
        /// Handles an invocation.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="parameters">The parameters.</param>
        public object Invoke(MethodInfo methodInfo, object[] parameters)
        {
            _invocationCount++;

            if (_exception != null)
                throw _exception;

            return ReturnValue;
        }

        /// <summary>
        /// Verifies the expectation.
        /// </summary>
        public void Verify()
        {
            if (_constraints.Any(c => !c.Evaluate()))
                throw new ExpectationException(String.Format("Constraint violation for '{0}'", Method));
        }

        #region IThrows Members

        /// <inheritdoc/>
        public ITimes Throws(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            _exception = exception;

            return this;
        }

        /// <inheritdoc/>
        public ITimes Throws<TException>() where TException : Exception, new()
        {
            _exception = new TException();

            return this;
        }

        #endregion

        #region ITimes Members

        /// <inheritdoc/>
        public void Exactly(int times)
        {
            var constraint = new AnonymousConstraint(() => (times == _invocationCount));

            _constraints.Add(constraint);
        }

        /// <inheritdoc/>
        public void AtMost(int times)
        {
            var constraint = new AnonymousConstraint(() => (times >= _invocationCount));

            _constraints.Add(constraint);
        }

        /// <inheritdoc/>
        public void AtLeast(int times)
        {
            var constraint = new AnonymousConstraint(() => (times <= _invocationCount));

            _constraints.Add(constraint);
        }

        #endregion
    }
}