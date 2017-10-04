namespace AotCheck.FormsApp
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using AotCheck.Library;

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

            var factory1 = ReflectionHelper.CreateReflectinFactory<Data>(ci);
            var getter1 = ReflectionHelper.CreateReflectionGetter<Data, string>(pi);
            var setter1 = ReflectionHelper.CreateReflectionSetter<Data, string>(pi);

            var factory2 = ReflectionHelper.CreateExpressionFactory<Data>(ci);
            var getter2 = ReflectionHelper.CreateExpressionGetter<Data, string>(pi);
            var setter2 = ReflectionHelper.CreateExpressionSetter<Data, string>(pi);

            var factory3 = ReflectionHelper.CreateEmitFactory<Data>(ci);
            var getter3 = ReflectionHelper.CreateEmitGetter<Data, string>(pi);
            var setter3 = ReflectionHelper.CreateEmitSetter<Data, string>(pi);

            // Reflection
            await AwaitHelper.SwitchOffMainThreadAsync();
            ReflectionActivator.Value = FactoryBench(factory1);

            // TODO getter
            // TODO setter

            // Expression
            await AwaitHelper.SwitchOffMainThreadAsync();
            ExpressionActivator.Value = FactoryBench(factory2);

            // TODO getter
            // TODO setter

            // Emit
            await AwaitHelper.SwitchOffMainThreadAsync();
            EmitActivator.Value = FactoryBench(factory3);

            // TODO getter
            // TODO setter
        }

        private static string FactoryBench(Func<Data> factory)
        {
            if (factory == null)
            {
                return string.Empty;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            factory.Invoke();

            var watch = Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                factory.Invoke();
            }

            return watch.ElapsedMilliseconds.ToString();
        }
    }
}
