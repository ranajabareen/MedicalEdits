using Newtonsoft.Json;
using System;

namespace ProviderApps.Core.Models
{
    /// <summary>
    /// Base class for all model classes, except Identity models{User,Role}
    /// </summary>
    public class BaseModel : IModel
    {
        /// <summary>
        /// Overrides <see cref="object.ToString()"/> to use <see cref="Newtonsoft.Json.JsonConvert.SerializeObject(object)"/> 
        /// </summary>
        /// <returns>Serialized json string</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class BaseModelInt : BaseModel, IIntIdModel
    {
        public int Id { get; set; }
    }

    public class BaseModelIntActive : BaseModelInt, IActiveModel
    {
        public bool Active { get; set; } = true;
    }

    public class BaseModelIntActiveFromTo : BaseModelIntActive, IActiveFromToModel
    {
        public DateTimeOffset ActiveFrom { get; set; } 
        public DateTimeOffset? ActiveTo { get; set; }
    }

    public class BaseModelIntActiveFromToDataList : BaseModelIntActiveFromTo
    {
        public int DataListId { get; set; }
        public int? SyncedListId { get; set; }
        public int? SyncedListRecordId { get; set; }
    }
 
    public interface IBaseModelIsDeleted
    {
        bool IsDeleted { get; set; }
    }
}
