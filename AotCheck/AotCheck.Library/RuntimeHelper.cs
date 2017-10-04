namespace AotCheck.Library
{
    using System;
    using System.Reflection.Emit;

    public static class RuntimeHelper
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
    }
}
