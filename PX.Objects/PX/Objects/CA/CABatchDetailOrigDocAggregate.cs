// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABatchDetailOrigDocAggregate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXProjection(typeof (SelectFromBase<CABatchDetail, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<CABatchDetail.batchNbr>, GroupBy<CABatchDetail.origModule>, GroupBy<CABatchDetail.origDocType>, GroupBy<CABatchDetail.origRefNbr>>))]
[PXCacheName("Aggregated CA Batch Details")]
[Serializable]
public class CABatchDetailOrigDocAggregate : CABatchDetail
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CABatchDetail.batchNbr))]
  public override 
  #nullable disable
  string BatchNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (CABatchDetail.origModule))]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  public override string OrigModule { get; set; }

  [PXDBString(3, IsFixed = true, IsKey = true, BqlField = typeof (CABatchDetail.origDocType))]
  [PXUIField(DisplayName = "Doc. Type")]
  public override string OrigDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (CABatchDetail.origRefNbr))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public override string OrigRefNbr { get; set; }

  public new abstract class batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetailOrigDocAggregate.batchNbr>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetailOrigDocAggregate.origModule>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetailOrigDocAggregate.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABatchDetailOrigDocAggregate.origRefNbr>
  {
  }
}
