using System;
using codeRR.Client.Contracts;
using codeRR.Client.Uploaders;

namespace codeRR.Client.AspNet.Mvc5.Demo
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