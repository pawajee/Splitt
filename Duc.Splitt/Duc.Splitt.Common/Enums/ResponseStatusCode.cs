
using Duc.Splitt.Common.Extensions;

namespace Duc.Splitt.Common.Enums
{
    public enum ResponseStatusCode
    {
        [LocalizedDescription("Success", typeof(ResponseStatusCodeMessages))]
        Success = 200,

        [LocalizedDescription("ServerError", typeof(ResponseStatusCodeMessages))]
        ServerError = 500,

        [LocalizedDescription("NoDataFound", typeof(ResponseStatusCodeMessages))]
        NoDataFound = 404,

        [LocalizedDescription("Unauthorized", typeof(ResponseStatusCodeMessages))]
        Unauthorized = 4011,

        [LocalizedDescription("BadRequest", typeof(ResponseStatusCodeMessages))]
        BadRequest = 400,

        [LocalizedDescription("InvalidToken", typeof(ResponseStatusCodeMessages))]
        InvalidToken = 4030,

        [LocalizedDescription("NotPermitted", typeof(ResponseStatusCodeMessages))]
        NotPermitted = 4031,

        [LocalizedDescription("UserTerminated", typeof(ResponseStatusCodeMessages))]
        UserTerminated = 4032,

        [LocalizedDescription("UserSuspended", typeof(ResponseStatusCodeMessages))]
        UserSuspended = 4033,

        //[LocalizedDescription("UserExpired", typeof(ResponseStatusCodeMessages))]
        //UserExpired = 4034,

        [LocalizedDescription("JWTTokenEmailNotFound", typeof(ResponseStatusCodeMessages))]
        JWTTokenEmailNotFound = 4035,

        [LocalizedDescription("NewUser", typeof(ResponseStatusCodeMessages))]
        NewUser = 4036,

        [LocalizedDescription("Conflict", typeof(ResponseStatusCodeMessages))]
        Conflict = 409,

        [LocalizedDescription("Duplicate", typeof(ResponseStatusCodeMessages))]
        Duplicate = 410,


        [LocalizedDescription("LimitNotAllowrd", typeof(ResponseStatusCodeMessages))]
        LimitNotAllowrd = 6001,

        [LocalizedDescription("StratRemoteTransactionCustomMessage", typeof(ResponseStatusCodeMessages))]
        StratRemoteTransactionCustomMessage = 6010,

        OTPExpired = 6233,
        OTPVerifyLimitExceeded = 6234,
        OTPAlreadyUsed = 6235,
        OTPRequestLimitExceeded = 5003,
        OTPRequestNotFound = 6233,
        OTPVerificationNotCompleted = 6233,
        EmailNotSent = 6236,
        MIDAPIIssue = 5001,
        InvalidPACIData = 5002,

    }
}
