using System;
using System.ComponentModel;
namespace GettingThingsDone.src.model
{
    public interface ISchedule : INotifyPropertyChanged
    {
        System.Collections.IEnumerable NextMonth { get; }
        System.Collections.IEnumerable ThisMonth { get; }
        System.Collections.IEnumerable ThisWeek { get; }
        System.Collections.IEnumerable Today { get; }
        System.Collections.IEnumerable Tomorrow { get; }

        void update();
    }
}
