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

    public required string Title { get; set; }

    public required string Description { get; set; }

    public DateTime? ReminderDate { get; set; }

    public bool IsCompleted { get; set; }
}
}

