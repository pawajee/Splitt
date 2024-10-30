namespace Duc.SmartEv.CentralProcessor.Common.Dtos.Responses
{
    public class StatusNotificationResponseDto : StatusNotificationRequestDto
    {
        public Guid? connectorGuid { get; set; }
        public Guid? chargePointGuid { get; set; }
    }
}
