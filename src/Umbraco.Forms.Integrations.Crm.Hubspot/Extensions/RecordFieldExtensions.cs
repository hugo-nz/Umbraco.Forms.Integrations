using System.Collections.Generic;
using System.Linq;
using Umbraco.Forms.Core.Data.Helpers;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Forms.Integrations.Crm.Hubspot.Models.Responses;

namespace Umbraco.Forms.Integrations.Crm.Hubspot.Extensions;

public static class RecordFieldExtensions
{
    public static string ValuesAsHubspotString(this RecordField recordField, bool escaped = true)
    {
        if (!recordField.HasValue())
        {
            return string.Empty;
        }

        if (!escaped)
        {
            return string.Join(";", recordField.Values.ToArray());
        }

        return string.Join(";", recordField.Values.ConvertAll((object input) => JsonHelper.EscapeStringValue(input.ToString())).ToArray());
    }

    /// <summary>
    /// Converts form field values to HubSpot compatible values, translating labels to internal values for enumeration properties.
    /// </summary>
    /// <param name="recordField">The record field containing the values.</param>
    /// <param name="propertyOptions">The HubSpot property options for translation.</param>
    /// <param name="escaped">Whether to escape the values.</param>
    /// <returns>A semicolon-separated string of HubSpot internal values.</returns>
    public static string ValuesAsHubspotString(this RecordField recordField, IEnumerable<PropertyOption> propertyOptions, bool escaped = true)
    {
        if (!recordField.HasValue())
        {
            return string.Empty;
        }

        var options = propertyOptions?.ToList() ?? new List<PropertyOption>();
        
        // If no options, fall back to standard behavior
        if (options.Count == 0)
        {
            return recordField.ValuesAsHubspotString(escaped);
        }

        // Convert each value, translating labels to internal values where applicable
        var translatedValues = recordField.Values.ConvertAll(input =>
        {
            var inputString = input?.ToString() ?? string.Empty;

            // Skip empty values - return as-is
            if (string.IsNullOrWhiteSpace(inputString))
            {
                return escaped ? JsonHelper.EscapeStringValue(inputString) : inputString;
            }
            
            // Try to find matching option by label (case-insensitive)
            var matchingOption = options.FirstOrDefault(o => 
                string.Equals(o.Label, inputString, System.StringComparison.OrdinalIgnoreCase));
            
            // If no match by label, try matching by value (in case the value is already the internal value)
            if (matchingOption == null)
            {
                matchingOption = options.FirstOrDefault(o => 
                    string.Equals(o.Value, inputString, System.StringComparison.OrdinalIgnoreCase));
            }

            var translatedValue = matchingOption?.Value ?? inputString;

            return escaped ? JsonHelper.EscapeStringValue(translatedValue) : translatedValue;
        });

        return string.Join(";", translatedValues.ToArray());
    }
}
