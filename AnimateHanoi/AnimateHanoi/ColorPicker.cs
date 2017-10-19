using System.Collections.Generic;
using System.Windows.Media;

namespace AnimateHanoi
{
    class ColorPicker
    {
        List<Color> colors; 

        public ColorPicker()
        {
            colors = new List<Color>();

            colors.Add(Colors.Blue);
            colors.Add(Colors.Red);
            colors.Add(Colors.Orange);
            colors.Add(Colors.Green);
            colors.Add(Colors.Purple);
            colors.Add(Colors.Pink);
            colors.Add(Colors.Cyan);
            colors.Add(Colors.Brown);
        }
        public Color getColor()
        {
            Color theColor = colors[colors.Count - 1];
            colors.RemoveAt(colors.Count - 1);

            return theColor;
        }
    }
}
