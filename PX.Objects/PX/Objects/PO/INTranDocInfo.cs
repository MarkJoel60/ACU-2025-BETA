// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.INTranDocInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>
/// Represents information about referenced documents from grouping inventory transactions.
/// </summary>
[PXProjection(typeof (SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<INTran.docType>, GroupBy<INTran.refNbr>, GroupBy<INTran.pOReceiptNbr>, GroupBy<INTran.pOReceiptLineNbr>, GroupBy<INTran.pOReceiptType>>))]
[PXHidden]
[Serializable]
public class INTranDocInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="T:PX.Objects.IN.INTran.docType" />
  [PXDBString(1, IsFixed = true, IsKey = true, BqlField = typeof (INTran.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.refNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTran.refNbr))]
  public virtual string RefNbr { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.pOReceiptNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTran.pOReceiptNbr))]
  public virtual string POReceiptNbr { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.pOReceiptLineNbr" />
  [PXDBInt(IsKey = true, BqlField = typeof (INTran.pOReceiptLineNbr))]
  public virtual int? POReceiptLineNbr { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.pOReceiptType" />
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (INTran.pOReceiptType))]
  public virtual string POReceiptType { get; set; }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.docType" />
  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDocInfo.docType>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.refNbr" />
  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDocInfo.refNbr>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.pOReceiptNbr" />
  public abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranDocInfo.pOReceiptNbr>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.pOReceiptLineNbr" />
  public abstract class pOReceiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTranDocInfo.pOReceiptLineNbr>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.IN.INTran.pOReceiptType" />
  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranDocInfo.pOReceiptType>
  {
  }
}
