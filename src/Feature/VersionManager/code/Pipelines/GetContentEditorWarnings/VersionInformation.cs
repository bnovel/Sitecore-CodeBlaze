using SC.CodeBlaze.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Pipelines.GetContentEditorWarnings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.CodeBlaze.Feature.VersionManager.Pipelines.GetContentEditorWarnings
{
    public class VersionInformation
    {
        public VersionInformation()
        {

        }

        public void Process(GetContentEditorWarningsArgs args)
        {
            Sitecore.Data.Items.Item item = args.Item;
            if (item.IsNull())
                return;
            if (!item.Access.CanReadLanguage())
                return;
            var languages = item.GetLanguages();
            args.Add("Available languages", String.Join(", ", languages.Select(x => $"{x.CultureInfo.EnglishName} ({x.Name}): Versions: {item.GetVersions(x)}").ToArray()));
        }
    }
}