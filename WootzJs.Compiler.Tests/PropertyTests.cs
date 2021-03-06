#region License
//-----------------------------------------------------------------------
// <copyright>
// The MIT License (MIT)
// 
// Copyright (c) 2014 Kirk S Woll
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Linq.Expressions;
using System.Runtime.WootzJs;
using WootzJs.Testing;

namespace WootzJs.Compiler.Tests
{
    public class PropertyTests : TestFixture
    {
        [Test]
        public void StaticProperty()
        {
            StaticPropertyClass.StringProperty = "foo";
            AssertEquals(StaticPropertyClass.StringProperty, "foo");
        }

        [Test]
        public void PrimitiveFields()
        {
            var primitiveFields = new PrimitiveFieldsClass();
            AssertEquals(primitiveFields.Boolean, false);
            AssertEquals(primitiveFields.Byte, 0);
            AssertEquals(primitiveFields.SByte, 0);
            AssertEquals(primitiveFields.Short, 0);
            AssertEquals(primitiveFields.UShort, 0);
            AssertEquals(primitiveFields.Int, 0);
            AssertEquals(primitiveFields.UInt, 0);
            AssertEquals(primitiveFields.Long, 0);
            AssertEquals(primitiveFields.ULong, 0);
        }

        [Test]
        public void TestBaseReferences()
        {
            var instance = new SubclassWithBasePropertyReferences();
            instance.MyProperty = "foo";
            AssertEquals(instance.MyProperty, "foo");
        }

        public class StaticPropertyClass
        {
            public static string StringProperty { get; set; }
        }

        public class PrimitiveFieldsClass
        {
            public bool Boolean { get; set; }
            public byte Byte { get; set; }
            public short Short { get; set; }
            public int Int { get; set; }
            public long Long { get; set; }
            public sbyte SByte { get; set; }
            public ushort UShort { get; set; }
            public uint UInt { get; set; }
            public ulong ULong { get; set; }
        }

        public class ClassWithBasePropertyReferences
        {
            public virtual string MyProperty { get; set; }
        }

        public class SubclassWithBasePropertyReferences : ClassWithBasePropertyReferences
        {
            public override string MyProperty
            {
                get
                {
                    return base.MyProperty;
                }
                set
                {
                    base.MyProperty = value;
                }
            }
        }

        [Test]
        public void Indexer()
        {
            var indexer = new IndexerClass();
            indexer[0] = "foo";
            AssertEquals(indexer[0], "foo");
        }

        public class IndexerClass
        {
            private string[] stringArray = new string[10];

            public string this[int index]
            {
                get { return stringArray[index]; }
                set { stringArray[index] = value; }
            }
        }

        [Test]
        public void InstanceInitializer()
        {
            var obj = new InitializerClass();
            AssertEquals(obj.InstanceInitializer, "InstanceInitializer");
        }

        [Test]
        public void MinimizedInstanceInitializer()
        {
            var obj = new InitializerClass();
            AssertEquals(obj.MinimizedInstanceInitializer, "MinimizedInstanceInitializer");
        }

        [Test]
        public void StaticInitializer()
        {
            AssertEquals(InitializerClass.StaticInitializer, "StaticInitializer");
        }

        [Test]
        public void MinimizedStaticInitializer()
        {
            AssertEquals(InitializerClass.MinimizedStaticInitializer, "MinimizedStaticInitializer");
        }

        public class InitializerClass
        {
            public string InstanceInitializer { get; private set; } = "InstanceInitializer";

            [Js(AreAutoPropertiesMinimized = true)]
            public string MinimizedInstanceInitializer { get; private set; } = "MinimizedInstanceInitializer";

            public static string StaticInitializer { get; private set; } = "StaticInitializer";

            [Js(AreAutoPropertiesMinimized = true)]
            public static string MinimizedStaticInitializer { get; private set; } = "MinimizedStaticInitializer";
        }

        [Test]
        public void ExpressionBodiedProperty()
        {
            var obj = new ExpressionBodiedClass();
            var result = obj.Foo;
            AssertEquals(result, "foo");
        }

        public class ExpressionBodiedClass
        {
            public string Foo => "foo";
        }

        [Test]
        public void ImmutableProperty()
        {
            var value = new ImmutablePropertyClass("foo").StringProperty;
            AssertEquals(value, "foo");
        }

        public class ImmutablePropertyClass
        {
            public string StringProperty { get; }

            public ImmutablePropertyClass(string stringProperty)
            {
                StringProperty = stringProperty;
            }
        }

        [Test]
        public void AutoPropertyWithInitializer()
        {
            var value = new AutoPropertyWithInitializerClass().StringProperty;
            AssertEquals(value, "foo");
        }

        public class AutoPropertyWithInitializerClass
        {
            public string StringProperty { get; set; } = "foo";
        }

        [Test]
        public void StaticAutoPropertyWithInitializer()
        {
            var value = StaticAutoPropertyWithInitializerClass.StringProperty;
            AssertEquals(value, "foo");
        }

        public class StaticAutoPropertyWithInitializerClass
        {
            public static string StringProperty { get; } = "foo";
        }

        [Test]
        public void ImmutableAutoPropertyWithInitializer()
        {
            var value = new ImmutableAutoPropertyWithInitializerClass().StringProperty;
            AssertEquals(value, "foo");
        }

        public class ImmutableAutoPropertyWithInitializerClass
        {
            public string StringProperty { get; } = "foo";
        }

        [Test]
        public void ImmutableStaticAutoPropertyWithInitializer()
        {
            var value = ImmutableStaticAutoPropertyWithInitializerClass.StringProperty;
            AssertEquals(value, "foo");
        }

        public class ImmutableStaticAutoPropertyWithInitializerClass
        {
            public static string StringProperty { get; } = "foo";
        }

        [Test]
        public void MinimizedImmutableAutoPropertyWithInitializer()
        {
            var value = new MinimizedImmutableAutoPropertyWithInitializerClass().StringProperty;
            AssertEquals(value, "foo");
        }

        [Js(AreAutoPropertiesMinimized = true)]
        public class MinimizedImmutableAutoPropertyWithInitializerClass
        {
            public string StringProperty { get; } = "foo";
        }

        [Test]
        public void MinimizedImmutableStaticAutoPropertyWithInitializer()
        {
            var value = MinimizedImmutableStaticAutoPropertyWithInitializerClass.StringProperty;
            AssertEquals(value, "foo");
        }

        [Js(AreAutoPropertiesMinimized = true)]
        public class MinimizedImmutableStaticAutoPropertyWithInitializerClass
        {
            public static string StringProperty { get; } = "foo";
        }

        [Test]
        public void ExplicitInterfaceProperty()
        {
            var obj = new ExplicitInterface();
            obj.Property = "foo";
            IExplicitInterface intf = obj;
            AssertEquals(intf.Property, "foo");
        }

        [Test]
        public void ExplicitInterfacePropertyReflection()
        {
            var obj = new ExplicitInterface();
            obj.Property = "foo";
            IExplicitInterface intf = obj;
            Expression<Func<IExplicitInterface, object>> expression = x => x.Property;
            var compiled = expression.Compile();
            var value = compiled(intf);
            AssertEquals(value, "foo");
        }

        public interface IExplicitInterface
        {
            object Property { get; }
        }

        public class ExplicitInterface : IExplicitInterface
        {
            public string Property { get; set; }

            object IExplicitInterface.Property => Property;
        }

        [Test]
        public void ExplicitInterfaceInitializer()
        {
            IExplicitInterface obj = new ExplicitInterfaceWithInitializer();
            AssertEquals(obj.Property, "foo");
        }

        public class ExplicitInterfaceWithInitializer : IExplicitInterface
        {
            object IExplicitInterface.Property { get; } = "foo";            
        }
    }
}
