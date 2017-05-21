using Snowinmars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snowinmars.Ui.Controllers
{
    public static class LocalizationHelper
    {
        /// <summary>
        /// Get request url corrected according to logic of routing with locale
        /// </summary>
        /// <param name="initialUri"></param>
        /// <param name="controllersNames"></param>
        /// <param name="userLang"></param>
        /// <returns></returns>
        public static string GetLocalisedUrl(Uri initialUri, IList<string> controllersNames, string userLang)
        {
            IList<string> supportedLocales = LocalizationHelper.GetSupportedLocales();

            // Dicide requested url to parts
            List<string> cleanedSegments = initialUri.Segments.Select(s => s.Replace("/", "")).ToList();

            ////

            // This condition is for default (initial) route
            bool isDefaultUrl = cleanedSegments.Count == 1;

            if (isDefaultUrl)
            {
                return "";
            }

            ////

            // does request need to be changed
            var isRequestPathToHandle =
                // Url has controller's name part
                (cleanedSegments.Count > 1 && cleanedSegments.Intersect(controllersNames).Any()) ||

                // initial route with lang parameter that is not supported -> need to change it
                (cleanedSegments.Count == 2 && !supportedLocales.Contains(cleanedSegments[1]));

            if (isRequestPathToHandle)
            {
                string langVal;

                // Get user preffered language from Accept-Language header
                if (!string.IsNullOrWhiteSpace(userLang))
                {
                    // For our locale name approach we'll take only first part of lang-locale definition
                    langVal = userLang;

                    if (!supportedLocales.Contains(langVal))
                    {
                        langVal = "en";
                    }
                }
                else
                {
                    langVal = "";
                }

                // If we don't support requested language - then redirect to requested page with default language
                string normalisedPathAndQuery = initialUri.PathAndQuery;

                if ((cleanedSegments.Count > 2 &&
                    !controllersNames.Contains(cleanedSegments[1]) &&
                    controllersNames.Contains(cleanedSegments[2])) ||
                    (cleanedSegments.Count == 2) &&
                    (!controllersNames.Contains(cleanedSegments[1])))
                {
                    // Second segment contains lang parameter, third segment contains controller name
                    cleanedSegments.RemoveAt(1);

                    // Remove wrong locale name from initial Uri
                    normalisedPathAndQuery = string.Join("/", cleanedSegments) + initialUri.Query;
                }

                // Finally, create new uri with language loocale
                return $"{initialUri.Scheme}://{initialUri.Host}:{initialUri.Port}/{langVal.ToLower()}{normalisedPathAndQuery}";
            }

            return "";
        }

        public static IList<string> GetSupportedLocales()
        {
            return Enum.GetNames(typeof(Language)).Select(s => s.ToLowerInvariant()).ToList();
        }
    }
}