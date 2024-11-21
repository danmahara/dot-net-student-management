using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace StudentManagement.Helper
{
    public class SlugHelper
    {

        public static string CreateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            // Step 1: Trim whitespace
            name = name.Trim();

            // Step 2: Convert to lowercase
            name = name.ToLowerInvariant();

            // Step 3: Remove invalid characters using Regex
            name = Regex.Replace(name, @"[^a-z0-9\s-]", "");

            // Step 4: Replace spaces and multiple dashes with a single dash
            name = Regex.Replace(name, @"\s+", "_");  // Replace spaces with hyphens
            name = Regex.Replace(name, @"-+", "_");  // Replace multiple dashes with one

            // Step 5: Optionally, truncate the slug
            const int maxLength = 50; // Set a maximum length if needed
            if (name.Length > maxLength)
                name = name.Substring(0, maxLength);

            // Step 6: Remove trailing dash if exists
            name = name.Trim('_');

            return name;
        }
    }
}