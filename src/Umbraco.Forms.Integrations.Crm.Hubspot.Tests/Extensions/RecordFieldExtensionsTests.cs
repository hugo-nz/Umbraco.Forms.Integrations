using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Forms.Integrations.Crm.Hubspot.Extensions;
using Umbraco.Forms.Integrations.Crm.Hubspot.Models.Responses;

namespace Umbraco.Forms.Integrations.Crm.Hubspot.Tests.Extensions
{
    public class RecordFieldExtensionsTests
    {
        [Test]
        public void ValuesAsHubspotString_WithOptions_TranslatesLabelToValue()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "He/Him" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "He/Him", Value = "he_him" },
                new PropertyOption { Label = "She/Her", Value = "she_her" },
                new PropertyOption { Label = "They/Them", Value = "they_them" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("he_him", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_TranslatesYesNoLabels()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "Yes" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "Yes", Value = "yes" },
                new PropertyOption { Label = "No", Value = "no" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("yes", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_CaseInsensitiveLabelMatch()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "YES" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "Yes", Value = "yes" },
                new PropertyOption { Label = "No", Value = "no" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("yes", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_AlreadyInternalValue_ReturnsAsIs()
        {
            // Arrange - Value is already the internal value
            var recordField = new RecordField
            {
                Values = new List<object> { "he_him" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "He/Him", Value = "he_him" },
                new PropertyOption { Label = "She/Her", Value = "she_her" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("he_him", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_NoMatchingOption_ReturnsOriginalValue()
        {
            // Arrange - Value doesn't match any option
            var recordField = new RecordField
            {
                Values = new List<object> { "Unknown Value" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "Yes", Value = "yes" },
                new PropertyOption { Label = "No", Value = "no" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("Unknown Value", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithEmptyOptions_ReturnsOriginalValue()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "He/Him" }
            };

            var options = new List<PropertyOption>();

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("He/Him", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithNullOptions_ReturnsOriginalValue()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "He/Him" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(null, escaped: false);

            // Assert
            Assert.AreEqual("He/Him", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_MultipleValues_TranslatesAll()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "He/Him", "They/Them" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "He/Him", Value = "he_him" },
                new PropertyOption { Label = "She/Her", Value = "she_her" },
                new PropertyOption { Label = "They/Them", Value = "they_them" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual("he_him;they_them", result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_NoValues_ReturnsEmptyString()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object>()
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "Yes", Value = "yes" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: false);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void ValuesAsHubspotString_WithOptions_Escaped_EscapesSpecialCharacters()
        {
            // Arrange
            var recordField = new RecordField
            {
                Values = new List<object> { "He/Him" }
            };

            var options = new List<PropertyOption>
            {
                new PropertyOption { Label = "He/Him", Value = "he_him" }
            };

            // Act
            var result = recordField.ValuesAsHubspotString(options, escaped: true);

            // Assert
            Assert.AreEqual("he_him", result);
        }
    }
}
