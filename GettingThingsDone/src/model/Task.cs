using System;


public interface Task : GTDItem
{
    String Title { get; set; }
    String Description { get; set; }
    Boolean Done { get; set; }
    DateTimeOffset? DueDate { get; set; }
    DateTimeOffset CreationDate { get; }
}

