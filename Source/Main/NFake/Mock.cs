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
using System.Linq.Expressions;
using NFake.Expectations;
using NFake.Language;
using NProxy.Core;

namespace NFake
{
    /// <summary>
    /// Represents a mock.
    /// </summary>
    /// <typeparam name="TMock">The mock type.</typeparam>
    public sealed class Mock<TMock> : MockBase where TMock : class
    {
        /// <summary>
        /// The expectation invocation handler.
        /// </summary>
        private readonly ExpectationInvocationHandler _invocationHandler;

        /// <summary>
        /// The mock object.
        /// </summary>
        private readonly Lazy<TMock> _object;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mock{TMock}"/> class.
        /// </summary>
        /// <param name="arguments">The constructor arguments.</param>
        public Mock(params object[] arguments)
        {
            _invocationHandler = new ExpectationInvocationHandler();
            _object = new Lazy<TMock>(() => ProxyFactory.CreateProxy<TMock>(Type.EmptyTypes, _invocationHandler, arguments));
        }

        /// <summary>
        /// Returns the mock object.
        /// </summary>
        public TMock Object
        {
            get { return _object.Value; }
        }

        /// <summary>
        /// Defines a property get expectation.
        /// </summary>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>The <c>Gets</c> and <c>Throws</c> verbs.</returns>
        public IGetsThrows<TValue> ExpectGet<TValue>(Expression<Func<TMock, TValue>> lambdaExpression)
        {
            var expectation = new PropertyGetExpectation<TValue>(lambdaExpression);

            _invocationHandler.AddExpectation(expectation);

            return expectation;
        }

        /// <summary>
        /// Defines a property set expectation.
        /// </summary>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>The <c>Sets</c> and <c>Throws</c> verbs.</returns>
        public ISetsThrows<TValue> ExpectSet<TValue>(Expression<Func<TMock, TValue>> lambdaExpression)
        {
            var expectation = new PropertySetExpectation<TValue>(lambdaExpression);

            _invocationHandler.AddExpectation(expectation);

            return expectation;
        }

        /// <summary>
        /// Defines a method call expectation.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>The <c>Throws</c> verb.</returns>
        public IThrows Expect(Expression<Action<TMock>> lambdaExpression)
        {
            ExpectationBase expectation;

            if (lambdaExpression.Body is MethodCallExpression)
                expectation = new MethodCallExpectation(lambdaExpression);
            else if (lambdaExpression.Body is InvocationExpression)
                expectation = new InvocationExpectation(lambdaExpression);
            else
                throw new ArgumentException("Invalid lambda expression.");

            _invocationHandler.AddExpectation(expectation);

            return expectation;
        }

        /// <summary>
        /// Defines a method call expectation.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="lambdaExpression">The lambda expression.</param>
        /// <returns>The <c>Returns</c> and <c>Throws</c> verbs.</returns>
        public IReturnsThrows<TReturn> Expect<TReturn>(Expression<Func<TMock, TReturn>> lambdaExpression)
        {
            ExpectationBase expectation;

            if (lambdaExpression.Body is MethodCallExpression)
                expectation = new MethodCallExpectation<TReturn>(lambdaExpression);
            else if (lambdaExpression.Body is InvocationExpression)
                expectation = new InvocationExpectation<TReturn>(lambdaExpression);
            else
                throw new ArgumentException("Invalid lambda expression.");

            _invocationHandler.AddExpectation(expectation);

            return (IReturnsThrows<TReturn>) expectation;
        }

        /// <summary>
        /// Verifies all expectations.
        /// </summary>
        public void Verify()
        {
            _invocationHandler.VerifyExpectations();
        }
    }
}