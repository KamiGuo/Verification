using SST.Main.Commons;
using SST.Main.ViewModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SST.Main.UC
{
    /// <summary>
    /// 会议记录的子项
    /// </summary>
    public partial class UCRecordItemLanguage : UserControl
    {
        private WinMeetingRecord winParent = null;
        private SSRecordDataItemLanguage curSSRecordDataItemLanguage = null;

        public UCRecordItemLanguage()
        {
            InitializeComponent();

            this.DataContextChanged += UCRecordItemLanguage_DataContextChanged;
        }

        private void UCRecordItemLanguage_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            curSSRecordDataItemLanguage = e.NewValue as SSRecordDataItemLanguage;
            RefreshUI();

        }

        #region 依赖属性
        public string CustomeContent
        {
            get { return (string)GetValue(CustomeContentProperty); }
            set { SetValue(CustomeContentProperty, value); }
        }

        public static readonly DependencyProperty CustomeContentProperty =
            DependencyProperty.Register("CustomeContent", typeof(string), typeof(UCRecordItemLanguage),
            new UIPropertyMetadata(null, OnCustomeContentPropertyChanged));

        private static void OnCustomeContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //UCCorrenctinItemLanguage item = d as UCCorrenctinItemLanguage;
            //item.RefreshUI();

        }
        #endregion

        private void RefreshUI()
        {
            Shared.SetFontFamily(curSSRecordDataItemLanguage.LanguageCode, this);

            if (curSSRecordDataItemLanguage.IsSource)
            {
                lblSourceTip.Text = "原";
            }
            else
            {
                lblSourceTip.Text = "译";
            }
        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCommit_Click(object sender, RoutedEventArgs e)
        {
            InvokeWinParent(() => { winParent.Commit(curSSRecordDataItemLanguage); });
        }

        private void rad_Checked(object sender, RoutedEventArgs e)
        {
            txt.Focusable = true;
            txt.Focus();
        }

        private void InvokeWinParent(Action act)
        {
            if (winParent == null)
            {
                winParent = Window.GetWindow(this) as WinMeetingRecord;
            }

            if (winParent != null)
            {
                act();
            }
        }
    }
}
