using FSH.Framework.Application.Common;

namespace FSH.Framework.Application.Exporters;

public interface IExcelWriter : ITransientService
{
    Stream WriteToStream<T>(IList<T> data);
}