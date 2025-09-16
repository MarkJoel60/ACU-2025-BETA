// Decompiled with JetBrains decompiler
// Type: PX.SM.TableNameSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

internal class TableNameSelectorAttribute : PXCustomSelectorAttribute
{
  public TableNameSelectorAttribute()
    : base(typeof (AUAuditTable.tableName))
  {
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    foreach (PXResult<AUAuditTable> pxResult in PXSelectBase<AUAuditTable, PXSelect<AUAuditTable, Where<AUAuditTable.tableName, Equal<Required<AUAuditTable.tableName>>, And<AUAuditTable.isActive, Equal<PX.Data.True>, And<AUAuditTable.screenID, Equal<Current<AUAuditFilter.screenID>>>>>>.Config>.Select(this._Graph, e.NewValue))
    {
      AUAuditTable auAuditTable = (AUAuditTable) pxResult;
      if (auAuditTable.TableType != null && PXBuildManager.GetType(auAuditTable.TableType, false) == (System.Type) null)
        throw new PXSetPropertyException(PXMessages.LocalizeFormat("Published customizations do not contain the '{0}' type.", (object) auAuditTable.TableType));
    }
  }

  protected virtual IEnumerable GetRecords()
  {
    foreach (PXResult<AUAuditTable> pxResult in PXSelectBase<AUAuditTable, PXSelect<AUAuditTable, Where<AUAuditTable.screenID, Equal<Current<AUAuditFilter.screenID>>, And<AUAuditTable.isActive, Equal<PX.Data.True>>>>.Config>.Select(this._Graph))
    {
      AUAuditTable record = (AUAuditTable) pxResult;
      string str = record.TableName;
      if (record.TableType != null)
      {
        System.Type type = PXBuildManager.GetType(record.TableType, false);
        if (type != (System.Type) null)
        {
          object[] customAttributes = type.GetCustomAttributes(typeof (PXCacheNameAttribute), false);
          if (customAttributes.Length != 0)
            str = ((PXNameAttribute) customAttributes[0]).Name;
        }
      }
      record.TableDisplayName = str;
      yield return (object) record;
    }
  }
}
