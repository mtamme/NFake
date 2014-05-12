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

            mock.ExpectCall(m => m.Method(2)).Returns("2").Once();
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

            mock.ExpectCall(m => m.Method(2)).Returns("2").Once();
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

            mock.ExpectCall(m => m.Method(2)).Returns("2").Once();
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

            mock.ExpectCall(m => m(2)).Returns("2").Once();

            var value = mock.Object(2);

            mock.Verify();

            Assert.That(value, Is.EqualTo("2"));
        }
    }
}