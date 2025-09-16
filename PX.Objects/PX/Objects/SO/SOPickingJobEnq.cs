// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingJobEnq
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
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.SO;

public class SOPickingJobEnq : PXGraph<
#nullable disable
SOPickingJobEnq>
{
  public FbqlSelect<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>, SOPickingWorksheet>.View DummyWorksheet;
  public FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>, SOShipment>.View DummyShipment;
  public FbqlSelect<SelectFromBase<PX.Objects.CS.Carrier, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CS.Carrier>.View DummyCarrier;
  public FbqlSelect<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.AR.Customer>.View DummyCustomer;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.Location>.View DummyLocation;
  public PXFilter<SOPickingJobEnq.HeaderFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public SOPickingJobEnq.SelectPickingJobs<Where<BqlOperand<
  #nullable enable
  SOPickingJob.status, IBqlString>.IsNotIn<
  #nullable disable
  SOPickingJob.status.onHold, SOPickingJob.status.picked, SOPickingJob.status.completed, SOPickingJob.status.cancelled>>> PickingJobs;
  [PXFilterable(new System.Type[] {})]
  public SOPickingJobEnq.SelectPickingJobs<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOPickingJob.status, 
  #nullable disable
  Equal<SOPickingJob.status.picked>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SOPickingJobEnq.HeaderFilter.showPick>, 
  #nullable disable
  Equal<False>>>>>.And<BqlOperand<
  #nullable enable
  SOPickingJob.status, IBqlString>.IsNotIn<
  #nullable disable
  SOPickingJob.status.onHold, SOPickingJob.status.completed, SOPickingJob.status.cancelled>>>>> PackingJobs;
  public PXCancel<SOPickingJobEnq.HeaderFilter> Cancel;
  public PXAction<SOPickingJobEnq.HeaderFilter> HoldJob;
  public PXAction<SOPickingJobEnq.HeaderFilter> StartWatching;

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
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Quantity")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipmentQty> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Volume")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipmentVolume> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Weight")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipmentWeight> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.shipDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CS.Carrier.carrierID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Ship Via Description")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CS.Carrier.description> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.Customer.acctCD> e)
  {
  }

  [PXMergeAttributes]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.Customer.acctName> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PX.Objects.CS.LocationRawAttribute), "DisplayName", "Customer Location ID")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.locationCD> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Customer Location Name")]
  [PXUIVisible(typeof (BqlOperand<Current<SOPickingJobEnq.HeaderFilter.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Location.descr> e)
  {
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove from Queue")]
  protected virtual void holdJob()
  {
    if (((PXSelectBase<SOPickingJob>) this.PickingJobs).Current == null || !EnumerableExtensions.IsIn<string>(((PXSelectBase<SOPickingJob>) this.PickingJobs).Current.Status, "ENQ", "RNQ", "ASG"))
      return;
    ((PXSelectBase) this.PickingJobs).Cache.SetValueExt<SOPickingJob.status>((object) ((PXSelectBase<SOPickingJob>) this.PickingJobs).Current, (object) "HLD");
    ((PXSelectBase<SOPickingJob>) this.PickingJobs).UpdateCurrent();
  }

  [PXButton]
  [PXUIField(DisplayName = "Start Watching")]
  protected virtual void startWatching()
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) null, __methodptr(Watching)));
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOPickingJobEnq.HeaderFilter> args)
  {
    ((PXSelectBase) this.PickingJobs).Cache.AllowInsert = false;
    ((PXSelectBase) this.PickingJobs).Cache.AllowDelete = false;
    if (!PXLongOperation.Exists((PXGraph) this))
      return;
    ((PXSelectBase) this.Filter).Cache.AllowUpdate = false;
    ((PXSelectBase) this.PickingJobs).Cache.AllowUpdate = false;
    ((PXAction) this.Cancel).SetEnabled(false);
    ((PXAction) this.HoldJob).SetEnabled(false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOPickingJob> args)
  {
    KeyValuePair<string, (object, object)>[] array = PXCacheEx.GetDifference(((PXSelectBase) this.PickingJobs).Cache, (IBqlTable) args.OldRow, (IBqlTable) args.Row, false).OrderByDescending<KeyValuePair<string, (object, object)>, bool>((Func<KeyValuePair<string, (object, object)>, bool>) (p => EnumerableExtensions.IsIn<string>(p.Key, "Status", "Priority", "PreferredAssigneeID", "AutomaticShipmentConfirmation"))).ToArray<KeyValuePair<string, (object, object)>>();
    if (array.Length == 0)
      return;
    try
    {
      SOPickingJobEnq instance = PXGraph.CreateInstance<SOPickingJobEnq>();
      SOPickingJob soPickingJob = (SOPickingJob) PrimaryKeyOf<SOPickingJob>.By<SOPickingJob.jobID>.Find((PXGraph) instance, (SOPickingJob.jobID) args.Row, (PKFindOptions) 0);
      foreach (KeyValuePair<string, (object, object)> keyValuePair in array)
        ((PXSelectBase) instance.PickingJobs).Cache.SetValue((object) soPickingJob, keyValuePair.Key, keyValuePair.Value.Item2);
      GraphHelper.MarkUpdated(((PXSelectBase) instance.PickingJobs).Cache, (object) soPickingJob, true);
      ((PXGraph) instance).Persist();
    }
    catch (Exception ex)
    {
      if (array.Length != 0)
      {
        KeyValuePair<string, (object, object)> keyValuePair = array[0];
        PXCache<SOPickingJob>.RestoreCopy(args.Row, args.OldRow);
        ((PXSelectBase) this.PickingJobs).Cache.RaiseExceptionHandling(keyValuePair.Key, (object) args.Row, keyValuePair.Value.Item1, ex);
      }
    }
    ((PXSelectBase) this.PickingJobs).Cache.SetStatus((object) args.Row, (PXEntryStatus) 0);
    ((PXSelectBase) this.PickingJobs).Cache.IsDirty = false;
    ((PXSelectBase) this.PickingJobs).View.RequestRefresh();
  }

  private static void Watching()
  {
    while (true)
      Thread.Yield();
  }

  [PXCacheName("Filter")]
  public class HeaderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(2, IsFixed = true)]
    [SOPickingJobProcess.HeaderFilter.worksheetType.List]
    [PXUnboundDefault(typeof (BqlOperand<SOPickingJobProcess.HeaderFilter.worksheetType.all, IBqlString>.When<Where<FeatureInstalled<FeaturesSet.wMSAdvancedPicking>>>.Else<SOPickingWorksheet.worksheetType.single>))]
    [PXUIField(DisplayName = "Pick List Type", FieldClass = "WMSAdvancedPicking")]
    public virtual string WorksheetType { get; set; }

    [Site(Required = true)]
    [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>))]
    public virtual int? SiteID { get; set; }

    [PXGuid]
    [PXSelector(typeof (Search<Users.pKID, Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>>), SubstituteKey = typeof (Users.username))]
    [PXUIField(DisplayName = "Picker")]
    public Guid? AssigneeID { get; set; }

    [Customer]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobEnq.HeaderFilter.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
    public virtual int? CustomerID { get; set; }

    [PXActiveCarrierPluginSelector(typeof (Search<CarrierPlugin.carrierPluginID>))]
    [PXUIField(DisplayName = "Carrier")]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobEnq.HeaderFilter.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
    public virtual string CarrierPluginID { get; set; }

    [PXActiveCarrierSelectorUnbound(typeof (Search<PX.Objects.CS.Carrier.carrierID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOPickingJobEnq.HeaderFilter.carrierPluginID>, IsNull>>>>.Or<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOPickingJobEnq.HeaderFilter.carrierPluginID, IBqlString>.FromCurrent>>>>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
    [PXUIField(DisplayName = "Ship Via")]
    [PXUIVisible(typeof (BqlOperand<SOPickingJobEnq.HeaderFilter.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>))]
    public virtual string ShipVia { get; set; }

    [PXBool]
    [PXUnboundDefault(typeof (SearchFor<SOPickPackShipSetup.showPickTab>.Where<BqlOperand<SOPickPackShipSetup.branchID, IBqlInt>.IsEqual<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>))]
    public virtual bool? ShowPick { get; set; }

    [PXBool]
    [PXUnboundDefault(typeof (SearchFor<SOPickPackShipSetup.showPackTab>.Where<BqlOperand<SOPickPackShipSetup.branchID, IBqlInt>.IsEqual<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>))]
    public virtual bool? ShowPack { get; set; }

    public abstract class worksheetType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.worksheetType>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickingJobEnq.HeaderFilter.siteID>
    {
    }

    public abstract class assigneeID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.assigneeID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.customerID>
    {
    }

    public abstract class carrierPluginID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.carrierPluginID>
    {
    }

    public abstract class shipVia : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.shipVia>
    {
    }

    public abstract class showPick : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.showPick>
    {
    }

    public abstract class showPack : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOPickingJobEnq.HeaderFilter.showPack>
    {
    }
  }

  [PXUIField(DisplayName = "Time in Queue", Enabled = false)]
  public class timeInQueue : 
    PXFieldAttachedTo<SOPickingJob>.By<SOPickingJobEnq>.AsString.Named<SOPickingJobEnq.timeInQueue>
  {
    public override string GetValue(SOPickingJob row)
    {
      return row == null ? (string) null : row.EnqueuedAt.With<DateTime?, TimeSpan>((Func<DateTime?, TimeSpan>) (date => this.ServerTime.Value - date.Value)).With<TimeSpan, string>((Func<TimeSpan, string>) (diff => ((int) diff.TotalHours).ToString() + diff.ToString("\\:mm\\:ss")));
    }

    private Lazy<DateTime> ServerTime { get; } = Lazy.By<DateTime>(new Func<DateTime>(SOPickingJobEnq.timeInQueue.GetServerTime));

    private static DateTime GetServerTime()
    {
      DateTime dateTime1;
      DateTime dateTime2;
      PXDatabase.SelectDate(ref dateTime1, ref dateTime2);
      return PXTimeZoneInfo.ConvertTimeFromUtc(dateTime2, LocaleInfo.GetTimeZone());
    }
  }

  public class SelectPickingJobs<TWhere> : 
    FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOPickingWorksheet.FK.Site>>, FbqlJoins.Left<SOPickerToShipmentLink>.On<KeysRelation<CompositeKey<Field<SOPickerToShipmentLink.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerToShipmentLink.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerToShipmentLink>, SOPicker, SOPickerToShipmentLink>.And<BqlOperand<
    #nullable enable
    SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<
    #nullable disable
    SOPickingWorksheet.worksheetType.single>>>>, FbqlJoins.Left<SOShipment>.On<SOPickerToShipmentLink.FK.Shipment>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOShipment.FK.Carrier>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOShipment.FK.Customer>>, FbqlJoins.Left<PX.Objects.CR.Location>.On<SOShipment.FK.CustomerLocation>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.IN.INSite, BqlField<
    #nullable enable
    AccessInfo.userName, IBqlString>.FromCurrent>>, 
    #nullable disable
    And<BqlOperand<
    #nullable enable
    SOPickingWorksheet.siteID, IBqlInt>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.siteID, IBqlInt>.FromCurrent>>>, 
    #nullable disable
    And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.assigneeID>, 
    #nullable disable
    IsNull>>>, Or<BqlOperand<
    #nullable enable
    SOPickingJob.preferredAssigneeID, IBqlGuid>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.assigneeID, IBqlGuid>.FromCurrent>>>>.Or<
    #nullable disable
    BqlOperand<
    #nullable enable
    SOPickingJob.actualAssigneeID, IBqlGuid>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.assigneeID, IBqlGuid>.FromCurrent>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.worksheetType>, 
    #nullable disable
    Equal<SOPickingJobProcess.HeaderFilter.worksheetType.all>>>>>.Or<BqlOperand<
    #nullable enable
    SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.worksheetType, IBqlString>.FromCurrent>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.worksheetType>, 
    #nullable disable
    NotEqual<SOPickingWorksheet.worksheetType.single>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.customerID>, 
    #nullable disable
    IsNull>>>>.Or<BqlOperand<
    #nullable enable
    SOShipment.customerID, IBqlInt>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.customerID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.carrierPluginID>, 
    #nullable disable
    IsNull>>>>.Or<BqlOperand<
    #nullable enable
    PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.carrierPluginID, IBqlString>.FromCurrent>>>>>.And<
    #nullable disable
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.shipVia>, 
    #nullable disable
    IsNull>>>>.Or<BqlOperand<
    #nullable enable
    PX.Objects.CS.Carrier.carrierID, IBqlString>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJobEnq.HeaderFilter.shipVia, IBqlString>.FromCurrent>>>>>>>.And<
    #nullable disable
    TWhere>>.Order<By<BqlField<
    #nullable enable
    SOPickingJob.priority, IBqlInt>.Desc, 
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJob.enqueuedAt, IBqlDateTime>.Asc>>, 
    #nullable disable
    SOPickingJob>.View
    where TWhere : IBqlWhere, new()
  {
    public SelectPickingJobs(PXGraph graph)
      : base(graph)
    {
    }

    public SelectPickingJobs(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ShowPickListPopup : 
    PX.Objects.SO.GraphExtensions.ShowPickListPopup.On<SOPickingJobEnq, SOPickingJobEnq.HeaderFilter>.FilteredBy<Where<BqlOperand<
    #nullable enable
    SOPickingJob.jobID, IBqlInt>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    SOPickingJob.jobID, IBqlInt>.FromCurrent>>>
  {
  }
}
