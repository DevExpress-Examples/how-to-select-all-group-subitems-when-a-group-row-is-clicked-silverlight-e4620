using System;
using System.Collections.Generic;
using System.Windows;

namespace dxExample {
    public class SampleDataRow : DependencyObject {
        public int Id {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public string Group {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }
        public string Name {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public bool HasFlag {
            get { return (bool)GetValue(HasFlagProperty); }
            set { SetValue(HasFlagProperty, value); }
        }

        public static readonly DependencyProperty HasFlagProperty = DependencyProperty.Register("HasFlag", typeof(bool), typeof(SampleDataRow), new PropertyMetadata(false));
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(SampleDataRow), new PropertyMetadata(""));
        public static readonly DependencyProperty GroupProperty = DependencyProperty.Register("Group", typeof(string), typeof(SampleDataRow), new PropertyMetadata(""));
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(SampleDataRow), new PropertyMetadata(0));

        public static IList<SampleDataRow> CreateRows() {
            IList<SampleDataRow> result = new List<SampleDataRow>();
            for(int i = 0; i < 100; i++) {
                result.Add(new SampleDataRow() {
                    Id = i,
                    Group = String.Format("group {0}", i % 10),
                    Name = String.Format("name {0}", i % 3),
                    HasFlag = (i % 3 == 0),
                });
            }
            return result;
        }
    }
}
