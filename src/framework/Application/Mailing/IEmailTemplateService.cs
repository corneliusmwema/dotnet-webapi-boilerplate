using FSH.Framework.Application.Common;

namespace FSH.Framework.Application.Mailing;

public interface IEmailTemplateService : ITransientService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}