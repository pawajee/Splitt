using Duc.Splitt.Core.Contracts.Repositories;
using Microsoft.Extensions.Localization;

namespace Duc.Splitt.Service
{
	public class LocalizationService : ILocalizationService
    {

        private readonly IStringLocalizer<APIMessages> _localizer;
        public LocalizationService(IStringLocalizer<APIMessages> localizer)
        {
            _localizer = localizer;
        }

        public string GetLocalizedText(string key)
        {
            return _localizer[key];
        }

    }

    public class APIMessages
    {
    }
}