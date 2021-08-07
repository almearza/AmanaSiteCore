using System.Globalization;
using AmanaSite.Interfaces;
using AmanaSite.Models;

namespace AmanaSite.Repositories
{
    public class CurrentLang : ICurrentLang
    {
        public LangCode Get()
        {
            var _langCode = CultureInfo.CurrentCulture.ToString() switch
            {
                "ar" =>
                   LangCode.Ar,
                "en" =>
                LangCode.En,
                _ =>
                LangCode.Ur

            };
            return _langCode;
        }
    }
}