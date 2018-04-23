using System.Windows;
using System.Windows.Controls;

namespace dxExample {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
        }

        private void OnMainPageLoaded(object sender, RoutedEventArgs e) {
            this.grid.ItemsSource = SampleDataRow.CreateRows();
        }
    }
}
