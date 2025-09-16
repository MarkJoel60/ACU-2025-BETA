// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Lite.PMProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CT.Lite;
using PX.Objects.GL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.Lite;

/// <exclude />
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProject : Contract, IBqlTable, IBqlTableSystemDataStorage, IProjectAccountsSource
{
  [PXDBIdentity]
  [PXReferentialIntegrityCheck]
  [PXUIField(DisplayName = "Project ID")]
  public override int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  public int? ProjectID
  {
    get => this.ContractID;
    set
    {
    }
  }

  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  public override 
  #nullable disable
  string ContractCD
  {
    get => this._ContractCD;
    set => this._ContractCD = value;
  }

  [SubAccount(typeof (PMProject.defaultSalesAccountID), typeof (PMProject.defaultBranchID), false)]
  public override int? DefaultSalesSubID { get; set; }

  [SubAccount(typeof (PMProject.defaultExpenseAccountID), typeof (PMProject.defaultBranchID), false)]
  public override int? DefaultExpenseSubID { get; set; }

  [SubAccount]
  public override int? DefaultAccrualSubID
  {
    get => this._DefaultAccrualSubID;
    set => this._DefaultAccrualSubID = value;
  }

  public new class PK : PrimaryKeyOf<PMProject>.By<PMProject.contractID>.Dirty
  {
    public static PMProject Find(PXGraph graph, int? projectID)
    {
      PXGraph pxGraph = graph;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) projectID;
      int? nullable = projectID;
      int num1 = 0;
      int num2 = nullable.GetValueOrDefault() < num1 & nullable.HasValue ? 1 : 0;
      return (PMProject) PrimaryKeyOf<PMProject>.By<PMProject.contractID>.Dirty.FindBy(pxGraph, (object) local, num2 != 0);
    }
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.contractID>
  {
  }

  public new abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProject.contractCD>
  {
  }

  public new abstract class defaultSalesAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultSalesAccountID>
  {
  }

  public new abstract class defaultSalesSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultSalesSubID>
  {
  }

  public new abstract class defaultExpenseAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultExpenseAccountID>
  {
  }

  public new abstract class defaultExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultExpenseSubID>
  {
  }

  public new abstract class defaultAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultAccrualAccountID>
  {
  }

  public new abstract class defaultAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProject.defaultAccrualSubID>
  {
  }

  public new abstract class defaultBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProject.defaultBranchID>
  {
  }
}
