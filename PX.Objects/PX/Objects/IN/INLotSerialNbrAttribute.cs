// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(100, IsUnicode = true)]
[PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial")]
[PXDefault("")]
public class INLotSerialNbrAttribute : 
  PXEntityAttribute,
  IPXFieldVerifyingSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXRowPersistingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXRowSelectedSubscriber
{
  private const string _NumFormatStr = "{0}";
  protected Type _InventoryType;
  protected Type _SubItemType;
  protected Type _LocationType;
  protected Type _CostCenterType;

  public virtual bool ForceDisable { get; set; }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    PXDBStringAttribute dbAttribute = (PXDBStringAttribute) this.DBAttribute;
    if (dbAttribute == null)
      return;
    dbAttribute.InputMask = new string('C', dbAttribute.Length);
    dbAttribute.PromptChar = new char?(' ');
  }

  public INLotSerialNbrAttribute()
  {
  }

  public INLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType)
  {
    Type itemType = BqlCommand.GetItemType(InventoryType);
    if (!typeof (ILSMaster).IsAssignableFrom(itemType))
      throw new PXArgumentException("itemType", "The specified type {0} must implement the {1} interface.", new object[2]
      {
        (object) MainTools.GetLongName(itemType),
        (object) MainTools.GetLongName(typeof (ILSMaster))
      });
    this._InventoryType = InventoryType;
    this._SubItemType = SubItemType;
    this._LocationType = LocationType;
    this._CostCenterType = CostCenterType;
    this.InitializeSelector(this.GetLotSerialSearch(InventoryType, SubItemType, LocationType, CostCenterType), typeof (INLotSerialStatusByCostCenter.lotSerialNbr), typeof (INLotSerialStatusByCostCenter.siteID), typeof (INLotSerialStatusByCostCenter.locationID), typeof (INLotSerialStatusByCostCenter.qtyOnHand), typeof (INLotSerialStatusByCostCenter.qtyAvail), typeof (INLotSerialStatusByCostCenter.expireDate));
  }

  public INLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type ParentLotSerialNbrType,
    Type CostCenterType)
    : this(InventoryType, SubItemType, LocationType, CostCenterType)
  {
    this.InitializeDefault(ParentLotSerialNbrType);
  }

  protected virtual Type GetLotSerialSearch(
    Type inventoryType,
    Type subItemType,
    Type locationType,
    Type costCenterType)
  {
    Type type = typeof (IConstant).IsAssignableFrom(costCenterType) ? costCenterType : BqlTemplate.FromType(typeof (Optional<BqlPlaceholder.D>)).Replace<BqlPlaceholder.D>(costCenterType).ToType();
    return ((IBqlTemplate) BqlTemplate.OfCommand<Search<INLotSerialStatusByCostCenter.lotSerialNbr, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Optional<BqlPlaceholder.A>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Optional<BqlPlaceholder.B>>, And<INLotSerialStatusByCostCenter.locationID, Equal<Optional<BqlPlaceholder.C>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<BqlPlaceholder.D>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>>>>>>>>.Replace<BqlPlaceholder.A>(inventoryType).Replace<BqlPlaceholder.B>(subItemType).Replace<BqlPlaceholder.C>(locationType).Replace<BqlPlaceholder.D>(type)).ToType();
  }

  protected virtual Type GetIntransitLotSerialSearch(
    Type inventoryType,
    Type subItemType,
    Type locationType,
    Type costCenterType,
    Type tranType,
    Type transferNbrType,
    Type transferLineNbrType)
  {
    Type type = typeof (IConstant).IsAssignableFrom(costCenterType) ? costCenterType : BqlTemplate.FromType(typeof (Optional<BqlPlaceholder.G>)).Replace<BqlPlaceholder.G>(costCenterType).ToType();
    return ((IBqlTemplate) BqlTemplate.OfCommand<Search2<INLotSerialStatusByCostCenter.lotSerialNbr, LeftJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLotSerialStatusByCostCenter.locationID>, And<Optional<BqlPlaceholder.A>, Equal<INTranType.transfer>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Optional<BqlPlaceholder.B>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Optional<BqlPlaceholder.C>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<BqlPlaceholder.G>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>, And<Where<Optional<BqlPlaceholder.A>, NotEqual<INTranType.transfer>, And<INLotSerialStatusByCostCenter.locationID, Equal<Optional<BqlPlaceholder.D>>, Or<INTransitLine.transferNbr, Equal<Optional<BqlPlaceholder.E>>, And<INTransitLine.transferLineNbr, Equal<Optional<BqlPlaceholder.F>>>>>>>>>>>>>.Replace<BqlPlaceholder.A>(tranType).Replace<BqlPlaceholder.B>(inventoryType).Replace<BqlPlaceholder.C>(subItemType).Replace<BqlPlaceholder.D>(locationType).Replace<BqlPlaceholder.E>(transferNbrType).Replace<BqlPlaceholder.F>(transferLineNbrType).Replace<BqlPlaceholder.G>(type)).ToType();
  }

  protected virtual void InitializeSelector(Type searchType, params Type[] fieldList)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(searchType, fieldList));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  protected virtual void InitializeDefault(Type parentLotSerialNbrType)
  {
    ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex] = (PXEventSubscriberAttribute) new PXDefaultAttribute(parentLotSerialNbrType)
    {
      PersistingCheck = (PXPersistingCheck) 1
    };
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber) || typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber) || typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber))
      subscribers.Add(this as ISubscriber);
    else if (typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber))
    {
      base.GetSubscriber<ISubscriber>(subscribers);
      subscribers.Remove(this as ISubscriber);
      subscribers.Add(this as ISubscriber);
      subscribers.Reverse();
    }
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID);
    if (pxResult == null || !((ILSMaster) e.Row).SubItemID.HasValue || !((ILSMaster) e.Row).LocationID.HasValue)
      return;
    short? invtMult;
    if (PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "N" && !string.IsNullOrEmpty((string) e.NewValue))
    {
      invtMult = ((ILSMaster) e.Row).InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        this.RaiseFieldIsDisabledException();
    }
    Decimal result = ((ILSMaster) e.Row).Qty.GetValueOrDefault();
    object valuePending = sender.GetValuePending(e.Row, "Qty");
    if (valuePending != null && valuePending != PXCache.NotSetValue)
      Decimal.TryParse(valuePending.ToString(), out result);
    invtMult = ((ILSMaster) e.Row).InvtMult;
    Decimal? nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = result;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
    Decimal num2 = 0M;
    bool flag = nullable2.GetValueOrDefault() < num2 & nullable2.HasValue;
    if (!(PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack != "N" & flag) || !(PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerAssign == "R") || string.IsNullOrEmpty((string) e.NewValue))
      return;
    ((IPXFieldVerifyingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).FieldVerifying(sender, e);
  }

  protected virtual void RaiseFieldIsDisabledException()
  {
    throw new PXSetPropertyException("The field {0} cannot be updated in this record because the field is disabled.", new object[1]
    {
      (object) this.FieldName
    });
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID);
    if (pxResult == null || !((PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack ?? "N") == "N"))
      return;
    ((IPXFieldDefaultingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex]).FieldDefaulting(sender, e);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID);
    if (pxResult == null || !((ILSMaster) e.Row).SubItemID.HasValue || !((ILSMaster) e.Row).LocationID.HasValue)
      return;
    ILSMaster row = (ILSMaster) e.Row;
    INLotSerClass lotSerClass = PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = ((ILSMaster) e.Row).TranType;
    short? invtMult = ((ILSMaster) e.Row).InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (!this.IsTracked(row, lotSerClass, tranType, invMult))
      return;
    Decimal? qty = ((ILSMaster) e.Row).Qty;
    Decimal num = 0M;
    if (qty.GetValueOrDefault() == num & qty.HasValue)
      return;
    ((IPXRowPersistingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex]).RowPersisting(sender, e);
  }

  protected virtual PXResult<InventoryItem, INLotSerClass> ReadInventoryItem(
    PXCache sender,
    int? InventoryID)
  {
    InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, InventoryID);
    if (inventoryItem == null)
      return (PXResult<InventoryItem, INLotSerClass>) null;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem.LotSerClassID);
    return new PXResult<InventoryItem, INLotSerClass>(inventoryItem, inLotSerClass ?? new INLotSerClass());
  }

  protected virtual bool IsTracked(
    ILSMaster row,
    INLotSerClass lotSerClass,
    string tranType,
    int? invMult)
  {
    if (lotSerClass.LotSerAssign == "U")
    {
      int? nullable = invMult;
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue && row.IsIntercompany.GetValueOrDefault())
        tranType = "TRX";
    }
    return INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXCache.TryDispose((object) sender.GetAttributes(e.Row, ((PXEventSubscriberAttribute) this)._FieldName));
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ILSMaster row1 = (ILSMaster) e.Row;
    if (row1 == null)
      return;
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, row1.InventoryID);
    PXUIFieldAttribute attribute = (PXUIFieldAttribute) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex];
    int num;
    if (!this.ForceDisable && pxResult != null && sender.AllowUpdate)
    {
      ILSMaster row2 = row1;
      INLotSerClass lotSerClass = PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult);
      string tranType = row1.TranType;
      short? invtMult = row1.InvtMult;
      int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      num = this.IsTracked(row2, lotSerClass, tranType, invMult) ? 1 : 0;
    }
    else
      num = 0;
    attribute.Enabled = num != 0;
  }

  public static string MakeFormatStr(PXCache sender, INLotSerClass lsclass)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (lsclass != null)
    {
      foreach (PXResult<INLotSerSegment> pxResult in PXSelectBase<INLotSerSegment, PXSelect<INLotSerSegment, Where<INLotSerSegment.lotSerClassID, Equal<Required<INLotSerSegment.lotSerClassID>>>, OrderBy<Asc<INLotSerSegment.lotSerClassID, Asc<INLotSerSegment.segmentID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) lsclass.LotSerClassID
      }))
      {
        INLotSerSegment inLotSerSegment = PXResult<INLotSerSegment>.op_Implicit(pxResult);
        string segmentType = inLotSerSegment.SegmentType;
        if (segmentType != null && segmentType.Length == 1)
        {
          switch (segmentType[0])
          {
            case 'A':
              stringBuilder.Append("{0:MMM}");
              continue;
            case 'C':
              stringBuilder.Append(inLotSerSegment.SegmentValue);
              continue;
            case 'D':
              stringBuilder.Append("{0");
              if (!string.IsNullOrEmpty(inLotSerSegment.SegmentValue))
                stringBuilder.Append(":").Append(inLotSerSegment.SegmentValue);
              stringBuilder.Append("}");
              continue;
            case 'L':
              stringBuilder.Append("{0:yyyy}");
              continue;
            case 'M':
              stringBuilder.Append("{0:MM}");
              continue;
            case 'N':
              stringBuilder.Append("{1}");
              continue;
            case 'U':
              stringBuilder.Append("{0:dd}");
              continue;
            case 'Y':
              stringBuilder.Append("{0:yy}");
              continue;
          }
        }
        throw new PXException();
      }
    }
    return stringBuilder.ToString();
  }

  public static ILotSerNumVal ReadLotSerNumVal(
    PXGraph graph,
    PXResult<InventoryItem, INLotSerClass> item)
  {
    if (item == null || PXResult<InventoryItem, INLotSerClass>.op_Implicit(item) == null)
      return (ILotSerNumVal) null;
    return PXResult<InventoryItem, INLotSerClass>.op_Implicit(item).LotSerNumShared.GetValueOrDefault() ? (ILotSerNumVal) INLotSerClassLotSerNumVal.PK.FindDirty(graph, PXResult<InventoryItem, INLotSerClass>.op_Implicit(item).LotSerClassID) : (ILotSerNumVal) InventoryItemLotSerNumVal.PK.FindDirty(graph, PXResult<InventoryItem, INLotSerClass>.op_Implicit(item).InventoryID);
  }

  /// <summary>Return the length of auto-incremental number</summary>
  /// <param name="lotSerNum">auto-incremental number value</param>
  /// <returns></returns>
  public static int GetNumberLength(ILotSerNumVal lotSerNum)
  {
    return lotSerNum != null && !string.IsNullOrEmpty(lotSerNum.LotSerNumVal) ? lotSerNum.LotSerNumVal.Length : 6;
  }

  /// <summary>Return default(empty) auto-incremental number value</summary>
  /// <param name="sender">cache</param>
  /// <param name="lsClass">Lot/Ser class</param>
  /// <param name="lotSerNum">Auto-incremental number value</param>
  /// <returns></returns>
  public static string GetNextNumber(
    PXCache sender,
    INLotSerClass lsClass,
    ILotSerNumVal lotSerNum)
  {
    string str = new string('0', INLotSerialNbrAttribute.GetNumberLength(lotSerNum));
    return string.Format(lsClass.LotSerFormatStr, (object) sender.Graph.Accessinfo.BusinessDate, (object) str).ToUpper();
  }

  /// <summary>Return  auto-incremental number format</summary>
  /// <param name="lsClass">Lot/Ser class</param>
  /// <param name="lotSerNum">Auto-incremental number value</param>
  /// <returns></returns>
  public static string GetNextFormat(INLotSerClass lsClass, ILotSerNumVal lotSerNum)
  {
    string newValue = $"{{1:{new string('0', INLotSerialNbrAttribute.GetNumberLength(lotSerNum))}}}";
    return lsClass.LotSerFormatStr.Replace("{1}", newValue);
  }

  /// <summary>Return shared Lot\Ser Class identifier</summary>
  /// <param name="lsClass">Lot/Ser class</param>
  /// <returns></returns>
  public static string GetNextClassID(INLotSerClass lsClass)
  {
    return !lsClass.LotSerNumShared.Value ? (string) null : lsClass.LotSerClassID;
  }

  /// <summary>Return auto-incremental number mask</summary>
  /// <param name="sender">cache</param>
  /// <param name="lsClass">Lot/Ser class</param>
  /// <param name="lotSerNum">Auto-incremental number value</param>
  /// <returns></returns>
  public static string GetDisplayMask(
    PXCache sender,
    INLotSerClass lsClass,
    ILotSerNumVal lotSerNum)
  {
    if (lsClass == null)
      return (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PXResult<INLotSerSegment> pxResult in PXSelectBase<INLotSerSegment, PXSelect<INLotSerSegment, Where<INLotSerSegment.lotSerClassID, Equal<Required<INLotSerSegment.lotSerClassID>>>, OrderBy<Asc<INLotSerSegment.lotSerClassID, Asc<INLotSerSegment.segmentID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) lsClass.LotSerClassID
    }))
    {
      INLotSerSegment inLotSerSegment = PXResult<INLotSerSegment>.op_Implicit(pxResult);
      string segmentType = inLotSerSegment.SegmentType;
      if (segmentType != null && segmentType.Length == 1)
      {
        switch (segmentType[0])
        {
          case 'A':
            stringBuilder.Append(new string('C', 3));
            continue;
          case 'C':
            stringBuilder.Append(!string.IsNullOrEmpty(inLotSerSegment.SegmentValue) ? new string('C', inLotSerSegment.SegmentValue.Length) : string.Empty);
            continue;
          case 'D':
            stringBuilder.Append(!string.IsNullOrEmpty(inLotSerSegment.SegmentValue) ? new string('C', inLotSerSegment.SegmentValue.Length) : new string('C', $"{sender.Graph.Accessinfo.BusinessDate}".Length));
            continue;
          case 'L':
            stringBuilder.Append(new string('9', 4));
            continue;
          case 'M':
          case 'U':
          case 'Y':
            stringBuilder.Append(new string('9', 2));
            continue;
          case 'N':
            stringBuilder.Append(new string('9', INLotSerialNbrAttribute.GetNumberLength(lotSerNum)));
            continue;
        }
      }
      throw new INLotSerialNbrAttribute.PXUnknownSegmentTypeException();
    }
    return stringBuilder.ToString();
  }

  /// <summary>Get auto-incremantal number parts settings</summary>
  /// <param name="sender">cache</param>
  /// <param name="lsclass">Lot/Ser class</param>
  /// <param name="lotSerNum">auto-incremantal number value</param>
  /// <returns></returns>
  public static INLotSerialNbrAttribute.LSParts GetLSParts(
    PXCache sender,
    INLotSerClass lsclass,
    ILotSerNumVal lotSerNum)
  {
    if (lsclass == null)
      return (INLotSerialNbrAttribute.LSParts) null;
    int flen = 0;
    int nlen = 0;
    int llen = 0;
    foreach (PXResult<INLotSerSegment> pxResult in PXSelectBase<INLotSerSegment, PXSelect<INLotSerSegment, Where<INLotSerSegment.lotSerClassID, Equal<Required<INLotSerSegment.lotSerClassID>>>, OrderBy<Asc<INLotSerSegment.lotSerClassID, Asc<INLotSerSegment.segmentID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) lsclass.LotSerClassID
    }))
    {
      INLotSerSegment inLotSerSegment = PXResult<INLotSerSegment>.op_Implicit(pxResult);
      int num = 0;
      string segmentType = inLotSerSegment.SegmentType;
      if (segmentType != null && segmentType.Length == 1)
      {
        switch (segmentType[0])
        {
          case 'A':
            num = 3;
            break;
          case 'C':
            num = inLotSerSegment.SegmentValue.Length;
            break;
          case 'D':
            num = !string.IsNullOrEmpty(inLotSerSegment.SegmentValue) ? inLotSerSegment.SegmentValue.Length : $"{sender.Graph.Accessinfo.BusinessDate}".Length;
            break;
          case 'L':
            num = 4;
            break;
          case 'M':
          case 'U':
          case 'Y':
            num = 2;
            break;
          case 'N':
            nlen = INLotSerialNbrAttribute.GetNumberLength(lotSerNum);
            break;
          default:
            goto label_12;
        }
        if (nlen == 0)
        {
          flen += num;
          continue;
        }
        llen += num;
        continue;
      }
label_12:
      throw new INLotSerialNbrAttribute.PXUnknownSegmentTypeException();
    }
    return new INLotSerialNbrAttribute.LSParts(flen, nlen, llen);
  }

  /// <summary>
  /// Return the new child(detail) object with filled properties for further generation of lot/ser number
  /// </summary>
  /// <typeparam name="TLSDetail"></typeparam>
  /// <param name="sender"></param>
  /// <param name="lsClass"></param>
  /// <param name="lotSerNum"></param>
  /// <returns></returns>
  public static TLSDetail GetNextSplit<TLSDetail>(
    PXCache sender,
    INLotSerClass lsClass,
    ILotSerNumVal lotSerNum)
    where TLSDetail : class, ILSDetail, new()
  {
    TLSDetail nextSplit = new TLSDetail();
    nextSplit.LotSerialNbr = INLotSerialNbrAttribute.GetNextNumber(sender, lsClass, lotSerNum);
    nextSplit.AssignedNbr = INLotSerialNbrAttribute.GetNextFormat(lsClass, lotSerNum);
    nextSplit.LotSerClassID = INLotSerialNbrAttribute.GetNextClassID(lsClass);
    if (nextSplit is ILSGeneratedDetail lsGeneratedDetail)
    {
      bool? nullable = new bool?(lsClass.AutoNextNbr.GetValueOrDefault() && !string.IsNullOrEmpty(nextSplit.LotSerialNbr));
      lsGeneratedDetail.HasGeneratedLotSerialNbr = nullable;
    }
    return nextSplit;
  }

  public static string MakeNumber(string FormatStr, string NumberStr, DateTime date)
  {
    if (!FormatStr.Contains("{0}"))
      return string.Format(FormatStr, (object) date, (object) 0).ToUpper();
    string str = new string('0', NumberStr.Length - FormatStr.Length + "{0}".Length);
    return string.Format(FormatStr, (object) date, (object) str).ToUpper();
  }

  public static bool StringsEqual(string FormatStr, string NumberStr)
  {
    int num1 = 0;
    for (int index1 = 0; index1 < FormatStr.Length; ++index1)
    {
      if (FormatStr[index1] == '{' && index1 + 5 <= FormatStr.Length && FormatStr[index1 + 2] == ':')
      {
        int num2 = FormatStr.IndexOf("}", index1 + 3);
        if (num2 != -1)
        {
          int num3 = num2 - index1 - 3;
          if (FormatStr[index1 + 1] == '1')
          {
            if (NumberStr.Length < num1 + num3)
              return false;
            for (int index2 = 0; index2 < num3; ++index2)
            {
              if (NumberStr[num1 + index2] != '0')
                return false;
            }
          }
          num1 += num3;
          index1 = num2;
          if (index1 >= FormatStr.Length - 1 && num1 < NumberStr.Length)
            return false;
          continue;
        }
      }
      if (NumberStr == null || NumberStr.Length <= num1 || (int) char.ToUpper(FormatStr[index1]) != (int) char.ToUpper(NumberStr[num1++]))
        return false;
    }
    return true;
  }

  public static string UpdateNumber(string FormatStr, string NumberStr, string number)
  {
    int startIndex = 0;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < FormatStr.Length; ++index)
    {
      if (FormatStr[index] == '{' && index + 5 <= FormatStr.Length && FormatStr[index + 2] == ':')
      {
        int num = FormatStr.IndexOf("}", index + 3);
        if (num != -1)
        {
          int length = num - index - 3;
          if (FormatStr[index + 1] == '1')
            stringBuilder.Append(number);
          else
            stringBuilder.Append(NumberStr.Substring(startIndex, length));
          startIndex += length;
          index = num;
          continue;
        }
      }
      if (NumberStr.Length > startIndex)
        stringBuilder.Append(NumberStr[startIndex++]);
      else
        break;
    }
    return stringBuilder.ToString().ToUpper();
  }

  public static List<TLSDetail> CreateNumbers<TLSDetail>(
    PXCache sender,
    PXResult<InventoryItem, INLotSerClass> item,
    INLotSerTrack.Mode mode,
    Decimal count)
    where TLSDetail : class, ILSDetail, new()
  {
    return INLotSerialNbrAttribute.CreateNumbers<TLSDetail>(sender, PXResult<InventoryItem, INLotSerClass>.op_Implicit(item), INLotSerialNbrAttribute.ReadLotSerNumVal(sender.Graph, item), mode, false, count);
  }

  /// <summary>
  /// Return child(detail) objects list with filled properties for further generation of lot/ser number
  /// </summary>
  /// <typeparam name="TLSDetail">child(detail) entity type</typeparam>
  /// <param name="sender">cache</param>
  /// <param name="lsClass">Lot/Ser class</param>
  /// <param name="lotSerNum">Auto-incremental number value</param>
  /// <param name="mode">Track mode</param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static List<TLSDetail> CreateNumbers<TLSDetail>(
    PXCache sender,
    INLotSerClass lsClass,
    ILotSerNumVal lotSerNum,
    INLotSerTrack.Mode mode,
    Decimal count)
    where TLSDetail : class, ILSDetail, new()
  {
    return INLotSerialNbrAttribute.CreateNumbers<TLSDetail>(sender, lsClass, lotSerNum, mode, false, count);
  }

  /// <summary>
  /// Return child(detail) objects list with filled properties for further generation of lot/ser number
  /// </summary>
  /// <typeparam name="TLSDetail">child(detail) entity type</typeparam>
  /// <param name="sender">cache</param>
  /// <param name="lsClass">Lot/Ser class</param>
  /// <param name="lotSerNum">Auto-incremental number value</param>
  /// <param name="mode">Track mode</param>
  /// <param name="ForceAutoNextNbr"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static List<TLSDetail> CreateNumbers<TLSDetail>(
    PXCache sender,
    INLotSerClass lsClass,
    ILotSerNumVal lotSerNum,
    INLotSerTrack.Mode mode,
    bool ForceAutoNextNbr,
    Decimal count)
    where TLSDetail : class, ILSDetail, new()
  {
    List<TLSDetail> numbers = new List<TLSDetail>();
    if (lsClass != null)
    {
      string str = (mode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None ? lsClass.LotSerTrack : "N";
      bool flag = (mode & INLotSerTrack.Mode.Manual) == INLotSerTrack.Mode.None && lsClass.AutoNextNbr.GetValueOrDefault() | ForceAutoNextNbr;
      switch (str)
      {
        case "N":
          TLSDetail lsDetail = new TLSDetail();
          lsDetail.AssignedNbr = string.Empty;
          lsDetail.LotSerialNbr = string.Empty;
          lsDetail.LotSerClassID = string.Empty;
          if (lsDetail is ILSGeneratedDetail lsGeneratedDetail)
            lsGeneratedDetail.HasGeneratedLotSerialNbr = new bool?(false);
          numbers.Add(lsDetail);
          break;
        case "L":
          if (flag)
          {
            numbers.Add(INLotSerialNbrAttribute.GetNextSplit<TLSDetail>(sender, lsClass, lotSerNum));
            break;
          }
          break;
        case "S":
          if (flag)
          {
            for (int index = 0; index < (int) count; ++index)
              numbers.Add(INLotSerialNbrAttribute.GetNextSplit<TLSDetail>(sender, lsClass, lotSerNum));
            break;
          }
          break;
      }
    }
    return numbers;
  }

  public static INLotSerTrack.Mode TranTrackMode(
    INLotSerClass lotSerClass,
    string tranType,
    int? invMult)
  {
    if (lotSerClass == null || lotSerClass.LotSerTrack == null || lotSerClass.LotSerTrack == "N" || tranType == null || tranType.Length != 3)
      return INLotSerTrack.Mode.None;
    switch (tranType[2])
    {
      case 'A':
        if (tranType == "RCA")
          goto label_35;
        goto default;
      case 'C':
        if (tranType == "ASC" || tranType == "NSC")
          goto label_35;
        goto default;
      case 'I':
        if (tranType == "III")
          break;
        goto default;
      case 'J':
        if (tranType == "ADJ")
          goto label_35;
        goto default;
      case 'M':
        switch (tranType)
        {
          case "DRM":
            break;
          case "CRM":
            goto label_43;
          default:
            goto label_44;
        }
        break;
      case 'P':
        if (tranType == "RCP" && !(lotSerClass.LotSerAssign == "U"))
          return INLotSerTrack.Mode.Create;
        goto default;
      case 'T':
        if (tranType == "RET")
          goto label_43;
        goto default;
      case 'V':
        if (tranType == "INV")
          break;
        goto default;
      case 'X':
        if (tranType == "TRX" && !(lotSerClass.LotSerAssign == "U"))
          return INLotSerTrack.Mode.Issue;
        goto default;
      case 'Y':
        switch (tranType)
        {
          case "DSY":
            if (lotSerClass.LotSerAssign == "U")
              return INLotSerTrack.Mode.None;
            if (invMult.GetValueOrDefault() == 1)
              return INLotSerTrack.Mode.Create;
            return invMult.GetValueOrDefault() != -1 ? INLotSerTrack.Mode.Manual : INLotSerTrack.Mode.Issue;
          case "ASY":
            if (invMult.GetValueOrDefault() == -1)
              return !(lotSerClass.LotSerAssign == "U") ? INLotSerTrack.Mode.Issue : INLotSerTrack.Mode.Create;
            if (invMult.GetValueOrDefault() != 1)
              return INLotSerTrack.Mode.Manual;
            return !(lotSerClass.LotSerAssign == "U") ? INLotSerTrack.Mode.Create : INLotSerTrack.Mode.None;
          default:
            goto label_44;
        }
      default:
label_44:
        return INLotSerTrack.Mode.None;
    }
    if (lotSerClass.LotSerAssign == "U")
      return INLotSerTrack.Mode.Create;
    return invMult.GetValueOrDefault() != 1 ? INLotSerTrack.Mode.Issue : INLotSerTrack.Mode.Create | INLotSerTrack.Mode.Manual;
label_35:
    if (lotSerClass.LotSerAssign == "U")
      return INLotSerTrack.Mode.None;
    if (invMult.GetValueOrDefault() == 1)
      return INLotSerTrack.Mode.Create | INLotSerTrack.Mode.Manual;
    return invMult.GetValueOrDefault() != -1 ? INLotSerTrack.Mode.Manual : INLotSerTrack.Mode.Issue | INLotSerTrack.Mode.Manual;
label_43:
    return INLotSerTrack.Mode.Create | INLotSerTrack.Mode.Manual;
  }

  public static bool IsTrack(INLotSerClass lotSerClass, string tranType, int? invMult)
  {
    return INLotSerialNbrAttribute.TranTrackMode(lotSerClass, tranType, invMult) != 0;
  }

  public static bool IsTrackExpiration(INLotSerClass lotSerClass, string tranType, int? invMult)
  {
    return lotSerClass != null && lotSerClass.LotSerTrackExpiration.GetValueOrDefault() && INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult);
  }

  public static bool IsTrackSerial(INLotSerClass lotSerClass, string tranType, int? invMult)
  {
    return lotSerClass?.LotSerTrack == "S" && INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult);
  }

  public static bool IsTrackLot(INLotSerClass lotSerClass, string tranType, int? invMult)
  {
    return lotSerClass?.LotSerTrack == "L" && INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult);
  }

  protected class PXUnknownSegmentTypeException : PXException
  {
    public PXUnknownSegmentTypeException()
      : base("Unknown segment type")
    {
    }

    public PXUnknownSegmentTypeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  public class LSParts
  {
    private readonly int _flen;
    private readonly int _nlen;
    private readonly int _llen;

    public LSParts(int flen, int nlen, int llen)
    {
      this._flen = flen;
      this._nlen = nlen;
      this._llen = llen;
    }

    public int flen => this._flen;

    public int nlen => this._nlen;

    public int llen => this._llen;

    public int len => this._flen + this._nlen + this._llen;

    public int nidx => this._flen;

    public int lidx => this._flen + this._nlen;
  }
}
