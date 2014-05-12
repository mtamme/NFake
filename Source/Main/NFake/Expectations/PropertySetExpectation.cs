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
    /// Represents a property set expectation.
    /// </summary>
    internal sealed class PropertySetExpectation<TValue> : ExpectationBase, ISetsThrows<TValue>
    {
        /// <summary>
        /// The expression.
        /// </summary>
        private readonly MemberExpression _expression;

        /// <summary>
        /// The value.
        /// </summary>
        private TValue _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySetExpectation`1"/> class.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        public PropertySetExpectation(LambdaExpression lambdaExpression)
        {
            var expression = lambdaExpression.Body as MemberExpression;

            if (expression == null)
                throw new ArgumentException("Invalid lambda expression", "lambdaExpression");

            if (!(expression.Member is PropertyInfo))
                throw new ArgumentException("Invalid lambda expression", "lambdaExpression");

            _expression = expression;
        }

        #region ExpectationBase Members

        /// <inheritdoc/>
        protected override MethodInfo Method
        {
            get
            {
                var propertyInfo = (PropertyInfo) _expression.Member;

                return propertyInfo.GetSetMethod(true);
            }
        }

        /// <inheritdoc/>
        protected override ICollection<Expression> Arguments
        {
            get { return new Expression[] {Expression.Constant(_value, typeof (TValue))}; }
        }

        /// <inheritdoc/>
        protected override object ReturnValue
        {
            get { return null; }
        }

        #endregion

        #region ISets<TValue> Members

        /// <inheritdoc/>
        public ITimes Sets(TValue value)
        {
            _value = value;

            return this;
        }

        #endregion
    }
}