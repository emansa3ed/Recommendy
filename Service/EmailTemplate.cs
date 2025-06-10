using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal static class EmailTemplates
    {
        internal static class Organization
        {
            public static (string subject, string htmlBody) GetVerificationTemplate(bool isApproved, string notes)
            {
                return (
                    subject: isApproved ? "Your Organization Has Been Verified" : "Organization Verification Update",
                    htmlBody: isApproved
                        ? $@"<h2>Congratulations!</h2>
                       <p>Your organization has been verified by our admin team.</p>
                       <p>You can now post opportunities on our platform.</p>
                       {(string.IsNullOrEmpty(notes) ? "" : $"<p><strong>Admin Notes:</strong> {notes}</p>")}
                       <p>Thank you for joining Recommendy!</p>"
                        : $@"<h2>Verification Update</h2>
                       <p>We're sorry, but your organization could not be verified at this time.</p>
                       <p><strong>Reason:</strong> {notes}</p>
                       <p>If you believe this is an error, please contact our support team.</p>"
                );
            }
        }

        internal static class Account
        {
            public static (string subject, string htmlBody) GetBanTemplate(string reason)
            {
                return (
                    subject: "Account Suspended",
                    htmlBody: $@"<h2>Account Suspended</h2>
                           <p>Your Recommendy account has been suspended.</p>
                           <p><strong>Reason:</strong> {reason}</p>
                           <p>If you believe this is a mistake, please contact our support team.</p>"
                );
            }

            public static (string subject, string htmlBody) GetUnbanTemplate()
            {
                return (
                    subject: "Account Reinstated",
                    htmlBody: @"<h2>Account Reinstated</h2>
                          <p>Your account has been reinstated. You can now resume using Recommendy.</p>
                          <p>Thank you for your cooperation.</p>"
                );
            }
        }
    }
}
