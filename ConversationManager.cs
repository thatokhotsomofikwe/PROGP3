using PROGP3;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Windows.Media;

namespace PROGP3
{
    public class ConversationManager
    {
        private string userName = "User";
        private string botName = "Bot";

        private string favouriteTopic = "";
        private string lastTopic = "";
        private int followUpCounter = 0;

        private readonly TaskManager taskManager = new TaskManager();
        private readonly ResponseBank responseBank;
        private readonly SentimentService sentimentService;

        private List<string> activityLog = new List<string>();

        private string GetFollowUpQuestion()
        {
            string[] questions =
            {
        "Would you like a tip on passwords, scams, privacy, or phishing?",
        "Do you want another cybersecurity tip?",
        "Is there anything else you're worried about online safety?",
        "Would you like to learn how to protect your accounts better?"
    };

            Random rng = new Random();
            return questions[rng.Next(questions.Length)];


        }



        public ConversationManager()
        {
            responseBank = new ResponseBank();
            sentimentService = new SentimentService();
        }

        public List<ChatMessage> ProcessInput(string input)
        {
            List<ChatMessage> messages = new List<ChatMessage>();

            if (string.IsNullOrWhiteSpace(input))
                return messages;

            string trimmedInput = input.Trim();
            string message = trimmedInput.ToLower();

            if (message.Contains("thank you") ||
    message.Contains("thanks"))
            {
                AppendUserMessage(trimmedInput, messages);

                string sentiment = sentimentService.DetectSentiment(message);



                if (sentiment == "happy")
                {
                    messages.Add(new ChatMessage(
                        "Bot: You're very welcome! I'm glad you're feeling positive — keep up those good cybersecurity habits. ",
                        Colors.Yellow));
                }
                else
                {
                    messages.Add(new ChatMessage(
                        "Bot: You're welcome! I'm always here if you need help staying safe online. ",
                        Colors.Yellow));
                }

                return messages;
            }


            if (message.Contains("how are you"))
            {
                AppendUserMessage(trimmedInput, messages);

                messages.Add(new ChatMessage(
                    "Bot: I'm doing well, thank you! I'm here and ready to help you stay safe online. How are you feeling today?",
                    Colors.Yellow));

                return messages;
            }

            if (IsFirstUserNameEntry())
            {
                HandleNameEntry(trimmedInput, messages);
                return messages;
            }

            AppendUserMessage(trimmedInput, messages);

            if (HandleExitCommand(message, messages))
                return messages;

            if (HandleSentiment(message, trimmedInput, messages))
                return messages;

            if (HandleFollowUp(message, messages))
                return messages;
            if (message.Contains("what can i ask") || message.Contains("what can i ask you about"))
            {
                messages.Add(new ChatMessage("Bot: You can ask me about passwords, scams, privacy, phishing, safe browisng and more.", Colors.Yellow));
                return messages;
            }
            // TASK: Add Task
            if (message.Contains("add task"))
            {
                string response = taskManager.AddTask(trimmedInput);
                messages.Add(new ChatMessage("Bot: " + response, Colors.Yellow));
                return messages;
            }

            // TASK: View tasks
            if (message.Contains("view tasks") || message.Contains("show tasks"))
            {
                messages.Add(new ChatMessage("Bot: " + taskManager.ViewTasks(), Colors.Yellow));
                return messages;
            }

            // TASK: Delete task
            if (message.Contains("delete task"))
            {
                string idStr = new string(message.Where(char.IsDigit).ToArray());

                if (int.TryParse(idStr, out int id))
                {
                    messages.Add(new ChatMessage("Bot: " + taskManager.DeleteTask(id), Colors.Yellow));
                }
                else
                {
                    messages.Add(new ChatMessage("Bot: Please specify a task ID to delete.", Colors.Yellow));
                }

                return messages;
            }

            // TASK: Complete task
            if (message.Contains("complete task") || message.Contains("mark task"))
            {
                string idStr = new string(message.Where(char.IsDigit).ToArray());

                if (int.TryParse(idStr, out int id))
                {
                    messages.Add(new ChatMessage("Bot: " + taskManager.CompleteTask(id), Colors.Yellow));
                }
                else
                {
                    messages.Add(new ChatMessage("Bot: Please specify a task ID to complete.", Colors.Yellow));
                }

                return messages;
            }
            // REMINDER detection
            if (message.Contains("remind me"))
            {
                messages.Add(new ChatMessage(
                    "Bot: I can set reminders for tasks. Please specify how many days (e.g. 'remind me in 3 days').",
                    Colors.Yellow));

                return messages;
            }

            if (message.Contains("in ") && message.Contains("day"))
            {
                int days = ExtractNumber(message);

                if (days > 0)
                {
                    messages.Add(new ChatMessage(
                        "Bot: Reminder noted for " + days + " day(s). (Linking to latest task)",
                        Colors.Yellow));
                }

                return messages;
            }

            if (HandleKeywordMessage(message, messages))
                return messages;

            ShowDefaultResponse(messages);
            return messages;
        }

        private bool IsFirstUserNameEntry()
        {
            return userName == "User";
        }

        private void HandleNameEntry(string input, List<ChatMessage> messages)
        {
            userName = input;
            messages.Add(new ChatMessage(userName + ": " + input, Colors.White));
            messages.Add(new ChatMessage("Hello, " + userName + "! I'm " + botName + ".", Colors.Green));
            messages.Add(new ChatMessage("Type a question or type 'exit/quit/bye' to end the session.", Colors.DarkCyan));
            messages.Add(new ChatMessage(""));
        }

        private void AppendUserMessage(string input, List<ChatMessage> messages)
        {
            messages.Add(new ChatMessage(userName + ": " + input, Colors.White));
        }

        private bool HandleExitCommand(string message, List<ChatMessage> messages)
        {
            if (!IsExitCommand(message))
                return false;

            messages.Add(new ChatMessage(
                "Bot: Goodbye, " + userName + ". Stay safe on the net! I'm always available if ever you have questions about cybersecurity & online safety.",
                Colors.Yellow));

            return true;
        }

        private bool IsExitCommand(string message)
        {
            return message == "exit" || message == "quit" || message == "bye" || message == "goodbye";
        }

        private bool HandleSentiment(string message, string originalMessage, List<ChatMessage> messages)
        {
            string sentiment = sentimentService.DetectSentiment(message);

            if (string.IsNullOrEmpty(sentiment))
                return false;

            string empathetic = sentimentService.GetEmpatheticMessage(sentiment);
            string topic = ResolveTopicFromMessage(originalMessage);

            messages.Add(new ChatMessage("Bot: " + empathetic, Colors.Yellow));
            messages.Add(new ChatMessage("Bot: " + responseBank.GetRandomResponse(topic), Colors.Yellow));

            return true;
        }

        private string ResolveTopicFromMessage(string originalMessage)
        {
            string topic = FindMatchingTopic(originalMessage);

            if (string.IsNullOrEmpty(topic))
            {
                if (!string.IsNullOrEmpty(favouriteTopic))
                    topic = favouriteTopic;
                else
                    topic = "phishing";
            }

            return topic;
        }

        private bool HandleFollowUp(string message, List<ChatMessage> messages)
        {
            if (!IsFollowUpRequest(message))
                return false;

            ShowFollowUpResponse(messages);
            return true;
        }

        private bool IsFollowUpRequest(string message)
        {
            string[] followUps = { "give me another", "another tip", "tell me more", "explain more", "more", "another" };
            return followUps.Any(fu => message.Contains(fu)) && !string.IsNullOrEmpty(lastTopic);
        }

        private void ShowFollowUpResponse(List<ChatMessage> messages)
        {
            followUpCounter++;

            if (followUpCounter > 3)
            {
                messages.Add(new ChatMessage(
                    "Bot: You seem interested in this topic. Would you like to explore another area? You can ask about passwords, scams, privacy, or phishing.",
                    Colors.Yellow));

                followUpCounter = 0;
                return;
            }

            messages.Add(new ChatMessage("Bot: " + responseBank.GetRandomResponse(lastTopic), Colors.Yellow));
        }



        private bool HandleKeywordMessage(string message, List<ChatMessage> messages)
        {
            string matchedTopic = FindMatchingTopic(message);

            if (string.IsNullOrEmpty(matchedTopic))
                return false;

            UpdateMemoryForTopic(message, matchedTopic, messages);

            messages.Add(new ChatMessage("Bot: " + responseBank.GetRandomResponse(matchedTopic), Colors.Yellow));
            return true;
        }

        private void UpdateMemoryForTopic(string message, string matchedTopic, List<ChatMessage> messages)
        {
            if (message.Contains("interested in") || message.Contains("favourite") || message.Contains("favorite"))
            {
                favouriteTopic = matchedTopic;
                messages.Add(new ChatMessage(
                    "Bot: Great! I'll remember that you're interested in " + favouriteTopic + ". It's a crucial part of staying safe online.",
                    Colors.Yellow));
            }

            lastTopic = matchedTopic;
            followUpCounter = 0;
        }

        private string FindMatchingTopic(string message)
        {
            if (message.Contains("password")) return "password";
            if (message.Contains("scam")) return "scam";
            if (message.Contains("privacy")) return "privacy";
            if (message.Contains("phishing")) return "phishing";
            if (message.Contains("safety") || message.Contains("safe")) return "safety";
            if (message.Contains("browsing") || message.Contains("link") || message.Contains("website")) return "browsing";

            return "";
        }

        private void ShowDefaultResponse(List<ChatMessage> messages)
        {
            messages.Add(new ChatMessage(
                "Bot: I'm not sure I understand. Can you try rephrasing? You can ask me about passwords, scams, privacy, phishing, safe browsing, and more.",
                Colors.Yellow));
        }

        private int ExtractNumber(string input)
        {
            string digits = new string(input.Where(char.IsDigit).ToArray());

            if (int.TryParse(digits, out int result))
                return result;

            return 0;
        }
    }
}
