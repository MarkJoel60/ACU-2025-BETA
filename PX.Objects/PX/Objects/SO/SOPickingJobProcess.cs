// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingJobProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

public class SOPickingJobProcess : 
  PXGraph<
  #nullable disable
  SOPickingJobProcess>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXFilter<SOPickingJobProcess.HeaderFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXProcessingViewOf<SOPickingJob>.BasedOn<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOPickingWorksheet.FK.Site>>, FbqlJoins.Left<SOPickerToShipmentLink>.On<KeysRelation<CompositeKey<Field<SOPickerToShipmentLink.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerToShipmentLink.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerToShipmentLink>, SOPicker, SOPickerToShipmentLink>.And<BqlOperand<
  #nullable enable
  SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<
  #nullable disable
  SOPickingWorksheet.worksheetType.single>>>>, FbqlJoins.Left<SOShipment>.On<SOPickerToShipmentLink.FK.Shipment>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOShipment.FK.Carrier>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOShipment.FK.Customer>>, FbqlJoins.Left<PX.Objects.CR.Location>.On<SOShipment.FK.CustomerLocation>>>>.FilteredBy<SOPickingJobProcess.HeaderFilter> PickingJobs;
  public PXCancel<SOPickingJobProcess.HeaderFilter> Cancel;
  public FbqlSelect<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>, SOPickingWorksheet>.View DummyWorksheet;
  public FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>, SOShipment>.View DummyShipment;
  public FbqlSelect<SelectFromBase<PX.Objects.CS.Carrier, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CS.Carrier>.View DummyCarrier;
  public FbqlSelect<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.AR.Customer>.View DummyCustomer;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.Location>.View DummyLocation;

  public IEnumerable pickingJobs() => (IEnumerable) this.GetPickingJobs();

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Pick List Type")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOPickingWorksheet.worksheetType> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Pick List Date")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOPickingWorksheet.pickDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipmentQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipmentVolume> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipmentWeight> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CS.Carrier.carrierID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Ship Via Description")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CS.Carrier.description> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.Customer.acctCD> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.Customer.acctName> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PX.Objects.CS.LocationRawAttribute), "DisplayName", "Customer Location ID")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.locationCD> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Customer Location Name")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobProcess.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.descr> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOPickingJobProcess.HeaderFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<SOPickingJob>) this.PickingJobs).SetProcessDelegate(new PXProcessingBase<SOPickingJob>.ProcessListDelegate((object) new SOPickingJobProcess.\u003C\u003Ec__DisplayClass24_0()
    {
      action = e.Row.Action,
      settings = PXCacheEx.GetExtension<SOPickingJobProcess.HeaderSettings>((IBqlTable) e.Row)
    }, __methodptr(\u003C_\u003Eb__0)));
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
  }

  protected SOPickingJobProcess.HeaderSettings ProcessSettings { get; set; }

  protected virtual IEnumerable<PXResult<SOPickingJob>> GetPickingJobs()
  {
    SOPickingJobProcess pickingJobProcess1 = this;
    SOPickingJobProcess.HeaderFilter filter = ((PXSelectBase<SOPickingJobProcess.HeaderFilter>) pickingJobProcess1.Filter).Current;
    if (!(filter.Action == "N"))
    {
      List<object> parameters = new List<object>();
      BqlCommand bqlCommand = pickingJobProcess1.AppendFilter(((PXSelectBase) pickingJobProcess1.PickingJobs).View.BqlSelect, (IList<object>) parameters, filter);
      PXView pxView = new PXView((PXGraph) pickingJobProcess1, false, bqlCommand);
      int startRow = PXView.StartRow;
      int num = 0;
      foreach (PXResult<SOPickingJob> pickingJob in pxView.Select(PXView.Currents, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        SOPickingJob soPickingJob1 = PXResult.Unwrap<SOPickingJob>((object) pickingJob);
        SOPicker soPicker = PXResult.Unwrap<SOPicker>((object) pickingJob);
        int? ofLinesInPickList = filter.MaxNumberOfLinesInPickList;
        int? nullable1 = new int?();
        int? nullable2 = nullable1;
        int? nullable3 = new int?(0);
        if (EnumerableExtensions.IsNotIn<int?>(ofLinesInPickList, nullable2, nullable3))
        {
          nullable1 = PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>.Aggregate<To<Count>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) pickingJobProcess1, (object[]) new SOPicker[1]
          {
            soPicker
          }, Array.Empty<object>()).RowCount;
          int valueOrDefault1 = nullable1.GetValueOrDefault();
          nullable1 = filter.MaxNumberOfLinesInPickList;
          int valueOrDefault2 = nullable1.GetValueOrDefault();
          if (valueOrDefault1 > valueOrDefault2 & nullable1.HasValue)
            continue;
        }
        int? maxQtyInLines = filter.MaxQtyInLines;
        nullable1 = new int?();
        int? nullable4 = nullable1;
        int? nullable5 = new int?(0);
        if (EnumerableExtensions.IsNotIn<int?>(maxQtyInLines, nullable4, nullable5))
        {
          SOPickingJobProcess pickingJobProcess2 = pickingJobProcess1;
          object[] objArray1 = (object[]) new SOPicker[1]
          {
            soPicker
          };
          object[] objArray2 = new object[1];
          nullable1 = filter.MaxQtyInLines;
          objArray2[0] = (object) nullable1.Value;
          nullable1 = PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent.And<BqlOperand<SOPickerListEntry.qty, IBqlDecimal>.IsGreater<P.AsDecimal>>>.Aggregate<To<Count>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) pickingJobProcess2, objArray1, objArray2).RowCount;
          if (nullable1.GetValueOrDefault() > 0)
            continue;
        }
        SOPickingJob soPickingJob2 = ((PXSelectBase<SOPickingJob>) pickingJobProcess1.PickingJobs).Locate(soPickingJob1);
        if (soPickingJob2 != null)
          soPickingJob1.Selected = soPickingJob2.Selected;
        yield return pickingJob;
      }
      PXView.StartRow = 0;
      ((PXSelectBase) pickingJobProcess1.PickingJobs).Cache.IsDirty = false;
    }
  }

  public virtual BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    SOPickingJobProcess.HeaderFilter filter)
  {
    switch (filter.Action)
    {
      case "S":
        cmd = cmd.WhereAnd<Where<BqlOperand<SOPickingJob.status, IBqlString>.IsEqual<SOPickingJob.status.onHold>>>();
        break;
      case "R":
        cmd = cmd.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.status, In3<SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued, SOPickingJob.status.assigned>>>>>.And<BqlOperand<SOPickingJob.actualAssigneeID, IBqlGuid>.IsNull>>>();
        break;
      case "P":
        cmd = cmd.WhereAnd<Where<BqlOperand<SOPickingJob.status, IBqlString>.IsIn<SOPickingJob.status.onHold, SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued, SOPickingJob.status.assigned, SOPickingJob.status.picking>>>();
        break;
      case "A":
        cmd = cmd.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.status, In3<SOPickingJob.status.onHold, SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued, SOPickingJob.status.assigned>>>>>.And<BqlOperand<SOPickingJob.actualAssigneeID, IBqlGuid>.IsNull>>>();
        break;
    }
    if (filter.WorksheetType != "AL")
      cmd = cmd.WhereAnd<Where<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<BqlField<SOPickingJobProcess.HeaderFilter.worksheetType, IBqlString>.FromCurrent>>>();
    if (filter.Priority.GetValueOrDefault() != -1)
      cmd = cmd.WhereAnd<Where<BqlOperand<SOPickingJob.priority, IBqlInt>.IsEqual<BqlField<SOPickingJobProcess.HeaderFilter.priority, IBqlInt>.FromCurrent>>>();
    if (filter.WorksheetType == "SS")
    {
      if (!string.IsNullOrEmpty(filter.CarrierPluginID))
        cmd = cmd.WhereAnd<Where<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOPickingJobProcess.HeaderFilter.carrierPluginID, IBqlString>.FromCurrent>>>();
      if (!string.IsNullOrEmpty(filter.ShipVia))
        cmd = cmd.WhereAnd<Where<BqlOperand<PX.Objects.CS.Carrier.carrierID, IBqlString>.IsEqual<BqlField<SOPickingJobProcess.HeaderFilter.shipVia, IBqlString>.FromCurrent>>>();
      if (filter.CustomerID.HasValue)
        cmd = cmd.WhereAnd<Where<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<BqlField<SOPickingJobProcess.HeaderFilter.customerID, IBqlInt>.FromCurrent>>>();
    }
    return cmd;
  }

  private static void ProcessPickingJobsHandler(
    string action,
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs)
  {
    PXGraph.CreateInstance<SOPickingJobProcess>().ProcessPickingJobs(action, settings, jobs);
  }

  protected virtual void ProcessPickingJobs(
    string action,
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs)
  {
    switch (action)
    {
      case "S":
        this.SendToQueue(settings, jobs);
        break;
      case "R":
        this.RemoveFromQueue(settings, jobs);
        break;
      case "P":
        this.ChangePriority(settings, jobs);
        break;
      case "A":
        this.AssignToPicker(settings, jobs);
        break;
    }
  }

  protected virtual void SendToQueue(
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs)
  {
    this.BulkUpdateJobs(settings, jobs, (Action<SOPickingJob, SOPickingJobProcess.HeaderSettings>) ((j, s) => ((PXSelectBase) this.PickingJobs).Cache.SetValueExt<SOPickingJob.status>((object) j, (object) "ENQ")));
  }

  protected virtual void RemoveFromQueue(
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs)
  {
    this.BulkUpdateJobs(settings, jobs, (Action<SOPickingJob, SOPickingJobProcess.HeaderSettings>) ((j, s) => ((PXSelectBase) this.PickingJobs).Cache.SetValueExt<SOPickingJob.status>((object) j, (object) "HLD")));
  }

  protected virtual void ChangePriority(
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs)
  {
    this.BulkUpdateJobs(settings, jobs, (Action<SOPickingJob, SOPickingJobProcess.HeaderSettings>) ((j, s) => ((PXSelectBase) this.PickingJobs).Cache.SetValueExt<SOPickingJob.priority>((object) j, (object) s.NewPriority)));
  }

  protected virtual void AssignToPicker(
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs)
  {
    this.BulkUpdateJobs(settings, jobs, (Action<SOPickingJob, SOPickingJobProcess.HeaderSettings>) ((j, s) => ((PXSelectBase) this.PickingJobs).Cache.SetValueExt<SOPickingJob.preferredAssigneeID>((object) j, (object) s.AssigneeID)));
  }

  protected virtual void BulkUpdateJobs(
    SOPickingJobProcess.HeaderSettings settings,
    IEnumerable<SOPickingJob> jobs,
    Action<SOPickingJob, SOPickingJobProcess.HeaderSettings> change)
  {
    foreach (SOPickingJob job in jobs)
    {
      change(job, settings);
      ((PXSelectBase<SOPickingJob>) this.PickingJobs).Update(job);
    }
    if (!((PXSelectBase) this.PickingJobs).Cache.IsDirty)
      return;
    ((PXGraph) this).Persist();
  }

  public class ProcessAction
  {
    public const string None = "N";
    public const string Send = "S";
    public const string Remove = "R";
    public const string Priority = "P";
    public const string Assign = "A";

    public class none : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingJobProcess.ProcessAction.none>
    {
      public none()
        : base("N")
      {
      }
    }

    public class send : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingJobProcess.ProcessAction.send>
    {
      public send()
        : base("S")
      {
      }
    }

    public class remove : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingJobProcess.ProcessAction.remove>
    {
      public remove()
        : base("R")
      {
      }
    }

    public class priority : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingJobProcess.ProcessAction.priority>
    {
      public priority()
        : base("P")
      {
      }
    }

    public class assign : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOPickingJobProcess.ProcessAction.assign>
    {
      public assign()
        : base("A")
      {
      }
    }

    [PXLocalizable]
    public abstract class DisplayNames
    {
      public const string None = "<SELECT>";
      public const string Send = "Send to Picking Queue";
      public const string Remove = "Remove from Picking Queue";
      public const string Priority = "Change Picking Priority";
      public const string Assign = "Assign Pick Lists";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("N", "<SELECT>"),
          PXStringListAttribute.Pair("S", "Send to Picking Queue"),
          PXStringListAttribute.Pair("P", "Change Picking Priority"),
          PXStringListAttribute.Pair("A", "Assign Pick Lists"),
          PXStringListAttribute.Pair("R", "Remove from Picking Queue")
        })
      {
      }
    }
  }

  [PXCacheName("Filter")]
  public class HeaderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(15, IsUnicode = true)]
    [SOPickingJobProcess.ProcessAction.List]
    [PXUnboundDefault("N")]
    [PXUIField(DisplayName = "Action", Required = true)]
    public virtual string Action { get; set; }

    [PXString(2, IsFixed = true)]
    [SOPickingJobProcess.HeaderFilter.worksheetType.List]
    [PXUnboundDefault(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.worksheetType.all, IBqlString>.When<Where<FeatureInstalled<FeaturesSet.wMSAdvancedPicking>>>.Else<SOPickingWorksheet.worksheetType.single>))]
    [PXUIField(DisplayName = "Pick List Type", FieldClass = "WMSAdvancedPicking")]
    public virtual string WorksheetType { get; set; }

    [PXInt]
    [SOPickingJobProcess.HeaderFilter.priority.List]
    [PXUnboundDefault(-1)]
    [PXUIField(DisplayName = "Priority")]
    public virtual int? Priority { get; set; }

    [PXDate]
    [PXUnboundDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? EndDate { get; set; }

    [Site(Required = true)]
    [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>))]
    public virtual int? SiteID { get; set; }

    [Customer]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
    public virtual int? CustomerID { get; set; }

    [PXActiveCarrierPluginSelector(typeof (Search<CarrierPlugin.carrierPluginID>))]
    [PXUIField(DisplayName = "Carrier")]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
    public virtual string CarrierPluginID { get; set; }

    [PXActiveCarrierSelectorUnbound(typeof (Search<PX.Objects.CS.Carrier.carrierID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOPickingJobProcess.HeaderFilter.carrierPluginID>, IsNull>>>>.Or<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOPickingJobProcess.HeaderFilter.carrierPluginID, IBqlString>.FromCurrent>>>>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
    [PXUIField(DisplayName = "Ship Via")]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
    public virtual string ShipVia { get; set; }

    [Inventory]
    public virtual int? InventoryID { get; set; }

    [Location(typeof (SOPickingJobProcess.HeaderFilter.siteID))]
    public virtual int? LocationID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Max. Number of Lines in Pick List")]
    public int? MaxNumberOfLinesInPickList { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Max. Quantity in Lines")]
    public int? MaxQtyInLines { get; set; }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.action>
    {
    }

    public abstract class worksheetType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.worksheetType>
    {
      public const string All = "AL";

      public class all : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOPickingJobProcess.HeaderFilter.worksheetType.all>
      {
        public all()
          : base("AL")
        {
        }
      }

      [PXLocalizable]
      public static class DisplayNames
      {
        public const string All = "All";
      }

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new Tuple<string, string>[4]
          {
            PXStringListAttribute.Pair("AL", "All"),
            PXStringListAttribute.Pair("SS", "Single-Shipment"),
            PXStringListAttribute.Pair("WV", "Wave"),
            PXStringListAttribute.Pair("BT", "Batch")
          })
        {
        }
      }
    }

    public abstract class priority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.priority>
    {
      public const int All = -1;

      public class all : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Constant<
        #nullable disable
        SOPickingJobProcess.HeaderFilter.priority.all>
      {
        public all()
          : base(-1)
        {
        }
      }

      [PXLocalizable]
      public static class DisplayNames
      {
        public const string All = "All";
      }

      public class ListAttribute : PXIntListAttribute
      {
        public ListAttribute()
          : base(new Tuple<int, string>[5]
          {
            PXIntListAttribute.Pair(-1, "All"),
            PXIntListAttribute.Pair(4, "Urgent"),
            PXIntListAttribute.Pair(3, "High"),
            PXIntListAttribute.Pair(2, "Medium"),
            PXIntListAttribute.Pair(1, "Low")
          })
        {
        }
      }
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.endDate>
    {
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.siteID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.customerID>
    {
    }

    public abstract class carrierPluginID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.carrierPluginID>
    {
    }

    public abstract class shipVia : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.shipVia>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.inventoryID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.locationID>
    {
    }

    public abstract class maxNumberOfLinesInPickList : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.maxNumberOfLinesInPickList>
    {
    }

    public abstract class maxQtyInLines : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderFilter.maxQtyInLines>
    {
    }
  }

  public sealed class HeaderSettings : PXCacheExtension<SOPickingJobProcess.HeaderFilter>
  {
    [PXInt]
    [WMSJob.priority.List]
    [PXUnboundDefault(2)]
    [PXUIField(DisplayName = "Set Picking Priority to")]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.action, IBqlString>.IsEqual<SOPickingJobProcess.ProcessAction.priority>))]
    public int? NewPriority { get; set; }

    [PXGuid]
    [PXSelector(typeof (Search<Users.pKID, Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>>), SubstituteKey = typeof (Users.username))]
    [PXUIField(DisplayName = "Assign to Picker")]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.action, IBqlString>.IsEqual<SOPickingJobProcess.ProcessAction.assign>))]
    public Guid? AssigneeID { get; set; }

    public abstract class newPriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderSettings.newPriority>
    {
    }

    public abstract class assigneeID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SOPickingJobProcess.HeaderSettings.assigneeID>
    {
    }
  }

  public class ShowPickListPopup : 
    PX.Objects.SO.GraphExtensions.ShowPickListPopup.On<SOPickingJobProcess, SOPickingJobProcess.HeaderFilter>.FilteredBy<Where<BqlOperand<
    #nullable enable
    SOPickingJob.jobID, IBqlInt>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJob.jobID, IBqlInt>.FromCurrent>>>
  {
  }

  public class InventoryLinkFilterExt : 
    InventoryLinkFilterExtensionBase<
    #nullable disable
    SOPickingJobProcess, SOPickingJobProcess.HeaderFilter, SOPickingJobProcess.HeaderFilter.inventoryID>
  {
    [PXMergeAttributes]
    [Inventory(IsKey = true)]
    protected override void _(
      PX.Data.Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
    {
    }

    /// Overrides <see cref="M:PX.Objects.SO.SOPickingJobProcess.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.SO.SOPickingJobProcess.HeaderFilter)" />
    [PXOverride]
    public BqlCommand AppendFilter(
      BqlCommand cmd,
      IList<object> parameters,
      SOPickingJobProcess.HeaderFilter filter,
      Func<BqlCommand, IList<object>, SOPickingJobProcess.HeaderFilter, BqlCommand> base_AppendFilter)
    {
      cmd = base_AppendFilter(cmd, parameters, filter);
      int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
      if (array.Length != 0)
      {
        cmd = cmd.WhereAnd<Where<Not<Exists<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.And<BqlOperand<SOPickerListEntry.inventoryID, IBqlInt>.IsNotIn<P.AsInt>>>>>>>();
        parameters.Add((object) array);
      }
      return cmd;
    }

    public class descr : 
      InventoryLinkFilterExtensionBase<SOPickingJobProcess, SOPickingJobProcess.HeaderFilter, SOPickingJobProcess.HeaderFilter.inventoryID>.AttachedInventoryDescription<SOPickingJobProcess.InventoryLinkFilterExt.descr>
    {
    }
  }

  public class LocationFilterExt : 
    LocationLinkFilterExtensionBase<SOPickingJobProcess, SOPickingJobProcess.HeaderFilter, SOPickingJobProcess.HeaderFilter.locationID>
  {
    [PXMergeAttributes]
    [Location(typeof (SOPickingJobProcess.HeaderFilter.siteID), IsKey = true)]
    protected override void _(
      PX.Data.Events.CacheAttached<LocationLinkFilter.locationID> e)
    {
    }

    /// Overrides <see cref="M:PX.Objects.SO.SOPickingJobProcess.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.SO.SOPickingJobProcess.HeaderFilter)" />
    [PXOverride]
    public BqlCommand AppendFilter(
      BqlCommand cmd,
      IList<object> parameters,
      SOPickingJobProcess.HeaderFilter filter,
      Func<BqlCommand, IList<object>, SOPickingJobProcess.HeaderFilter, BqlCommand> base_AppendFilter)
    {
      cmd = base_AppendFilter(cmd, parameters, filter);
      int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
      if (array.Length != 0)
      {
        cmd = cmd.WhereAnd<Where<Not<Exists<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.And<BqlOperand<SOPickerListEntry.locationID, IBqlInt>.IsNotIn<P.AsInt>>>>>>>();
        parameters.Add((object) array);
      }
      return cmd;
    }

    public class descr : 
      LocationLinkFilterExtensionBase<SOPickingJobProcess, SOPickingJobProcess.HeaderFilter, SOPickingJobProcess.HeaderFilter.locationID>.AttachedLocationDescription<SOPickingJobProcess.LocationFilterExt.descr>
    {
    }
  }

  [PXLocalizable]
  public abstract class CacheNames
  {
    public const string Filter = "Filter";
  }
}
