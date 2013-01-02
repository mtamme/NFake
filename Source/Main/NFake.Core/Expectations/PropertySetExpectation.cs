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
using System.Linq.Expressions;
using System.Reflection;
using NFake.Core.Language;

namespace NFake.Core.Expectations
{
    internal sealed class PropertySetExpectation<TValue> : ExpectationBase, ISetsThrows<TValue>
    {
        private readonly MemberExpression _expression;

        private TValue _value;

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
