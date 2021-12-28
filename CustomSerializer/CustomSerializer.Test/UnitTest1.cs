using System;
using CustomSerializer.Serializer;
using NUnit.Framework;

namespace CustomSerializer.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CheckThat_RegularClass_Should_Not_Serialized()
        {
            var regularClass = new RegularClass()
            {
                Property1 = "Regular",
                Property2 = 22.366
            };

            Assert.That(Serizalizer.Serialize(regularClass), Is.EqualTo(string.Empty), "Should not serialize without PropertyAttributes");
        }

        [Test]
        public void CheckThat_Srializer_Should_Throw_Exception_When_Same_Property_Position_Define_MoreThanOnce()
        {
            var classA = new ClassA()
            {
                Property1 = "AA",
                Property2 = "BB"
            };
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.Contains("An item with the same key has already been added."), () => Serizalizer.Serialize(classA));
        }

        [Test]
        public void CheckThat_Serializer_Should_Serilzer_Appropriate_SerializableClass()
        {
            var classB = new ClassB()
            {
                Property1 = "AA",
                Property2 = "BB"
            };
            Assert.That(Serizalizer.Serialize(classB), Is.EqualTo("AABB"));
        }

        [Test]
        public void CheckThat_Serializer_Should_Format_Properties_With_DefaultFormatter_If_NoneDefined()
        {
            var classC = new ClassC()
            {
                Property1 = "AA",
                Property2 = 25.36
            };
            Assert.That(Serizalizer.Serialize(classC), Is.EqualTo("AA25.36"));
        }

        [Test]
        public void CheckThat_Serializer_Should_Serialized_Properties_In_Defined_Order()
        {
            var DateTime = new DateTime(2021, 12, 28, 10, 0, 0, 0);
            var classD = new ClassD()
            {
                Property1 = "AA",
                Property2 = 25.36,
                Property3 = DateTime
            };
            Assert.That(Serizalizer.Serialize(classD), Is.EqualTo("2021122825.36AA"));
        }
    }
}
