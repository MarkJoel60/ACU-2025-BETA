// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

public class INSetupMaint : PXGraph<INSetupMaint>, IFeatureAccessProvider
{
  public PXSelect<INSetup> setup;
  public PXSelect<GS1UOMSetup> gs1setup;
  public PXSave<INSetup> Save;
  public PXCancel<INSetup> Cancel;
  public PXSelect<INSite> sites;
  public PXSelect<INLocation> locations;
  public PXSelect<RelationGroup, Where<RelationGroup.specificModule, Equal<inventoryModule>, And<RelationGroup.specificType, Equal<segmentValueType>>>> Groups;
  public PXSelect<INScanSetup, Where<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>> ScanSetup;
  public PXSelect<INScanUserSetup, Where<INScanUserSetup.isOverridden, Equal<False>>> ScanUserSetups;
  public CRNotificationSetupList<INNotification> Notifications;
  public PXAction<INSetup> viewRestrictionGroup;

  protected virtual IEnumerable scanSetup()
  {
    return (IEnumerable) new INScanSetup[1]
    {
      PXResultset<INScanSetup>.op_Implicit(PXSelectBase<INScanSetup, PXSelect<INScanSetup, Where<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) ?? ((PXSelectBase<INScanSetup>) this.ScanSetup).Insert()
    };
  }

  public INSetupMaint()
  {
    PXCache cache = ((PXSelectBase) this.setup).Cache;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INSite.siteID), DefaultForInsert = true, DefaultForUpdate = true)]
  [PXParent(typeof (INLocation.FK.Site))]
  protected virtual void INLocation_SiteID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void INSetup_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    INSetup row = (INSetup) e.Row;
    if (row.TransitSiteID.HasValue)
      return;
    PXCache cache = ((PXSelectBase) this.sites).Cache;
    INSite instance = (INSite) cache.CreateInstance();
    instance.BranchID = row.TransitSiteID;
    instance.SiteCD = "INTR";
    instance.Descr = "Transit default warehouse";
    instance.OverrideInvtAccSub = new bool?(true);
    cache.Current = (object) instance;
    INSite inSite = (INSite) cache.Insert((object) instance);
    row.TransitSiteID = inSite.SiteID;
    sender.Update((object) row);
    PXContext.ClearSlot<SiteAnyAttribute.Definition>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<INSetup> e)
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.setup).Cache
    }))
    {
      if (e.Row != null && e.Row.TransferReasonCode == null && PXAccess.FeatureInstalled<FeaturesSet.wMSInventory>())
      {
        if (PXResultset<INScanSetup>.op_Implicit(((PXSelectBase<INScanSetup>) this.ScanSetup).Select(Array.Empty<object>())).UseDefaultReasonCodeInTransfer.GetValueOrDefault())
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSetup>>) e).Cache.RaiseExceptionHandling<INSetup.transferReasonCode>((object) e.Row, (object) e.Row.TransferReasonCode, (Exception) new PXSetPropertyException("The Use Default Reason Code in Transfers check box is selected on the Warehouse Management tab. Select the default reason code or clear the check box.", (PXErrorLevel) 2));
        else
          PXUIFieldAttribute.SetWarning<INSetup.transferReasonCode>(((PXGraph) this).Caches[typeof (INSetup)], (object) e.Row, (string) null);
      }
      else
        PXUIFieldAttribute.SetWarning<INSetup.transferReasonCode>(((PXGraph) this).Caches[typeof (INSetup)], (object) e.Row, (string) null);
    }
  }

  protected virtual void INSetup_TransitBranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INSetup row = (INSetup) e.Row;
    if (!row.TransitBranchID.HasValue)
      return;
    using (new PXReadBranchRestrictedScope())
    {
      INSite inSite = INSite.PK.Find((PXGraph) this, row.TransitSiteID, (PKFindOptions) 1);
      if (inSite != null)
        inSite.BranchID = row.TransitBranchID;
      ((PXSelectBase) this.sites).Cache.Update((object) inSite);
    }
  }

  protected virtual void INSetup_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    INSetup row = (INSetup) e.Row;
    if (row == null || (e.Operation & 3) == 3)
      return;
    short num = row.TurnoverPeriodsPerYear.Value;
    if (num > (short) 12 || num < (short) 1 || num != (short) 0 && (int) (short) (12 / (int) num) * (int) num != 12)
      cache.RaiseExceptionHandling<INSetup.turnoverPeriodsPerYear>((object) row, (object) num, (Exception) new PXSetPropertyException("Possible Values are: 1,2,3,4,6,12."));
    PXDefaultAttribute.SetPersistingCheck<INSetup.iNTransitAcctID>(cache, e.Row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<INSetup.iNTransitSubID>(cache, e.Row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<INScanSetup> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<INScanUserSetup> pxResult in ((PXSelectBase<INScanUserSetup>) this.ScanUserSetups).Select(Array.Empty<object>()))
      ((PXSelectBase<INScanUserSetup>) this.ScanUserSetups).Update(PXResult<INScanUserSetup>.op_Implicit(pxResult).ApplyValuesFrom(e.Row));
  }

  protected virtual void RelationGroup_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    RelationGroup row = (RelationGroup) e.Row;
    row.SpecificModule = typeof (INSetup).Namespace;
    row.SpecificType = typeof (SegmentValue).FullName;
    int num1 = GroupHelper.Count;
    foreach (RelationGroup relationGroup in sender.Inserted)
    {
      if (relationGroup.GroupMask != null && relationGroup.GroupMask.Length != 0)
      {
        int num2 = 0;
        for (int index1 = 0; index1 < relationGroup.GroupMask.Length; ++index1)
        {
          if (relationGroup.GroupMask[index1] == (byte) 0)
          {
            num2 += 8;
          }
          else
          {
            for (int index2 = 1; index2 <= 8; ++index2)
            {
              if ((int) relationGroup.GroupMask[index1] >> index2 == 0)
              {
                num2 += 9 - index2;
                break;
              }
            }
            break;
          }
        }
        if (num2 > num1)
          num1 = num2;
      }
    }
    byte[] numArray;
    if (num1 == 0)
      numArray = new byte[4]
      {
        (byte) 128 /*0x80*/,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
    else if (num1 == 0 || num1 % 32 /*0x20*/ != 0)
    {
      numArray = new byte[(num1 + 31 /*0x1F*/) / 32 /*0x20*/ * 4];
      numArray[num1 / 8] = (byte) (128 /*0x80*/ >> num1 % 8);
    }
    else
    {
      numArray = new byte[(num1 + 31 /*0x1F*/) / 32 /*0x20*/ * 4 + 4];
      numArray[numArray.Length - 4] = (byte) 128 /*0x80*/;
    }
    row.GroupMask = numArray;
    if (GroupHelper.Count >= numArray.Length * 8)
      return;
    if (!((PXGraph) this).Views.Caches.Contains(typeof (Neighbour)))
      ((PXGraph) this).Views.Caches.Add(typeof (Neighbour));
    PXCache cach = ((PXGraph) this).Caches[typeof (Neighbour)];
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      Neighbour neighbour = PXResult<Neighbour>.op_Implicit(pxResult);
      byte[] coverageMask = neighbour.CoverageMask;
      Array.Resize<byte>(ref coverageMask, numArray.Length);
      neighbour.CoverageMask = coverageMask;
      byte[] inverseMask = neighbour.InverseMask;
      Array.Resize<byte>(ref inverseMask, numArray.Length);
      neighbour.InverseMask = inverseMask;
      byte[] winCoverageMask = neighbour.WinCoverageMask;
      Array.Resize<byte>(ref winCoverageMask, numArray.Length);
      neighbour.WinCoverageMask = winCoverageMask;
      byte[] winInverseMask = neighbour.WinInverseMask;
      Array.Resize<byte>(ref winInverseMask, numArray.Length);
      neighbour.WinInverseMask = winInverseMask;
      cach.Update((object) neighbour);
    }
    cach.IsDirty = false;
  }

  protected virtual void RelationGroup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RelationGroup row = (RelationGroup) e.Row;
    if (!(row.SpecificModule != typeof (INSetup).Namespace) && !(row.SpecificType != typeof (SegmentValue).FullName))
      return;
    RelationGroup relationGroup = PXResultset<RelationGroup>.op_Implicit(PXSelectBase<RelationGroup, PXSelectReadonly<RelationGroup, Where<RelationGroup.groupName, Equal<Required<RelationGroup.groupName>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.GroupName
    }));
    if (relationGroup != null)
      sender.RestoreCopy((object) row, (object) relationGroup);
    row.SpecificModule = typeof (INSetup).Namespace;
    row.SpecificType = typeof (SegmentValue).FullName;
  }

  protected virtual void RelationGroup_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    RelationGroup row = (RelationGroup) e.Row;
    row.SpecificModule = (string) null;
    row.SpecificType = (string) null;
    row.Active = new bool?(false);
    if (sender.GetStatus((object) row) == 4)
      return;
    GraphHelper.MarkUpdated(sender, (object) row, true);
  }

  public virtual int Persist(System.Type cacheType, PXDBOperation operation)
  {
    if (cacheType == typeof (INUnit) && operation == 1)
      ((PXGraph) this).Persist(cacheType, (PXDBOperation) 2);
    return ((PXGraph) this).Persist(cacheType, operation);
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<INSetup>) this.setup).Current != null && string.IsNullOrEmpty(((PXSelectBase<INSetup>) this.setup).Current.DfltLotSerClassID) && !this.IsFeatureInstalled<FeaturesSet.lotSerialTracking>())
      ((PXSelectBase<INSetup>) this.setup).Current.DfltLotSerClassID = INLotSerClass.GetDefaultLotSerClass((PXGraph) this);
    ((PXGraph) this).Persist();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewRestrictionGroup(PXAdapter adapter)
  {
    if (((PXSelectBase<RelationGroup>) this.Groups).Current != null)
    {
      RelationGroups instance = PXGraph.CreateInstance<RelationGroups>();
      ((PXSelectBase<RelationHeader>) instance.HeaderGroup).Current = PXResultset<RelationHeader>.op_Implicit(((PXSelectBase<RelationHeader>) instance.HeaderGroup).Search<RelationGroup.groupName>((object) ((PXSelectBase<RelationGroup>) this.Groups).Current.GroupName, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Group Details");
    }
    return adapter.Get();
  }

  public bool IsFeatureInstalled<TFeature>()
  {
    return !(typeof (TFeature).Name.ToLower() == "warehouselocation") && PXAccess.FeatureInstalled(typeof (TFeature).Name);
  }
}
