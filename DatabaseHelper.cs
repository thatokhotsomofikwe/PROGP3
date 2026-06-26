using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace PROGP3
{
    public class DatabaseHelper
    {
        private readonly string connectionString =
            "server=localhost;database=CyberSecurityBot;uid=root;pwd=Rosebank;";

        public void AddTask(CyberTask task)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql =
                    @"INSERT INTO Tasks
                    (Title, Description, ReminderDate, IsCompleted)
                    VALUES
                    (@Title,@Description,@ReminderDate,@Completed)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@ReminderDate", task.ReminderDate);
                cmd.Parameters.AddWithValue("@Completed", task.IsCompleted);

                cmd.ExecuteNonQuery();
            }
        }

        public List<CyberTask> GetTasks()
        {
            List<CyberTask> tasks = new List<CyberTask>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Tasks";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new CyberTask
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        ReminderDate =
                            reader["ReminderDate"] == DBNull.Value
                            ? (DateTime?)null
                            : Convert.ToDateTime(reader["ReminderDate"]),
                        IsCompleted =
                            Convert.ToBoolean(reader["IsCompleted"])
                    });
                }
            }

            return tasks;
        }

        public void DeleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM Tasks WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void CompleteTask(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql =
                    "UPDATE Tasks SET IsCompleted=true WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateReminder(int id, DateTime reminder)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql =
                    @"UPDATE Tasks
                      SET ReminderDate=@Reminder
                      WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Reminder", reminder);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}