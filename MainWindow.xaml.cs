
using PROGP3;
using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PROGP3
{
    public partial class MainWindow : Window
    {
        private readonly ConversationManager conversationManager;

        public MainWindow()
        {
            InitializeComponent();
            conversationManager = new ConversationManager();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PlayGreeting();
            ShowWelcomeMessages();
            PromptForName();
        }

        private void PlayGreeting()
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, "Assets", "greet.wav");

                if (File.Exists(filePath))
                {
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        player.Play();
                    }
                }
                else
                {
                    AppendToChat("[greet.wav not found. Continuing without sound.]", Colors.Yellow);
                }
            }
            catch
            {
                AppendToChat("[Audio could not be played. Continuing without sound.]", Colors.Yellow);
            }
        }

        private void ShowWelcomeMessages()
        {
            AppendToChat("Hi! Welcome to the Cybersecurity Awareness Bot.", Colors.Cyan);
            AppendToChat("I'm here to help you stay safe online.", Colors.Cyan);
            AppendToChat("");
        }

        private void PromptForName()
        {
            AppendToChat("Please enter your name:", Colors.Green);
            UserInput.Focus();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
                e.Handled = true;
            }
        }

        private async Task ProcessUserInput()
        {
            string input = UserInput.Text.Trim();
            UserInput.Clear();

            if (string.IsNullOrWhiteSpace(input))
                return;

            foreach (ChatMessage message in conversationManager.ProcessInput(input))
            {
                AppendToChat(message);
            }
            string lowerInput = input.ToLower();

            if (lowerInput == "exit" ||
                lowerInput == "quit" ||
                lowerInput == "bye" ||
                lowerInput == "goodbye")
            {
                await Task.Delay(800);
                Application.Current.Shutdown();
            }
        }

        private void AppendToChat(ChatMessage message)
        {
            AppendToChat(message.Text, message.Color);
        }

        private void AppendToChat(string text, Color? color = null)
        {
            if (color.HasValue)
                ChatBox.Foreground = new SolidColorBrush(color.Value);
            else
                ChatBox.Foreground = Brushes.White;

            ChatBox.AppendText(text + Environment.NewLine);
            ChatBox.ScrollToEnd();
        }
    }
}