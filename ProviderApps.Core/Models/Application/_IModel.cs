using System;

namespace ProviderApps.Core.Models
{
    public interface IModel
    {
         //int OrganizationId { get; set; }
        string ToString();
    }

    public interface IActiveModel : IModel
    {
        bool Active { get; set; }
    }

    public interface IActiveFromToModel : IModel
    {
        DateTimeOffset ActiveFrom { get; set; }
        DateTimeOffset? ActiveTo { get; set; }
    }

    public interface IIntIdModel : IModel
    {
        int Id { get; set; }
    }
}
