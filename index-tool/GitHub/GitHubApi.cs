// 
// GitHubApi.cs
//  
// Author:
//       Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (c) 2011 Novell, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitHub
{
	public class GitHubApi
	{
		const string BaseUrl = "https://github.com/api/v2/json";
		RestClient client = new RestClient (BaseUrl);
		
		public GitHubApi ()
		{
		}
		
		public GitHubOrganization GetOrganization (string org)
		{
			var request = new RestRequest ("organizations/{org}");
			request.AddParameter ("org", org, ParameterType.UrlSegment);
			request.RootElement = "organizations";
			return client.Get<GitHubOrganization> (request);
		}
		
		/// <summary>
		/// Gets the organizations of which the given user is a member.
		/// </summary>
		public List<GitHubOrganization> GetOrganizations (string user)
		{
			var request = new RestRequest ("organizations/{user}");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.RootElement = "organizations";
			return client.Get<List<GitHubOrganization>> (request);
		}
		
		/// <summary>
		/// Gets the organizations of which the current user is a member.
		/// </summary>
		public List<GitHubOrganization> GetOrganizations ()
		{
			var request = new RestRequest ("organizations");
			request.RootElement = "organizations";
			return client.Get<List<GitHubOrganization>> (request);
		}
		
		/// <summary>
		/// Gets the repositories of organizations accessible by the current user.
		/// </summary>
		public List<GitHubRepository> GetRepositories ()
		{
			var request = new RestRequest ("organizations/repositories");
			request.RootElement = "respositories";
			return client.Get<List<GitHubRepository>> (request);
		}
		
		/// <summary>
		/// Gets the public repositories of a given repo.
		/// </summary>
		public List<GitHubRepository> GetPublicRepositories (string org)
		{
			var request = new RestRequest ("organizations/{org}/public_repositories");
			request.AddParameter ("org", org, ParameterType.UrlSegment);
			request.RootElement = "repositories";
			return client.Get<List<GitHubRepository>> (request);
		}
		
		/// <summary>
		/// Gets the public members of a given repo.
		/// </summary>
		public List<GitHubUser> GetPublicMembers (string org)
		{
			var request = new RestRequest ("organizations/{org}/public_members");
			request.AddParameter ("org", org, ParameterType.UrlSegment);
			request.RootElement = "respositories";
			return client.Get<List<GitHubUser>> (request);
		}
		
		public List<GitHubTeam> GetTeams (string org)
		{
			var request = new RestRequest ("organizations/{org}/teams");
			request.AddParameter ("org", org, ParameterType.UrlSegment);
			request.RootElement = "teams";
			return client.Get<List<GitHubTeam>> (request);
		}
		
		public GitHubTeam GetTeam (string teamId)
		{
			var request = new RestRequest ("teams/{teamId}");
			request.AddParameter ("teamId", teamId, ParameterType.UrlSegment);
			request.RootElement = "team";
			return client.Get<GitHubTeam> (request);
		}
		
		public List<GitHubUser> GetTeamMembers (string teamId)
		{
			var request = new RestRequest ("teams/{teamId}/members");
			request.AddParameter ("teamId", teamId, ParameterType.UrlSegment);
			request.RootElement = "users";
			return client.Get<List<GitHubUser>> (request);
		}
		
		public List<GitHubRepository> GetTeamRepositories (string teamId)
		{
			var request = new RestRequest ("teams/{teamId}/repositories");
			request.AddParameter ("teamId", teamId, ParameterType.UrlSegment);
			request.RootElement = "repositories";
			return client.Get<List<GitHubRepository>> (request);
		}
		
		public List<GitHubUser> SearchUsers (string name)
		{
			var request = new RestRequest ("user/search/{name}");
			request.AddParameter ("name", name, ParameterType.UrlSegment);
			request.RootElement = "users";
			return client.Get<List<GitHubUser>> (request);
		}
		
		public List<GitHubUser> SearchUserEmail (string email)
		{
			var request = new RestRequest ("user/email/{email}");
			request.AddParameter ("email", email, ParameterType.UrlSegment);
			request.RootElement = "user";
			return client.Get<List<GitHubUser>> (request);
		}
		
		public GitHubUser GetUser (string name)
		{
			var request = new RestRequest ("user/show/{name}");
			request.AddParameter ("name", name, ParameterType.UrlSegment);
			request.RootElement = "user";
			return client.Get<GitHubUser> (request);
		}
		
		public GitHubAuthenticatedUser GetCurrentUser ()
		{
			var request = new RestRequest ("user/show/");
			request.RootElement = "user";
			return client.Get<GitHubAuthenticatedUser> (request);
		}
		
		public List<GitHubUser> GetUserFollowing (string user)
		{
			var request = new RestRequest ("user/show/{user}/following");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.RootElement = "users";
			return client.Get<List<GitHubUser>> (request);
		}
		
		public List<GitHubUser> GetUserFollowers (string user)
		{
			var request = new RestRequest ("user/show/{user}/followers");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.RootElement = "users";
			return client.Get<List<GitHubUser>> (request);
		}
		
		public List<GitHubRepository> GetWatchedRepositories (string user)
		{
			var request = new RestRequest ("repos/watched/{user}");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.RootElement = "respositories";
			return client.Get<List<GitHubRepository>> (request);
		}
		
		public List<GitHubPullRequest> GetPullRequests (string user, string repo)
		{
			var request = new RestRequest ("/pulls/{user}/{repo}");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.AddParameter ("repo", repo, ParameterType.UrlSegment);
			request.RootElement = "pulls";
			return client.Get<List<GitHubPullRequest>> (request);
		}
		
		public List<GitHubPullRequest> GetPullRequests (string user, string repo, string state)
		{
			var request = new RestRequest ("/pulls/{user}/{repo}/{state}");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.AddParameter ("repo", repo, ParameterType.UrlSegment);
			request.AddParameter ("state", state, ParameterType.UrlSegment);
			request.RootElement = "pulls";
			return client.Get<List<GitHubPullRequest>> (request);
		}
		
		public List<GitHubPullRequest> GetPullRequest (string user, string repo, long number)
		{
			var request = new RestRequest ("/pulls/{user}/{repo}/{number}");
			request.AddParameter ("user", user, ParameterType.UrlSegment);
			request.AddParameter ("repo", repo, ParameterType.UrlSegment);
			request.AddParameter ("number", number, ParameterType.UrlSegment);
			request.RootElement = "pulls";
			return client.Get<List<GitHubPullRequest>> (request);
		}
	}
	
	enum ParameterType
	{
		UrlSegment
	}
	
	class RestRequest
	{
		public string RootElement { get; set; }
		
		string url;
		
		public RestRequest (string url)
		{
			this.url = url;
		}
		
		public void AddParameter (string name, string value, ParameterType type)
		{
			url = url.Replace ("{" + name + "}", System.Web.HttpUtility.UrlPathEncode (value));
		}
		
		public void AddParameter (string name, long value, ParameterType type)
		{
			AddParameter (name, Convert.ToString (value), type);
		}
		
		internal string GetUrl ()
		{
			return url;
		}
	}
	
	class RestClient
	{
		JsonSerializer ser = new JsonSerializer ();
		
		public string BaseUrl { get; private set; }
		
		public RestClient (string baseUrl)
		{
			this.BaseUrl = baseUrl;
		}
		
		public T Get<T> (RestRequest request)
		{
			var wr = (HttpWebRequest) WebRequest.Create (BaseUrl + "/" + request.GetUrl ());
			wr.ContentType = "application/json";
			var response = wr.GetResponse ();
			var jr = new JsonTextReader (new StreamReader (response.GetResponseStream ()));
			while (jr.Read ()) {
				if (jr.TokenType == JsonToken.PropertyName) {
					if (!string.IsNullOrEmpty (request.RootElement) && !string.Equals (request.RootElement, jr.Value))
						throw new Exception (string.Format ("Expected root element '{0}', found '{1}'", request.RootElement, jr.Value));
					break;
				}
			}
			
			while (jr.Read () && jr.TokenType != JsonToken.StartArray);
			
			Type valueType = typeof (T);
			T value;
			if (typeof (System.Collections.IList).IsAssignableFrom (valueType)) {
				valueType = valueType.GetGenericArguments ()[0];
				value = (T) Activator.CreateInstance (typeof (T));
				var list = (System.Collections.IList) value;
				while (jr.Read ()) {
					if (jr.TokenType == JsonToken.StartObject)
						list.Add (ser.Deserialize (jr, valueType));
				}
			} else {
				while (jr.Read () && jr.TokenType != JsonToken.StartObject);
				value = ser.Deserialize<T> (jr);
			}
			return value;
		}
	}
}