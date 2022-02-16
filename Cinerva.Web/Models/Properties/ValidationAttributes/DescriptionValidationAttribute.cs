using System;
using System.ComponentModel.DataAnnotations;

namespace Cinerva.Web.Models.Properties.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DescriptionValidationAttribute : ValidationAttribute
    {
        private string _prefix = "Descrip$";
        private string _suffix = "$endOfDescrip";
        #region [Public Methods]
        public override bool IsValid(object value)
        {
            var inputString = value as string;

            if (CheckIfInputIsEmpty(inputString) && CheckIfPrefixIsCorrect(inputString) && CheckISuffixIsCorrect(inputString))
            {
                return true;
            }

            return false;
        }
        #endregion
        #region [Private Methods]
        private bool CheckIfInputIsEmpty(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) && text.Length > _prefix.Length + _suffix.Length)
            {
                return true;
            }

            return false;
        }
        private bool CheckIfPrefixIsCorrect(string text)
        {
            if (text.Length <= _prefix.Length)
            {
                return false;
            }

            return text.Substring(0, _prefix.Length) == _prefix;
        }
        private bool CheckISuffixIsCorrect(string text)
        {
            if (_suffix.Length >= text.Length)
            {
                return false;
            }

            var textSuffix = text.Substring(text.Length - _suffix.Length, _suffix.Length);
            return textSuffix == _suffix;
        }
        #endregion
    }
}
