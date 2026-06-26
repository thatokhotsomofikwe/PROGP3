PROGP3 Cybersecurity Awareness Bot

Overview
The Cybersecurity Awareness Bot is a Windows Presentation Foundation (WPF) chatbot developed in C#. The application is designed to educate users about cybersecurity through interactive conversations, task management, quizzes, and intelligent keyword recognition.
The chatbot provides users with cybersecurity advice, allows them to manage cybersecurity-related tasks, tests their knowledge using a quiz, and keeps track of important activities performed during each session.

Features

Interactive chatbot interface.
User greeting with optional audio.
Personalised welcome message.
Keyword recognition for common cybersecurity topics.
Random cybersecurity tips.
Exit commands (Exit, Quit, Bye, Goodbye).

Dynamic responses.
Sentiment detection.
Empathetic responses based on user emotions.
Follow-up questions.
Conversation memory for favourite cybersecurity topics.

Add cybersecurity tasks.
Store tasks in a MySQL database.
View all stored tasks.
Delete tasks.
Mark tasks as completed.
Optional reminders for tasks.

Interactive quiz containing more than 10 cybersecurity questions.
Multiple Choice and True/False questions.
Immediate feedback after every answer.
Final score displayed at the end of the quiz.
Personalised feedback based on the user’s score.

The chatbot recognises different ways users phrase requests using keyword detection and string manipulation.

Examples include:

Add a task
Set a reminder
Start a quiz
View tasks
Show activity log
This provides a more natural conversation experience without requiring exact commands.
The chatbot records important actions performed during the session, including:

Tasks added
Tasks completed
Tasks deleted
Reminders created
Quiz started
Quiz completed
NLP actions recognised

Users can display the most recent activity log by asking:

Show activity log
What have you done for me?

The application includes:
WPF interface
Chat display window
User input field
Send button
Quick-access buttons for common features
Colour-coded chatbot responses

Technologies Used

C#
Windows Presentation Foundation (WPF)
.NET
MySQL
Visual Studio

Project Structure

MainWindow.xaml – User interface.
MainWindow.xaml.cs – GUI event handling.
ConversationManager.cs – Main chatbot logic.
ResponseBank.cs – Cybersecurity responses.
SentimentService.cs – Sentiment detection.
TaskManager.cs – Task management.
QuizManager.cs – Quiz functionality.
DatabaseHelper.cs – MySQL database operations.
ActivityLogEntry.cs – Activity log model.
ChatMessage.cs – Chat message object.

Database
The chatbot uses a MySQL database to store cybersecurity tasks.

Each task contains:

Task ID
Title
Description
Reminder Date
Completion Status

The database allows users to:

Add tasks
View tasks
Delete tasks
Mark tasks as completed

How to Run

1. Open the solution in Visual Studio.
2. Ensure MySQL Server is installed and running.
3. Create the required database and tables.
4. Update the MySQL connection string in the project.
5. Restore NuGet packages (including MySQL Connector).
6. Build the solution.
7. Run the application.

Example Commands

The chatbot recognises commands such as:

Add task Enable Two-Factor Authentication
View tasks
Delete task 1
Complete task 2
Remind me to update my password tomorrow
Start quiz
Show activity log
What can I ask?
Tell me about phishing
Tell me about password safety
Exit
