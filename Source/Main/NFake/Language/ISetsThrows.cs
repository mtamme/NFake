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

namespace NFake.Language
{
    /// <summary>
    /// Defines the <c>Sets</c> and <c>Throws</c> verb.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public interface ISetsThrows<in TValue> : ISets<TValue>, IThrows
    {
    }
}