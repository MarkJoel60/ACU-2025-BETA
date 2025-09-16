// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<APRegister, InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APRegister.vendorID>>>>))]
[PXBreakInheritance]
[Serializable]
public class APRegisterAccess : Vendor
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected bool? _Scheduled;
  protected string _ScheduleID;

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.docType))]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APRegister.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBBool(BqlField = typeof (APRegister.scheduled))]
  public virtual bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (APRegister.scheduleID))]
  public virtual string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAccess.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAccess.refNbr>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAccess.scheduled>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAccess.scheduleID>
  {
  }
}
