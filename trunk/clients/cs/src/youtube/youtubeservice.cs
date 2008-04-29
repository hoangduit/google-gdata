/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/


using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net; 
using Google.GData.Client;
using Google.GData.Extensions;


namespace Google.GData.YouTube {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// The YouTube Data API allows applications to perform functions normally 
    /// executed on the YouTube website. The API enables your application to search 
    /// for YouTube videos and to retrieve standard video feeds, comments and video
    /// responses. 
    /// In addition, the API lets your application upload videos to YouTube or 
    /// update existing videos. Your can also retrieve playlists, subscriptions, 
    /// user profiles and more. Finally, your application can submit 
    /// authenticated requests to enable users to create playlists, 
    /// subscriptions, contacts and other account-specific entities.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class YouTubeService : Service
    {
       
        /// <summary>This service's User-Agent string</summary> 
        public const string YTAgent = "GYouTube-CS/1.0.0";
        /// <summary>The Calendar service's name</summary> 
        public const string YTService = "youtube";

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="applicationName">the applicationname</param>
        public YouTubeService(string applicationName) : base(YTService, applicationName, YTAgent)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed); 
        }
   
        /// <summary>
        /// overloaded to create typed version of Query
        /// </summary>
        /// <param name="feedQuery"></param>
        /// <returns>EventFeed</returns>
        public YouTubeFeed Query(YouTubeQuery feedQuery) 
        {
            return base.Query(feedQuery) as YouTubeFeed;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>eventchaining. We catch this by from the base service, which 
        /// would not by default create an atomFeed</summary> 
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new YouTube Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e"); 
            }

            e.Feed = new YouTubeFeed(e.Uri, e.Service);
        }
        /////////////////////////////////////////////////////////////////////////////
    }
}
