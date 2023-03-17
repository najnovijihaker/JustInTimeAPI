using Application.Account.Dtos.Request;
using Application.Account.Dtos.Response;
using Application.Account.Queries;
using Application.Common;
using Application.Project.Dtos;
using Domain.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Application.Account.Commands
{
    public class SendReportCommand : SendReportRequestDto, IRequest<ResponseDto>
    {
    }

    public class SendReportCommandHandler : IRequestHandler<SendReportCommand, ResponseDto>
    {
        private readonly IDataContext dataContext;

        public SendReportCommandHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseDto> Handle(SendReportCommand request, CancellationToken cancellationToken)
        {
            var sender = await dataContext.Accounts.FirstOrDefaultAsync(s => s.Id == request.SenderId, cancellationToken);
            var reciever = await dataContext.Accounts.FirstOrDefaultAsync(r => r.Id == request.RecieverId, cancellationToken);

            if (sender == null || reciever == null)
            {
                return new ResponseDto("Invalid account data");
            }

            if (reciever.role.ToLower() != "administrator")
            {
                return new ResponseDto("Unable to send to non-admin user");
            }

            var monthlyHours = GetMonthlyHours(sender.Id);

            var accountProjects = await dataContext.AccountProjects.Where(x => x.AccountId == sender.Id).ToListAsync(cancellationToken);

            if (accountProjects == null || accountProjects.Count == 0)
            {
                return new ResponseDto("Account has no projects");
            }

            // generating PDF report
            var fileName = $"{sender.LastName}_{sender.FirstName}_{DateTime.Now.ToString("MMM")}_{DateTime.Now.ToString("yyyy")}.pdf";
            var writer = new PdfWriter(fileName);
            var report = new PdfDocument(writer);

            var doc = new Document(report);

            // adding content
            doc.Add(new Paragraph($"{sender.LastName}, {sender.FirstName} - {DateTime.Now.ToString("MMMM")}, {DateTime.Now.ToString("yyyy")}")
                .SetFontSize(30));
            doc.Add(new Paragraph($"Total H: {monthlyHours}h")).SetFontSize(20).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetBold();
            doc.Add(new Paragraph("Hours per project").SetFontSize(14));

            foreach (var project in accountProjects)
            {
                doc.Add(new Paragraph($"Worked Xh on {project.ProjectId}"));
            }

            // closing the document to prevent memory leak
            doc.Close();

            byte[] pdfBytes = File.ReadAllBytes(fileName);

            Emailer emailer = new Emailer();
            emailer.sendMontlyReport(reciever, sender, pdfBytes);

            // Delete the file
            File.Delete(fileName);

            return new ResponseDto("Successful");
        }

        public double GetMonthlyHours(int accountId)
        {
            var startOfMonth = GetStartOfMonth();
            var endOfMonth = GetEndOfMonth();

            var result = GetHoursWorked(startOfMonth, endOfMonth, accountId);

            return result;
        }

        private DateTime GetStartOfMonth()
        {
            // Get the current date and time
            var currentDate = DateTime.Now;

            // Calculate the start of the month by setting the day to 1
            return new DateTime(currentDate.Year, currentDate.Month, 1);
        }

        private DateTime GetEndOfMonth()
        {
            // Get the current date and time
            var currentDate = DateTime.Now;

            // Calculate the end of the month by setting the day to the last day of the month
            return new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
        }

        private double GetHoursWorked(DateTime startDate, DateTime endDate, int accountId)
        {
            var timeEntries = dataContext.TimeKeep.Where(x => x.Time >= startDate && x.Time < endDate && x.AccountId == accountId);
            var result = timeEntries.Sum(x => x.HoursWorked);

            return result;
        }
    }
}