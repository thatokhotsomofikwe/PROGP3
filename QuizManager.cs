using PROGP3;
using System;
using System.Collections.Generic;

namespace PROGP3
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentIndex;
        private int score;
        private bool quizActive;

        public QuizManager()
        {
            questions = new List<QuizQuestion>();
            currentIndex = 0;
            score = 0;
            quizActive = false;

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions.Add(new QuizQuestion
            {
                Question = "What should you do if you receive a suspicious email asking for your password?",
                Options = new string[]
                {
                    "Reply with your password",
                    "Ignore it completely",
                    "Report it as phishing",
                    "Click the link to verify"
                },
                CorrectIndex = 2,
                Explanation = "Reporting phishing emails helps prevent scams.",
                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Using the same password for multiple accounts is safe.",
                Options = new string[] { "True", "False" },
                CorrectIndex = 1,
                Explanation = "Reusing passwords is dangerous because one breach can expose all accounts.",
                IsTrueFalse = true
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is two-factor authentication?",
                Options = new string[]
                {
                    "A backup password",
                    "A second layer of security",
                    "A type of virus",
                    "A firewall setting"
                },
                CorrectIndex = 1,
                Explanation = "2FA adds an extra layer of security beyond just a password.",
                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Public Wi-Fi is always safe for banking.",
                Options = new string[] { "True", "False" },
                CorrectIndex = 1,
                Explanation = "Public Wi-Fi is often insecure and should be avoided for sensitive tasks.",
                IsTrueFalse = true
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is phishing?",
                Options = new string[]
                {
                    "A type of firewall",
                    "A scam to steal personal info",
                    "A password manager",
                    "A browser update"
                },
                CorrectIndex = 1,
                Explanation = "Phishing is when attackers trick you into giving personal information.",
                IsTrueFalse = false
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Strong passwords should include letters, numbers, and symbols.",
                Options = new string[] { "True", "False" },
                CorrectIndex = 0,
                Explanation = "Strong passwords use a mix of characters for better security.",
                IsTrueFalse = true
            });
        }

        public string StartQuiz()
        {
            quizActive = true;
            currentIndex = 0;
            score = 0;

            return GetQuestion();
        }

        public string GetQuestion()
        {
            if (currentIndex >= questions.Count)
            {
                quizActive = false;
                return $"Quiz complete! Your score: {score}/{questions.Count}. " +
                       GetFinalMessage();
            }

            var q = questions[currentIndex];

            string output = $"Question {currentIndex + 1}: {q.Question}\n";

            for (int i = 0; i < q.Options.Length; i++)
            {
                output += $"{i + 1}) {q.Options[i]}\n";
            }

            return output;
        }

        public string SubmitAnswer(int answer)
        {
            if (!quizActive)
                return "Quiz is not active. Type 'start quiz' to begin.";

            var q = questions[currentIndex];

            if (answer - 1 == q.CorrectIndex)
            {
                score++;
            }

            string response =
                (answer - 1 == q.CorrectIndex)
                ? "Correct! " + q.Explanation
                : "Incorrect. " + q.Explanation;

            currentIndex++;

            response += "\n\n" + GetQuestion();

            return response;
        }

        private string GetFinalMessage()
        {
            if (score >= 5)
                return "Great job! You're a cybersecurity pro!";
            if (score >= 3)
                return "Good effort! Keep learning to stay safe online.";
            return "Keep learning! Cybersecurity awareness is important.";
        }
    }
}