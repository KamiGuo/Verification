using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows.Media.Effects;
using SST.Main.ViewModes;

namespace SST.Main.UC
{
    /// <summary>
    /// UCTextBlockWrapBind.xaml 的交互逻辑
    /// </summary>
    public partial class UCTextBlockWrapBind : UserControl
    {
        public UCTextBlockWrapBind()
        {
            InitializeComponent();
            this.Unloaded += new RoutedEventHandler(UCTextBlockWrapBind_Unloaded);
        }

        void UCTextBlockWrapBind_Unloaded(object sender, RoutedEventArgs e)
        {
            if (obc != null)
            {
                obc.CollectionChanged -= new NotifyCollectionChangedEventHandler(obc_CollectionChanged);
            }
        }


        #region 依赖属性

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(UCTextBlockWrapBind),
            new UIPropertyMetadata(null, OnItemsSourcePropertyChanged));

        /// <summary>
        /// 显示的字段
        /// </summary>
        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(UCTextBlockWrapBind),
            new UIPropertyMetadata(null, OnItemsSourcePropertyChanged));

        /// <summary>
        /// 字体对齐方式
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(UCTextBlockWrapBind),
            new UIPropertyMetadata(TextAlignment.Left));

        /// <summary>
        /// 字体阴影
        /// </summary>
        public Effect FontEffect
        {
            get { return (Effect)GetValue(FontEffectProperty); }
            set { SetValue(FontEffectProperty, value); }
        }

        public static readonly DependencyProperty FontEffectProperty =
            DependencyProperty.Register("FontEffect", typeof(Effect), typeof(UCTextBlockWrapBind),
            new UIPropertyMetadata(null));

        /// <summary>
        /// 行距
        /// </summary>
        public double LineHeight
        {
            get { return (double)GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); }
        }

        public static readonly DependencyProperty LineHeightProperty =
            DependencyProperty.Register("LineHeight", typeof(double), typeof(UCTextBlockWrapBind),
            new UIPropertyMetadata(1d));

        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = d as UCTextBlockWrapBind;
            if (c != null)
            {
                c.RefreshItemSource();
            }


        }


        #endregion

        private ObservableCollection<SSTextBlockBindDataItem> obc = null;

        private void RefreshItemSource()
        {

            txb.Inlines.Clear();

            if (ItemsSource == null || string.IsNullOrEmpty(DisplayMemberPath))
            {
                return;
            }

            obc = ItemsSource as ObservableCollection<SSTextBlockBindDataItem>;

            foreach (var item in ItemsSource)
            {
                AddRun(item);
            }

            if (obc != null)
            {
                obc.CollectionChanged += new NotifyCollectionChangedEventHandler(obc_CollectionChanged);
            }
        }

        void obc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    AddRun(item);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    RemoveRun((item));
                }
            }
        }

        private void AddRun(object dataItem)
        {

            Run run = new Run();
            run.DataContext = dataItem;
            run.SetBinding(Run.TextProperty, new Binding(DisplayMemberPath));

            int index = obc.IndexOf(dataItem as SSTextBlockBindDataItem);
            if (index == -1)
            {
                return;
            }
            if (obc.Count == index + 1)
            {
                txb.Inlines.Add(run);
            }
            else if(index ==0)
            {
                var indexOneData = obc[1];
                Inline oneItem = QueryItem(indexOneData);
                if(oneItem!= null)
                {
                    txb.Inlines.InsertBefore(oneItem, run);
                }
            }
            else
            {
                var indexBeforOneData = obc[index - 1];
                Inline oneItem = QueryItem(indexBeforOneData);
                if (oneItem != null)
                {
                    txb.Inlines.InsertAfter(oneItem, run);
                }
            }
        }

        private void RemoveRun(object dataItem)
        {
            Inline delItem = QueryItem(dataItem);
            if (delItem != null)
            {
                txb.Inlines.Remove(delItem);
            }
        }

        private Inline QueryItem(object itemData)
        {
            Inline reulst = null;
            foreach (var item in txb.Inlines)
            {
                if (item.DataContext.Equals(itemData))
                {
                    reulst = item;
                    break;
                }
            }
            return reulst;
        }
    }
}
