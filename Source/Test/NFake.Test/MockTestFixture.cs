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
using NFake.Test.Types;
using NUnit.Framework;

namespace NFake.Test
{
    [TestFixture]
    public sealed class MockTestFixture
    {
        [Test]
        public void CreateInterfaceMockTest()
        {
            var mock = new Mock<IMocked>();

            mock.Expect(m => m.Method(2)).Returns("2").Once();
            mock.ExpectGet(m => m.Property).Gets(2).Never();
            mock.ExpectSet(m => m.Property).Sets(2).Never();

            var value = mock.Object.Method(2);

            mock.Verify();

            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateAbstractClassMockTest()
        {
            var mock = new Mock<MockedBase>();

            mock.Expect(m => m.Method(2)).Returns("2").Once();
            mock.ExpectGet(m => m.Property).Gets(2).Never();
            mock.ExpectSet(m => m.Property).Sets(2).Never();

            var value = mock.Object.Method(2);

            mock.Verify();

            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateClassMockTest()
        {
            var mock = new Mock<Mocked>();

            mock.Expect(m => m.Method(2)).Returns("2").Once();
            mock.ExpectGet(m => m.Property).Gets(2).Never();
            mock.ExpectSet(m => m.Property).Sets(2).Never();

            var value = mock.Object.Method(2);

            mock.Verify();

            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateDelegateMockTest()
        {
            var mock = new Mock<Func<int, string>>();

            mock.Expect(m => m(2)).Returns("2").Once();

            var value = mock.Object(2);

            mock.Verify();

            Assert.That(value, Is.EqualTo("2"));
        }
    }
}