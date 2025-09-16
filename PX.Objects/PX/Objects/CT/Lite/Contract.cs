// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.Lite.Contract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.CT.Lite;

/// <exclude />
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class Contract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractID;
  protected 
  #nullable disable
  string _ContractCD;
  protected int? _DefaultAccrualAccountID;
  protected int? _DefaultAccrualSubID;
  protected int? _DefaultBranchID;

  [PXDBIdentity]
  [PXUIField(DisplayName = "Contract ID")]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string ContractCD
  {
    get => this._ContractCD;
    set => this._ContractCD = value;
  }

  [Account(DisplayName = "Default Sales Account", AvoidControlAccounts = true)]
  public virtual int? DefaultSalesAccountID { get; set; }

  [SubAccount]
  public virtual int? DefaultSalesSubID { get; set; }

  [Account(DisplayName = "Default Cost Account", AvoidControlAccounts = true)]
  public virtual int? DefaultExpenseAccountID { get; set; }

  [SubAccount]
  public virtual int? DefaultExpenseSubID { get; set; }

  [Account(DisplayName = "Accrual Account", AvoidControlAccounts = true)]
  public virtual int? DefaultAccrualAccountID
  {
    get => this._DefaultAccrualAccountID;
    set => this._DefaultAccrualAccountID = value;
  }

  [SubAccount]
  public virtual int? DefaultAccrualSubID
  {
    get => this._DefaultAccrualSubID;
    set => this._DefaultAccrualSubID = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? DefaultBranchID
  {
    get => this._DefaultBranchID;
    set => this._DefaultBranchID = value;
  }

  public class PK : PrimaryKeyOf<Contract>.By<Contract.contractID>
  {
    public static Contract Find(PXGraph graph, int? contractID, PKFindOptions options = 0)
    {
      return (Contract) PrimaryKeyOf<Contract>.By<Contract.contractID>.FindBy(graph, (object) contractID, options);
    }
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.contractID>
  {
  }

  public abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.contractCD>
  {
  }

  public abstract class defaultSalesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultSalesAccountID>
  {
  }

  public abstract class defaultSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.defaultSalesSubID>
  {
  }

  public abstract class defaultExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultExpenseAccountID>
  {
  }

  public abstract class defaultExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultExpenseSubID>
  {
  }

  public abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultAccrualAccountID>
  {
  }

  public abstract class defaultAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Contract.defaultAccrualSubID>
  {
  }

  public abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.defaultBranchID>
  {
  }
}
