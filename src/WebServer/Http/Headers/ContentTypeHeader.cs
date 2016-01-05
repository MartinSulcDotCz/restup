﻿using Devkoes.Restup.WebServer.Models.Contracts;
using Devkoes.Restup.WebServer.Models.Schemas;
using System.Text;

namespace Devkoes.Restup.WebServer.Http.Headers
{
    internal class ContentTypeHeader : HttpSingleQuantifiedHeaderBase
    {
        internal static string NAME = "Content-Type";
        internal static string CHARSET_QUANTIFIER_NAME = "charset";

        public MediaType ContentType { get; internal set; }
        public Encoding ContentEncoding { get; internal set; }

        public ContentTypeHeader(string value, QuantifiedHeaderValue quantifiedHeaderValue) : base(NAME, value, quantifiedHeaderValue)
        {
            ContentType = HttpCodesTranslator.Default.GetMediaType(QuantifiedHeaderValue.HeaderValue);
            string charset = QuantifiedHeaderValue.FindQuantifierValue(CHARSET_QUANTIFIER_NAME);
            ContentEncoding = charset != null ? Encoding.GetEncoding(charset) : Constants.DefaultHttpMessageCharset;
        }

        public override void Visit<T>(IHttpHeaderVisitor<T> v, T arg)
        {
            v.Visit(this, arg);
        }
    }
}