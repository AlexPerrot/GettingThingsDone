using System;
namespace GettingThingsDone.src.model
{
    interface ISchedule
    {
        System.Collections.IEnumerable NextMonth { get; }
        System.Collections.IEnumerable ThisMonth { get; }
        System.Collections.IEnumerable ThisWeek { get; }
        System.Collections.IEnumerable Today { get; }
        System.Collections.IEnumerable Tomorrow { get; }
    }
}
