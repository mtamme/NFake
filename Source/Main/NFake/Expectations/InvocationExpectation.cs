﻿//
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
using System.Linq.Expressions;
using System.Reflection;
using NFake.Language;

namespace NFake.Expectations
{
    /// <summary>
    /// Represents an invocation expectation.
    /// </summary>
    internal class InvocationExpectation : ExpectationBase
    {
        /// <summary>
        /// The expression.
        /// </summary>
        private readonly InvocationExpression _expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationExpectation"/> class.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        public InvocationExpectation(LambdaExpression lambdaExpression)
        {
            var expression = lambdaExpression.Body as InvocationExpression;

            if (expression == null)
                throw new ArgumentException("Invalid lambda expression", "lambdaExpression");

            _expression = expression;
        }

        #region ExpectationBase Members

        /// <inheritdoc/>
        protected override MethodInfo Method
        {
            get
            {
                var type = _expression.Expression.Type;

                return type.GetMethod("Invoke");
            }
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
    /// Represents an invocation expectation.
    /// </summary>
    internal sealed class InvocationExpectation<TReturn> : InvocationExpectation, IReturnsThrows<TReturn>
    {
        /// <summary>
        /// The return value.
        /// </summary>
        private object _returnValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationExpectation{TReturn}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public InvocationExpectation(LambdaExpression expression)
            : base(expression)
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