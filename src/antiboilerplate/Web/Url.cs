using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Antiboilerplate.Functional;

namespace antiboilerplate.Web
{
    public class Url
    {
        private Url(UriBuilder uriBuilder) => _builder = uriBuilder;
        private readonly UriBuilder _builder;
        private readonly List<KeyValuePair<string, string>> _queryParameters
            = new List<KeyValuePair<string, string>>();
        public static Url Create(UriBuilder uriBuilder = null)
            => uriBuilder == null ? new Url(new UriBuilder().Then(b => b.Scheme = "https")) : new Url(uriBuilder);

        public static Url Create(string url) => Create(new UriBuilder(url));

        public static Url CreateForHost(string host) => Create(new UriBuilder().Then(b => b.Host = host));

        public Url WithPath(string path) => this.Then(x => x._builder.Path = path);
        public Url With(Scheme scheme)
        {
            switch (scheme)
            {
                case Scheme.Http: _builder.Scheme = "http"; break;
                case Scheme.Https: _builder.Scheme = "https"; break;
                default:
                    throw new Exception("Library error. Cannot handle scheme = " + scheme);
            }
            return this;
        }
        public Url WithHost(string host) => this.Then(x => x._builder.Host = host);
        public Url WithUsername(string userName) => this.Then(x => x._builder.UserName = userName);
        public Url WithPassword(string password) => this.Then(x => x._builder.Password = password);

        public Url WithQueryParameter(string key, string value)
            => WithQueryParameter(new KeyValuePair<string, string>(key, value));

        public Url WithQueryParameter(KeyValuePair<string, string> keyValuePair)
        {
            _queryParameters.Add(keyValuePair);
            return this;
        }

        public Url WithFragment(string fragment) => this.Then(x => x._builder.Fragment = fragment);

        public Url WithQuery(string query)
        {
            _queryParameters.AddRange(query.ParseQueryString());
            return this;
        }

        private Uri ToUri()
        {
            if (_queryParameters.Any())
            {
                _builder.Query = _queryParameters
                    .Select(e => WebUtility.UrlEncode(e.Key) + "=" + WebUtility.UrlEncode(e.Value))
                    .Aggregate((e1, e2) => e1 + "&" + e2);
            }
            return _builder.Uri;
        }

        public static implicit operator Uri(Url from) => from.ToUri();

        public static implicit operator string(Url from) => from.ToUri().AbsoluteUri;

        public enum Scheme
        {
            Http,
            Https
        }
    }
}