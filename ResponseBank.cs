using System;
using System.Collections.Generic;

namespace PROGP3
{
    public class ResponseBank
    {
        private readonly Dictionary<string, List<string>> keywordResponses;
        private readonly List<string> phishingTips;
        private readonly Random rng;
        private readonly Dictionary<string, int> lastUsedIndexes;

        public ResponseBank()
        {
            rng = new Random();
            keywordResponses = new Dictionary<string, List<string>>();
            phishingTips = new List<string>();
            lastUsedIndexes = new Dictionary<string, int>();

            InitResponses();
        }

        private void InitResponses()
        {
            keywordResponses["password"] = new List<string>
            {
                "Use strong, unique passwords for each account. Avoid personal details like your name or birth date.",
                "A good password is at least 12 characters long, with a mix of upper/lower case, numbers, and symbols.",
                "Consider using a password manager to generate and store complex passwords securely.",
                "Never reuse passwords across sites – a single breach can compromise many accounts."
            };

            keywordResponses["scam"] = new List<string>
            {
                "Be cautious of unsolicited messages asking for money or personal information. Verify the sender first.",
                "Scammers often pretend to be from well-known companies. Always contact the company through official channels.",
                "If an offer sounds too good to be true, it probably is. Don’t click links in unexpected messages.",
                "Watch out for fake invoices and urgent payment requests – they’re common scam tactics."
            };

            keywordResponses["privacy"] = new List<string>
            {
                "Review your privacy settings on social media and limit the personal information you share publicly.",
                "Use two-factor authentication wherever possible to add an extra layer of protection.",
                "Be mindful of what permissions you grant to apps – some may access your contacts or location unnecessarily."
            };



            keywordResponses["phishing"] = new List<string>
            {
            "Look at the sender's email address carefully – phishers often use addresses that look almost right.",
            "Hover over links before clicking to see where they really lead.",
            "Be suspicious of emails that create a sense of urgency, like 'Your account will be suspended!'.",
            "Never enter your login credentials after clicking a link in an email – go to the site directly.",
            "Check for poor grammar and spelling; many phishing attempts originate from non-native speakers."
            };

            keywordResponses["safety"] = new List<string>
            {
                "Always look for the padlock icon in the address bar before entering sensitive information.",
                "Keep your operating system and software up to date to protect against known vulnerabilities."
            };

            keywordResponses["browsing"] = new List<string>
            {
                "Stick to well-known websites and avoid clicking on random pop-up ads.",
                "Use a VPN when on public Wi-Fi to encrypt your internet traffic."
            };
        }

        public string GetRandomResponse(string topic)
        {
            if (!keywordResponses.ContainsKey(topic) || keywordResponses[topic].Count == 0)
            {
                return "I don't have a specific tip for that right now. Try asking about passwords, scams, privacy, or phishing.";
            }

            List<string> responses = keywordResponses[topic];

            int newIndex;

            do
            {
                newIndex = rng.Next(responses.Count);
            }
            while (
                responses.Count > 1 &&
                lastUsedIndexes.ContainsKey(topic) &&
                lastUsedIndexes[topic] == newIndex
            );

            lastUsedIndexes[topic] = newIndex;

            return responses[newIndex];
        }
    }
}