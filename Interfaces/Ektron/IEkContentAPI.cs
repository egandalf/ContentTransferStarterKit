using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Ektron
{
    public interface IEkContentAPI
    {
        T GetContent<T>(long ContentId, int LanguageId);
        T GetContentList<T>(long SourceId, Common.Enumeration.ContentSourceType SourceType, int LanguageId);
    }
}
