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
    /// Represents a property get expectation.
    /// </summary>
    internal sealed class PropertyGetExpectation<TValue> : ExpectationBase, IGetsThrows<TValue>
    {
        /// <summary>
        /// The expression.
        /// </summary>
        private readonly MemberExpression _expression;

        /// <summary>
        /// The return value.
        /// </summary>
        private object _returnValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGetExpectation`1"/> class.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        public PropertyGetExpectation(LambdaExpression lambdaExpression)
        {
            var expression = lambdaExpression.Body as MemberExpression;

            if (expression == null)
                throw new ArgumentException("Invalid lambda expression", "lambdaExpression");

            if (!(expression.Member is PropertyInfo))
                throw new ArgumentException("Invalid lambda expression", "lambdaExpression");

            _expression = expression;

            _returnValue = default(TValue);
        }

        #region ExpectationBase Members

        /// <inheritdoc/>
        protected override MethodInfo Method
        {
            get
            {
                var propertyInfo = (PropertyInfo) _expression.Member;

                return propertyInfo.GetGetMethod(true);
            }
        }

        /// <inheritdoc/>
        protected override ICollection<Expression> Arguments
        {
            get { return new Expression[0]; }
        }

        /// <inheritdoc/>
        protected override object ReturnValue
        {
            get { return _returnValue; }
        }

        #endregion

        #region IGets<TValue> Members

        /// <inheritdoc/>
        public ITimes Gets(TValue value)
        {
            _returnValue = value;

            return this;
        }

        #endregion
    }
}