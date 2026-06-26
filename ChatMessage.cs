using System.Windows.Media;

namespace PROGP3
{
    public class ChatMessage
    {
        public string Text { get; set; }
        public Color? Color { get; set; }

        public ChatMessage(string text, Color? color = null)
        {
            Text = text;
            Color = color;
        }
    }
}