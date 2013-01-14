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

namespace NFake.Core.Expectations
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