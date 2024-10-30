using System.Text.Json.Serialization;

namespace Duc.Splitt.Common.Dtos.Responses
{

	public class UserDetailsDto
	{
		//public bool IsFirstTimeLogin { get; set; }

		public bool IsProfileUpdateRequired { get; set; }
		public string Name { get; set; } = null!;
		public int? PreferenceLanguageId { get; set; }
		public Int32? TokenStatusId { get; set; }


		[JsonIgnore]
		public Guid UserId { get; set; }



		[JsonIgnore]
		public Guid TokenId { get; set; }


	}
}
