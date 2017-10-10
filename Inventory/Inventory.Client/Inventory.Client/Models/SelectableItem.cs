namespace Inventory.Client.Models
{
    using Smart.ComponentModel;

    public class SelectableItem<T> : NotificationObject
    {
        private bool isSelected;

        public T Item { get; }

        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        public SelectableItem(T item)
        {
            Item = item;
        }
    }
}
