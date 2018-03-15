using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EntityMaker.ViewController
{
    public class ControlAttachManager : DependencyObject
    {
        public static bool GetIsTableSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTableSelectedProperty);
        }

        public static void SetIsTableSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTableSelectedProperty, value);
        }

        //数据表是否被选中
        // Using a DependencyProperty as the backing store for TBSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTableSelectedProperty =
            DependencyProperty.RegisterAttached("IsTableSelected", typeof(bool), typeof(ControlAttachManager));
        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }


        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        //水印内容
        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(ControlAttachManager));

    }
}
