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