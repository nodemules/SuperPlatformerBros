using UnityEngine;

namespace Interfaces
{
    public interface ITrigger
    {
        int TriggerCount { get; set; }
        int NumTriggers { get; set; }
        GameObject Target { get; set; }
        ITriggerable Triggerable { get; set; }
    }
}