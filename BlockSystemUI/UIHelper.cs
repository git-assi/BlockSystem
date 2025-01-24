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

        public static ImageAwesome CreatePic(string blockType)
        {
            var newPic = new ImageAwesome();
            newPic.Icon = EFontAwesomeIcon.Solid_GripLinesVertical;            
            int rot = 0;

            switch(blockType)
            {
                case BlockSystemLib.Constants.BLOCK_TYPES.Gerade:
                    rot = 90;
                    break;

                case BlockSystemLib.Constants.BLOCK_TYPES.WEICHE:
                    rot = 45;
                    break;
                case BlockSystemLib.Constants.BLOCK_TYPES.WEICHE2:
                    rot = 135;
                    break;

                case BlockSystemLib.Constants.BLOCK_TYPES.START:
                case BlockSystemLib.Constants.BLOCK_TYPES.ENDE:
                    rot = 0;
                    break;
            }

            newPic.Rotation = rot;
            newPic.Width = 20;
            newPic.Height = 50;
            return newPic;
        }

        public static Label CreateDefaultLabel(bool istFrei, string blockName)
        {
            var lbl = new Label();
            lbl.Content = blockName;
            lbl.VerticalAlignment = VerticalAlignment.Top;
            lbl.Foreground = new SolidColorBrush(Colors.White);
            lbl.Background = new SolidColorBrush(istFrei ? Colors.Green : Colors.Red);
            return lbl;
        }
    }
}
