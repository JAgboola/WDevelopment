using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.Configuration;
using IronPdf;
using System.IO;
using System.Web.Services.Description;
using static System.Net.WebRequestMethods;
using System.Net.Mime;
using System.Runtime.CompilerServices;

//for pdf writer remember to add extension above
namespace CIS325_Master_Project.Assignments
{
    public partial class ConsultingFees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void Certfication_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        protected void EmailClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EmailClient.SelectedValue == "Yes")
            {
                ClientName.Visible = true;
                ClientEmail.Visible = true;
            }

        }

        protected void Calculate_Click(object sender, EventArgs e)
        {
            const double FULL_TIME = 40;

            string name = CustomerName.Text;
            string title = JobTitle.Text;
            string mscdCert = Certification.Text;
            string email = ClientEmail.Text;
            string output = DisplayMsg.Text;
            double hours = Convert.ToDouble(HoursWorked.Text);
            double hourlyRate = 0;
            double certBonus = 0;
            double otCal = 0;
            int numberOfSkills = 0;
            string skillsCountList = "";

            switch (JobTitle.SelectedValue)
            {
                case "Developer": 
                    hourlyRate = 100;
                    break;
                case "Analyst":
                    hourlyRate = 120;
                    break;
                case "Architect":
                    hourlyRate = 150;
                    break;
                case "Project Lead":
                    hourlyRate = 200;
                    break;
                default:
                    break;

            }

            //MCSD Certification Bonus

            if (Certification.SelectedValue == "Yes")
            {
                certBonus = 1.2;
            }


            foreach (ListItem skillsChoiceItem in SkillsList.Items) //instantiation of item object 
            {
                if (skillsChoiceItem.Selected)
                {
                    numberOfSkills++;
                    skillsCountList += skillsChoiceItem.Text + ", ";  //SubString function
                }
            }
    
            double payoutAmount = PayoutBrain(hours, hourlyRate, certBonus, hours, hourlyRate);

            //double overTimeCal = OvertimeBrain(hours, hourlyRate);

            //Hide web form
            ConsultPanel.Visible = false;

            //Certification
            if (Certification.SelectedValue == "Yes")
            {
                mscdCert = "Certified";
            }
            else
            {
                mscdCert = "Non-Certified";
            }

            // optional sentence for overtime pay
            if (hours > FULL_TIME)
            {
                otCal = hours - FULL_TIME;

                DisplayMsg.Text = "Thank you, " + name + " for using the Consulting Fees Calculator!";
                DisplayMsg.Text += "<p> Based on your selected profile – <strong>" + title + ", </strong> your MCSD certificate status (" + mscdCert +
                ") and " + hours + " work hours, your weekly consulting fees are - </p> " + payoutAmount.ToString("C");
                DisplayMsg.Text += "<p>The technical skills you have applied this week include: " + skillsCountList;
                DisplayMsg.Text += "<p>Your overtime hours this week are " + otCal + " hours, and your overtime consulting fees are </p>" +
                overTimeCal.ToString("C");
            }
            else
            {
                DisplayMsg.Text = "Thank you, " + name + " for using the Consulting Fees Calculator!";
                DisplayMsg.Text += "<p>Based on your selected profile – <strong> " + title + ", </strong> your MCSD certificate status (" + mscdCert +
                ") and " + hours + " work hours, your weekly consulting fees are - " + payoutAmount.ToString("C");
                DisplayMsg.Text += "<p>The technical skills you have applied this week include: " + skillsCountList;
            }

            //Send email notifications to user
            if (EmailClient.SelectedValue == "Yes")
            {
              SendCustomerEmail(ClientEmail.Text, ClientName.Text, mscdCert, skillsCountList, hours, payoutAmount);

            }
        }

        public double PayoutBrain(double weeklyHours,
                         double payoutRate,
                         double payoutBonus, 
                         double totalHours, 
                         double otPayRate)

        {
            //40 hour work week
            const double FULL_TIME = 40;
            const double OT_RATE = 1.5;
            double weeklyPay = 0;
            double overTime = 0;

            if (payoutBonus > 0)
            {
               payoutRate = payoutRate * payoutBonus;

            }

            if (weeklyHours <= FULL_TIME)
            {
                weeklyPay = weeklyHours * payoutRate;
            }
            else
            {
                weeklyPay = payoutRate * FULL_TIME;
                weeklyPay = (weeklyHours - FULL_TIME) * payoutRate * OT_RATE + weeklyPay;
            }

            if (totalHours > FULL_TIME)
            {
                overTime = (totalHours - FULL_TIME) * otPayRate * OT_RATE;
            }

            return weeklyPay;

        }

        //public double OvertimeBrain(double totalHours, double otPayRate)
        //{
        //    const int FULL_TIME = 40;
        //    const double OT_RATE = 1.5;
        //    double overTime = 0;

        //    if (totalHours > FULL_TIME)
        //    {
        //        overTime = (totalHours - FULL_TIME) * otPayRate * OT_RATE;
        //    }

        //    return overTime;
        //}

        protected void SendCustomerEmail(string sendCustomerEmail, string sendCustomerName, string sendCert, string sendSkills, double sendHours, double sendWage, double sendOT)
        {
            //string file = "C:\\temp\\Invoice.Pdf";
            const double FULL_TIME = 40;
            double otHours = 0;
            
            string sendFromEmail = "Agboola278@flagler.edu";
            string sendFromName = "Consulting Calculator";

            string sendToEmail = ClientEmail.Text;
            string sendToName = ClientName.Text;

            string messageSubject = "Your Weekly Wage";
            string messageBody = "Here's the weekly form prepared for: ";
            messageBody += "<strong>" + sendToName + "<strong>";

            // OT calculation
            if (sendHours > FULL_TIME)
            {
                otHours = sendHours - FULL_TIME;
            }
            else
            {
                otHours = 0;
            }


            // This is standard procedure for object instantiation
            MailAddress from = new MailAddress(sendFromEmail, sendFromName);
            MailAddress to = new MailAddress(sendToEmail, sendToName);
            MailMessage emailMessage = new MailMessage(from, to);

            emailMessage.Subject = messageSubject;
            emailMessage.Body = messageBody;
            emailMessage.IsBodyHtml = true;

            //PdfSharpConvert
            //pdf writing
            //source: procodeguide.com

            var ChromePdfRenderer = new ChromePdfRenderer();  // new instance of ChromePdfRenderer
            var html = "Dear " + sendToName;
            html += "<p> Thanks so much for using our consulting service. The following is a billing summary for the week ending on </p>";
            html += "<p> Consultant Name: </p>" + sendToName;
            html += "<p> MCSD Certificate: </p>" + sendCert;
            html += "<p> Billing Hours: </p>" + sendHours;
            html += "<p> Technical Skills Applied: </p>" + sendSkills;
            html += "<p> Total Consulting Fees: </p>" + sendWage.ToString("C");
            html += "<p> Overtime: </p>" + otHours;
            html += "<p> Overtime Charge: </p>" + sendOT.ToString("C");

            DisplayMsg.Text = "Thank you, " + sendToName + "! Your email has been sent!";

            // turn html to pdf
            var pdf = ChromePdfRenderer.RenderHtmlAsPdf(html);

            // save resulting pdf into file
            // pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "ChromePdfRenderer.Pdf"));
            pdf.SaveAs("C:\\temp\\Invoice.Pdf");
            IronPdf.Installation.TempFolderPath = Server.MapPath(@"/tmp");

            // Using email client/server to send out emails. Watch out for run time errors

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("jagboola26@gmail.com", "krsbemckbummnwwb");
            client.Port = int.Parse("587");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicauthenticationinfo;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //attachment - add one here using the attachment property.
            // source: StackedOverflow
            emailMessage.Attachments.Add(new Attachment(@"C:\temp\Invoice.Pdf"));
            client.Send(emailMessage);
        }

    }
}