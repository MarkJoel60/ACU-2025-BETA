// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class RMReportGL : PXCacheExtension<
#nullable disable
RMReport>
{
  protected string _Type;
  protected bool? _RequestLedgerID;
  protected bool? _RequestAccountClassID;
  protected bool? _RequestStartAccount;
  protected bool? _RequestEndAccount;
  protected bool? _RequestStartSub;
  protected bool? _RequestEndSub;
  protected bool? _RequestStartBranch;
  protected bool? _RequestEndBranch;

  [PXDBString(2, IsFixed = true)]
  [RMType.List]
  [PXDefault("GL")]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestOrganizationID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestUseMasterCalendar { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestLedgerID
  {
    get => this._RequestLedgerID;
    set => this._RequestLedgerID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestAccountClassID
  {
    get => this._RequestAccountClassID;
    set => this._RequestAccountClassID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartAccount
  {
    get => this._RequestStartAccount;
    set => this._RequestStartAccount = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndAccount
  {
    get => this._RequestEndAccount;
    set => this._RequestEndAccount = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartSub
  {
    get => this._RequestStartSub;
    set => this._RequestStartSub = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndSub
  {
    get => this._RequestEndSub;
    set => this._RequestEndSub = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartBranch
  {
    get => this._RequestStartBranch;
    set => this._RequestStartBranch = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndBranch
  {
    get => this._RequestEndBranch;
    set => this._RequestEndBranch = value;
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReportGL.type>
  {
  }

  public abstract class requestOrganizationID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportGL.requestOrganizationID>
  {
  }

  public abstract class requestUseMasterCalendar : IBqlField, IBqlOperand
  {
  }

  public abstract class requestLedgerID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMReportGL.requestLedgerID>
  {
  }

  public abstract class requestAccountClassID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportGL.requestAccountClassID>
  {
  }

  public abstract class requestStartAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportGL.requestStartAccount>
  {
  }

  public abstract class requestEndAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportGL.requestEndAccount>
  {
  }

  public abstract class requestStartSub : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMReportGL.requestStartSub>
  {
  }

  public abstract class requestEndSub : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMReportGL.requestEndSub>
  {
  }

  public abstract class requestStartBranch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportGL.requestStartBranch>
  {
  }

  public abstract class requestEndBranch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMReportGL.requestEndBranch>
  {
  }
}
