// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ARDocumentVendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[PXProjection(typeof (Select2<BranchAlias, InnerJoin<BAccount2, On<BAccount2.bAccountID, Equal<BranchAlias.bAccountID>, And<BAccount2.type, In3<BAccountType.vendorType, BAccountType.combinedType>, And<BAccount2.isBranch, Equal<True>>>>>>), Persistent = false)]
public class ARDocumentVendor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (BranchAlias.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlField = typeof (BranchAlias.bAccountID))]
  public virtual int? VendorID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctCD))]
  public virtual 
  #nullable disable
  string VendorCD { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentVendor.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentVendor.vendorID>
  {
  }

  public abstract class vendorCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentVendor.vendorCD>
  {
  }
}
