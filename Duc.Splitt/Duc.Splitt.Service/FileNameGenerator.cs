using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duc.Splitt.Service
{
    public class FileNameGenerator
    {
        private static readonly Dictionary<string, string> MimeToExtension = new Dictionary<string, string>
    {
        { "image/jpeg", ".jpg" },
        { "image/png", ".png" },
        { "application/pdf", ".pdf" },

    };

        public static string GenerateFileName(string mimeType, Guid guid)
        {

            // Determine file extension based on MIME type
            string fileExtension = MimeToExtension.ContainsKey(mimeType) ? MimeToExtension[mimeType] : string.Empty;

            // Concatenate GUID with file extension
            return $"{guid}{fileExtension}";
        }
    }


}
