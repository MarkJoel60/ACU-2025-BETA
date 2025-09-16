// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAutoApplyParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARAutoApplyParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _ApplyCreditMemos;
  protected bool? _ReleaseBatchWhenFinished;
  protected DateTime? _ApplicationDate;
  protected 
  #nullable disable
  string _FinPeriodID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ApplyCreditMemos
  {
    get => this._ApplyCreditMemos;
    set => this._ApplyCreditMemos = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ReleaseBatchWhenFinished
  {
    get => this._ReleaseBatchWhenFinished;
    set => this._ReleaseBatchWhenFinished = value;
  }

  [PXDBString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Include Child Documents")]
  [ARPaymentEntry.LoadOptions.loadChildDocuments.List]
  [PXDefault("NOONE")]
  public virtual string LoadChildDocuments { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ApplicationDate
  {
    get => this._ApplicationDate;
    set => this._ApplicationDate = value;
  }

  [AROpenPeriod(typeof (ARAutoApplyParameters.applicationDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, ValidatePeriod = PeriodValidation.DefaultSelectUpdate)]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class applyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAutoApplyParameters.applyCreditMemos>
  {
  }

  public abstract class releaseBatchWhenFinished : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAutoApplyParameters.releaseBatchWhenFinished>
  {
  }

  public abstract class loadChildDocuments : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAutoApplyParameters.loadChildDocuments>
  {
  }

  public abstract class applicationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAutoApplyParameters.applicationDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAutoApplyParameters.finPeriodID>
  {
  }
}
