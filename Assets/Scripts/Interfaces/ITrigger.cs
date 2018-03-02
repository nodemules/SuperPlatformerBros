using UnityEngine;

namespace Interfaces
{
    public interface ITrigger
    {
        #region editor fields
        
        bool IsToggle { get; set; }
        int NumberOfTriggers { get; set; }
        GameObject Target { get; set; }
        
        #endregion
        
        #region package private fields
        
        int TriggerFiredCount { get; set; }
        ITriggerable Triggerable { get; set; }
        
        #endregion
    }
}