// Decompiled with JetBrains decompiler
// Type: PX.CS.RMUnitSetMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

#nullable enable
namespace PX.CS;

[Serializable]
public class RMUnitSetMaint : PXGraph<
#nullable disable
RMUnitSetMaint, RMUnitSet>
{
  private const string ROOT_UNIT_CODE = "Root";
  private const string ROOT_UNIT_SET_CODE = "Root";
  private const string NEW_KEY = "<NEW>";
  public PXSelect<RMUnitSet> UnitSet;
  public PXSelect<RMUnit, Where<RMUnit.parentCode, Equal<Argument<string>>>> Items;
  public PXSelect<RMUnit, Where<RMUnit.parentCode, Equal<Argument<string>>>> ItemsWORoot;
  public PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>>> Units;
  public PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.unitCode, Equal<Current<RMUnit.unitCode>>>>> CurrentUnit;
  public PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Current<RMUnit.dataSourceID>>>> CurrentDataSource;
  public PXSelectJoin<RMReport, LeftJoin<RMUnitSet, On<RMReport.unitSetCode, Equal<RMUnitSet.unitSetCode>>>, Where<RMUnitSet.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>>> Reports;
  public PXFilter<RMUnitSetMaint.ParamFilter> Parameter;
  public PXFilter<RMUnitSetMaint.RMNewUnitSetPanel> NewUnitSetPanel;
  public PXAction<RMUnitSet> copyUnitSet;
  public PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Argument<string>>>>> Subitems;
  public PXAction<RMUnitSet> Left;
  public PXAction<RMUnitSet> Right;
  public PXAction<RMUnitSet> Up;
  public PXAction<RMUnitSet> Down;
  public PXAction<RMUnitSet> InsertUnit;
  public PXAction<RMUnitSet> DeleteUnit;
  public PXAction<RMUnitSet> ChangeUnitCode;
  public PXAction<RMUnitSet> SelectUnit;
  private bool inFieldUpdating;

  [InjectDependency]
  private ISiteMapUITypeProvider SiteMapUITypeProvider { get; set; }

  private RMUnit Root
  {
    get
    {
      return new RMUnit()
      {
        UnitSetCode = nameof (Root),
        UnitCode = nameof (Root)
      };
    }
  }

  protected virtual void unitset(ref string currentCode)
  {
  }

  public virtual IEnumerable items(string UnitCode)
  {
    if (UnitCode == null && this.UnitSet.Current != null && !string.IsNullOrEmpty(this.UnitSet.Current.UnitSetCode))
      return (IEnumerable) new PXResultset<RMUnit>()
      {
        new PXResult<RMUnit>(this.Root)
      };
    if (string.Equals(UnitCode, "Root", StringComparison.OrdinalIgnoreCase))
      return (IEnumerable) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, PX.Data.IsNull>>>.Config>.Select((PXGraph) this);
    return (IEnumerable) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>>>>.Config>.Select((PXGraph) this, (object) UnitCode);
  }

  public virtual IEnumerable itemsWORoot(string UnitCode)
  {
    IEnumerable enumerable;
    if (UnitCode == null && this.UnitSet.Current != null && !string.IsNullOrEmpty(this.UnitSet.Current.UnitSetCode))
      enumerable = (IEnumerable) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, PX.Data.IsNull>>>.Config>.Select((PXGraph) this);
    else
      enumerable = (IEnumerable) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>>>>.Config>.Select((PXGraph) this, (object) UnitCode);
    return enumerable;
  }

  [PXInsertButton(Tooltip = "Add New Record (Ctrl+Ins)")]
  [PXUIField(DisplayName = "Add New Record (Ctrl+Ins)")]
  protected virtual IEnumerable Insert(PXAdapter adapter)
  {
    if (this.IsNewUi())
    {
      if (this.NewUnitSetPanel.AskExt() != WebDialogResult.OK)
        return adapter.Get();
      RMMaintHelper.CheckKeyAndDescription<RMUnitSet, RMUnitSet.unitSetCode, RMUnitSetMaint.RMNewUnitSetPanel.unitSetCode, RMUnitSetMaint.RMNewUnitSetPanel.description>((PXGraph) this, this.NewUnitSetPanel.Cache, (object) this.NewUnitSetPanel.Current);
      RMUnitSetMaint instance = PXGraph.CreateInstance<RMUnitSetMaint>();
      RMUnitSet rmUnitSet = instance.UnitSet.Insert();
      rmUnitSet.UnitSetCode = this.NewUnitSetPanel.Current.UnitSetCode;
      rmUnitSet.Type = this.NewUnitSetPanel.Current.Type;
      rmUnitSet.Description = this.NewUnitSetPanel.Current.Description;
      instance.UnitSet.Current = rmUnitSet;
      instance.UnitSet.Cache.Update((object) rmUnitSet);
      instance.UnitSet.Cache.IsDirty = false;
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.InlineWindow);
    }
    else
      PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<RMUnitSetMaint>(), PXRedirectHelper.WindowMode.InlineWindow);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Copy Unit Set", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXButton(ConfirmationMessage = "Any unsaved changes will be discarded.", ConfirmationType = PXConfirmationType.IfDirty)]
  protected virtual IEnumerable CopyUnitSet(PXAdapter adapter)
  {
    RMUnitSetMaint graph = this;
    if (graph.Parameter.View.Answer == WebDialogResult.None)
      graph.Parameter.Current.Description = graph.UnitSet.Current.Description;
    if (graph.UnitSet.Current != null && graph.Parameter.AskExt() == WebDialogResult.OK)
    {
      RMMaintHelper.CheckKeyAndDescription<RMUnitSet, RMUnitSet.unitSetCode, RMUnitSetMaint.ParamFilter.newUnitSetCode, RMUnitSetMaint.ParamFilter.description>((PXGraph) graph, graph.Parameter.Cache, (object) graph.Parameter.Current);
      string newUnitSetCode = graph.Parameter.Current.NewUnitSetCode;
      string description = graph.Parameter.Current.Description;
      foreach (RMUnitSet rmUnitSet in graph.Cancel.Press(adapter))
        ;
      List<PXResult<RMUnit>> list = graph.Units.Select().ToList<PXResult<RMUnit>>();
      RMUnitSet copy1 = (RMUnitSet) graph.UnitSet.Cache.CreateCopy((object) graph.UnitSet.Current);
      copy1.UnitSetCode = newUnitSetCode;
      if (!string.IsNullOrEmpty(description))
        copy1.Description = description;
      copy1.NoteID = new Guid?();
      RMUnitSet rmUnitSet1 = graph.UnitSet.Insert(copy1);
      if (rmUnitSet1 != null)
      {
        PXSelectBase<RMDataSource> pxSelectBase = (PXSelectBase<RMDataSource>) new PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMDataSource.dataSourceID>>>>((PXGraph) graph);
        foreach (PXResult<RMUnit> pxResult in list)
        {
          RMUnit rmUnit1 = (RMUnit) pxResult;
          rmUnit1.UnitSetCode = (string) null;
          rmUnit1.NoteID = new Guid?();
          RMUnit rmUnit2 = graph.Units.Insert(rmUnit1);
          if (rmUnit2 != null)
          {
            RMDataSource copy2 = (RMDataSource) pxSelectBase.Select((object) rmUnit1.DataSourceID);
            if (copy2 != null && pxSelectBase.Current != null)
            {
              pxSelectBase.Cache.RestoreCopy((object) pxSelectBase.Current, (object) copy2);
              pxSelectBase.Current.DataSourceID = rmUnit2.DataSourceID;
            }
          }
        }
        yield return (object) rmUnitSet1;
        yield break;
      }
    }
    foreach (RMUnitSet rmUnitSet in adapter.Get())
      yield return (object) rmUnitSet;
  }

  public virtual IEnumerable subitems([PXString] string parentCode)
  {
    if ((this.IsExport || this.IsImport) && parentCode == null && this.Items.Current != null && !PXGraph.ProxyIsActive)
      parentCode = this.Items.Current.UnitCode;
    this.CurrentSelected.ParentCode = parentCode;
    if (parentCode == null || string.Equals(parentCode, "Root", StringComparison.OrdinalIgnoreCase))
      return (IEnumerable) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, PX.Data.IsNull>>>.Config>.Select((PXGraph) this);
    return (IEnumerable) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>>>>.Config>.Select((PXGraph) this, (object) parentCode);
  }

  protected virtual RMUnit GetCurrentTreeItem(PXAdapter adapter)
  {
    return adapter.Parameters == null || adapter.Parameters.Length == 0 || adapter.Parameters[0] == null ? this.CurrentUnit.Current : (RMUnit) this.Units.Search<RMUnit.unitCode>(adapter.Parameters[0]);
  }

  [PXButton(Tooltip = "Move to external node.", ImageSet = "main", ImageKey = "ArrowLeft")]
  [PXUIField(DisplayName = " ")]
  protected virtual IEnumerable left(PXAdapter adapter)
  {
    RMUnit currentTreeItem = this.GetCurrentTreeItem(adapter);
    if (currentTreeItem == null || currentTreeItem.ParentCode == null)
      return adapter.Get();
    RMUnit rmUnit = (RMUnit) this.Units.Search<RMUnit.unitCode>((object) currentTreeItem.ParentCode);
    if (rmUnit != null)
    {
      currentTreeItem.ParentCode = rmUnit.ParentCode;
      this.Units.Update(currentTreeItem);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Move to internal node.", ImageSet = "main", ImageKey = "ArrowRight")]
  [PXUIField(DisplayName = " ")]
  protected virtual IEnumerable right(PXAdapter adapter)
  {
    RMUnit currentTreeItem = this.GetCurrentTreeItem(adapter);
    if (currentTreeItem == null)
      return adapter.Get();
    RMUnit rmUnit1 = (RMUnit) null;
    foreach (PXResult<RMUnit> pxResult in this.Units.Select())
    {
      RMUnit rmUnit2 = (RMUnit) pxResult;
      if (rmUnit2.ParentCode == currentTreeItem.ParentCode && rmUnit2.UnitCode != currentTreeItem.UnitCode)
      {
        rmUnit1 = rmUnit2;
        if (string.Compare(rmUnit1.UnitCode, currentTreeItem.UnitCode, StringComparison.OrdinalIgnoreCase) > 0)
          break;
      }
    }
    if (rmUnit1 != null)
    {
      currentTreeItem.ParentCode = rmUnit1.UnitCode;
      this.Units.Update(currentTreeItem);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Move Up")]
  [PXUIField(DisplayName = " ", Visible = false)]
  protected virtual IEnumerable up(PXAdapter adapter)
  {
    if (adapter.Parameters != null && adapter.Parameters.Length != 0 && adapter.Parameters[0] != null)
    {
      RMUnit rmUnit1 = (RMUnit) this.Units.Search<RMUnit.unitCode>(adapter.Parameters[0]);
      if (rmUnit1 != null && rmUnit1.ParentCode != null)
      {
        RMUnit rmUnit2 = (RMUnit) this.Units.Search<RMUnit.unitCode>((object) rmUnit1.ParentCode);
        if (rmUnit2 != null)
        {
          RMUnit rmUnit3;
          if (rmUnit2.ParentCode != null)
            rmUnit3 = (RMUnit) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>, And<RMUnit.unitCode, Less<Required<RMUnit.unitCode>>>>>, OrderBy<Desc<RMUnit.unitCode>>>.Config>.Select((PXGraph) this, (object) rmUnit2.ParentCode, (object) rmUnit2.UnitCode);
          else
            rmUnit3 = (RMUnit) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, PX.Data.IsNull, And<RMUnit.unitCode, Less<Required<RMUnit.unitCode>>>>>, OrderBy<Desc<RMUnit.unitCode>>>.Config>.Select((PXGraph) this, (object) rmUnit2.UnitCode);
          if (rmUnit3 != null)
          {
            rmUnit1.ParentCode = rmUnit3.UnitCode;
            this.Units.Update(rmUnit1);
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Move Down")]
  [PXUIField(DisplayName = " ", Visible = false)]
  protected virtual IEnumerable down(PXAdapter adapter)
  {
    if (adapter.Parameters != null && adapter.Parameters.Length != 0 && adapter.Parameters[0] != null)
    {
      RMUnit rmUnit1 = (RMUnit) this.Units.Search<RMUnit.unitCode>(adapter.Parameters[0]);
      if (rmUnit1 != null && rmUnit1.ParentCode != null)
      {
        RMUnit rmUnit2 = (RMUnit) this.Units.Search<RMUnit.unitCode>((object) rmUnit1.ParentCode);
        if (rmUnit2 != null)
        {
          RMUnit rmUnit3;
          if (rmUnit2.ParentCode != null)
            rmUnit3 = (RMUnit) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.parentCode>>, And<RMUnit.unitCode, Greater<Required<RMUnit.unitCode>>>>>>.Config>.Select((PXGraph) this, (object) rmUnit2.ParentCode, (object) rmUnit2.UnitCode);
          else
            rmUnit3 = (RMUnit) PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, PX.Data.IsNull, And<RMUnit.unitCode, Greater<Required<RMUnit.unitCode>>>>>>.Config>.Select((PXGraph) this, (object) rmUnit2.UnitCode);
          if (rmUnit3 != null)
          {
            rmUnit1.ParentCode = rmUnit3.UnitCode;
            this.Units.Update(rmUnit1);
          }
        }
      }
    }
    return adapter.Get();
  }

  private bool IsNewUi()
  {
    return string.Equals(this.SiteMapUITypeProvider.GetUIByScreenId(PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(typeof (RMUnitSetMaint).FullName).ScreenID), "T", StringComparison.InvariantCultureIgnoreCase);
  }

  [PXButton(ImageKey = "AddNew", Tooltip = "Insert")]
  [PXUIField(DisplayName = "New Row (Ctrl+Ins)", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  protected virtual IEnumerable insertUnit(PXAdapter adapter)
  {
    string unitSetCode = this.UnitSet.Current.UnitSetCode;
    RMUnit currentTreeItem = this.GetCurrentTreeItem(adapter);
    RMUnit rmUnit = this.Units.Insert(new RMUnit()
    {
      UnitSetCode = unitSetCode,
      ParentCode = currentTreeItem?.UnitCode != "Root" ? currentTreeItem?.UnitCode : (string) null,
      NoteID = new Guid?(),
      Description = "<NEW>",
      UnitCode = this.GetNextKeyWithPrefix("NEW", unitSetCode)
    });
    this.Units.Cache.ActiveRow = (IBqlTable) rmUnit;
    this.Units.Current = rmUnit;
    this.Units.View.RequestRefresh();
    this.CurrentUnit.View.RequestRefresh();
    return adapter.Get();
  }

  private string GetNextKeyWithPrefix(string prefix, string unitSetCode)
  {
    this.Units.View.SelectMultiBound((object[]) null, (object[]) null);
    int num = 0;
    string nextKeyWithPrefix;
    while (true)
    {
      string str = num == 0 ? "" : num.ToString();
      nextKeyWithPrefix = prefix + str;
      if (this.Units.Cache.IsPresent((object) new RMUnit()
      {
        UnitCode = nextKeyWithPrefix,
        UnitSetCode = unitSetCode
      }))
        ++num;
      else
        break;
    }
    return nextKeyWithPrefix;
  }

  [PXButton(ImageKey = "RecordDel", Tooltip = "Delete")]
  [PXUIField(DisplayName = "Delete Row", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, Visible = false)]
  protected virtual IEnumerable deleteUnit(PXAdapter adapter)
  {
    RMUnit currentTreeItem = this.GetCurrentTreeItem(adapter);
    if (currentTreeItem == null)
      return adapter.Get();
    this.Units.Delete(currentTreeItem);
    this.Units.View.RequestRefresh();
    return adapter.Get();
  }

  [PXButton(ConfirmationMessage = "")]
  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, Visible = false)]
  protected virtual IEnumerable changeUnitCode(PXAdapter adapter) => adapter.Get();

  [PXButton(ConfirmationMessage = "")]
  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  protected virtual IEnumerable selectUnit(PXAdapter adapter)
  {
    this.CurrentUnit.Current = (RMUnit) this.Units.Search<RMUnit.unitCode>((object) JsonNode.Parse(adapter.CommandArguments, new JsonNodeOptions?(), new JsonDocumentOptions())["id"].ToString());
    return adapter.Get();
  }

  protected virtual void _(Events.RowDeleted<RMUnit> e)
  {
    RMUnit row = e.Row;
    foreach (PXResult<RMUnit> pxResult in PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.unitCode>>>>>.Config>.Select((PXGraph) this, (object) row.UnitCode))
    {
      RMUnit rmUnit = (RMUnit) pxResult;
      rmUnit.ParentCode = row.ParentCode;
      this.Units.Update(rmUnit);
    }
  }

  protected virtual void _(Events.RowPersisting<RMUnit> e)
  {
    if (string.Equals(e.Row.UnitCode, "Root", StringComparison.OrdinalIgnoreCase))
      e.Cancel = true;
    if (!string.Equals(e.Row.ParentCode, "Root", StringComparison.OrdinalIgnoreCase))
      return;
    e.Row.ParentCode = (string) null;
  }

  protected virtual void _(Events.FieldDefaulting<RMUnit.parentCode> e)
  {
    if (!(e.Row is RMUnit))
      return;
    e.NewValue = (object) (this.CurrentSelected.ParentCode ?? (string) null);
    e.Cancel = true;
  }

  protected virtual void _(Events.FieldVerifying<RMUnit.unitCode> e)
  {
    if ((e.Row is RMUnit row ? row.UnitCode : (string) null) != null && this.Units.Search<RMUnit.unitCode>(e.NewValue).FirstTableItems.FirstOrDefault<RMUnit>() != null)
      throw new PXSetPropertyException("The value of this field must be unique among all records.");
  }

  protected virtual void _(Events.FieldUpdating<RMUnit.unitCode> e)
  {
    if (this.inFieldUpdating)
      return;
    try
    {
      this.inFieldUpdating = true;
      if (!(e.Row is RMUnit row) || row.UnitCode == null)
        return;
      RMUnit rmUnit = this.Units.Search<RMUnit.unitCode>(e.NewValue).FirstTableItems.FirstOrDefault<RMUnit>();
      if (rmUnit != null && row != rmUnit)
        throw new PXSetPropertyException("The value of this field must be unique among all records.");
    }
    finally
    {
      this.inFieldUpdating = false;
    }
  }

  protected virtual void _(Events.FieldUpdated<RMUnit.unitCode> e)
  {
    RMUnit row = (RMUnit) e.Row;
    // ISSUE: explicit non-virtual call
    if (e == null || __nonvirtual (e.OldValue) == null || e.OldValue as string == row.UnitCode)
      return;
    foreach (PXResult<RMUnit> pxResult in PXSelectBase<RMUnit, PXSelect<RMUnit, Where<RMUnit.unitSetCode, Equal<Current<RMUnitSet.unitSetCode>>, And<RMUnit.parentCode, Equal<Required<RMUnit.unitCode>>>>>.Config>.Select((PXGraph) this, e.OldValue))
    {
      RMUnit rmUnit = (RMUnit) pxResult;
      rmUnit.ParentCode = row.UnitCode;
      this.Units.Update(rmUnit);
    }
  }

  private RMUnitSetMaint.SelectedNode CurrentSelected
  {
    get
    {
      PXCache cach = this.Caches[typeof (RMUnitSetMaint.SelectedNode)];
      if (cach.Current == null)
      {
        cach.Insert();
        cach.IsDirty = false;
      }
      return (RMUnitSetMaint.SelectedNode) cach.Current;
    }
  }

  protected virtual void _(Events.RowSelected<RMUnitSet> e)
  {
    this.copyUnitSet.SetEnabled(this.UnitSet.Current != null && this.UnitSet.Cache.GetStatus((object) this.UnitSet.Current) != PXEntryStatus.Inserted && !string.IsNullOrEmpty(this.UnitSet.Current.UnitSetCode));
    bool isVisible = this.IsNewUi();
    this.InsertUnit.SetVisible(isVisible);
    this.DeleteUnit.SetVisible(isVisible);
    if (!isVisible || this.CurrentUnit.Current != null)
      return;
    this.CurrentUnit.Current = this.Units.SelectSingle();
    this.Units.Cache.ActiveRow = (IBqlTable) this.CurrentUnit.Current;
    this.CurrentUnit.View.RequestRefresh();
    this.Units.View.RequestRefresh();
  }

  [Serializable]
  public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _ParentCode;

    [PXDBString(10, IsUnicode = true)]
    [PXUIField(Visible = false)]
    public virtual string ParentCode
    {
      get => this._ParentCode;
      set => this._ParentCode = value;
    }

    public abstract class parentCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMUnitSetMaint.SelectedNode.parentCode>
    {
    }
  }

  [Serializable]
  public class ParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <inheritdoc cref="!:RMUnitSet.RowSetCode" />
    [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "New Code", Required = true)]
    [PXDefault]
    public virtual string NewUnitSetCode { get; set; }

    /// <summary>Description of the new column set.</summary>
    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Required = true)]
    [PXDefault]
    public virtual string Description { get; set; }

    public abstract class newUnitSetCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMUnitSetMaint.ParamFilter.newUnitSetCode>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMUnitSetMaint.ParamFilter.description>
    {
    }
  }

  [Serializable]
  public class RMNewUnitSetPanel : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <inheritdoc cref="!:RMUnitSet.RowSetCode" />
    [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Unit Set Code", Required = true)]
    [PXDefault]
    public virtual string UnitSetCode { get; set; }

    /// <summary>Type of the new column set.</summary>
    [PXString(2, IsFixed = true)]
    [PXStringList(new string[] {"GL", "PM"}, new string[] {"GL", "PM"})]
    [PXUIField(DisplayName = "Type", Required = true)]
    [PXDefault("GL")]
    public virtual string Type { get; set; }

    /// <summary>Description of the new column set.</summary>
    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Required = true)]
    [PXDefault]
    public virtual string Description { get; set; }

    public abstract class unitSetCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMUnitSetMaint.RMNewUnitSetPanel.unitSetCode>
    {
    }

    public abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMUnitSetMaint.RMNewUnitSetPanel.type>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RMUnitSetMaint.RMNewUnitSetPanel.description>
    {
    }
  }
}
