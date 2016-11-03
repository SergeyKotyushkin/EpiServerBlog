using System;
using EpiServerBlogs.Logic;
using EpiServerBlogs.Web.Models.DynamicData;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;

namespace EpiServerBlogs.Web.Jobs
{
    [ScheduledPlugIn(DisplayName = "Bad Words")]
    public class ScheduledJobBadWords : ScheduledJobBase
    {
        private bool _stopSignaled;

        public ScheduledJobBadWords()
        {
            IsStoppable = true;
        }

        /// <summary>
        /// Called when a user clicks on Stop for a manually started job, or when ASP.NET shuts down.
        /// </summary>
        public override void Stop()
        {
            _stopSignaled = true;
        }

        /// <summary>
        /// Called when a scheduled job executes
        /// </summary>
        /// <returns>A status message to be stored in the database log and visible from admin mode</returns>
        public override string Execute()
        {
            //Call OnStatusChanged to periodically notify progress of job for manually started jobs
            OnStatusChanged(string.Format("Starting execution of {0}", GetType()));

            //Add implementation
            var uncheckedComments = Comment.GetUncheckedComments();
            if(uncheckedComments.Length == 0)
                return "No one new comments";

            foreach (var uncheckedComment in uncheckedComments)
            {
                uncheckedComment.Text = CommentProvider.GetTextWithoutBadWords(uncheckedComment.Text);
                uncheckedComment.Checked = true;
                uncheckedComment.Save();

                if (_stopSignaled)
                    return "Stop of job was called. Some comments were not checked yet";
            }

            return "All new comments were checked during this job execution";
        }
    }
}
