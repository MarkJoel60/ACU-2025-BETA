// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBForeignIdentityAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBForeignIdentityAttribute : PXDBIdentityAttribute, IPXRowPersistingSubscriber
{
  private System.Type _ForeignIdentity;
  /// <summary>
  /// Represents the field of the foreign table that holds the name of the DAC,
  /// which uses that table as a source of its identity.
  /// Such field of the identity providing table should be marked with the <see cref="T:PX.Data.PXDBForeignIdentityTypeAttribute" />.
  /// </summary>
  private string _typeDiscriminatorField;

  public PXDBForeignIdentityAttribute(System.Type ForeignIdentity)
  {
    this._ForeignIdentity = ForeignIdentity;
  }

  public override void CacheAttached(PXCache sender)
  {
    sender._RowId = this._FieldName;
    this._typeDiscriminatorField = sender.Graph.Caches[this._ForeignIdentity].GetAttributesReadonly((string) null).OfType<PXDBForeignIdentityTypeAttribute>().Select<PXDBForeignIdentityTypeAttribute, string>((Func<PXDBForeignIdentityTypeAttribute, string>) (attr => attr.FieldName)).FirstOrDefault<string>();
    base.CacheAttached(sender);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert)
      return;
    try
    {
      if (this._typeDiscriminatorField == null)
        PXDatabase.Insert(this._ForeignIdentity, PXDataFieldAssign.OperationSwitchAllowed);
      else
        PXDatabase.Insert(this._ForeignIdentity, this.GetTypeAssign(), PXDataFieldAssign.OperationSwitchAllowed);
    }
    catch (PXDbOperationSwitchRequiredException ex)
    {
      if (this._typeDiscriminatorField == null)
        PXDatabase.Update(this._ForeignIdentity);
      else
        PXDatabase.Update(this._ForeignIdentity, (PXDataFieldParam) this.GetTypeAssign());
    }
    this._KeyToAbort = (int?) sender.GetValue(e.Row, this._FieldOrdinal);
    Decimal? identity;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) Convert.ToInt32((object) (identity = PXDatabase.SelectIdentity())));
    PXTransactionScope.SetIdentityAudit(this._BqlTable.Name, this._FieldName, identity);
  }

  protected virtual PXDataFieldAssign GetTypeAssign()
  {
    return new PXDataFieldAssign(this._typeDiscriminatorField, PXDbType.NVarChar, (object) this._BqlTable.Name.ToString());
  }

  public override object GetLastInsertedIdentity(object valueFromCache) => valueFromCache;

  protected override void assignIdentityValue(PXCache sender, PXRowPersistedEventArgs e)
  {
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this.PrepareFieldName(this._DatabaseFieldName, e);
    e.DataType = PXDbType.Int;
    e.DataValue = e.Value;
    e.DataLength = new int?(4);
    e.IsRestriction = e.IsRestriction || this._IsKey;
  }
}
