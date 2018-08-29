using System;
using Coderr.Client.Contracts;
using Coderr.Client.Uploaders;

namespace Coderr.Client.AspNet.Mvc5.Demo
{
    public class CustomSubmitter : IReportUploader
    {
        public void UploadFeedback(FeedbackDTO feedback)
        {
        }

        public void UploadReport(ErrorReportDTO report)
        {
        }

        public event EventHandler<UploadReportFailedEventArgs> UploadFailed;
    }
}