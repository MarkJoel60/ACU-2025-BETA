// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.Handlers.RecordInfoProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes.Handlers;

internal class RecordInfoProvider
{
  private readonly EntityHelper _entityHelper;

  public RecordInfoProvider(PXCache cache) => this._entityHelper = new EntityHelper(cache.Graph);

  public string GetParentInfo(Reference reference)
  {
    return EntityHelper.GetFriendlyEntityName(reference.Parent.Table);
  }

  public string GetRecordInfo(object entity)
  {
    string entityFriendlyName = RecordInfoProvider.GetEntityFriendlyName(entity);
    RecordInfoProvider.GetEntityClassFullName(entity);
    string entityRowId = this.GetEntityRowID(entity);
    string str = !string.IsNullOrEmpty(entityRowId) ? $" ({entityRowId})" : string.Empty;
    return entityFriendlyName + str;
  }

  private static string GetEntityFriendlyName(object entity)
  {
    return EntityHelper.GetFriendlyEntityName(entity.GetType(), entity);
  }

  private string GetEntityRowID(object entity)
  {
    return this._entityHelper.GetEntityRowID(entity.GetType(), entity, ", ");
  }

  private static string GetEntityClassFullName(object entity) => entity.GetType().FullName;

  public string BehaviorToReason(ReferenceBehavior referenceBehavior)
  {
    switch (referenceBehavior)
    {
      case ReferenceBehavior.NoAction:
      case ReferenceBehavior.Cascade:
        return "Can exist without parent-row";
      case ReferenceBehavior.Restrict:
        return "Cannot exist without parent-row";
      case ReferenceBehavior.SetNull:
        return "Cannot exist without parent-row, because not all components of foreign key can be set to NULL";
      case ReferenceBehavior.SetDefault:
        return "Cannot exist without parent-row, because not all components of foreign key has DEFAULT value";
      default:
        throw new ArgumentOutOfRangeException(nameof (referenceBehavior), (object) referenceBehavior, (string) null);
    }
  }

  public class Messages
  {
    public const string NoAction = "Can exist without parent-row";
    public const string Restrict = "Cannot exist without parent-row";
    public const string SetNull = "Cannot exist without parent-row, because not all components of foreign key can be set to NULL";
    public const string SetDefault = "Cannot exist without parent-row, because not all components of foreign key has DEFAULT value";
  }
}
