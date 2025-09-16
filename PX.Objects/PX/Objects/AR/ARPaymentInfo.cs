// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARPaymentInfo : ARPayment
{
  protected 
  #nullable disable
  string _PMInstanceDescr;
  protected string _CCTranDescr;
  protected bool? _IsCCExpired;

  [PXString(255 /*0xFF*/)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Card/Account Nbr.", Enabled = false)]
  public virtual string PMInstanceDescr
  {
    get => this._PMInstanceDescr;
    set => this._PMInstanceDescr = value;
  }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Error Descr.", Enabled = false)]
  public virtual string CCTranDescr
  {
    get => this._CCTranDescr;
    set => this._CCTranDescr = value;
  }

  [PXBool]
  [PXDefault("")]
  [PXUIField(DisplayName = "Expired", Enabled = false)]
  public virtual bool? IsCCExpired
  {
    get => this._IsCCExpired;
    set => this._IsCCExpired = value;
  }

  public abstract class pMInstanceDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPaymentInfo.pMInstanceDescr>
  {
  }

  public abstract class cCTranDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPaymentInfo.cCTranDescr>
  {
  }

  public abstract class isCCExpired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPaymentInfo.isCCExpired>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPaymentInfo.branchID>
  {
  }
}
