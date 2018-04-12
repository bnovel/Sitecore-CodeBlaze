using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.CodeBlaze.Foundation.SitecoreExtensions.Extensions
{
    public static class ItemExtensions
    {
        public static ICollection<Language> GetLanguages(this Sitecore.Data.Items.Item item)
        {
            return GetLanguages(item, false, false);
        }

        public static ICollection<Language> GetLanguages(this Sitecore.Data.Items.Item item, Boolean sortBySortOrderField)
        {
            return GetLanguages(item, sortBySortOrderField, false);
        }

        public static ICollection<Language> GetLanguages(this Sitecore.Data.Items.Item item, Boolean sortBySortOrderField, Boolean returnOnlyVersionedLanguages)
        {
            var languages = new List<Language>();
            if (sortBySortOrderField)
            {
                return GetLanguagesSortedBySortOrderField(item, returnOnlyVersionedLanguages);
            }
            return GetLanguagesNonSorted(item, returnOnlyVersionedLanguages);
        }

        public static Int32 GetVersions(this Sitecore.Data.Items.Item item, Language language)
        {
            var versionedItem = item.Database.GetItem(item.ID, language);
            if (!versionedItem.IsNull())
            {
                return versionedItem.Versions.Count;
            }
            return 0;
        }

        private static ICollection<Language> GetLanguagesNonSorted(Item item, bool returnOnlyVersionedLanguages)
        {
            var languages = new List<Language>();
            var database = item.Database;
            foreach(var language in item.Languages)
            {
                AddLanguageToList(language, item, returnOnlyVersionedLanguages, languages);
            }
            return languages;
        }

        private static ICollection<Language> GetLanguagesSortedBySortOrderField(Item item, Boolean returnOnlyVersionedLanguages)
        {
            var languages = new List<Language>();
            var database = item.Database;
            var languageItems = database.SelectItems("/sitecore/System/Languages//*[@@templateid='{0}']".FormatWith(new object[] { TemplateIDs.Language })).OrderBy(x => x[FieldIDs.Sortorder]);
            if (languageItems.IsNull())
            {
                return null;
            }
            foreach(var languageItem in languageItems)
            {
                var language = Sitecore.Data.Managers.LanguageManager.GetLanguage(languageItem.Name);
                if (!language.IsNull())
                {
                    AddLanguageToList(language, item, returnOnlyVersionedLanguages, languages);
                }
            }
            return languages;
        }

        private static void AddLanguageToList(Language language, Item item, Boolean returnOnlyVersionedLanguages, List<Language> languages)
        {
            if (!returnOnlyVersionedLanguages)
                languages.Add(language);
            else
            {
                var versionedLanguageItem = item.Database.GetItem(item.ID, language);
                if (!versionedLanguageItem.IsNull() && versionedLanguageItem.Versions.Count == 0)
                {
                    languages.Add(versionedLanguageItem.Language);
                }
            }
        }
    }
}