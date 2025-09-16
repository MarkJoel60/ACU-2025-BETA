// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARRegister, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>>>>))]
[PXBreakInheritance]
[Serializable]
public class ARRegisterAccess : Customer
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected bool? _Scheduled;
  protected string _ScheduleID;

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARRegister.docType))]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARRegister.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBBool(BqlField = typeof (ARRegister.scheduled))]
  public virtual bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (ARRegister.scheduleID))]
  public virtual string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAccess.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAccess.refNbr>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAccess.scheduled>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAccess.scheduleID>
  {
  }
}
