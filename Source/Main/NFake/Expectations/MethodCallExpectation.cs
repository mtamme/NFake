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
using System.Linq.Expressions;
using System.Reflection;
using NFake.Language;

namespace NFake.Expectations
{
    /// <summary>
    /// Represents a method call expectation.
    /// </summary>
    internal class MethodCallExpectation : ExpectationBase
    {
        /// <summary>
        /// The expression.
        /// </summary>
        private readonly MethodCallExpression _expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCallExpectation"/> class.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        public MethodCallExpectation(LambdaExpression lambdaExpression)
        {
            var expression = lambdaExpression.Body as MethodCallExpression;

            if (expression == null)
                throw new ArgumentException("Invalid lambda expression", "lambdaExpression");

            _expression = expression;
        }

        #region ExpectationBase Members

        /// <inheritdoc/>
        protected override MethodInfo Method
        {
            get { return _expression.Method; }
        }

        /// <inheritdoc/>
        protected override ICollection<Expression> Arguments
        {
            get { return _expression.Arguments; }
        }

        /// <inheritdoc/>
        protected override object ReturnValue
        {
            get { return null; }
        }

        #endregion
    }

    /// <summary>
    /// Represents a method call expectation.
    /// </summary>
    internal sealed class MethodCallExpectation<TReturn> : MethodCallExpectation, IReturnsThrows<TReturn>
    {
        /// <summary>
        /// The return value.
        /// </summary>
        private object _returnValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCallExpectation`1"/> class.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        public MethodCallExpectation(LambdaExpression lambdaExpression)
            : base(lambdaExpression)
        {
            _returnValue = default(TReturn);
        }

        #region ExpectationBase Members

        /// <inheritdoc/>
        protected override object ReturnValue
        {
            get { return _returnValue; }
        }

        #endregion

        #region IReturns<TReturn> Members

        /// <inheritdoc/>
        public ITimes Returns(TReturn returnValue)
        {
            _returnValue = returnValue;

            return this;
        }

        #endregion
    }
}