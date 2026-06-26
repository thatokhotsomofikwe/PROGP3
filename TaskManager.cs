using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;

namespace PROGP3
{
    public class TaskManager
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public string AddTask(string input)
        {
            // Basic NLP extraction (title only)
            string title = input;

            if (input.ToLower().Contains("add task"))
            {
                title = input.ToLower().Replace("add task", "").Trim();
            }

            if (string.IsNullOrWhiteSpace(title))
                title = "Cybersecurity Task";

            CyberTask task = new CyberTask
            {
                Title = title,
                Description = "Cybersecurity task added via chatbot",
                IsCompleted = false,
                ReminderDate = null
            };

            db.AddTask(task);

            return $"Task added: '{task.Title}'. Would you like to set a reminder?";
        }

        public string SetReminder(int taskId, int days)
        {
            DateTime reminder = DateTime.Now.AddDays(days);
            db.UpdateReminder(taskId, reminder);

            return $"Reminder set for {days} day(s) from now.";
        }

        public string ViewTasks()
        {
            var tasks = db.GetTasks();

            if (tasks.Count == 0)
                return "No tasks found.";

            string output = "Your Cybersecurity Tasks:\n";

            foreach (var t in tasks)
            {
                output +=
                    $"\nID: {t.Id}" +
                    $"\nTitle: {t.Title}" +
                    $"\nDescription: {t.Description}" +
                    $"\nReminder: {(t.ReminderDate.HasValue ? t.ReminderDate.ToString() : "None")}" +
                    $"\nCompleted: {t.IsCompleted}\n";
            }

            return output;
        }

        public string DeleteTask(int id)
        {
            db.DeleteTask(id);
            return $"Task {id} deleted successfully.";
        }

        public string CompleteTask(int id)
        {
            db.CompleteTask(id);
            return $"Task {id} marked as completed.";
        }
    }
}