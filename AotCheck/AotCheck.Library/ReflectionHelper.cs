namespace AotCheck.Library
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class ReflectionHelper
    {
        public static bool IsCodegenAllowed()
        {
            try
            {
                //var type = Type.GetType("System.Reflection.Emit.DynamicMethod");
                //if (type == null)
                //{
                //    return false;
                //}

                //Activator.CreateInstance(type, string.Empty, typeof(object), Type.EmptyTypes, true);

                // ReSharper disable once ObjectCreationAsStatement
                new DynamicMethod(
                    string.Empty,
                    typeof(object),
                    Type.EmptyTypes,
                    true);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        // Reflection

        public static Func<TTarget> CreateReflectinFactory<TTarget>(ConstructorInfo ci)
        {
            // Supported default constructor only
            // Activator.CreateInstance(Type) fast than ConstructoInfo.Invoke() when zero parameters
            return () => (TTarget)Activator.CreateInstance(ci.DeclaringType);
        }

        public static Func<TTarget, TMember> CreateReflectionGetter<TTarget, TMember>(PropertyInfo pi)
        {
            return target => (TMember)pi.GetValue(target);
        }

        public static Action<TTarget, TMember> CreateReflectionSetter<TTarget, TMember>(PropertyInfo pi)
        {
            return (target, value) => pi.SetValue(target, value);
        }

        // Expression

        public static Func<TTarget> CreateExpressionFactory<TTarget>(ConstructorInfo ci)
        {
            try
            {
                // Supported default constructor only
                return Expression.Lambda<Func<TTarget>>(Expression.New(ci)).Compile();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public static Func<TTarget, TMember> CreateExpressionGetter<TTarget, TMember>(PropertyInfo pi)
        {
            try
            {
                var parameterExpression = Expression.Parameter(typeof(TTarget));
                var propertyExpression = Expression.Property(parameterExpression, pi);
                return Expression.Lambda<Func<TTarget, TMember>>(
                    propertyExpression,
                    parameterExpression).Compile();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public static Action<TTarget, TMember> CreateExpressionSetter<TTarget, TMember>(PropertyInfo pi)
        {
            try
            {
                var parameterExpression = Expression.Parameter(typeof(TTarget));
                var parameterExpression2 = Expression.Parameter(typeof(TMember));
                var propertyExpression = Expression.Property(parameterExpression, pi);
                return Expression.Lambda<Action<TTarget, TMember>>(
                    Expression.Assign(propertyExpression, parameterExpression2),
                    parameterExpression,
                    parameterExpression2).Compile();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        // Emit

        public static Func<TTarget> CreateEmitFactory<TTarget>(ConstructorInfo ci)
        {
            try
            {
                // Supported default constructor only
                var dynamic = new DynamicMethod(string.Empty, typeof(TTarget), Type.EmptyTypes, true);
                var il = dynamic.GetILGenerator();

                il.Emit(OpCodes.Newobj, ci);
                il.Emit(OpCodes.Ret);

                return (Func<TTarget>)dynamic.CreateDelegate(typeof(Func<TTarget>));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public static Func<TTarget, TMember> CreateEmitGetter<TTarget, TMember>(PropertyInfo pi)
        {
            try
            {
                var method = new DynamicMethod(string.Empty, typeof(TMember), new[] { typeof(TTarget) }, true);
                var generator = method.GetILGenerator();
                var getter = pi.GetGetMethod(true);

                generator.DeclareLocal(typeof(object));
                generator.Emit(OpCodes.Ldarg_0);
                EmitTypeConversion(generator, pi.DeclaringType, true);
                EmitCall(generator, getter);
                if (pi.PropertyType.IsValueType)
                {
                    generator.Emit(OpCodes.Box, pi.PropertyType);
                }

                generator.Emit(OpCodes.Ret);

                return (Func<TTarget, TMember>)method.CreateDelegate(typeof(Func<TTarget, TMember>), null);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public static Action<TTarget, TMember> CreateEmitSetter<TTarget, TMember>(PropertyInfo pi)
        {
            try
            {
                var method = new DynamicMethod(string.Empty, typeof(void), new[] { typeof(TTarget), typeof(TMember) }, true);
                var generator = method.GetILGenerator();
                var setter = pi.GetSetMethod(true);

                generator.Emit(OpCodes.Ldarg_0);
                EmitTypeConversion(generator, pi.DeclaringType, true);
                generator.Emit(OpCodes.Ldarg_1);
                EmitTypeConversion(generator, pi.PropertyType, false);
                EmitCall(generator, setter);
                generator.Emit(OpCodes.Ret);

                return (Action<TTarget, TMember>)method.CreateDelegate(typeof(Action<TTarget, TMember>));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        private static void EmitTypeConversion(ILGenerator generator, Type castType, bool isContainer)
        {
            if (castType.IsValueType)
            {
                generator.Emit(isContainer ? OpCodes.Unbox : OpCodes.Unbox_Any, castType);
            }
            else if (castType != typeof(object))
            {
                generator.Emit(OpCodes.Castclass, castType);
            }
        }

        private static void EmitCall(ILGenerator generator, MethodInfo method)
        {
            var opcode = (method.IsStatic || method.DeclaringType.IsValueType) ? OpCodes.Call : OpCodes.Callvirt;
            generator.EmitCall(opcode, method, null);
        }
    }
}
