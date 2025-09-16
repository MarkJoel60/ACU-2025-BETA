// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTFormulaDescriptionContainer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CT;

public class CTFormulaDescriptionContainer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public 
  #nullable disable
  UsageData usageData;
  public List<long?> pmTranIDs;

  public virtual string ActionInvoice { get; set; }

  public virtual string ActionItem { get; set; }

  public virtual string InventoryPrefix { get; set; }

  [PXInt]
  public virtual int? ContractID { get; set; }

  [PXInt]
  public virtual int? CustomerID { get; set; }

  [PXInt]
  public virtual int? CustomerLocationID { get; set; }

  [PXInt]
  public virtual int? InventoryID { get; set; }

  [PXInt]
  public virtual int? ContractItemID { get; set; }

  [PXInt]
  public virtual int? ContractDetailID { get; set; }

  public abstract class actionInvoice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.actionInvoice>
  {
  }

  public abstract class actionItem : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.actionItem>
  {
  }

  public abstract class inventoryPrefix : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.inventoryPrefix>
  {
  }

  public abstract class contractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.contractID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.customerLocationID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.inventoryID>
  {
  }

  public abstract class contractItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.contractItemID>
  {
  }

  public abstract class contractDetailID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CTFormulaDescriptionContainer.contractDetailID>
  {
  }
}
