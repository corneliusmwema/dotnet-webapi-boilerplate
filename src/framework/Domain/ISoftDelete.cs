namespace FSH.Framework.Domain;

public interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
    DefaultIdType? DeletedBy { get; set; }
}