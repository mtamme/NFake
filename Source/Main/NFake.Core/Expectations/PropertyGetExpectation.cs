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
    internal sealed class PropertyGetExpectation<TValue> : ExpectationBase, IGetsThrows<TValue>
    {
        private readonly MemberExpression _expression;

        private object _returnValue;

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