// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ARDocumentCustomer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[PXProjection(typeof (Select<BAccount2, Where<BAccount2.isBranch, Equal<True>, And<BAccount2.type, In3<BAccountType.customerType, BAccountType.combinedType>>>>), Persistent = false)]
public class ARDocumentCustomer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (PX.Objects.CR.BAccount.bAccountID))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctCD))]
  public virtual 
  #nullable disable
  string CustomerCD { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentCustomer.customerID>
  {
  }

  public abstract class customerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentCustomer.customerCD>
  {
  }
}
