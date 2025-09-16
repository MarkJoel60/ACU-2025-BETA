// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DimensionMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using PX.Metadata;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.CS;

public class DimensionMaint : PXGraph<
#nullable disable
DimensionMaint, Dimension>
{
  private static readonly HashSet<string> TreeCompatibleDimensions = new HashSet<string>()
  {
    "INITEMCLASS",
    "COSTCODE"
  };
  public PXSelect<Dimension, Where<Dimension.dimensionID, InFieldClassActivated, Or<Dimension.dimensionID, IsNull>>> Header;
  public PXSelect<Segment, Where<Segment.dimensionID, Equal<Current<Dimension.dimensionID>>>> Detail;
  public PXSelect<SegmentValue> Values;
  public PXAction<Dimension> ViewSegment;
  private static readonly List<KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>> SegmentKeysModifiers = new List<KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>>()
  {
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("ACCGROUP", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PMAccountGroup, PMAccountGroup.groupID, PMAccountGroup.groupCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("ACCOUNT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.GL.Account, PX.Objects.GL.Account.accountID, PX.Objects.GL.Account.accountCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("BIZACCT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID, PX.Objects.CR.BAccount.acctCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("BIZACCT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.GL.Branch, PX.Objects.GL.Branch.branchID, PX.Objects.GL.Branch.branchCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("CASHACCOUNT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.CA.CashAccount, PX.Objects.CA.CashAccount.cashAccountID, PX.Objects.CA.CashAccount.cashAccountCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("CONTRACTITEM", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<ContractItem, ContractItem.contractItemID, ContractItem.contractItemCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("INLOCATION", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<INLocation, INLocation.locationID, INLocation.locationCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("INSITE", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<INSite, INSite.siteID, INSite.siteCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("INSUBITEM", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<INSubItem, INSubItem.subItemID, INSubItem.subItemCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("INVENTORY", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.inventoryID, PX.Objects.IN.InventoryItem.inventoryCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("MLISTCD", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<CRMarketingList, CRMarketingList.marketingListID, CRMarketingList.mailListCode>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("LOCATION", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.CR.Location, PX.Objects.CR.Location.locationID, PX.Objects.CR.Location.locationCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("PROJECT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.CT.Contract, PX.Objects.CT.Contract.contractID, PX.Objects.CT.Contract.contractCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("PROTASK", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PMTask, PMTask.taskID, PMTask.taskCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("SALESPER", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.AR.SalesPerson, PX.Objects.AR.SalesPerson.salesPersonID, PX.Objects.AR.SalesPerson.salesPersonCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("SUBACCOUNT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PX.Objects.GL.Sub, PX.Objects.GL.Sub.subID, PX.Objects.GL.Sub.subCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("INITEMCLASS", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifierLight<INItemClass, INItemClass.itemClassID, INItemClass.itemClassCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("COSTCODE", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PMCostCode, PMCostCode.costCodeID, PMCostCode.costCodeCD>()),
    new KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>("TMPROJECT", (DimensionMaint.ISegmentKeysModifier) new DimensionMaint.SegmentKeysModifier<PMProject, PMProject.contractID, PMProject.contractCD, Where<PMProject.baseType, Equal<CTPRType.projectTemplate>>>())
  };

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  protected virtual IEnumerable detail()
  {
    if (((PXSelectBase<Dimension>) this.Header).Current == null)
      return (IEnumerable) new Segment[0];
    return ((PXSelectBase<Dimension>) this.Header).Current.ParentDimensionID == null ? this.GetSimpleDetails(((PXSelectBase<Dimension>) this.Header).Current) : this.GetComplexDetails(((PXSelectBase<Dimension>) this.Header).Current);
  }

  protected virtual IEnumerable GetDetails(string dimId)
  {
    Dimension dim;
    if (string.IsNullOrEmpty(dimId) || (dim = PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension>.Config>.Search<Dimension.dimensionID>((PXGraph) this, (object) dimId, Array.Empty<object>()))) == null)
      return (IEnumerable) new Segment[0];
    return dim.ParentDimensionID == null ? this.GetSimpleDetails(dim) : this.GetComplexDetails(dim);
  }

  private IEnumerable GetComplexDetails(Dimension dim)
  {
    DimensionMaint dimensionMaint1 = this;
    Hashtable hashtable = new Hashtable();
    foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select((PXGraph) dimensionMaint1, new object[1]
    {
      (object) dim.ParentDimensionID
    }))
    {
      Segment segment = PXResult<Segment>.op_Implicit(pxResult);
      Segment copy = (Segment) ((PXSelectBase) dimensionMaint1.Detail).Cache.CreateCopy((object) segment);
      short key = copy.SegmentID.Value;
      copy.ParentDimensionID = dim.ParentDimensionID;
      copy.Inherited = new bool?(true);
      copy.DimensionID = dim.DimensionID;
      hashtable.Add((object) key, (object) copy);
    }
    DimensionMaint dimensionMaint2 = dimensionMaint1;
    object[] objArray = new object[1]
    {
      (object) dim.DimensionID
    };
    foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select((PXGraph) dimensionMaint2, objArray))
    {
      Segment complexDetail = PXResult<Segment>.op_Implicit(pxResult);
      if (complexDetail.ParentDimensionID != null && hashtable.ContainsKey((object) complexDetail.SegmentID))
        hashtable.Remove((object) complexDetail.SegmentID);
      yield return (object) complexDetail;
    }
    foreach (Segment complexDetail in (IEnumerable) hashtable.Values)
    {
      if (((PXSelectBase) dimensionMaint1.Detail).Cache.GetStatus((object) complexDetail) == null)
        ((PXSelectBase) dimensionMaint1.Detail).Cache.SetStatus((object) complexDetail, (PXEntryStatus) 2);
      yield return (object) complexDetail;
    }
  }

  protected virtual IEnumerable GetSimpleDetails(Dimension dim)
  {
    object[] objArray = new object[1]
    {
      (object) dim.DimensionID
    };
    foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>, And<Segment.dimensionID, InFieldClassActivated>>>.Config>.Select((PXGraph) this, objArray))
      yield return (object) PXResult<Segment>.op_Implicit(pxResult);
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable cancel(PXAdapter a)
  {
    DimensionMaint dimensionMaint = this;
    foreach (Dimension dimension in ((PXAction) new PXCancel<Dimension>((PXGraph) dimensionMaint, "Cancel")).Press(a))
    {
      if (((PXSelectBase) dimensionMaint.Header).Cache.GetStatus((object) dimension) == 2)
      {
        if (PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelectReadonly<Dimension, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>>>.Config>.Select((PXGraph) dimensionMaint, new object[1]
        {
          (object) dimension.DimensionID
        })) != null)
          ((PXSelectBase) dimensionMaint.Header).Cache.RaiseExceptionHandling<Dimension.dimensionID>((object) dimension, (object) dimension.DimensionID, (Exception) new PXSetPropertyException("Segmented Key is restricted by features configuration."));
      }
      yield return (object) dimension;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "View Segment")]
  protected virtual IEnumerable viewSegment(PXAdapter adapter)
  {
    Segment current = ((PXSelectBase<Segment>) this.Detail).Current;
    if (current != null)
    {
      Segment segment;
      if (current.Inherited.GetValueOrDefault())
        segment = PXResultset<Segment>.op_Implicit(PXSelectBase<Segment, PXSelectReadonly2<Segment, InnerJoin<Dimension, On<Dimension.parentDimensionID, Equal<Segment.dimensionID>>>, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>, And<Segment.segmentID, Equal<Required<Segment.segmentID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) current.DimensionID,
          (object) current.SegmentID
        }));
      else
        segment = PXResultset<Segment>.op_Implicit(PXSelectBase<Segment, PXSelectReadonly<Segment>.Config>.Search<Segment.dimensionID, Segment.segmentID>((PXGraph) this, (object) current.DimensionID, (object) current.SegmentID, Array.Empty<object>()));
      PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (Segment)], (object) segment, string.Empty);
    }
    return adapter.Get();
  }

  public virtual void Persist()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DimensionMaint.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new DimensionMaint.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.\u003C\u003E4__this = this;
    if (((PXSelectBase) this.Header).Cache.IsDirty && ((PXSelectBase<Segment>) this.Detail).Select(Array.Empty<object>()).Count == 0 && ((PXSelectBase<Dimension>) this.Header).Current != null)
      throw new PXException("Segmented key must have at least one segment.");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.dimension = ((PXSelectBase<Dimension>) this.Header).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass150.dimension == null)
    {
      ((PXGraph) this).Persist();
    }
    else
    {
      try
      {
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass150, __methodptr(\u003CPersist\u003Eb__0)));
      }
      catch (PXDatabaseException ex)
      {
        if (ex.ErrorCode == null)
          throw new PXException("Segment '{0}' cannot be deleted as it has one or more segment values defined.", new object[1]
          {
            ex.Keys[1]
          });
        throw;
      }
    }
    PXPageCacheUtils.InvalidateCachedPages();
  }

  protected virtual void Dimension_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    Dimension newRow = (Dimension) e.NewRow;
    int maxLength = PXDimensionAttribute.GetMaxLength(((PXSelectBase<Dimension>) this.Header).Current.DimensionID);
    short? length = newRow.Length;
    int? nullable = length.HasValue ? new int?((int) length.GetValueOrDefault()) : new int?();
    int num = maxLength;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    cache.RaiseExceptionHandling<Dimension.length>(e.Row, (object) newRow.Length, (Exception) new PXSetPropertyException<Dimension.length>("The key length is greater than the maximum allowed value."));
  }

  protected virtual void Dimension_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    Dimension row = (Dimension) e.Row;
    row.MaxLength = new int?(PXDimensionAttribute.GetMaxLength(row.DimensionID));
    PXUIFieldAttribute.SetEnabled<Dimension.length>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<Dimension.maxLength>(cache, (object) row, this.HasMaxLength(row));
    PXUIFieldAttribute.SetEnabled<Dimension.segments>(cache, (object) row, false);
    bool isTreeCompatible = DimensionMaint.TreeCompatibleDimensions.Contains(row.DimensionID);
    var data = EnumerableExtensions.UnZip(cache.GetAttributesReadonly<Dimension.lookupMode>().OfType<PXStringListAttribute>().First<PXStringListAttribute>().ValueLabelDic.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (kvp => kvp.Key != "SC" | isTreeCompatible)), (values, labels) => new
    {
      Values = values.ToArray<string>(),
      Labels = labels.ToArray<string>()
    });
    PXStringListAttribute.SetLocalizable<Dimension.lookupMode>(cache, (object) null, false);
    PXStringListAttribute.SetList<Dimension.lookupMode>(cache, e.Row, data.Values, data.Labels);
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = false;
    ((PXSelectBase) this.Detail).Cache.AllowInsert = true;
    ((PXSelectBase) this.Detail).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Detail).Cache.AllowDelete = true;
    string dimensionId = row.DimensionID;
    bool? validate;
    if (dimensionId != null)
    {
      switch (dimensionId.Length)
      {
        case 4:
          if (dimensionId == "LEAD")
            break;
          goto label_21;
        case 6:
          switch (dimensionId[0])
          {
            case 'I':
              if (dimensionId == "INSITE")
                break;
              goto label_21;
            case 'V':
              if (dimensionId == "VENDOR")
                break;
              goto label_21;
            default:
              goto label_21;
          }
          break;
        case 7:
          switch (dimensionId[0])
          {
            case 'A':
              if (dimensionId == "ACCOUNT")
                goto label_22;
              goto label_21;
            case 'B':
              if (dimensionId == "BIZACCT")
                break;
              goto label_21;
            default:
              goto label_21;
          }
          break;
        case 8:
          switch (dimensionId[0])
          {
            case 'C':
              if (dimensionId == "CUSTOMER")
                break;
              goto label_21;
            case 'E':
              if (dimensionId == "EMPLOYEE")
                break;
              goto label_21;
            case 'S':
              if (dimensionId == "SALESPER")
                break;
              goto label_21;
            default:
              goto label_21;
          }
          break;
        case 9:
          switch (dimensionId[2])
          {
            case 'S':
              if (dimensionId == "INSUBITEM")
              {
                flag3 = true;
                PXUIFieldAttribute.SetEnabled<Segment.autoNumber>(((PXSelectBase) this.Detail).Cache, (object) null, false);
                goto label_22;
              }
              goto label_21;
            case 'V':
              if (dimensionId == "INVENTORY")
                break;
              goto label_21;
            default:
              goto label_21;
          }
          break;
        case 10:
          if (dimensionId == "INLOCATION")
            break;
          goto label_21;
        default:
          goto label_21;
      }
      validate = row.Validate;
      bool flag4 = false;
      flag3 = validate.GetValueOrDefault() == flag4 & validate.HasValue;
      PXUIFieldAttribute.SetEnabled<Segment.autoNumber>(((PXSelectBase) this.Detail).Cache, (object) null, true);
      PXUIFieldAttribute.SetVisible<Segment.isCosted>(((PXSelectBase) this.Detail).Cache, (object) null, false);
      goto label_22;
    }
label_21:
    flag3 = true;
label_22:
    bool flag5 = flag3 && row.LookupMode == "SC";
    PXCache pxCache = cache;
    validate = row.Internal;
    int num = !validate.GetValueOrDefault() ? 1 : 0;
    pxCache.AllowDelete = num != 0;
    PXUIFieldAttribute.SetVisible<Segment.consolOrder>(((PXSelectBase) this.Detail).Cache, (object) null, row.DimensionID == "SUBACCOUNT");
    PXUIFieldAttribute.SetVisible<Segment.consolNumChar>(((PXSelectBase) this.Detail).Cache, (object) null, row.DimensionID == "SUBACCOUNT");
    PXUIFieldAttribute.SetVisible<Segment.isCosted>(((PXSelectBase) this.Detail).Cache, (object) null, row.DimensionID == "INSUBITEM");
    bool flag6 = row.ParentDimensionID != null;
    ((PXSelectBase) this.Detail).Cache.AllowInsert &= !flag6;
    PXUIFieldAttribute.SetVisible<Segment.inherited>(((PXSelectBase) this.Detail).Cache, (object) null, flag6);
    PXUIFieldAttribute.SetVisible<Segment.isOverrideForUI>(((PXSelectBase) this.Detail).Cache, (object) null, flag6);
    PXUIFieldAttribute.SetEnabled<Dimension.numberingID>(cache, e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<Dimension.specificModule>(cache, e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<Dimension.validate>(cache, e.Row, flag5);
    PXUIFieldAttribute.SetEnabled<Dimension.lookupMode>(cache, e.Row, flag3);
  }

  protected virtual void Dimension_Validate_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue == null)
      return;
    e.ReturnValue = (object) !(bool) e.ReturnValue;
  }

  protected virtual void Dimension_Validate_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    PXBoolAttribute.ConvertValue(e);
    if (e.NewValue == null)
      return;
    e.NewValue = (object) !(bool) e.NewValue;
  }

  protected virtual void Dimension_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    this.CheckLength(cache, e.Row);
  }

  protected virtual void Dimension_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    ((ICacheControl) this.ScreenInfoCacheControl).InvalidateCache();
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Dimension.dimensionID))]
  [PXUIField]
  [PXSelector(typeof (Dimension.dimensionID), DirtyRead = true)]
  protected virtual void Segment_DimensionID_CacheAttached(PXCache sender)
  {
  }

  [PXDBShort(IsKey = true)]
  [PXUIField]
  protected virtual void Segment_SegmentID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Segment_SegmentID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is Segment row))
      return;
    short num1 = 0;
    foreach (Segment detail in this.GetDetails(row.DimensionID))
    {
      short? segmentId = detail.SegmentID;
      int? nullable = segmentId.HasValue ? new int?((int) segmentId.GetValueOrDefault()) : new int?();
      int num2 = (int) num1;
      if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      {
        segmentId = detail.SegmentID;
        num1 = segmentId.Value;
      }
    }
    short num3;
    e.NewValue = (object) (num3 = (short) ((int) num1 + 1));
    row.ConsolOrder = new short?(num3);
    ((CancelEventArgs) e).Cancel = true;
    PXUIFieldAttribute.SetEnabled<Segment.segmentID>(cache, (object) row, false);
  }

  protected virtual void Segment_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    Segment row = (Segment) e.Row;
    if (row == null)
      return;
    ((CancelEventArgs) e).Cancel = row.ParentDimensionID != null && row.Inherited.GetValueOrDefault() && cache.GetStatus((object) row) == 2;
  }

  protected virtual void Segment_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    ((ICacheControl) this.ScreenInfoCacheControl).InvalidateCache();
  }

  protected virtual void Segment_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    this.UpdateHeader(e.Row);
    this.CheckLength(((PXSelectBase) this.Header).Cache, (object) ((PXSelectBase<Dimension>) this.Header).Current);
  }

  protected virtual void Segment_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    Segment row = (Segment) e.Row;
    if (row == null)
      return;
    if (row.ParentDimensionID != null)
      row.Inherited = new bool?(false);
    this.UpdateHeader(e.Row);
    this.CheckLength(((PXSelectBase) this.Header).Cache, (object) ((PXSelectBase<Dimension>) this.Header).Current);
  }

  protected virtual void Segment_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this.UpdateHeader(e.Row);
    this.CheckLength(((PXSelectBase) this.Header).Cache, (object) ((PXSelectBase<Dimension>) this.Header).Current);
  }

  protected virtual void Segment_DimensionID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Segment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is Segment row) || row.ParentDimensionID == null)
      return;
    Segment segment1 = row;
    bool? nullable = row.Inherited;
    if (!nullable.GetValueOrDefault())
      segment1 = PXResultset<Segment>.op_Implicit(PXSelectBase<Segment, PXSelectReadonly<Segment, Where<Segment.dimensionID, Equal<Current<Segment.parentDimensionID>>, And<Segment.segmentID, Equal<Current<Segment.segmentID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) row
      }, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<Segment.descr>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<Segment.editMask>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<Segment.autoNumber>(sender, (object) row, true);
    PXCache pxCache = sender;
    Segment segment2 = row;
    int num;
    if (segment1 != null)
    {
      nullable = segment1.Validate;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<Segment.validate>(pxCache, (object) segment2, num != 0);
  }

  protected virtual void Segment_Length_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Segment row))
      return;
    row.ConsolNumChar = row.Length;
  }

  protected virtual void Segment_Length_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if ((short) e.NewValue <= (short) 0)
      throw new PXSetPropertyException("Segment length must have positive value.");
  }

  protected virtual void Segment_AutoNumber_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Segment row = e.Row as Segment;
    if (!row.Validate.HasValue)
      return;
    bool? nullable = row.Validate;
    if (!nullable.Value)
      return;
    nullable = row.AutoNumber;
    bool newValue = (bool) e.NewValue;
    if (nullable.GetValueOrDefault() == newValue & nullable.HasValue || !(bool) e.NewValue)
      return;
    row.Validate = new bool?(false);
    cache.Update((object) row);
  }

  protected virtual void Segment_Validate_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Segment row = e.Row as Segment;
    if (!row.AutoNumber.HasValue)
      return;
    bool? nullable = row.AutoNumber;
    if (!nullable.Value)
      return;
    nullable = row.Validate;
    bool newValue = (bool) e.NewValue;
    if (nullable.GetValueOrDefault() == newValue & nullable.HasValue || !(bool) e.NewValue)
      return;
    row.AutoNumber = new bool?(false);
    cache.Update((object) row);
  }

  protected virtual void Segment_ConsolNumChar_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    short newValue = (short) e.NewValue;
    if (newValue < (short) 0)
      throw new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Segment.consolNumChar>(cache)
      });
    if (newValue <= (short) 0)
      return;
    foreach (PXResult<SegmentValue> pxResult in PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Optional<Segment.dimensionID>>, And<SegmentValue.segmentID, Equal<Optional<Segment.segmentID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<Segment>) this.Detail).Current.DimensionID,
      (object) ((PXSelectBase<Segment>) this.Detail).Current.SegmentID
    }))
    {
      SegmentValue segmentValue = PXResult<SegmentValue>.op_Implicit(pxResult);
      string mappedSegValue1 = segmentValue.MappedSegValue;
      if ((mappedSegValue1 != null ? (mappedSegValue1.ToString().Length > 0 ? 1 : 0) : 0) != 0)
      {
        string mappedSegValue2 = segmentValue.MappedSegValue;
        if ((mappedSegValue2 != null ? (mappedSegValue2.ToString().Length > (int) newValue ? 1 : 0) : 0) != 0)
          throw new PXSetPropertyException("The number of characters cannot be fewer than {0}. To enter a new value, on the Segmented Values (CS203000) form, decrease the length of the strings in the Mapped Value column.", new object[1]
          {
            (object) segmentValue.MappedSegValue?.ToString().Length
          });
      }
    }
  }

  protected virtual void Segment_ConsolOrder_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Segment segment = PXResultset<Segment>.op_Implicit(PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Current<Dimension.dimensionID>>, And<Segment.segmentID, NotEqual<Required<Segment.segmentID>>, And<Segment.consolOrder, Equal<Required<Segment.consolOrder>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((Segment) e.Row).SegmentID,
      e.NewValue
    }));
    if (segment != null)
      throw new PXSetPropertyException("The segment '{0}' with Consol Order value equal to '{1}' already exists.", new object[2]
      {
        (object) segment.SegmentID,
        e.NewValue
      });
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<Segment, Segment.isCosted> e)
  {
    this.CheckSubItems(e.Row);
  }

  protected virtual void Segment_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    Segment row = (Segment) e.Row;
    string dimensionId = row.DimensionID;
    short? segmentId = row.SegmentID;
    if (((PXSelectBase<Dimension>) this.Header).Current.ParentDimensionID != null && row.Inherited.GetValueOrDefault())
      throw new PXException("Segment '{0}' is not overridden and cannot be deleted.", new object[1]
      {
        (object) segmentId
      });
    Segment[] array = GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelectReadonly<Segment, Where<Segment.parentDimensionID, Equal<Current<Segment.dimensionID>>, And<Segment.segmentID, Equal<Current<Segment.segmentID>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())).ToArray<Segment>();
    if (array.Length != 0)
      throw new PXException("The {0} segment cannot be deleted as it is overridden in at least one segmented key. To be able to delete the segment, you need to first delete the segments that have the Override check box selected in the following segmented keys: {1}.", new object[2]
      {
        (object) segmentId,
        (object) Str.Join(((IEnumerable<string>) ((IEnumerable<Segment>) array).Select<Segment, string>((Func<Segment, string>) (s => s.DimensionID)).ToArray<string>()).Distinct<string>(), ", ")
      });
    Segment segment = PXResultset<Segment>.op_Implicit(PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Current<Segment.dimensionID>>>, OrderBy<Desc<Segment.segmentID>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (segment != null)
    {
      short? nullable1 = segment.SegmentID;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = segmentId;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        throw new PXException("Segment '{0}' cannot be deleted as it is not last segment.", new object[1]
        {
          (object) segmentId
        });
    }
    if (PXResultset<SegmentValue>.op_Implicit(PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Optional<Segment.dimensionID>>, And<SegmentValue.segmentID, Equal<Optional<Segment.segmentID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) dimensionId,
      (object) segmentId
    })) == null)
      return;
    if (row.ParentDimensionID == null)
      throw new PXException("Segment '{0}' cannot be deleted as it has one or more segment values defined.", new object[1]
      {
        (object) segmentId
      });
    WebDialogResult webDialogResult = ((PXSelectBase<Dimension>) this.Header).Ask("Warning", PXMessages.LocalizeFormatNoPrefixNLA("Segment '{0}' has one or more segment values defined. Would you like to delete them?", new object[1]
    {
      (object) segmentId
    }), (MessageButtons) 3, (MessageIcon) 3);
    if (webDialogResult != 2)
    {
      if (webDialogResult != 6)
      {
        if (webDialogResult == 7)
          ;
        throw new PXException("Segment '{0}' cannot be deleted as it has one or more segment values defined.", new object[1]
        {
          (object) segmentId
        });
      }
    }
    else
      ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Segment_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    Segment row = (Segment) e.Row;
    Segment newRow = (Segment) e.NewRow;
    this.CheckSegmentValidateFieldIfNeeds(newRow);
    bool? autoNumber = newRow.AutoNumber;
    bool flag1 = false;
    if (autoNumber.GetValueOrDefault() == flag1 & autoNumber.HasValue)
    {
      short? length1 = row.Length;
      int? nullable1 = length1.HasValue ? new int?((int) length1.GetValueOrDefault()) : new int?();
      length1 = newRow.Length;
      int? nullable2 = length1.HasValue ? new int?((int) length1.GetValueOrDefault()) : new int?();
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) || row.EditMask != newRow.EditMask)
      {
        foreach (PXResult<SegmentValue> pxResult in PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Optional<Segment.dimensionID>>, And<SegmentValue.segmentID, Equal<Optional<Segment.segmentID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.DimensionID,
          (object) row.SegmentID
        }))
        {
          SegmentValue segmentValue = PXResult<SegmentValue>.op_Implicit(pxResult);
          length1 = newRow.Length;
          nullable2 = length1.HasValue ? new int?((int) length1.GetValueOrDefault()) : new int?();
          int length2 = segmentValue.Value.Length;
          if (!(nullable2.GetValueOrDefault() == length2 & nullable2.HasValue))
          {
            ((CancelEventArgs) e).Cancel = true;
            cache.RaiseExceptionHandling<Segment.length>(e.NewRow, (object) newRow.Length, (Exception) new PXSetPropertyException("Segment '{0}' has values and cannot be updated.", new object[1]
            {
              (object) row.SegmentID
            }));
            return;
          }
          bool flag2 = true;
          switch (newRow.EditMask[0])
          {
            case '9':
              if (segmentValue.Value.Any<char>((Func<char, bool>) (x => !char.IsDigit(x) && !char.IsWhiteSpace(x))))
              {
                flag2 = false;
                break;
              }
              break;
            case '?':
              if (segmentValue.Value.Any<char>((Func<char, bool>) (x => !char.IsLetter(x) && !char.IsWhiteSpace(x))))
              {
                flag2 = false;
                break;
              }
              break;
            case 'a':
              if (segmentValue.Value.Any<char>((Func<char, bool>) (x => !char.IsLetterOrDigit(x) && !char.IsWhiteSpace(x))))
              {
                flag2 = false;
                break;
              }
              break;
          }
          if (!flag2)
          {
            ((CancelEventArgs) e).Cancel = true;
            cache.RaiseExceptionHandling<Segment.editMask>(e.NewRow, (object) newRow.EditMask, (Exception) new PXSetPropertyException("Segment '{0}' has values and cannot be updated.", new object[1]
            {
              (object) row.SegmentID
            }));
            return;
          }
        }
      }
    }
    PXResultset<Segment> pxResultset = PXSelectBase<Segment, PXSelect<Segment, Where<Segment.parentDimensionID, Equal<Current<Segment.dimensionID>>, And<Segment.segmentID, Equal<Current<Segment.segmentID>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>());
    bool flag3 = false;
    bool flag4;
    short? nullable3;
    int? nullable4;
    int? nullable5;
    if (!(flag4 = row.Separator != newRow.Separator))
    {
      nullable3 = row.Align;
      int? nullable6 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      nullable3 = newRow.Align;
      nullable4 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      if (!(flag3 = !(nullable6.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable6.HasValue == nullable4.HasValue)))
      {
        nullable3 = row.CaseConvert;
        nullable4 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
        nullable3 = newRow.CaseConvert;
        nullable5 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
        if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
        {
          bool? validate1 = row.Validate;
          bool? validate2 = newRow.Validate;
          if (!(validate1.GetValueOrDefault() == validate2.GetValueOrDefault() & validate1.HasValue == validate2.HasValue))
          {
            string[] array = EnumerableExtensions.Distinct<Dimension, string>(GraphHelper.RowCast<Dimension>((IEnumerable) PXSelectBase<Dimension, PXViewOf<Dimension>.BasedOn<SelectFromBase<Dimension, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Segment>.On<BqlOperand<Dimension.dimensionID, IBqlString>.IsEqual<Segment.dimensionID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Dimension.dimensionID, InFieldClassActivated>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Dimension.parentDimensionID, Equal<BqlField<Dimension.dimensionID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Segment.parentDimensionID, IBqlString>.IsNull>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())), (Func<Dimension, string>) (dimension => dimension.DimensionID)).Select<Dimension, string>((Func<Dimension, string>) (dimension => $"'{dimension.DimensionID}'")).ToArray<string>();
            if (array.Length != 0)
            {
              string str = string.Join(", ", array);
              PXSetPropertyException propertyException = new PXSetPropertyException("The {0} segment is inherited in the following segmented keys, which this change will affect: {1}.", (PXErrorLevel) 2, new object[2]
              {
                (object) row.SegmentID,
                (object) str
              });
              cache.RaiseExceptionHandling<Segment.validate>(e.NewRow, (object) newRow.Validate, (Exception) propertyException);
              goto label_32;
            }
            goto label_32;
          }
          goto label_32;
        }
      }
    }
    if (pxResultset != null && pxResultset.Count > 0)
    {
      ((CancelEventArgs) e).Cancel = true;
      string str = string.Join(", ", EnumerableExtensions.Distinct<Segment, string>(GraphHelper.RowCast<Segment>((IEnumerable) pxResultset), (Func<Segment, string>) (x => x.DimensionID)).Select<Segment, string>((Func<Segment, string>) (x => $"'{x.DimensionID}'")));
      PXSetPropertyException propertyException = new PXSetPropertyException("Segment '{0}' cannot be updated because it has override segments in the following keys: {1}.", new object[2]
      {
        (object) row.SegmentID,
        (object) str
      });
      if (flag4)
      {
        cache.RaiseExceptionHandling<Segment.separator>(e.NewRow, (object) newRow.Separator, (Exception) propertyException);
        return;
      }
      if (flag3)
      {
        cache.RaiseExceptionHandling<Segment.align>(e.NewRow, (object) newRow.Align, (Exception) propertyException);
        return;
      }
      cache.RaiseExceptionHandling<Segment.caseConvert>(e.NewRow, (object) newRow.CaseConvert, (Exception) propertyException);
      return;
    }
label_32:
    nullable3 = row.Length;
    nullable5 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
    nullable3 = newRow.Length;
    nullable4 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
    if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue || pxResultset == null)
      return;
    foreach (PXResult<Segment> pxResult in pxResultset)
    {
      Segment segment = PXResult<Segment>.op_Implicit(pxResult);
      Segment copy = (Segment) cache.CreateCopy((object) segment);
      copy.Length = newRow.Length;
      if (((PXSelectBase<Segment>) this.Detail).Update(copy) == null)
      {
        ((CancelEventArgs) e).Cancel = true;
        cache.RaiseExceptionHandling<Segment.length>(e.NewRow, (object) newRow.Length, (Exception) new PXSetPropertyException("Segment '{0}' has values and cannot be updated.", new object[1]
        {
          (object) row.SegmentID
        }));
        break;
      }
    }
  }

  protected virtual List<KeyValuePair<string, DimensionMaint.ISegmentKeysModifier>> GetSegmentKeysModifiers()
  {
    return DimensionMaint.SegmentKeysModifiers;
  }

  private Dictionary<short?, DimensionMaint.SegmentChanges> GetChangesOfSegments()
  {
    Dictionary<short?, DimensionMaint.SegmentChanges> changesOfSegments = new Dictionary<short?, DimensionMaint.SegmentChanges>();
    if (((PXSelectBase<Dimension>) this.Header).Current.ParentDimensionID != null)
    {
      foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelectReadonly<Segment, Where<Segment.dimensionID, Equal<Required<Dimension.dimensionID>>>, OrderBy<Asc<Segment.segmentID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<Dimension>) this.Header).Current.ParentDimensionID
      }))
      {
        Segment oldValue = PXResult<Segment>.op_Implicit(pxResult);
        if (!oldValue.SegmentID.HasValue)
          throw new ArgumentNullException("SegmentID");
        changesOfSegments.Add(oldValue.SegmentID, new DimensionMaint.SegmentChanges(oldValue));
      }
    }
    foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelectReadonly<Segment, Where<Segment.dimensionID, Equal<Required<Dimension.dimensionID>>>, OrderBy<Asc<Segment.segmentID>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<Dimension>) this.Header).Current.DimensionID
    }))
    {
      Segment oldValue = PXResult<Segment>.op_Implicit(pxResult);
      if (!oldValue.SegmentID.HasValue)
        throw new ArgumentNullException("SegmentID");
      if (changesOfSegments.ContainsKey(oldValue.SegmentID))
        changesOfSegments.Remove(oldValue.SegmentID);
      changesOfSegments.Add(oldValue.SegmentID, new DimensionMaint.SegmentChanges(oldValue));
    }
    foreach (PXResult<Segment> pxResult in ((PXSelectBase<Segment>) this.Detail).Select(Array.Empty<object>()))
    {
      Segment segment = PXResult<Segment>.op_Implicit(pxResult);
      if (!segment.SegmentID.HasValue)
        throw new ArgumentNullException("SegmentID");
      DimensionMaint.SegmentChanges segmentChanges;
      if (changesOfSegments.TryGetValue(segment.SegmentID, out segmentChanges))
        segmentChanges.NewValue = segment;
    }
    return changesOfSegments;
  }

  private void CorrectChildDimensions()
  {
    Dimension current = ((PXSelectBase<Dimension>) this.Header).Current;
    if (current == null)
      return;
    string str = current.DimensionID.With<string, Dimension>((Func<string, Dimension>) (id => PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelectReadonly<Dimension>.Config>.Search<Dimension.dimensionID>((PXGraph) this, (object) id, Array.Empty<object>())))).With<Dimension, string>((Func<Dimension, string>) (dim => dim.NumberingID));
    foreach (PXResult<Dimension> pxResult in PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.parentDimensionID, Equal<Required<Dimension.parentDimensionID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.DimensionID
    }))
    {
      Dimension dimension = PXResult<Dimension>.op_Implicit(pxResult);
      dimension.Length = current.Length;
      dimension.Segments = current.Segments;
      if (string.IsNullOrEmpty(dimension.NumberingID))
        dimension.NumberingID = current.NumberingID;
      List<Exception> errors = new List<Exception>();
      string numberingId = dimension.NumberingID;
      if (numberingId == str)
      {
        dimension.NumberingID = current.NumberingID;
        this.CheckLength(((PXSelectBase) this.Header).Cache, (object) dimension, (ICollection<Exception>) errors);
        if (errors.Count > 0)
        {
          errors.Clear();
          dimension.NumberingID = numberingId;
        }
        else
          numberingId = dimension.NumberingID;
      }
      this.CheckLength(((PXSelectBase) this.Header).Cache, (object) dimension, (ICollection<Exception>) errors);
      if (errors.Count > 0 && numberingId != current.NumberingID)
      {
        int count = errors.Count;
        dimension.NumberingID = current.NumberingID;
        this.CheckLength(((PXSelectBase) this.Header).Cache, (object) dimension, (ICollection<Exception>) errors);
        if (errors.Count == count)
          errors.Clear();
        else
          dimension.NumberingID = numberingId;
      }
      if (errors.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Exception exception in errors)
          stringBuilder.AppendLine(exception.Message);
        throw new Exception(stringBuilder.ToString());
      }
      ((PXSelectBase) this.Header).Cache.Update((object) dimension);
      this.InsertNumberingValue(dimension);
    }
  }

  private void InsertNumberingValue(Dimension dimension)
  {
    foreach (Segment detail in this.GetDetails(dimension.DimensionID))
    {
      bool? nullable = detail.Inherited;
      if (!nullable.GetValueOrDefault())
      {
        nullable = detail.AutoNumber;
        if (nullable.GetValueOrDefault())
        {
          detail.EditMask = "C";
          detail.Validate = new bool?(false);
          detail.FillCharacter = " ";
          ((PXSelectBase<Segment>) this.Detail).Update(detail);
          Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) dimension.NumberingID
          }));
          bool flag = false;
          if (numbering == null)
            throw new PXException("Numbering ID must be specified if Auto Numbering is enabled for one of the Segments.");
          PXCache cach = ((PXGraph) this).Caches[typeof (SegmentValue)];
          foreach (PXResult<SegmentValue> pxResult in PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Required<SegmentValue.dimensionID>>, And<SegmentValue.segmentID, Equal<Required<SegmentValue.segmentID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) detail.DimensionID,
            (object) detail.SegmentID
          }))
          {
            SegmentValue segmentValue = PXResult<SegmentValue>.op_Implicit(pxResult);
            if (!object.Equals((object) segmentValue.Value, (object) numbering.NewSymbol))
              cach.Delete((object) segmentValue);
            else
              flag = true;
          }
          if (!flag)
            cach.Insert((object) new SegmentValue()
            {
              DimensionID = detail.DimensionID,
              SegmentID = detail.SegmentID,
              Value = numbering.NewSymbol
            });
        }
      }
    }
  }

  private void UpdateHeader(object row)
  {
    short num1 = 0;
    short num2 = 0;
    Segment segment = row as Segment;
    bool flag = false;
    if (segment.AutoNumber.Value)
    {
      foreach (Segment detail in this.GetDetails(segment.DimensionID))
      {
        bool? nullable1 = detail.Inherited;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = detail.AutoNumber;
          if (nullable1.Value)
          {
            short? segmentId = detail.SegmentID;
            int? nullable2 = segmentId.HasValue ? new int?((int) segmentId.GetValueOrDefault()) : new int?();
            segmentId = segment.SegmentID;
            int? nullable3 = segmentId.HasValue ? new int?((int) segmentId.GetValueOrDefault()) : new int?();
            if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
            {
              detail.AutoNumber = new bool?(false);
              ((PXSelectBase) this.Detail).Cache.Update((object) detail);
              flag = true;
            }
          }
        }
      }
    }
    foreach (Segment detail in this.GetDetails(segment.DimensionID))
    {
      short? nullable4;
      int? nullable5;
      int? nullable6;
      if (!detail.Inherited.GetValueOrDefault() && segment.DimensionID == "INSUBITEM")
      {
        if (((Segment) row).IsCosted.Value)
        {
          nullable4 = detail.SegmentID;
          nullable5 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
          nullable4 = ((Segment) row).SegmentID;
          nullable6 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
          if (nullable5.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable5.HasValue & nullable6.HasValue)
          {
            detail.IsCosted = new bool?(true);
            ((PXSelectBase) this.Detail).Cache.Update((object) detail);
            flag = true;
          }
        }
        else
        {
          nullable4 = detail.SegmentID;
          nullable6 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
          nullable4 = ((Segment) row).SegmentID;
          nullable5 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
          if (nullable6.GetValueOrDefault() > nullable5.GetValueOrDefault() & nullable6.HasValue & nullable5.HasValue)
          {
            detail.IsCosted = new bool?(false);
            ((PXSelectBase) this.Detail).Cache.Update((object) detail);
            flag = true;
          }
        }
      }
      ++num1;
      int num3 = (int) num2;
      nullable4 = detail.Length;
      int num4 = (int) nullable4.Value;
      num2 = (short) (num3 + num4);
    }
    Dimension current = ((PXSelectBase<Dimension>) this.Header).Current;
    current.Segments = new short?(num1);
    current.Length = new short?(num2);
    ((PXSelectBase) this.Header).Cache.Update((object) current);
    if (!flag)
      return;
    ((PXSelectBase) this.Detail).View.RequestRefresh();
  }

  private void CheckLength(PXCache cache, object row)
  {
    this.CheckLength(cache, row, (ICollection<Exception>) null);
  }

  private bool HasMaxLength(Dimension row)
  {
    return row.MaxLength.HasValue && row.MaxLength.GetValueOrDefault() != -1;
  }

  private void CheckLength(PXCache cache, object row, ICollection<Exception> errors)
  {
    Dimension row1 = row as Dimension;
    short? nullable1;
    int? nullable2;
    int? nullable3;
    if (this.HasMaxLength(row1))
    {
      nullable1 = row1.Length;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable3 = row1.MaxLength;
      if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        ((PXSelectBase) this.Header).Cache.RaiseExceptionHandling<Dimension.length>((object) row1, (object) row1.Length, (Exception) new PXSetPropertyException<Dimension.length>("The key length is greater than the maximum allowed value."));
    }
    short num1 = 0;
    foreach (Segment detail in this.GetDetails(row1.DimensionID))
    {
      int num2 = (int) num1;
      nullable1 = detail.Length;
      int num3 = (int) nullable1.Value;
      num1 = (short) (num2 + num3);
      if (detail.AutoNumber.Value)
      {
        if (row1.NumberingID == null)
        {
          if (errors == null)
            cache.RaiseExceptionHandling<Dimension.numberingID>((object) row1, (object) row1.NumberingID, (Exception) new PXSetPropertyException("Numbering ID must be specified if Auto Numbering is enabled for one of the Segments."));
          else
            errors.Add((Exception) new PXSetPropertyException("{0} - Numbering ID must be specified if Auto Numbering is enabled for one of the Segments.", new object[1]
            {
              (object) row1.DimensionID
            }));
        }
        else
        {
          foreach (PXResult<NumberingSequence> pxResult in PXSelectBase<NumberingSequence, PXSelect<NumberingSequence, Where<NumberingSequence.numberingID, Equal<Required<Dimension.numberingID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row1.NumberingID
          }))
          {
            NumberingSequence numberingSequence = PXResult<NumberingSequence>.op_Implicit(pxResult);
            int length1 = numberingSequence.StartNbr.Length;
            nullable1 = detail.Length;
            int? nullable4;
            if (!nullable1.HasValue)
            {
              nullable2 = new int?();
              nullable4 = nullable2;
            }
            else
              nullable4 = new int?((int) nullable1.GetValueOrDefault());
            nullable3 = nullable4;
            int valueOrDefault1 = nullable3.GetValueOrDefault();
            if (!(length1 == valueOrDefault1 & nullable3.HasValue))
            {
              nullable1 = row1.Length;
              int? nullable5;
              if (!nullable1.HasValue)
              {
                nullable2 = new int?();
                nullable5 = nullable2;
              }
              else
                nullable5 = new int?((int) nullable1.GetValueOrDefault());
              nullable3 = nullable5;
              int num4 = (int) num1;
              if (nullable3.GetValueOrDefault() == num4 & nullable3.HasValue)
              {
                int length2 = numberingSequence.StartNbr.Length;
                nullable1 = detail.Length;
                int? nullable6;
                if (!nullable1.HasValue)
                {
                  nullable2 = new int?();
                  nullable6 = nullable2;
                }
                else
                  nullable6 = new int?((int) nullable1.GetValueOrDefault());
                nullable3 = nullable6;
                int valueOrDefault2 = nullable3.GetValueOrDefault();
                if (length2 < valueOrDefault2 & nullable3.HasValue)
                  goto label_27;
              }
              if (errors == null)
              {
                PXCache pxCache = cache;
                Dimension dimension = row1;
                string numberingId = row1.NumberingID;
                object[] objArray = new object[1];
                nullable1 = detail.SegmentID;
                objArray[0] = (object) nullable1.ToString();
                PXSetPropertyException propertyException = new PXSetPropertyException("Numbering ID cannot be used with '{0}' segment: Ensure the segment's length matches the length of the numbering.", objArray);
                pxCache.RaiseExceptionHandling<Dimension.numberingID>((object) dimension, (object) numberingId, (Exception) propertyException);
              }
              else
              {
                ICollection<Exception> exceptions = errors;
                object[] objArray = new object[3]
                {
                  (object) row1.DimensionID.ToString(),
                  (object) row1.NumberingID.ToString(),
                  null
                };
                nullable1 = detail.SegmentID;
                objArray[2] = (object) nullable1.ToString();
                PXSetPropertyException propertyException = new PXSetPropertyException("{0} - Numbering ID '{1}' cannot be used with '{2}' segment: Ensure the segment's length matches the length of the numbering.", objArray);
                exceptions.Add((Exception) propertyException);
              }
            }
label_27:
            string str = Regex.Replace(Regex.Replace(numberingSequence.StartNbr, "[0-9]", "9"), "[^0-9]", "?");
            if (detail.EditMask == "?" && str.Contains("9") || detail.EditMask == "9" && str.Contains("?"))
            {
              if (errors == null)
              {
                PXCache pxCache = cache;
                Dimension dimension = row1;
                string numberingId = row1.NumberingID;
                object[] objArray = new object[1];
                nullable1 = detail.SegmentID;
                objArray[0] = (object) nullable1.ToString();
                PXSetPropertyException propertyException = new PXSetPropertyException("Numbering ID cannot be used with '{0}' segment: Ensure the segment's mask allows numerics.", objArray);
                pxCache.RaiseExceptionHandling<Dimension.numberingID>((object) dimension, (object) numberingId, (Exception) propertyException);
              }
              else
              {
                ICollection<Exception> exceptions = errors;
                object[] objArray = new object[3]
                {
                  (object) row1.DimensionID.ToString(),
                  (object) row1.NumberingID.ToString(),
                  null
                };
                nullable1 = detail.SegmentID;
                objArray[2] = (object) nullable1.ToString();
                PXSetPropertyException propertyException = new PXSetPropertyException("{0} - Numbering ID '{1}' cannot be used with '{2}' segment: Ensure the segment's mask allows numerics.", objArray);
                exceptions.Add((Exception) propertyException);
              }
            }
          }
        }
      }
    }
  }

  private void CheckSegmentValidateFieldIfNeeds(Segment segment)
  {
    if (!(segment.DimensionID == "SUBACCOUNT"))
      return;
    short? nullable1 = segment.ConsolNumChar;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable2.GetValueOrDefault() > num & nullable2.HasValue))
      return;
    nullable1 = segment.Length;
    nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = segment.ConsolNumChar;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) && !segment.Validate.GetValueOrDefault())
      throw new PXSetPropertyException("Select the Validate check box if the length of the subaccount segment in the consolidating company (Number of characters) differs from the length of the subaccount segment to be mapped (Length).");
  }

  private void CheckSubItems(Segment currentSegment)
  {
    if (currentSegment != null && !(currentSegment.DimensionID != "INSUBITEM") && PXResultset<INCostStatus>.op_Implicit(PXSelectBase<INCostStatus, PXSelect<INCostStatus, Where<INCostStatus.qtyOnHand, NotEqual<decimal0>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
      throw new PXSetPropertyException("Changes to the Include in Cost check box cannot be saved because quantity on hand of the subitems is non-zero.");
  }

  public static bool IsAutonumbered(PXGraph graph, string dimensionID, bool fully = true)
  {
    Dimension dimension = PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>>>.Config>.Select(graph, new object[1]
    {
      (object) dimensionID
    }));
    if (dimension.NumberingID == null)
      return false;
    Segment[] array1 = GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select(graph, new object[1]
    {
      (object) dimension.DimensionID
    })).ToArray<Segment>();
    if (dimension.ParentDimensionID != null)
    {
      Segment[] array2;
      if (array1.Length == 0)
        array2 = GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select(graph, new object[1]
        {
          (object) dimension.ParentDimensionID
        })).ToArray<Segment>();
      else
        array2 = GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>, And<Segment.segmentID, NotIn<Required<Segment.segmentID>>>>>.Config>.Select(graph, new object[2]
        {
          (object) dimension.ParentDimensionID,
          (object) ((IEnumerable<Segment>) array1).Select<Segment, short?>((Func<Segment, short?>) (s => s.SegmentID)).ToArray<short?>()
        })).ToArray<Segment>();
      array1 = ((IEnumerable<Segment>) array1).Union<Segment>((IEnumerable<Segment>) array2).ToArray<Segment>();
    }
    bool flag = fully;
    foreach (Segment segment in array1)
    {
      if (segment.AutoNumber.GetValueOrDefault())
      {
        if (!fully)
        {
          flag = true;
          break;
        }
      }
      else if (fully)
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  [PXProjection(typeof (Select4<DimensionMaint.INSubItemDup, Aggregate<GroupBy<DimensionMaint.INSubItemDup.subItemCD>>>))]
  [PXHidden]
  public class INSubItemDup : INSubItem
  {
    [PXDBCalced(typeof (Left<INSubItem.subItemCD, CurrentValue<Dimension.segments>>), typeof (string))]
    public override string SubItemCD { get; set; }

    public new abstract class subItemCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DimensionMaint.INSubItemDup.subItemCD>
    {
    }
  }

  public class NoSort : IBqlSortColumn, IBqlCreator, IBqlVerifier
  {
    public System.Type GetReferencedType() => (System.Type) null;

    public bool IsDescending => false;

    public void AppendQuery(
      Query query,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      info.SortColumns?.Add((IBqlSortColumn) this);
    }

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      info.SortColumns?.Add((IBqlSortColumn) this);
      return true;
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
    }
  }

  public class SegmentChanges
  {
    public SegmentChanges(Segment oldValue, Segment newValue)
    {
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }

    public SegmentChanges(Segment oldValue) => this.OldValue = oldValue;

    public Segment OldValue { get; }

    public Segment NewValue { get; set; }
  }

  public interface ISegmentKeysModifier
  {
    void UpdateSegment(
      PXGraph graph,
      Dictionary<short?, DimensionMaint.SegmentChanges> updatedSegments,
      Dimension header);
  }

  private class SegmentKeysModifier<TTable, TFieldId, TFieldCd> : 
    DimensionMaint.SegmentKeysModifier<TTable, TFieldId, TFieldCd, Where<True, Equal<True>>>
    where TTable : class, IBqlTable, new()
    where TFieldId : IBqlField
    where TFieldCd : IBqlField
  {
  }

  private class SegmentKeysModifier<TTable, TFieldId, TFieldCd, TWhere> : 
    DimensionMaint.ISegmentKeysModifier
    where TTable : class, IBqlTable, new()
    where TFieldId : IBqlField
    where TFieldCd : IBqlField
    where TWhere : IBqlWhere, new()
  {
    public virtual void UpdateSegment(
      PXGraph graph,
      Dictionary<short?, DimensionMaint.SegmentChanges> updatedSegments,
      Dimension header)
    {
      Dictionary<int?, string> updateKeys = new Dictionary<int?, string>();
      List<string> cantUpdateKeys = new List<string>();
      foreach (PXResult<TTable> pxResult in PXSelectBase<TTable, PXSelectReadonly<TTable>.Config>.Select(graph, Array.Empty<object>()))
      {
        TTable table = PXResult<TTable>.op_Implicit(pxResult);
        string str1 = (string) ((PXCache) GraphHelper.Caches<TTable>(graph)).GetValue((object) table, typeof (TFieldCd).Name);
        StringBuilder stringBuilder = new StringBuilder();
        int startIndex = 0;
        bool flag = false;
        if (str1 != null)
        {
          foreach (DimensionMaint.SegmentChanges segmentChanges in updatedSegments.Values)
          {
            int length1 = str1.Length;
            short? nullable1 = segmentChanges.OldValue.Length;
            int num = (int) nullable1.Value;
            int length2;
            if (length1 >= num)
            {
              nullable1 = segmentChanges.OldValue.Length;
              length2 = (int) nullable1.Value;
            }
            else
              length2 = str1.Length;
            int length3 = length2;
            Segment newValue1 = segmentChanges.NewValue;
            short? nullable2;
            if (newValue1 == null)
            {
              nullable1 = new short?();
              nullable2 = nullable1;
            }
            else
              nullable2 = newValue1.Length;
            nullable1 = nullable2;
            int valueOrDefault1 = (int) nullable1.GetValueOrDefault();
            string str2 = str1.Substring(startIndex, length3);
            if (!string.IsNullOrWhiteSpace(str2) && segmentChanges?.NewValue != null && !this.MatchMask(segmentChanges.NewValue.EditMask[0], str2))
            {
              flag = true;
              throw new PXException("Edit Mask cannot be changed. There are {0} identifier {1} to which the new edit mask cannot be applied.", new object[2]
              {
                (object) ((PXCache) GraphHelper.Caches<TTable>(graph)).DisplayName,
                (object) str1
              });
            }
            if (length3 > valueOrDefault1)
            {
              str2 = str2.TrimEnd();
              if (str2.Length > valueOrDefault1)
              {
                flag = true;
                break;
              }
            }
            string str3 = str2;
            Segment newValue2 = segmentChanges.NewValue;
            short? nullable3;
            if (newValue2 == null)
            {
              nullable1 = new short?();
              nullable3 = nullable1;
            }
            else
              nullable3 = newValue2.Length;
            nullable1 = nullable3;
            int valueOrDefault2 = (int) nullable1.GetValueOrDefault();
            string str4 = str3.PadRight(valueOrDefault2);
            startIndex += length3;
            stringBuilder.Append(str4);
            if (str1.Length <= startIndex)
              break;
          }
          string str5 = stringBuilder.ToString();
          if (flag)
          {
            cantUpdateKeys.Add($"\"{str1}\"");
            if (cantUpdateKeys.Count == 10)
              break;
          }
          else if (str1 != str5)
            updateKeys.Add((int?) ((PXCache) GraphHelper.Caches<TTable>(graph)).GetValue((object) table, typeof (TFieldId).Name), str5);
          if (updateKeys.Count >= 500)
          {
            DimensionMaint.SegmentKeysModifier<TTable, TFieldId, TFieldCd, TWhere>.TryToUpdateKeys(graph, updateKeys, cantUpdateKeys);
            updateKeys = new Dictionary<int?, string>();
          }
        }
      }
      DimensionMaint.SegmentKeysModifier<TTable, TFieldId, TFieldCd, TWhere>.TryToUpdateKeys(graph, updateKeys, cantUpdateKeys);
    }

    private static void TryToUpdateKeys(
      PXGraph graph,
      Dictionary<int?, string> updateKeys,
      List<string> cantUpdateKeys)
    {
      if (cantUpdateKeys.Any<string>())
        throw new PXException("There are {0} identifiers {1} whose length is greater than the length of the {0} segmented key. Before the length of the segmented key is decreased, the length of the existing identifiers should be decreased.", new object[2]
        {
          (object) ((PXCache) GraphHelper.Caches<TTable>(graph)).DisplayName,
          (object) string.Join(", ", cantUpdateKeys.ToArray())
        });
      foreach (KeyValuePair<int?, string> updateKey in updateKeys)
        PXUpdate<Set<TFieldCd, Required<TFieldCd>>, TTable, Where<TFieldId, Equal<Required<TFieldId>>, And<TWhere>>>.Update(graph, new object[2]
        {
          (object) updateKey.Value,
          (object) updateKey.Key
        });
    }

    protected bool MatchMask(char editMask, string value)
    {
      if (value == null)
        return false;
      value = value.TrimEnd();
      if (value.Length == 0)
        return false;
      switch (editMask)
      {
        case '9':
          if (value.Any<char>((Func<char, bool>) (x => !char.IsDigit(x) && !char.IsWhiteSpace(x))))
            return false;
          break;
        case '?':
          if (value.Any<char>((Func<char, bool>) (x => !char.IsLetter(x) && !char.IsWhiteSpace(x))))
            return false;
          break;
        case 'a':
          if (value.Any<char>((Func<char, bool>) (x => !char.IsLetterOrDigit(x) && !char.IsWhiteSpace(x))))
            return false;
          break;
      }
      return true;
    }
  }

  private class SegmentKeysModifierLight<TTable, TFieldId, TFieldCd> : 
    DimensionMaint.SegmentKeysModifier<TTable, TFieldId, TFieldCd>
    where TTable : class, IBqlTable, new()
    where TFieldId : IBqlField
    where TFieldCd : IBqlField
  {
    public override void UpdateSegment(
      PXGraph graph,
      Dictionary<short?, DimensionMaint.SegmentChanges> updatedSegments,
      Dimension header)
    {
      Dictionary<int?, string> updateKeys = new Dictionary<int?, string>();
      List<string> cantUpdateKeys = new List<string>();
      foreach (PXResult<TTable> pxResult in PXSelectBase<TTable, PXSelectReadonly<TTable>.Config>.Select(graph, Array.Empty<object>()))
      {
        TTable table = PXResult<TTable>.op_Implicit(pxResult);
        string str1 = (string) ((PXCache) GraphHelper.Caches<TTable>(graph)).GetValue((object) table, typeof (TFieldCd).Name);
        if (str1 != null)
        {
          string str2 = str1.Trim();
          int valueOrDefault = (int) header.Length.GetValueOrDefault();
          if (str2.Length > valueOrDefault)
          {
            cantUpdateKeys.Add($"\"{str2}\"");
            if (cantUpdateKeys.Count == 10)
              break;
          }
          else
          {
            string str3 = str2.PadRight(valueOrDefault);
            int startIndex = 0;
            foreach (DimensionMaint.SegmentChanges segmentChanges in updatedSegments.Values)
            {
              if (segmentChanges.NewValue != null)
              {
                int length = (int) segmentChanges.NewValue.Length.Value;
                string str4 = str3.Substring(startIndex, length);
                if (!string.IsNullOrWhiteSpace(str4) && !this.MatchMask(segmentChanges.NewValue.EditMask[0], str4))
                  throw new PXException("Edit Mask cannot be changed. There are {0} identifier {1} to which the new edit mask cannot be applied.", new object[2]
                  {
                    (object) ((PXCache) GraphHelper.Caches<TTable>(graph)).DisplayName,
                    (object) str2
                  });
                startIndex += length;
              }
            }
            int? key = (int?) ((PXCache) GraphHelper.Caches<TTable>(graph)).GetValue((object) table, typeof (TFieldId).Name);
            if (key.HasValue)
              updateKeys.Add(key, str3);
          }
          if (updateKeys.Count >= 500)
          {
            DimensionMaint.SegmentKeysModifierLight<TTable, TFieldId, TFieldCd>.TryToUpdateKeys(graph, updateKeys, cantUpdateKeys);
            updateKeys = new Dictionary<int?, string>();
          }
        }
      }
      DimensionMaint.SegmentKeysModifierLight<TTable, TFieldId, TFieldCd>.TryToUpdateKeys(graph, updateKeys, cantUpdateKeys);
    }

    private static void TryToUpdateKeys(
      PXGraph graph,
      Dictionary<int?, string> updateKeys,
      List<string> cantUpdateKeys)
    {
      if (cantUpdateKeys.Any<string>())
        throw new PXException("There are {0} identifiers {1} whose length is greater than the length of the {0} segmented key. Before the length of the segmented key is decreased, the length of the existing identifiers should be decreased.", new object[2]
        {
          (object) ((PXCache) GraphHelper.Caches<TTable>(graph)).DisplayName,
          (object) string.Join(", ", cantUpdateKeys.ToArray())
        });
      foreach (KeyValuePair<int?, string> updateKey in updateKeys)
        PXUpdate<Set<TFieldCd, Required<TFieldCd>>, TTable, Where<TFieldId, Equal<Required<TFieldId>>>>.Update(graph, new object[2]
        {
          (object) updateKey.Value,
          (object) updateKey.Key
        });
    }
  }
}
