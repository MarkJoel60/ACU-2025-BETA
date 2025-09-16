// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMUnbilledDailySummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMUnbilledDailySummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _AccountGroupID;
  protected DateTime? _Date;
  protected int? _Billable;
  protected int? _NonBillable;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBDate(IsKey = true)]
  [PXDefault]
  public virtual DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? Billable
  {
    get => this._Billable;
    set => this._Billable = value;
  }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? NonBillable
  {
    get => this._NonBillable;
    set => this._NonBillable = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMUnbilledDailySummary.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMUnbilledDailySummary.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMUnbilledDailySummary.accountGroupID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMUnbilledDailySummary.date>
  {
  }

  public abstract class billable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMUnbilledDailySummary.billable>
  {
  }

  public abstract class nonBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMUnbilledDailySummary.nonBillable>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMUnbilledDailySummary.Tstamp>
  {
  }
}
