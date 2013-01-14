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

namespace NFake.Core.Test.Types
{
    internal class Mocked : IMocked
    {
        #region IMocked Members

#pragma warning disable 0067

        public virtual event Action Event;

#pragma warning restore 0067

        public virtual int Property { get; set; }

        public virtual string Method(int i)
        {
            return Convert.ToString(i);
        }

        #endregion
    }
}