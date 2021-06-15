using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Performance_Webinar.Tests
{
	public class PropertiesTest : PerformanceTest
	{
		private const int DEFAULT_ITERATIONS = 5_000_000;

		public delegate object PropertyGetDelegate(object obj);

		public delegate void PropertySetDelegate(object obj, object value);

		public PropertiesTest() : base("Property access", "A:reflection, B:dynamic CIL, C:compile-time", DEFAULT_ITERATIONS)
		{
		}

		protected PropertyGetDelegate GetPropertyGetter(string typeName, string propertyName)
		{
			// get the property get method
			Type t = Type.GetType(typeName);
			PropertyInfo pi = t.GetProperty(propertyName);
			MethodInfo getter = pi.GetGetMethod();

			// create a new dynamic method that calls the property getter
			DynamicMethod dm = new DynamicMethod("GetValue", typeof(object), new Type[] { typeof(object) }, typeof(object), true);
			ILGenerator lgen = dm.GetILGenerator();

			// emit CIL
			lgen.Emit(OpCodes.Ldarg_0);
			lgen.Emit(OpCodes.Call, getter);

			if (getter.ReturnType.GetTypeInfo().IsValueType)
			{
				lgen.Emit(OpCodes.Box, getter.ReturnType);
			}

			lgen.Emit(OpCodes.Ret);

			return dm.CreateDelegate(typeof(PropertyGetDelegate)) as PropertyGetDelegate;
		}

		protected PropertySetDelegate GetPropertySetter(string typeName, string propertyName)
		{
			// get the property get method
			Type t = Type.GetType(typeName);
			PropertyInfo pi = t.GetProperty(propertyName);
			MethodInfo setter = pi.GetSetMethod(false);

			// create a new dynamic method that calls the property setter
			DynamicMethod dm = new DynamicMethod("SetValue", typeof(void), new Type[] { typeof(object), typeof(object) }, typeof(object), true);
			ILGenerator lgen = dm.GetILGenerator();

			// emit CIL
			lgen.Emit(OpCodes.Ldarg_0);
			lgen.Emit(OpCodes.Ldarg_1);

			Type parameterType = setter.GetParameters()[0].ParameterType;

			if (parameterType.GetTypeInfo().IsValueType)
			{
				lgen.Emit(OpCodes.Unbox_Any, parameterType);
			}

			lgen.Emit(OpCodes.Call, setter);
			lgen.Emit(OpCodes.Ret);

			return dm.CreateDelegate(typeof(PropertySetDelegate)) as PropertySetDelegate;
		}

		protected override bool MeasureTestA()
		{
			// get property using reflection
			var sb = new StringBuilder("Mark Duncan Farragher");
			PropertyInfo pi = sb.GetType().GetProperty("Length");
			for (int i = 0; i < Iterations; i++)
			{
				var length = pi.GetValue(sb);
				if (!21.Equals(length))
					throw new InvalidOperationException($"Invalid length {length} returned");
			}
			return true;
		}

		protected override bool MeasureTestB()
		{
			// get property using dynamic cil
			var sb = new StringBuilder("Mark Duncan Farragher");
			var getter = GetPropertyGetter("System.Text.StringBuilder", "Length");
			for (int i = 0; i < Iterations; i++)
			{
				var length = getter(sb);
				if (!21.Equals(length))
					throw new InvalidOperationException($"Invalid length {length} returned");
			}
			return true;
		}

		protected override bool MeasureTestC()
		{
			// get property using compiled code
			var sb = new StringBuilder("Mark Duncan Farragher");
			for (int i = 0; i < Iterations; i++)
			{
				var length = sb.Length;
				if (!21.Equals(length))
					throw new InvalidOperationException($"Invalid length {length} returned");
			}
			return true;
		}

	}
}
