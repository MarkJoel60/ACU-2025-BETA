// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScanJobFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class SMScanJobFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected System.DateTime? _StartDate;
  protected System.DateTime? _EndDate;
  protected bool? _HideProcessed;

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date", Visibility = PXUIVisibility.SelectorVisible, Required = false)]
  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual System.DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date", Visibility = PXUIVisibility.SelectorVisible, Required = false)]
  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual System.DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBDate]
  public virtual System.DateTime? EndDatePlusOne
  {
    get
    {
      if (!this._EndDate.HasValue)
        return this._EndDate;
      System.DateTime date = this._EndDate.Value;
      date = date.Date;
      return new System.DateTime?(date.AddDays(1.0));
    }
    set
    {
    }
  }

  [PXBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Hide Processed", Enabled = true)]
  public virtual bool? HideProcessed
  {
    get => this._HideProcessed;
    set => this._HideProcessed = value;
  }

  public abstract class startDate : BqlType<IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SMScanJobFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SMScanJobFilter.endDate>
  {
  }

  public abstract class endDatePlusOne : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMScanJobFilter.endDatePlusOne>
  {
  }

  public abstract class hideProcessed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMScanJobFilter.hideProcessed>
  {
  }
}
