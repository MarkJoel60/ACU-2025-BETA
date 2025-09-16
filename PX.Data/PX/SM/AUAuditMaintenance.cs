// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.Process;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class AUAuditMaintenance : PXGraph<AUAuditMaintenance>, ICaptionable
{
  private AuditMaintDataLoader auditDataLoader;
  public PXSave<AUAuditSetup> Save;
  public AUAuditMaintenance.PXCustomCancel Cancel;
  public AUAuditMaintenance.PXCustomCancel Refresh;
  public PXInsert<AUAuditSetup> Insert;
  public PXDelete<AUAuditSetup> Delete;
  public PXFirst<AUAuditSetup> First;
  public PXPrevious<AUAuditSetup> Prev;
  public PXNext<AUAuditSetup> Next;
  public PXLast<AUAuditSetup> Last;
  public PXSelect<AUAuditSetup> Audit;
  public PXSelect<AUAuditTable> Tables;
  public PXSelect<AUAuditField, Where<AUAuditField.fieldType, Equal<Current<AUAuditTable.showFieldsType>>>> Fields;

  public AUAuditMaintenance()
  {
    PXSiteMapNodeSelectorAttribute.SetRestriction<AUAuditSetup.screenID>(this.Audit.Cache, (object) null, (Func<SiteMap, bool>) (s => SiteMapRestrictionsTypes.IsReport(s) || PXSiteMap.IsGenericInquiry(s.Url)));
  }

  public IEnumerable tables()
  {
    AUAuditMaintenance auditGraph = this;
    if (auditGraph.Audit.Current != null && auditGraph.Audit.Current.ScreenID != null)
    {
      if (((IEnumerable<object>) auditGraph.Tables.Cache.Cached).Any<object>())
      {
        foreach (object obj in auditGraph.Tables.Cache.Cached)
          yield return obj;
      }
      else
      {
        bool isDirty = auditGraph.Tables.Cache.IsDirty;
        auditGraph.auditDataLoader = new AuditMaintDataLoader(auditGraph.Audit.Current.ScreenID, (PXGraph) auditGraph, auditGraph.Tables.Cache, auditGraph.Fields.Cache);
        auditGraph.auditDataLoader.LoadTablesAndFields(auditGraph.Audit.Current.ShowFieldsType);
        foreach (object obj in auditGraph.Tables.Cache.Cached)
          yield return obj;
        auditGraph.Tables.Cache.IsDirty = isDirty;
      }
    }
  }

  public IEnumerable fields()
  {
    if (this.Audit.Current != null && this.Audit.Current.ScreenID != null && this.Tables.Current != null)
    {
      bool filtering = (byte) this.Tables.Current.ShowFieldsType.Value > (byte) 0;
      foreach (AUAuditField auAuditField in this.Fields.Cache.Cached)
      {
        if (auAuditField.TableName == this.Tables.Current.TableName)
        {
          if (filtering)
          {
            int? fieldType = auAuditField.FieldType;
            int? showFieldsType = this.Tables.Current.ShowFieldsType;
            if (!(fieldType.GetValueOrDefault() == showFieldsType.GetValueOrDefault() & fieldType.HasValue == showFieldsType.HasValue))
              continue;
          }
          yield return (object) auAuditField;
        }
      }
    }
  }

  protected virtual void AUAuditSetup_ShowFieldsType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUAuditSetup row = (AUAuditSetup) e.Row;
    foreach (AUAuditTable newItem in this.Tables.Cache.Cached)
    {
      newItem.ShowFieldsType = row.ShowFieldsType;
      this.Tables.Cache.RaiseRowUpdated((object) newItem, (object) null);
    }
  }

  protected virtual void AUAuditSetup_ScreenID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is AUAuditSetup row))
      return;
    Exception error;
    ScreenUtils.ScreenInfo.TryGet(row.ScreenID, out error);
    if (error == null)
      return;
    this.Audit.Cache.RaiseExceptionHandling<AUAuditSetup.screenID>((object) row, (object) row.ScreenID, (Exception) new PXSetPropertyException<AUAuditSetup.screenID>(error, PXErrorLevel.Error, "This form cannot be automated.", Array.Empty<object>()));
  }

  protected virtual void AUAuditTable_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    AUAuditTable table = (AUAuditTable) e.Row;
    bool? isInserted = table.IsInserted;
    bool flag1 = true;
    if (isInserted.GetValueOrDefault() == flag1 & isInserted.HasValue)
      sender.SetStatus((object) table, PXEntryStatus.Inserted);
    else
      sender.SetStatus((object) table, PXEntryStatus.Updated);
    AUAuditField[] auAuditFieldArray = (AUAuditField[]) null;
    bool? nullable = new bool?();
    int? showFieldsType = table.ShowFieldsType;
    int num1 = 1;
    if (showFieldsType.GetValueOrDefault() == num1 & showFieldsType.HasValue)
    {
      auAuditFieldArray = this.Fields.Cache.Cached.Cast<AUAuditField>().Where<AUAuditField>((Func<AUAuditField, bool>) (field =>
      {
        if (field.TableName == table.TableName)
        {
          int? fieldType = field.FieldType;
          int num2 = 1;
          if (!(fieldType.GetValueOrDefault() == num2 & fieldType.HasValue))
          {
            bool? isActive = field.IsActive;
            bool flag2 = true;
            return isActive.GetValueOrDefault() == flag2 & isActive.HasValue;
          }
        }
        return false;
      })).ToArray<AUAuditField>();
      nullable = new bool?(false);
    }
    else
    {
      showFieldsType = table.ShowFieldsType;
      int num3 = 0;
      if (showFieldsType.GetValueOrDefault() == num3 & showFieldsType.HasValue)
      {
        auAuditFieldArray = this.Fields.Cache.Cached.Cast<AUAuditField>().Where<AUAuditField>((Func<AUAuditField, bool>) (field =>
        {
          if (field.TableName == table.TableName)
          {
            int? fieldType = field.FieldType;
            int num4 = 1;
            if (!(fieldType.GetValueOrDefault() == num4 & fieldType.HasValue))
            {
              bool? isActive = field.IsActive;
              bool flag3 = false;
              return isActive.GetValueOrDefault() == flag3 & isActive.HasValue;
            }
          }
          return false;
        })).ToArray<AUAuditField>();
        nullable = new bool?(true);
      }
    }
    if (auAuditFieldArray == null || auAuditFieldArray.Length == 0)
      return;
    foreach (AUAuditField row in auAuditFieldArray)
    {
      row.IsActive = nullable;
      this.Fields.Cache.RaiseFieldUpdated<AUAuditField.isActive>((object) row, (object) null);
    }
  }

  protected virtual void AUAuditField_IsActive_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUAuditField row = (AUAuditField) e.Row;
    bool? isInserted = row.IsInserted;
    bool flag1 = true;
    if (isInserted.GetValueOrDefault() == flag1 & isInserted.HasValue)
    {
      bool? isActive = row.IsActive;
      bool flag2 = false;
      if (isActive.GetValueOrDefault() == flag2 & isActive.HasValue)
      {
        sender.SetStatus((object) row, PXEntryStatus.Inserted);
      }
      else
      {
        isActive = row.IsActive;
        bool flag3 = true;
        if (!(isActive.GetValueOrDefault() == flag3 & isActive.HasValue))
          return;
        sender.SetStatus((object) row, PXEntryStatus.Held);
      }
    }
    else
    {
      bool? nullable = row.IsInserted;
      bool flag4 = false;
      if (!(nullable.GetValueOrDefault() == flag4 & nullable.HasValue))
        return;
      nullable = row.IsActive;
      bool flag5 = true;
      if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue))
        return;
      sender.SetStatus((object) row, PXEntryStatus.Deleted);
    }
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Screen Name")]
  protected virtual void AUAuditSetup_ScreenID_CacheAttached(PXCache sender)
  {
  }

  public override void Persist()
  {
    base.Persist();
    this.Tables.Cache.Clear();
    this.Fields.Cache.Clear();
    PXPageCacheUtils.InvalidateCachedPages();
  }

  public string Caption() => "";

  public class PXCustomCancel : PXCancel<AUAuditSetup>
  {
    public PXCustomCancel(PXGraph graph, string name)
      : base(graph, name)
    {
    }

    public PXCustomCancel(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
    [PXCancelButton]
    protected override IEnumerable Handler(PXAdapter adapter)
    {
      List<AUAuditSetup> auAuditSetupList = new List<AUAuditSetup>();
      foreach (AUAuditSetup auAuditSetup1 in base.Handler(adapter))
      {
        AUAuditSetup auAuditSetup2 = auAuditSetup1;
        if (!string.IsNullOrEmpty(auAuditSetup1.ScreenID))
        {
          string b = PXAuditHelper.GetAuditedScreenIDs(auAuditSetup1.ScreenID).FirstOrDefault<string>();
          if (!string.IsNullOrEmpty(b) && !string.Equals(auAuditSetup1.ScreenID, b, StringComparison.OrdinalIgnoreCase))
          {
            AUAuditSetup auAuditSetup3 = (AUAuditSetup) PXSelectBase<AUAuditSetup, PXSelect<AUAuditSetup, Where<AUAuditSetup.screenID, Equal<Required<AUAuditSetup.screenID>>>>.Config>.Select(this.Graph, (object) b);
            if (auAuditSetup3 != null)
              auAuditSetup2 = auAuditSetup3;
          }
        }
        auAuditSetupList.Add(auAuditSetup2);
      }
      return (IEnumerable) auAuditSetupList;
    }
  }
}
