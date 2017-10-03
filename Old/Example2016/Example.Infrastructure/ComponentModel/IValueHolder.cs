namespace Example.ComponentModel
{
    public interface IValueHolder<T>
    {
        T Value { get; set; }
    }
}
