using System;


interface Task : GTDItem
{
    String Title { get; set; }
    String Description { get; set; }
    Boolean Done { get; set; }
    DateTime DueDate { get; set; }
    DateTime CreationDate { get; }
}

