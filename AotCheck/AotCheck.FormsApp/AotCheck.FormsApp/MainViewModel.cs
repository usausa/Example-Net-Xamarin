namespace AotCheck.FormsApp
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Smart.ComponentModel;
    using Smart.Forms.Input;

    public class MainViewModel
    {
        public NotificationValue<string> GenerateSupported { get; } = new NotificationValue<string>();

        public NotificationValue<string> ReflectionActivator { get; } = new NotificationValue<string>();

        public NotificationValue<string> ReflectionGetter { get; } = new NotificationValue<string>();

        public NotificationValue<string> ReflectionSetter { get; } = new NotificationValue<string>();

        public NotificationValue<string> ExpressionActivator { get; } = new NotificationValue<string>();

        public NotificationValue<string> ExpressionGetter { get; } = new NotificationValue<string>();

        public NotificationValue<string> ExpressionSetter { get; } = new NotificationValue<string>();

        public NotificationValue<string> EmitActivator { get; } = new NotificationValue<string>();

        public NotificationValue<string> EmitGetter { get; } = new NotificationValue<string>();

        public NotificationValue<string> EmitSetter { get; } = new NotificationValue<string>();

        public ICommand ExecuteCommand { get; }

        public MainViewModel()
        {
            ExecuteCommand = new AsyncCommand(Execute);
        }

        private async Task Execute()
        {
            await AwaitHelper.SwitchOffMainThreadAsync();

            GenerateSupported.Value = ReflectionHelper.IsCodegenAllowed().ToString();

            var ci = typeof(Data).GetConstructors().First();
            var pi = typeof(Data).GetProperty(nameof(Data.Value));

            // Reflection
            ReflectionActivator.Value = FactoryBench(ReflectionHelper.CreateReflectinFactory<Data>(ci));
            ReflectionGetter.Value = GetterBench(ReflectionHelper.CreateReflectionGetter<Data, string>(pi));
            ReflectionSetter.Value = SetterBench(ReflectionHelper.CreateReflectionSetter<Data, string>(pi));

            // Expression
            ExpressionActivator.Value = FactoryBench(ReflectionHelper.CreateExpressionFactory<Data>(ci));
            ExpressionGetter.Value = GetterBench(ReflectionHelper.CreateExpressionGetter<Data, string>(pi));
            ExpressionSetter.Value = SetterBench(ReflectionHelper.CreateExpressionSetter<Data, string>(pi));

            // Emit
            EmitActivator.Value = FactoryBench(ReflectionHelper.CreateEmitFactory<Data>(ci));
            EmitGetter.Value = GetterBench(ReflectionHelper.CreateEmitGetter<Data, string>(pi));
            EmitSetter.Value = SetterBench(ReflectionHelper.CreateEmitSetter<Data, string>(pi));
        }

        private static string FactoryBench(Func<Data> factory)
        {
            if (factory == null)
            {
                return string.Empty;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            factory();

            var watch = Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                factory();
            }

            return watch.ElapsedMilliseconds.ToString();
        }

        private static string GetterBench(Func<Data, string> getter)
        {
            if (getter == null)
            {
                return string.Empty;
            }

            var data = new Data();
            getter(data);

            var watch = Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                getter(data);
            }

            return watch.ElapsedMilliseconds.ToString();
        }

        private static string SetterBench(Action<Data, string> setter)
        {
            if (setter == null)
            {
                return string.Empty;
            }

            var data = new Data();
            setter(data, null);

            var watch = Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                setter(data, null);
            }

            return watch.ElapsedMilliseconds.ToString();
        }
    }
}
