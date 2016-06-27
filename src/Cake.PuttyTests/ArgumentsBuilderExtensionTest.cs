using System.Linq;
using System.Reflection;
using Cake.Core.IO;
using NUnit.Framework;
using Cake.Putty;

namespace Cake.Putty.Tests
{
    public class ArgumentsBuilderExtensionTest
    {
        public static PropertyInfo StringProperty => GetProperty(nameof(TestSettings.String));
        public static PropertyInfo StringsProperty => GetProperty(nameof(TestSettings.Strings));
        public static PropertyInfo NullableIntProperty => GetProperty(nameof(TestSettings.NullableInt));
        public static PropertyInfo BoolProperty => GetProperty(nameof(TestSettings.Bool));
        public static PropertyInfo AttributedBoolProperty => GetProperty(nameof(TestSettings.AttributedBool));
        public static PropertyInfo EnumProperty => GetProperty(nameof(TestSettings.Enum));
        public static PropertyInfo GetProperty(string name)
        {
            return typeof(TestSettings).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
        }
        [TestFixture]
        public class GetArgumentFromBoolProperty
        {
            [Test]
            public void WhenTrue_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromBoolProperty(BoolProperty, true);

                Assert.That(actual, Is.EqualTo("-b"));
            }
            [Test]
            public void WhenFalse_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromBoolProperty(BoolProperty, false);

                Assert.That(actual, Is.Null);
            }
        }
        [TestFixture]
        public class GetArgumentFromAttributedBoolProperty
        {
            [Test]
            public void WhenAttributedIsNull_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(AttributedBoolProperty, null);

                Assert.That(actual, Is.Null);
            }
            [Test]
            public void WhenAttributedIsTrue_OnTrueIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(AttributedBoolProperty, true);

                Assert.That(actual, Is.EqualTo("-OnTrue"));
            }
            [Test]
            public void WhenAttributedIsFalse_OnFalseIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableBoolProperty(AttributedBoolProperty, false);

                Assert.That(actual, Is.EqualTo("-OnFalse"));
            }
        }
        [TestFixture]
        public class GetArgumentFromStringProperty
        {
            [Test]
            public void WhenGivenStringProperty_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, "tubo");

                Assert.That(actual, Is.EqualTo("-s tubo"));
            }
            [Test]
            public void WhenGivenNull_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromStringProperty(StringProperty, null);

                Assert.That(actual, Is.Null);
            }
        }
        [TestFixture]
        public class GetArgumentFromStringArrayProperty
        {
            [Test]
            public void WhenGivenStringArrayProperty_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromStringArrayProperty(StringsProperty, new string[] { "tubo1", "tubo2" });

                Assert.AreEqual(actual.ToArray(), new string[] { "-strs tubo1", "-strs tubo2" }); 
            }
            [Test]
            public void WhenGivenNull_EmptyArrayReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromStringArrayProperty(StringsProperty, null);

                Assert.That(actual, Is.Empty);
            }
        }
        [TestFixture]
        public class GetArgumentFromNullableIntProperty
        {
            [Test]
            public void WhenGivenValue_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableIntProperty, 5);

                Assert.That(actual, Is.EqualTo("-i 5"));
            }

            [Test]
            public void WhenGivenNull_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromNullableIntProperty(NullableIntProperty, null);

                Assert.That(actual, Is.Null);
            }
        }

        [TestFixture]
        public class GetArgumentFromEnumProperty
        {
            [Test]
            public void WhenGivenValue_FormatsProperly()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromEnumProperty(EnumProperty, TestEnum.One);

                Assert.That(actual, Is.EqualTo("-one"));
            }
            [Test]
            public void WhenGivenValue_PropertyIsHandled()
            {
                TestSettings settings = new TestSettings { Enum = TestEnum.One };
                var actual = ArgumentsBuilderExtension.GetArgumentFromProperty(EnumProperty, settings);

                Assert.That(actual.ToArray().Length, Is.EqualTo(1));
            }
            [Test]
            public void WhenGivenNull_NullIsReturned()
            {
                var actual = ArgumentsBuilderExtension.GetArgumentFromEnumProperty(EnumProperty, null);

                Assert.That(actual, Is.Null);
            }
        }

        [TestFixture]
        public class GetPropertyName
        {
            [Test]
            public void WhenInput_ReturnsCorrectlyFormatted()
            {
                string actual = ArgumentsBuilderExtension.GetPropertyName(StringProperty);

                Assert.That(actual, Is.EqualTo("s"));
            }
        }

        [TestFixture]
        public class AppendAll
        {
            [Test]
            public void WhenStringInput_AddsAsArgument()
            {
                TestSettings input = new TestSettings { String = "tubo" };

                ProcessArgumentBuilder builder = new ProcessArgumentBuilder();
                builder.AppendAll(new string[] { "cmd" }, input, new string[] { "arg1" });
                var actual = builder.Render();

                Assert.That(actual, Is.EqualTo("cmd -s tubo arg1"));
            }

        }

        [TestFixture]
        public class GetEnumName
        {
            [Test]
            public void WhenPassedMember_ExtractName()
            {
                TestEnum source = TestEnum.One;

                string actual = ArgumentsBuilderExtension.GetEnumName(typeof(TestEnum), source);

                Assert.That(actual, Is.EqualTo("one"));
            }
        }
    }

    public class TestSettings: AutoToolSettings
    {
        [Parameter("s")]
        public string String { get; set; }
        [Parameter("strs")]
        public string[] Strings { get; set; }
        [Parameter("i")]
        public int? NullableInt { get; set; }
        [Parameter("b")]
        public bool Bool { get; set; }
        [BoolParameter("OnTrue", "OnFalse")]
        public bool? AttributedBool { get; set; }
        public TestEnum? Enum { get; set; }
    }

    public enum TestEnum
    {
        [Parameter("one")]
        One,
        Two
    }
}
