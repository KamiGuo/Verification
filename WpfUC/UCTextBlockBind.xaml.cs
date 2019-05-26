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
using System.Windows.Media.Effects;

namespace SST.Main.UC
{
    /// <summary>
    /// UCTextBlockBind.xaml 的交互逻辑
    /// </summary>
    public partial class UCTextBlockBind : UserControl
    {
        public UCTextBlockBind()
        {
            InitializeComponent();
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
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(UCTextBlockBind),
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
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(UCTextBlockBind),
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
            DependencyProperty.Register("FontEffect", typeof(Effect), typeof(UCTextBlockBind),
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
            DependencyProperty.Register("LineHeight", typeof(double), typeof(UCTextBlockBind),
            new UIPropertyMetadata(1d));

        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var c = d as UCTextBlockBind;
            if (c != null)
            {
                c.RefreshItemSource();
            }
        }

        private void RefreshItemSource()
        {
            this.ic.ItemsSource = null;
            this.ic.ItemsSource = ItemsSource;
        }


        #endregion
    }
}