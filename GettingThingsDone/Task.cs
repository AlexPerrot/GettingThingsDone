using System;


    interface Task
    {
        String Title { get; set; }
        String Description { get; set; }
        Boolean Done { get; set; }
        DateTime DueDate { get; set; }
        DateTime CreationDate { get; }
    }

