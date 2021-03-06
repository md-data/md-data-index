// 
// Main.cs
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
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using MonoDevelop.Ide.OnlineTemplates;

namespace MDData.Index
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//all supported MonoDevelop versions, must be kept in ascending order
			string[] versions = { "2.6" };
			
			string orgName = "md-data";
			var api = new GitHub.GitHubApi ();
			var repos = api.GetPublicRepositories (orgName);
			
			var indexes = new ProjectTemplateIndex [versions.Length];
			for (int i = 0; i < versions.Length; i++)
				indexes[i] = new ProjectTemplateIndex (versions[i]);
			
			var webClient = new System.Net.WebClient ();
			foreach (var repo in repos) {
				if (repo.Name == "md-data-index")
					continue;
				
				string rawPrefix = repo.Url + "/raw/master/";
				
				ProjectTemplateManifest manifest;
				try {
					string manifestUrl = rawPrefix + "ProjectTemplateManifest.xml";
					string manifestRaw = webClient.DownloadString (manifestUrl);
					manifest = ProjectTemplateManifest.Load (XDocument.Parse (manifestRaw));
				} catch (Exception ex) {
					Console.Error.WriteLine ("Failed to load manifest from repo '{0}': {1}", repo.Name, ex);
					continue;
				}
				
				var desc = new ProjectTemplateDescription () {
					Name = manifest.Name,
					Summary = manifest.Summary,
					Description = manifest.Description,
					Author = manifest.Author,
					Tags = manifest.Tags,
					IconUrl = string.IsNullOrEmpty (manifest.IconFile)? null : rawPrefix + manifest.IconFile,
					ScreenshotUrl = string.IsNullOrEmpty (manifest.ScreenshotFile)? null : rawPrefix + manifest.ScreenshotFile,
					TemplateUrl = repo.Url + "/zipball/master",
					Modified = repo.PushedAt
				};
				
				if (string.IsNullOrWhiteSpace (manifest.MinimumVersion)) {
					foreach (var index in indexes)
						index.Add (desc);
				} else {
					var version = manifest.MinimumVersion.Trim ();
					var idx = Array.IndexOf (versions, version);
					if (idx < 0)
						Console.Error.WriteLine ("Unknown MinimumVersion '{0'} in repo '{1}'", version, repo.Name);
					for (int i = 0; i < version.Length; i++)
						indexes[i].Add (desc);
				}
			}
			
			foreach (var index in indexes)
				index.Write ("project-template-index-" + index.MDVersion + ".xml");
		}
	}
}