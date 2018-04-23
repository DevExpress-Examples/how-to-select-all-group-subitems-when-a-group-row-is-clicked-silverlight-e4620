using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Input;

namespace dxExample {
    public class GroupChildSelector : DependencyObject {
        static readonly DependencyProperty ModeProperty = DependencyProperty.RegisterAttached("Mode", typeof(ChildSelectionMode), typeof(GroupChildSelector), new PropertyMetadata(ChildSelectionMode.None, new PropertyChangedCallback(OnModeChanged)));

        public static ChildSelectionMode GetMode(DependencyObject obj) {
            return (ChildSelectionMode)obj.GetValue(ModeProperty);
        }
        public static void SetMode(DependencyObject obj, ChildSelectionMode value) {
            obj.SetValue(ModeProperty, value);
        }

        static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(!(d is TableView)) return;
            TableView view = (d as TableView);
            if(!view.IsLoaded) {
                view.Loaded += OnViewLoaded;
            } else {
                SubscribeViewEvents(view);
            }
        }

        static void OnViewLoaded(object sender, RoutedEventArgs e) {
            TableView view = (sender as TableView);
            SubscribeViewEvents(view);
        }
        static void SubscribeViewEvents(TableView view) {
            view.AddHandler(TableView.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), true);
            view.Grid.AddHandler(GridControl.GroupRowExpandingEvent, new RowAllowEventHandler(OnGroupRowExpanding));
        }
        static void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            TableView view = (sender as TableView);
            TableViewHitInfo hitInfo = view.CalcHitInfo(e.OriginalSource as DependencyObject);
            if(hitInfo.InRow && view.Grid.IsGroupRowHandle(hitInfo.RowHandle)) {
                view.BeginSelection();
                SelectChild(view, hitInfo.RowHandle);
                view.EndSelection();
            }
        }
        static void OnGroupRowExpanding(object sender, RowAllowEventArgs e) {
            TableView view = (e.Source as TableView);
            view.BeginSelection();
            SelectChild(view, e.RowHandle);
            view.EndSelection();
        }
        static void SelectChild(TableView view, int groupRowHandle) {
            int childRowCount = view.Grid.GetChildRowCount(groupRowHandle);
            view.BeginSelection();
            for(int i = 0; i < childRowCount; i++) {
                int childRowHandle = view.Grid.GetChildRowHandle(groupRowHandle, i);
                if(GetMode(view) == ChildSelectionMode.Hierarchical && view.Grid.IsGroupRowHandle(childRowHandle) && view.Grid.IsGroupRowExpanded(childRowHandle)) {
                    SelectChild(view, childRowHandle);
                }
                view.SelectRow(childRowHandle);
            }
            view.EndSelection();
        }
    }

    public enum ChildSelectionMode {
        None,
        Child,
        Hierarchical,
    }
}
