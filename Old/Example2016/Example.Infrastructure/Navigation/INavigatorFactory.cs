namespace Example.Navigation
{
    using System;

    public interface INavigatorFactory
    {
        object Create(Type type);
    }
}
