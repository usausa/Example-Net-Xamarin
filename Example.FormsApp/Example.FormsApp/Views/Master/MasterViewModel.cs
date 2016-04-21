namespace Example.FormsApp.Views.Master
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;

    using Example.FormsApp.Infrastructure;
    using Example.FormsApp.Models;

    public class MasterViewModel : ViewModelBase
    {
        public override string Title
        {
            get { return "Master"; }
        }

        public ObservableCollection<DataEntity> Items { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="service"></param>
        public MasterViewModel(DataService service)
        {
            Items = new ObservableCollection<DataEntity>(service.QueryEntityList());
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateToMenu()
        {
            Navigator.Forward(ViewId.Menu);
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateToDetailNew()
        {
            Navigator.Forward(ViewId.DetailNew);
        }

        /// <summary>
        ///
        /// </summary>
        public void NavigateToDetailEdit()
        {
            Navigator.Forward(ViewId.DetailEdit);
        }
    }
}
