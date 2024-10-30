namespace Duc.Splitt.Common.Enums
{
    public enum RequestStatuses
    {
        InProgress = 1,            // The request is currently being processed
        Approved = 2,          // The request has been approved
        Rejected = 3,          // The request has been rejected
                               // InProgress,        // The request is currently being processed
        Completed,         // The request has been fully processed
        OnHold,            // The request is paused or temporarily on hold
        Canceled,          // The request was canceled by the user or system
        UnderReview,       // The request is under review for further approval or evaluation
        Failed,            // The request processing encountered an error or failure
        NeedsApproval      // The request requires approval from a higher authority
    }
}
