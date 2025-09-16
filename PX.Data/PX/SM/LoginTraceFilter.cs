// Decompiled with JetBrains decompiler
// Type: PX.SM.LoginTraceFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class LoginTraceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Username;
  protected int? _Operation;
  protected System.DateTime? _DateFrom;
  protected System.DateTime? _DateTo;

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Username")]
  [PXSelector(typeof (Search<Users.username, Where<Users.isHidden, Equal<False>>>), DescriptionField = typeof (Users.fullName))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<LoginTraceFilter.username>.IsRelatedTo<Users.username>))]
  public virtual string Username
  {
    get => this._Username;
    set => this._Username = value;
  }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Operation")]
  [PXAuditJournal.OperationList]
  public virtual int? Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true)]
  [PXUIField(DisplayName = "From")]
  public System.DateTime? DateFrom
  {
    get => this._DateFrom;
    set => this._DateFrom = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true)]
  [PXUIField(DisplayName = "To")]
  public System.DateTime? DateTo
  {
    get => this._DateTo;
    set => this._DateTo = value;
  }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoginTraceFilter.username>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LoginTraceFilter.operation>
  {
  }

  public abstract class dateFrom : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  LoginTraceFilter.dateFrom>
  {
  }

  public abstract class dateTo : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  LoginTraceFilter.dateTo>
  {
  }
}
