﻿namespace MVCPlayground.Framework.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Url
    {
        public static Dictionary<string, string> ParseQuery(string queryString)
        {
            return queryString
                .Split('&', StringSplitOptions.RemoveEmptyEntries)
                .Select(pair => pair
                    .Split('=', StringSplitOptions.RemoveEmptyEntries)
                    .Select(piece => piece.Trim()))
                .Where(pieces => pieces.Count() == 2)
                .ToDictionary(prop => prop.First(), prop => prop.Last());
        }

        public static Url Parse(string urlString)
        {
            Url url = new Url();

            string[] pieces = urlString.Split('?', StringSplitOptions.RemoveEmptyEntries);

            url.Path = pieces[0];

            if (pieces.Length == 2) {
                url.Query = Url.ParseQuery(pieces[1]);
            }

            return url;
        }

        public Url()
        {
            this.Query = new Dictionary<string, string>();
        }

        public string Path { get; set; }

        public Dictionary<string, string> Query { get; private set; }

        public override string ToString()
        {
            string queryString = String.Join('&',
                this.Query.Select(x => String.Join('=', x)));

            return $"{this.Path}?{queryString}";
        }
    }
}