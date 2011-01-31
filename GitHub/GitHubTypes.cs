// 
// GitHubUser.cs
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

namespace GitHub
{
	public class GitHubUser
	{
		public string Name { get; set; }
		public string Company { get; set; }
		public string GravatarId { get; set; }
		public string Location { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Blog { get; set; }
		public int PublicGistCount { get; set; }
		public int PublicRepoCount { get; set; }
		public int FollowingCount { get; set; }
		public int Id { get; set; }
		public string Permission { get; set; }
		public string Type { get; set; }
		public int FollowersCount { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
	}
	
	public class GitHubAuthenticatedUser
	{
		public int TotalPrivateRepoCount { get; set; }
		public int Collaborators { get; set; }
		public long DiskUsage { get; set; }
		public int OwnedPrivateRepoCount { get; set; }
		public int PrivateGistCount { get; set; }
		public GitHubPlan Plan { get; set; }
	}
	
	public class GitHubPlan
	{
		public string Name { get; set; }
		public int Collaborators { get; set; }
		public long Space { get; set; }
		public int PrivateRepos { get; set; }
	}
	
	public class GitHubTeam
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public string Permission { get; set; }
	}
	
	public class GitHubPullRequest
	{
		public string State { get; set; }
		public GitHubPosition Base { get; set; }
		public GitHubPosition Head { get; set; }
		public GitHubUser IssueUser { get; set; }
		public GitHubUser User { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public float Position { get; set; }
		public int Number {get; set; }
		public int Votes { get; set; }
		public int Comments { get; set; }
		public string DiffUrl { get; set; }
		public string PatchUrl { get; set; }
		public List<string> Labels { get; set; }
		public string HtmlUrl { get; set; }
		public DateTime IssueCreatedAt { get; set; }
		public DateTime IssueUpdatedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdateAt { get; set; }
	}
	
	public class GitHubPosition
	{
		public string Label { get; set; }
		public string Ref { get; set; }
		public string Sha { get; set; }
		public GitHubUser User { get; set; }
		public GitHubRepository Repository { get; set; }
	}
	
	public class GitHubRepository
	{
		public string Name { get; set; }
		public string CreatedAt { get; set; }
		public bool HasWiki { get; set; }
		public int Watchers { get; set; }
		public bool Private { get; set; }
		public bool Fork { get; set; }
		public string Url { get; set; }
		public DateTime PushedAt { get; set; }
		public bool HasDownloads { get; set; }
		public int OpenIssues { get; set; }
		public string Organization { get; set; }
		public int Forks { get; set; }
		public string Description { get; set; }
		public string Owner { get; set; }
	}
	
	public class GitHubOrganization
	{
		public string Name { get; set; }
		public string Company { get; set; }
		public string GravatarId { get; set; }
		public string Location { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Blog { get; set; }
		public int PublicGistCount { get; set; }
		public int PublicRepoCount { get; set; }
		public int FollowingCount { get; set; }
		public int Id { get; set; }
		public string Permission { get; set; }
		public string Type { get; set; }
		public int FollowersCount { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
	}
	
	public class GitHubDiscussionItem
	{
		public string Type { get; set; }
		public DateTime CreatedAt { get; set; }
		public GitHubUser User { get; set; }
	}
	
	public class GitHubDiscussionCommit : GitHubDiscussionItem
	{
		public string Sha { get; set; }
		public string Author { get; set; }
		public string Subject { get; set; }
		public string Email { get; set; }
	}
	
	public class GitHubDiscussionComment
	{
		public string GravatarId { get; set; }
		public string Body { get; set; }
		public DateTime UpdatedAt { get; set; }
		public long Id { get; set; }
	}
	
	public class GitHubPullRequestFull : GitHubPullRequest
	{
		public List<GitHubDiscussionItem> Dicussion { get; set; }
	}
}