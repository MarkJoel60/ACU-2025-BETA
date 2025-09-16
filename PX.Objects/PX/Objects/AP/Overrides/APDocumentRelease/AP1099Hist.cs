// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.AP1099Hist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

[PXAccumulator(SingleRecord = true)]
[Serializable]
public class AP1099Hist : AP1099History
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXSelector(typeof (Search2<AP1099Year.finYear, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<AP1099Year.organizationID>>>, Where<AP1099Year.status, Equal<AP1099Year.status.open>, And<PX.Objects.GL.Branch.branchID, Equal<Current<AP1099Hist.branchID>>>>>), DirtyRead = true)]
  [PXDefault]
  public override 
  #nullable disable
  string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  [PXDBShort(IsKey = true)]
  [PXSelector(typeof (Search<AP1099Box.boxNbr>))]
  [PXDefault]
  public override short? BoxNbr
  {
    get => this._BoxNbr;
    set => this._BoxNbr = value;
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099Hist.branchID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099Hist.vendorID>
  {
  }

  public new abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Hist.finYear>
  {
  }

  public new abstract class boxNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AP1099Hist.boxNbr>
  {
  }
}
