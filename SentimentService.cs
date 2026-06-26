namespace PROGP3
{
    public class SentimentService
    {
        public string DetectSentiment(string message)
        {
            if (message.Contains("worried") ||
                message.Contains("scared") ||
                message.Contains("anxious") ||
                message.Contains("nervous") ||
                message.Contains("concerned"))
                return "worried";

            if (message.Contains("curious") ||
                message.Contains("interested") ||
                message.Contains("tell me more"))
                return "curious";

            if (message.Contains("frustrated") ||
                message.Contains("annoyed") ||
                message.Contains("confused") ||
                message.Contains("angry"))
                return "frustrated";

            if (message.Contains("sad") ||
                message.Contains("upset") ||
                message.Contains("down"))
                return "sad";

            if (message.Contains("happy") ||
                message.Contains("excited") ||
                message.Contains("great"))
                return "happy";

            return "";
        }

        public string GetEmpatheticMessage(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "It's completely understandable to feel concerned. Cybersecurity threats can be stressful, but learning safe habits helps a lot.";

                case "curious":
                    return "Great! I love your curiosity. Here’s something useful for you.";

                case "frustrated":
                    return "I understand that cybersecurity can feel overwhelming at times. Let’s take it step by step.";

                case "sad":
                    return "I'm sorry you're feeling that way. Staying informed and protected online can help you feel more confident and secure.";

                case "happy":
                    return "I'm glad you're feeling positive! Keeping good cybersecurity habits is a great achievement.";

                default:
                    return "";
            }
        }
    }
}