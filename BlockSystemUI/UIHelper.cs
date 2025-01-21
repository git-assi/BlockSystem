using FontAwesome5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using BlockSystemLib;

namespace BlockSystemUI
{
    internal class UIHelper
    {
        public static StackPanel CreatePanel(int col, int row)
        {
            var pnl = new StackPanel();
            pnl.Orientation = Orientation.Vertical;

            pnl.SetValue(Grid.ColumnProperty, col);
            pnl.SetValue(Grid.RowProperty, row);
            return pnl;
        }

        public static ImageAwesome CreatePic(Block block)
        {
            var newPic = new ImageAwesome();
            newPic.Icon = EFontAwesomeIcon.Solid_GripLinesVertical;
            //newPic.Background = block.IstFrei ? Brushes.Green : Brushes.Red;
            newPic.Rotation = block.BlockType == BlockSystemLib.Constants.BLOCK_TYPES.WEICHE ? 45 : 90;
            newPic.Width = 20;
            newPic.Height = 50;
            return newPic;
        }

        public static Label CreateDefaultLabel(Block block, string blockName)
        {
            var lbl = new Label();
            lbl.Content = blockName;
            lbl.VerticalAlignment = VerticalAlignment.Top;
            lbl.Foreground = new SolidColorBrush(Colors.White);
            lbl.Background = new SolidColorBrush(block.IstFrei ? Colors.Green : Colors.Red);
            return lbl;
        }
    }
}
