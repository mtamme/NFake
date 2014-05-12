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
            _object =
                new Lazy<TMock>(() => ProxyFactory.CreateProxy<TMock>(Type.EmptyTypes, _invocationHandler, arguments));
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
        public IThrows ExpectCall(Expression<Action<TMock>> lambdaExpression)
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
        public IReturnsThrows<TReturn> ExpectCall<TReturn>(Expression<Func<TMock, TReturn>> lambdaExpression)
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