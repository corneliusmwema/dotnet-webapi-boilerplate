namespace FSH.Framework.Domain.Entities;

public interface IAuditableEntity
{
    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}