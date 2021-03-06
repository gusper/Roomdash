﻿using Engine.Models;
using Engine.Secrets;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Engine
{
    public class TwitterPostProvider : IPostProvider
    {
        private TwitterContext _twitterCtx;

        public async Task ConnectAsync()
        {
            var auth = new ApplicationOnlyAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore()
                {
                    ConsumerKey = ApiKeys.TwitterConsumerKey,
                    ConsumerSecret = ApiKeys.TwitterConsumerSecret
                }
            };

            await auth.AuthorizeAsync();

            _twitterCtx = new TwitterContext(auth);
        }

        public async Task<List<Post>> GetPostsAsync(TopicModel requestedTopic)
        {
            var queryResults =
              from search in _twitterCtx.Search
              where search.Type == SearchType.Search &&
                    search.Query == requestedTopic.TwitterQuery + " exclude:retweets" &&
                    search.Count == 50 &&
                    search.SearchLanguage == "en" &&
                    search.IncludeEntities == false &&
                    search.ResultType == ResultType.Recent
              select search;

            try
            {
                var srch = await queryResults.SingleOrDefaultAsync();

                return srch.Statuses.Select(status => new Post()
                {
                    SourceService = "twitter",
                    ScreenName = status.User.ScreenNameResponse.ToLower(),
                    Name = status.User.Name,
                    DateCreated = status.CreatedAt,
                    ID = status.StatusID.ToString(),
                    Text = status.Text,
                    UrlToUserProfile = "http://twitter.com/" + status.User.ScreenNameResponse,
                    UrlToPost = "http://twitter.com/" + status.User.ScreenNameResponse + "/status/" + status.StatusID.ToString(),
                    UrlToUserAvatar = status.User.ProfileImageUrl,
                    FollowersCount = status.User.FollowersCount,
                }).ToList();
            }
            catch (LinqToTwitter.TwitterQueryException)
            {
                Debug.WriteLine("ERROR: LinqToTwitter call returned nothing.");
                return new List<Post>() { 
                    new Post()
                    {
                        SourceService = "twitter",
                        ScreenName = "",
                        Name = "Twitter",
                        DateCreated = DateTime.Now,
                        //ID = status.StatusID,
                        Text = "Failing to get data from Twitter.",
                        UrlToUserProfile = "http://twitter.com/",
                        UrlToPost = "http://twitter.com/",
                        //UrlToUserAvatar = status.User.ProfileImageUrl,
                        FollowersCount = 0,
                    } 
                };
            }
        }
    }
}
