using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGP3
{
public class CyberTask
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? ReminderDate { get; set; }

    public bool IsCompleted { get; set; }
}
}

