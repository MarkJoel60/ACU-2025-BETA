// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationDefinitionMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.CM;

[Serializable]
public class TranslationDefinitionMaint : PXGraph<
#nullable disable
TranslationDefinitionMaint, TranslDef>
{
  public PXSelect<TranslDef> TranslDefRecords;
  public PXSelect<TranslDefDet, Where<TranslDefDet.translDefId, Equal<Current<TranslDef.translDefId>>>, OrderBy<Asc<TranslDefDet.accountIdFrom, Asc<TranslDefDet.subIdFrom>>>> TranslDefDetailsRecords;
  public PXSetup<PX.Objects.CM.CMSetup> CMSetup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  public TranslationDefinitionMaint()
  {
    PX.Objects.CM.CMSetup current = ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      throw new Exception("Multi-Currency is not activated");
  }

  public virtual bool CheckDetail(
    PXCache cache,
    TranslDefDet newRow,
    bool active,
    int destLedgerId,
    TranslDef def,
    Exception e)
  {
    bool flag = true;
    if (!newRow.AccountIdFrom.HasValue)
    {
      cache.RaiseExceptionHandling<TranslDefDet.accountIdFrom>((object) newRow, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[accountIdFrom]"
      }));
      flag = false;
    }
    if (!newRow.AccountIdTo.HasValue)
    {
      cache.RaiseExceptionHandling<TranslDefDet.accountIdTo>((object) newRow, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[accountIdTo]"
      }));
      flag = false;
    }
    int? nullable1 = newRow.AccountIdFrom;
    int? nullable2 = newRow.AccountIdTo;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = newRow.SubIdFrom;
      if (nullable2.HasValue)
      {
        nullable2 = newRow.SubIdTo;
        if (nullable2.HasValue)
          goto label_13;
      }
      nullable2 = newRow.SubIdFrom;
      if (!nullable2.HasValue)
      {
        nullable2 = newRow.SubIdTo;
        if (!nullable2.HasValue)
          goto label_13;
      }
      nullable2 = newRow.SubIdFrom;
      nullable2 = nullable2.HasValue ? newRow.SubIdTo : throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (TranslDefDet.subIdFrom).Name
      });
      if (!nullable2.HasValue)
        throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) typeof (TranslDefDet.subIdTo).Name
        });
    }
label_13:
    string valueExt1 = (string) ((PXSelectBase<TranslDefDet>) this.TranslDefDetailsRecords).GetValueExt<TranslDefDet.accountIdFrom>(newRow);
    string valueExt2 = (string) ((PXSelectBase<TranslDefDet>) this.TranslDefDetailsRecords).GetValueExt<TranslDefDet.subIdFrom>(newRow);
    string valueExt3 = (string) ((PXSelectBase<TranslDefDet>) this.TranslDefDetailsRecords).GetValueExt<TranslDefDet.accountIdTo>(newRow);
    string valueExt4 = (string) ((PXSelectBase<TranslDefDet>) this.TranslDefDetailsRecords).GetValueExt<TranslDefDet.subIdTo>(newRow);
    nullable2 = newRow.AccountIdFrom;
    nullable1 = newRow.AccountIdTo;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && string.Compare(valueExt2, valueExt4) == 1 || string.Compare(valueExt1, valueExt3) == 1)
      throw new PXSetPropertyException("Invalid Combination of Accounts and Subaccounts");
    if (flag)
    {
      nullable1 = newRow.LineNbr;
      if (nullable1.HasValue && active)
      {
        foreach (PXResult<TranslDefDet, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo> pxResult in PXSelectBase<TranslDefDet, PXSelectJoin<TranslDefDet, InnerJoin<TranslationDefinitionMaint.AccountFrom, On<TranslDefDet.accountIdFrom, Equal<TranslationDefinitionMaint.AccountFrom.accountID>>, InnerJoin<TranslationDefinitionMaint.AccountTo, On<TranslDefDet.accountIdTo, Equal<TranslationDefinitionMaint.AccountTo.accountID>>, LeftJoin<TranslationDefinitionMaint.SubFrom, On<TranslDefDet.subIdFrom, Equal<TranslationDefinitionMaint.SubFrom.subID>>, LeftJoin<TranslationDefinitionMaint.SubTo, On<TranslDefDet.subIdTo, Equal<TranslationDefinitionMaint.SubTo.subID>>>>>>, Where<TranslDefDet.translDefId, Equal<Required<TranslDefDet.translDefId>>, And<TranslDefDet.lineNbr, NotEqual<Required<TranslDefDet.lineNbr>>, And<Where<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, LessEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And<TranslationDefinitionMaint.AccountFrom.accountCD, LessEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, LessEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, LessEqual<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, LessEqual<TranslationDefinitionMaint.AccountTo.accountCD>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And2<Where<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNull, Or<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, LessEqual<TranslationDefinitionMaint.SubTo.subCD>>>>, Or<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountFrom.accountCD>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Equal<TranslationDefinitionMaint.AccountFrom.accountCD>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And2<Where<TranslationDefinitionMaint.SubFrom.subCD, IsNull, Or<TranslationDefinitionMaint.SubFrom.subCD, IsNotNull, And<TranslationDefinitionMaint.SubFrom.subCD, LessEqual<Required<TranslationDefinitionMaint.SubTo.subCD>>>>>, Or<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And2<Where<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNull, Or<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNotNull, And<TranslationDefinitionMaint.SubFrom.subCD, LessEqual<Required<TranslationDefinitionMaint.SubTo.subCD>>>>>, Or<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And2<Where<TranslationDefinitionMaint.SubTo.subCD, IsNull, Or<TranslationDefinitionMaint.SubTo.subCD, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, LessEqual<TranslationDefinitionMaint.SubTo.subCD>>>>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, Greater<TranslationDefinitionMaint.AccountFrom.accountCD>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Greater<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, Or<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountFrom.accountCD>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Greater<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, Greater<TranslationDefinitionMaint.AccountFrom.accountCD>, Or<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Less<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Less<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, Or<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Less<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Less<TranslationDefinitionMaint.AccountTo.accountCD>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And2<Where<TranslationDefinitionMaint.SubFrom.subCD, IsNull, Or<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNull, Or<TranslationDefinitionMaint.SubFrom.subCD, IsNotNull, And<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNotNull, And<Required<TranslationDefinitionMaint.SubTo.subCD>, GreaterEqual<TranslationDefinitionMaint.SubFrom.subCD>>>>>>, Or<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And2<Where<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNull, Or<TranslationDefinitionMaint.SubTo.subCD, IsNull, Or<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNotNull, And<TranslationDefinitionMaint.SubTo.subCD, IsNotNull, And<TranslationDefinitionMaint.SubTo.subCD, GreaterEqual<Required<TranslationDefinitionMaint.SubFrom.subCD>>>>>>>, Or<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<Where<TranslationDefinitionMaint.SubFrom.subCD, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, LessEqual<TranslationDefinitionMaint.SubTo.subCD>, And<TranslationDefinitionMaint.SubFrom.subCD, LessEqual<Required<TranslationDefinitionMaint.SubTo.subCD>>, Or<TranslationDefinitionMaint.SubFrom.subCD, IsNull, Or<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNull>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.Config>.Select(cache.Graph, new object[62]
        {
          (object) newRow.TranslDefId,
          (object) newRow.LineNbr,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt4,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt2,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt1,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt4,
          (object) valueExt2
        }))
        {
          TranslDefDet translDefDet = PXResult<TranslDefDet, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.AccountFrom accountFrom = PXResult<TranslDefDet, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.AccountTo accountTo = PXResult<TranslDefDet, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.SubFrom subFrom = PXResult<TranslDefDet, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.SubTo subTo = PXResult<TranslDefDet, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          if (translDefDet != null)
          {
            cache.RaiseExceptionHandling<TranslDefDet.accountIdFrom>((object) newRow, (object) newRow.AccountIdFrom, (Exception) new PXSetPropertyException("The specified account/subaccount range should not intersect with the range '{1}{2} - {3}{4}' specified in the definition '{0}'.", (PXErrorLevel) 5, new object[5]
            {
              (object) translDefDet.TranslDefId,
              (object) accountFrom.AccountCD,
              subFrom.SubCD == null ? (object) string.Empty : (object) ("/" + subFrom.SubCD),
              (object) accountTo.AccountCD,
              subTo.SubCD == null ? (object) string.Empty : (object) ("/" + subTo.SubCD)
            }));
            if (e != null)
              throw new PXSetPropertyException(e.Message);
          }
        }
        foreach (PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo> pxResult in PXSelectBase<TranslDefDet, PXSelectJoin<TranslDefDet, InnerJoin<TranslDef, On<TranslDefDet.translDefId, Equal<TranslDef.translDefId>>, InnerJoin<TranslationDefinitionMaint.AccountFrom, On<TranslDefDet.accountIdFrom, Equal<TranslationDefinitionMaint.AccountFrom.accountID>>, InnerJoin<TranslationDefinitionMaint.AccountTo, On<TranslDefDet.accountIdTo, Equal<TranslationDefinitionMaint.AccountTo.accountID>>, LeftJoin<TranslationDefinitionMaint.SubFrom, On<TranslDefDet.subIdFrom, Equal<TranslationDefinitionMaint.SubFrom.subID>>, LeftJoin<TranslationDefinitionMaint.SubTo, On<TranslDefDet.subIdTo, Equal<TranslationDefinitionMaint.SubTo.subID>>>>>>>, Where<TranslDef.destLedgerId, Equal<Required<TranslDef.destLedgerId>>, And<TranslDef.active, Equal<boolTrue>, And<TranslDef.translDefId, NotEqual<Required<TranslDefDet.translDefId>>, And<Where<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, LessEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And<TranslationDefinitionMaint.AccountFrom.accountCD, LessEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, NotEqual<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And2<Where<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNull, Or<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, LessEqual<TranslationDefinitionMaint.SubTo.subCD>>>>, Or<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountFrom.accountCD>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Equal<TranslationDefinitionMaint.AccountFrom.accountCD>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And2<Where<TranslationDefinitionMaint.SubFrom.subCD, IsNull, Or<TranslationDefinitionMaint.SubFrom.subCD, IsNotNull, And<TranslationDefinitionMaint.SubFrom.subCD, LessEqual<Required<TranslationDefinitionMaint.SubTo.subCD>>>>>, Or<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, NotEqual<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And2<Where<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNull, Or<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNotNull, And<TranslationDefinitionMaint.SubFrom.subCD, LessEqual<Required<TranslationDefinitionMaint.SubTo.subCD>>>>>, Or<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And<TranslationDefinitionMaint.AccountFrom.accountCD, NotEqual<TranslationDefinitionMaint.AccountTo.accountCD>, And2<Where<TranslationDefinitionMaint.SubTo.subCD, IsNull, Or<TranslationDefinitionMaint.SubTo.subCD, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, LessEqual<TranslationDefinitionMaint.SubTo.subCD>>>>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, Greater<TranslationDefinitionMaint.AccountFrom.accountCD>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Greater<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, Or<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountFrom.accountCD>, And<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Greater<Required<TranslationDefinitionMaint.AccountFrom.accountCD>>, And<TranslationDefinitionMaint.AccountTo.accountCD, Greater<TranslationDefinitionMaint.AccountFrom.accountCD>, Or<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Less<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Less<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, Or<Required<TranslationDefinitionMaint.AccountTo.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Less<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Less<TranslationDefinitionMaint.AccountTo.accountCD>, Or<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And2<Where<TranslationDefinitionMaint.SubFrom.subCD, IsNull, Or<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNull, Or<TranslationDefinitionMaint.SubFrom.subCD, IsNotNull, And<Required<TranslationDefinitionMaint.SubTo.subCD>, IsNotNull, And<Required<TranslationDefinitionMaint.SubTo.subCD>, GreaterEqual<TranslationDefinitionMaint.SubFrom.subCD>>>>>>, Or<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<TranslationDefinitionMaint.AccountTo.accountCD>, And2<Where<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNull, Or<TranslationDefinitionMaint.SubTo.subCD, IsNull, Or<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNotNull, And<TranslationDefinitionMaint.SubTo.subCD, IsNotNull, And<TranslationDefinitionMaint.SubTo.subCD, GreaterEqual<Required<TranslationDefinitionMaint.SubFrom.subCD>>>>>>>, Or<TranslationDefinitionMaint.AccountTo.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<TranslationDefinitionMaint.AccountFrom.accountCD, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<Required<TranslationDefinitionMaint.AccountFrom.accountCD>, Equal<Required<TranslationDefinitionMaint.AccountTo.accountCD>>, And<Where<TranslationDefinitionMaint.SubFrom.subCD, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNotNull, And<Required<TranslationDefinitionMaint.SubFrom.subCD>, LessEqual<TranslationDefinitionMaint.SubTo.subCD>, And<TranslationDefinitionMaint.SubFrom.subCD, LessEqual<Required<TranslationDefinitionMaint.SubTo.subCD>>, Or<TranslationDefinitionMaint.SubFrom.subCD, IsNull, Or<Required<TranslationDefinitionMaint.SubFrom.subCD>, IsNull>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.Config>.Select(cache.Graph, new object[58]
        {
          (object) destLedgerId,
          (object) newRow.TranslDefId,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt4,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt2,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt4,
          (object) valueExt1,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt3,
          (object) valueExt3,
          (object) valueExt1,
          (object) valueExt3,
          (object) valueExt2,
          (object) valueExt2,
          (object) valueExt4,
          (object) valueExt2
        }))
        {
          TranslDefDet translDefDet = PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslDef translDef = PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.AccountFrom accountFrom = PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.AccountTo accountTo = PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.SubFrom subFrom = PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          TranslationDefinitionMaint.SubTo subTo = PXResult<TranslDefDet, TranslDef, TranslationDefinitionMaint.AccountFrom, TranslationDefinitionMaint.AccountTo, TranslationDefinitionMaint.SubFrom, TranslationDefinitionMaint.SubTo>.op_Implicit(pxResult);
          if (translDefDet != null && translDef != null)
          {
            int? branchId1 = def.BranchID;
            if (branchId1.HasValue)
            {
              branchId1 = translDef.BranchID;
              if (branchId1.HasValue)
              {
                branchId1 = translDef.BranchID;
                int? branchId2 = def.BranchID;
                if (!(branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue))
                  continue;
              }
            }
            cache.RaiseExceptionHandling<TranslDefDet.accountIdFrom>((object) newRow, (object) newRow.AccountIdFrom, (Exception) new PXSetPropertyException("The specified account/subaccount range should not intersect with the range '{1}{2} - {3}{4}' specified in the definition '{0}'.", (PXErrorLevel) 5, new object[5]
            {
              (object) translDefDet.TranslDefId,
              (object) accountFrom.AccountCD,
              subFrom.SubCD == null ? (object) string.Empty : (object) ("/" + subFrom.SubCD),
              (object) accountTo.AccountCD,
              subTo.SubCD == null ? (object) string.Empty : (object) ("/" + subTo.SubCD)
            }));
            if (e != null)
              throw new PXSetPropertyException(e.Message);
          }
        }
      }
    }
    if (flag)
    {
      PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
      if (current != null)
      {
        nullable2 = current.YtdNetIncAccountID;
        if (nullable2.HasValue)
        {
          string valueExt5 = (string) ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).GetValueExt<PX.Objects.GL.GLSetup.ytdNetIncAccountID>(current);
          int num1 = string.Compare(valueExt1, valueExt5);
          int num2 = string.Compare(valueExt3, valueExt5);
          if ((num1 == 0 || num1 == -1) && (num2 == 1 || num2 == 0))
            cache.RaiseExceptionHandling<TranslDefDet.accountIdFrom>((object) newRow, (object) valueExt1, (Exception) new PXSetPropertyException("The range of accounts includes the YTD Net Income Account. Translation for this account will not be performed", (PXErrorLevel) 3));
        }
      }
    }
    else if (e != null)
      throw new PXSetPropertyException(e.Message);
    return flag;
  }

  protected virtual void TranslDefDet_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    TranslDefDet newRow = (TranslDefDet) e.NewRow;
    TranslDef current = ((PXSelectBase<TranslDef>) this.TranslDefRecords).Current;
    if (newRow == null || current == null)
      return;
    this.CheckDetail(cache, newRow, current.Active.GetValueOrDefault(), current.DestLedgerId.Value, current, (Exception) null);
  }

  protected virtual void TranslDefDet_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    TranslDefDet row = (TranslDefDet) e.Row;
    TranslDef current = ((PXSelectBase<TranslDef>) this.TranslDefRecords).Current;
    if (row == null || current == null)
      return;
    this.CheckDetail(cache, row, current.Active.GetValueOrDefault(), current.DestLedgerId.Value, current, (Exception) null);
  }

  protected virtual void TranslDefDet_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TranslDefDet row = (TranslDefDet) e.Row;
    TranslDef current = ((PXSelectBase<TranslDef>) this.TranslDefRecords).Current;
    if (row == null || current == null)
      return;
    this.CheckDetail(cache, row, current.Active.GetValueOrDefault(), ((PXSelectBase<TranslDef>) this.TranslDefRecords).Current.DestLedgerId.Value, current, (Exception) null);
  }

  protected virtual void TranslDef_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 3)
      return;
    TranslDef row = (TranslDef) e.Row;
    TranslationHistory translationHistory = PXResultset<TranslationHistory>.op_Implicit(PXSelectBase<TranslationHistory, PXSelect<TranslationHistory, Where<TranslationHistory.translDefId, Equal<Required<TranslationHistory.translDefId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TranslDefId
    }));
    if (translationHistory != null && translationHistory.ReferenceNbr != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("Translation definition '{0}' is already used in existing translations!", new object[1]
      {
        (object) row.TranslDefId
      });
    }
  }

  protected virtual void TranslDef_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    TranslDef row = (TranslDef) e.Row;
    if (row.SourceLedgerId.HasValue)
    {
      PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectReadonly<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SourceLedgerId
      }));
      row.SourceCuryID = ledger?.BaseCuryID;
    }
    if (!row.DestLedgerId.HasValue)
      return;
    PX.Objects.GL.Ledger ledger1 = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectReadonly<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DestLedgerId
    }));
    row.DestCuryID = ledger1?.BaseCuryID;
  }

  protected virtual void TranslDef_Active_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    PXBoolAttribute.ConvertValue(e);
    TranslDef row = (TranslDef) e.Row;
    if (row == null || row == null || row.TranslDefId == null || !(bool) e.NewValue)
      return;
    foreach (PXResult<TranslDefDet> pxResult in ((PXSelectBase<TranslDefDet>) this.TranslDefDetailsRecords).Select(Array.Empty<object>()))
      this.CheckDetail(((PXGraph) this).Caches[typeof (TranslDefDet)], PXResult<TranslDefDet>.op_Implicit(pxResult), (bool) e.NewValue, row.DestLedgerId.Value, row, new Exception("Translation definition can not be active"));
  }

  protected virtual void TranslDef_DestLedgerId_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    TranslDef row = (TranslDef) e.Row;
    if (row == null || row == null || row.TranslDefId == null || e.NewValue == null)
      return;
    PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerCD, Equal<Required<PX.Objects.GL.Ledger.ledgerCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (ledger == null)
      return;
    bool? active = row.Active;
    if (!active.GetValueOrDefault())
      return;
    foreach (PXResult<TranslDefDet> pxResult in ((PXSelectBase<TranslDefDet>) this.TranslDefDetailsRecords).Select(Array.Empty<object>()))
    {
      TranslDefDet translDefDet = PXResult<TranslDefDet>.op_Implicit(pxResult);
      PXCache cach = ((PXGraph) this).Caches[typeof (TranslDefDet)];
      TranslDefDet newRow = translDefDet;
      active = row.Active;
      int num = active.GetValueOrDefault() ? 1 : 0;
      int destLedgerId = ledger.LedgerID.Value;
      TranslDef def = row;
      Exception e1 = new Exception("Translation Destination Ledeger ID can not be changed");
      this.CheckDetail(cach, newRow, num != 0, destLedgerId, def, e1);
    }
  }

  protected virtual void TranslDef_BranchID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    TranslDef row = (TranslDef) e.Row;
    if (row == null || row.TranslDefId == null || !row.Active.Value)
      return;
    row.Active = new bool?(false);
  }

  [PXHidden]
  [Serializable]
  public class AccountFrom : PX.Objects.GL.Account
  {
    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationDefinitionMaint.AccountFrom.accountID>
    {
    }

    public new abstract class accountCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationDefinitionMaint.AccountFrom.accountCD>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class AccountTo : PX.Objects.GL.Account
  {
    public new abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationDefinitionMaint.AccountTo.accountID>
    {
    }

    public new abstract class accountCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationDefinitionMaint.AccountTo.accountCD>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class SubFrom : PX.Objects.GL.Sub
  {
    public new abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationDefinitionMaint.SubFrom.subID>
    {
    }

    public new abstract class subCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationDefinitionMaint.SubFrom.subCD>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class SubTo : PX.Objects.GL.Sub
  {
    public new abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TranslationDefinitionMaint.SubTo.subID>
    {
    }

    public new abstract class subCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TranslationDefinitionMaint.SubTo.subCD>
    {
    }
  }
}
