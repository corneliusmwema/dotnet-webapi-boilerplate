using FSH.Framework.Application.Common;

namespace FSH.Framework.Application.Mailing;

public interface IMailService : ITransientService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}