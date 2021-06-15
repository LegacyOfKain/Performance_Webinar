using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Performance_Webinar.Tests
{
	public class InstantiationTest : PerformanceTest
	{
		private const int DEFAULT_ITERATIONS = 1_000_000;

		public delegate object ConstructorDelegate();

		public InstantiationTest() : base("Instantiation", "A:reflection, B:dynamic CIL, C:compile-time", DEFAULT_ITERATIONS)
		{
		}

		protected ConstructorDelegate GetConstructor(string typeName)
		{
			// get the default constructor of the type
			Type t = Type.GetType(typeName);
			ConstructorInfo ctor = t.GetConstructor(new Type[0]);

			// create a new dynamic method that constructs and returns the type
			string methodName = t.Name + "Ctor";
			DynamicMethod dm = new DynamicMethod(methodName, t, new Type[0], typeof(Activator));
			ILGenerator lgen = dm.GetILGenerator();
			lgen.Emit(OpCodes.Newobj, ctor);
			lgen.Emit(OpCodes.Ret);

			// add delegate to dictionary and return
			ConstructorDelegate creator = (ConstructorDelegate)dm.CreateDelegate(typeof(ConstructorDelegate));

			// return a delegate to the method
			return creator;
		}

		protected override bool MeasureTestA()
		{
			// instantiate stringbuilder using reflection
			var type = Type.GetType("System.Text.StringBuilder");
			for (int i = 0; i < Iterations; i++)
			{
				var obj = Activator.CreateInstance(type);
				if (obj.GetType() != typeof(System.Text.StringBuilder))
					throw new InvalidOperationException("Constructed object is not a StringBuilder");
			}
			return true;
		}

		protected override bool MeasureTestB()
		{
			// instantiate stringbuilder using dynamic CIL
			var constructor = GetConstructor("System.Text.StringBuilder");
			for (int i = 0; i < Iterations; i++)
			{
				var obj = constructor();
				if (obj.GetType() != typeof(System.Text.StringBuilder))
					throw new InvalidOperationException("Constructed object is not a StringBuilder");
			}
			return true;
		}

		protected override bool MeasureTestC()
		{
			// instantiate stringbuilder directly
			for (int i = 0; i < Iterations; i++)
			{
				var obj = new System.Text.StringBuilder();
				if (obj.GetType() != typeof(System.Text.StringBuilder))
					throw new InvalidOperationException("Constructed object is not a StringBuilder");
			}
			return true;
		}

	}
}
