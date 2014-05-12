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

namespace NFake.Expectations
{
    /// <summary>
    /// Represents an anonymous constraint.
    /// </summary>
    internal sealed class AnonymousConstraint : IConstraint
    {
        /// <summary>
        /// The evaluate function.
        /// </summary>
        private readonly Func<bool> _evaluate;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousConstraint"/> class.
        /// </summary>
        /// <param name="evaluate">The evaluate function.</param>
        public AnonymousConstraint(Func<bool> evaluate)
        {
            if (evaluate == null)
                throw new ArgumentNullException("evaluate");

            _evaluate = evaluate;
        }

        #region IConstraint Members

        public bool Evaluate()
        {
            return _evaluate();
        }

        #endregion
    }
}