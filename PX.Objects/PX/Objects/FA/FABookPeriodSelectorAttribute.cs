// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookPeriodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.FA.Descriptor;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class FABookPeriodSelectorAttribute : FABookPeriodIDAttribute, IPXFieldVerifyingSubscriber
{
  protected int _SelAttrIndex;

  public FABookPeriodSelectorAttribute(
    Type selectorSearchType = null,
    Type searchByDateType = null,
    Type defaultType = null,
    Type bookSourceType = null,
    bool isBookRequired = true,
    Type assetSourceType = null,
    Type dateType = null,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    Type[] fieldList = null,
    ReportParametersFlag reportParametersMask = ReportParametersFlag.None)
  {
    Type type1 = searchByDateType;
    Type type2 = defaultType;
    Type bookSourceType1 = bookSourceType;
    bool flag = isBookRequired;
    Type type3 = assetSourceType;
    Type dateType1 = dateType;
    int num = flag ? 1 : 0;
    Type assetSourceType1 = type3;
    Type branchSourceType1 = branchSourceType;
    Type branchSourceFormulaType1 = branchSourceFormulaType;
    Type organizationSourceType1 = organizationSourceType;
    Type searchByDateType1 = type1;
    Type defaultType1 = type2;
    // ISSUE: explicit constructor call
    base.\u002Ector(bookSourceType1, dateType1, num != 0, assetSourceType1, branchSourceType1, branchSourceFormulaType1, organizationSourceType1, searchByDateType1, defaultType1);
    this.DefaultType = this.GetCompleteDefaultType(defaultType, searchByDateType, selectorSearchType, dateType);
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    Type searchType = selectorSearchType;
    if ((object) searchType == null)
      searchType = this.GetDefaultSelectorSearchType();
    GenericFABookPeriodSelectorAttribute selectorAttribute = new GenericFABookPeriodSelectorAttribute(searchType, this.FABookPeriodKeyProvider, reportParametersMask, fieldList ?? this.GetDefaultFieldList());
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).Count - 1;
  }

  protected virtual Type GetDefaultSelectorSearchType()
  {
    return typeof (Search<FABookPeriod.finPeriodID, Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>);
  }

  protected virtual Type[] GetDefaultFieldList()
  {
    return new Type[2]
    {
      typeof (FABookPeriod.finPeriodID),
      typeof (FABookPeriod.descr)
    };
  }

  protected virtual Type GetCompleteDefaultType(
    Type defaultType,
    Type searchByDateType,
    Type selectorSearchType,
    Type dateType)
  {
    Type completeDefaultType = defaultType;
    if ((object) completeDefaultType != null)
      return completeDefaultType;
    Type type = searchByDateType;
    return (object) type != null ? type : this.GetSearchTypeRestrictedByDate(selectorSearchType, dateType);
  }

  protected virtual Type GetSearchTypeRestrictedByDate(Type selectorSearchType, Type dateType)
  {
    Type type = selectorSearchType;
    if ((object) type == null)
      type = this.GetDefaultSelectorSearchType();
    selectorSearchType = type;
    if (dateType != (Type) null)
    {
      Type[] typeArray = BqlCommand.Decompose((BqlCommand.CreateInstance(new Type[1]
      {
        selectorSearchType
      }) ?? throw new ArgumentNullException("BqlCommand.CreateInstance(selectorSearchType)")).WhereAnd(BqlCommand.Compose(new Type[10]
      {
        typeof (Where<,,>),
        typeof (FABookPeriod.startDate),
        typeof (LessEqual<>),
        typeof (Current<>),
        dateType,
        typeof (And<,>),
        typeof (FABookPeriod.endDate),
        typeof (Greater<>),
        typeof (Current<>),
        dateType
      })).GetSelectType());
      typeArray[0] = BqlHelper.SelectToSearch[typeArray[0]];
      typeArray[1] = typeof (FABookPeriod.finPeriodID);
      selectorSearchType = BqlCommand.Compose(typeArray);
    }
    return selectorSearchType;
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      if (this._SelAttrIndex == -1)
        return;
      ((IPXFieldVerifyingSubscriber) this._Attributes[this._SelAttrIndex]).FieldVerifying(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw;
    }
  }
}
