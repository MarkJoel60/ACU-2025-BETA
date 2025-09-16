// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.ReverseDisposalInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("Reverse Disposal Info")]
[Serializable]
public class ReverseDisposalInfo : DisposeParams
{
  protected DateTime? _ReverseDisposalDate;
  protected 
  #nullable disable
  string _ReverseDisposalPeriodID;

  [PXDBDate]
  [PXDefault(typeof (FADetails.disposalDate))]
  [PXUIField(DisplayName = "Disposal Date", Enabled = false)]
  public override DateTime? DisposalDate
  {
    get => this._DisposalDate;
    set => this._DisposalDate = value;
  }

  [PXUIField(DisplayName = "Disposal Period", Enabled = false)]
  [PXDBString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  [PXDefault(typeof (FADetails.disposalPeriodID))]
  public override string DisposalPeriodID
  {
    get => this._DisposalPeriodID;
    set => this._DisposalPeriodID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(typeof (FADetails.saleAmount))]
  [PXUIField(DisplayName = "Proceeds Amount", Enabled = false)]
  public override Decimal? DisposalAmt
  {
    get => this._DisposalAmt;
    set => this._DisposalAmt = value;
  }

  [PXDBInt]
  [PXDefault(typeof (FADetails.disposalMethodID))]
  [PXSelector(typeof (Search<FADisposalMethod.disposalMethodID>), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
  [PXUIField(DisplayName = "Disposal Method", Enabled = false)]
  public override int? DisposalMethodID
  {
    get => this._DisposalMethodID;
    set => this._DisposalMethodID = value;
  }

  [PXDefault(typeof (FixedAsset.disposalAccountID))]
  [Account(DisplayName = "Proceeds Account", DescriptionField = typeof (PX.Objects.GL.Account.description), Enabled = false)]
  public override int? DisposalAccountID
  {
    get => this._DisposalAccountID;
    set => this._DisposalAccountID = value;
  }

  [PXDefault(typeof (FixedAsset.disposalSubID))]
  [SubAccount(typeof (FixedAsset.disposalAccountID), DisplayName = "Proceeds Sub.", DescriptionField = typeof (Sub.description), Enabled = false)]
  public override int? DisposalSubID
  {
    get => this._DisposalSubID;
    set => this._DisposalSubID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Reversal Date")]
  public virtual DateTime? ReverseDisposalDate
  {
    get => this._ReverseDisposalDate;
    set => this._ReverseDisposalDate = value;
  }

  [PXUIField(DisplayName = "Reversal Period", Required = true)]
  [FABookPeriodSelector(typeof (Search2<FABookPeriod.finPeriodID, InnerJoin<FABook, On<FABookPeriod.bookID, Equal<FABook.bookID>>, LeftJoin<FinPeriod, On<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>, And<FABookPeriod.finPeriodID, Equal<FinPeriod.finPeriodID>, And<FABook.updateGL, Equal<True>>>>>>, Where<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>, And<FinPeriod.finPeriodID, GreaterEqual<Current<FADetails.disposalPeriodID>>, And<Where<FinPeriod.finPeriodID, IsNotNull, And<FinPeriod.fAClosed, NotEqual<True>, Or<FABook.updateGL, NotEqual<True>>>>>>>>), null, null, null, false, typeof (FixedAsset.assetID), typeof (ReverseDisposalInfo.reverseDisposalDate), null, null, null, null, ReportParametersFlag.None)]
  [PXDefault]
  public virtual string ReverseDisposalPeriodID
  {
    get => this._ReverseDisposalPeriodID;
    set => this._ReverseDisposalPeriodID = value;
  }

  public abstract class reverseDisposalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReverseDisposalInfo.reverseDisposalDate>
  {
  }

  public abstract class reverseDisposalPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReverseDisposalInfo.reverseDisposalPeriodID>
  {
  }
}
