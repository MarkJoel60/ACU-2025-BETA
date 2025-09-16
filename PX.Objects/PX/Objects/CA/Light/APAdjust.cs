// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.APAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[Serializable]
public class APAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string AdjgDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string AdjgRefNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  public virtual string AdjdDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? AdjNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? AdjdLineNbr { get; set; }

  [PXDBBool]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  public virtual bool? Voided { get; set; }

  public class PK : 
    PrimaryKeyOf<APAdjust>.By<APAdjust.adjgDocType, APAdjust.adjgRefNbr, APAdjust.adjdDocType, APAdjust.adjdRefNbr, APAdjust.adjNbr, APAdjust.adjdLineNbr>
  {
    public static APAdjust Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      string adjdDocType,
      string adjdRefNbr,
      int? adjNbr,
      int? adjdLineNbr,
      PKFindOptions options = 0)
    {
      return (APAdjust) PrimaryKeyOf<APAdjust>.By<APAdjust.adjgDocType, APAdjust.adjgRefNbr, APAdjust.adjdDocType, APAdjust.adjdRefNbr, APAdjust.adjNbr, APAdjust.adjdLineNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) adjdDocType, (object) adjdRefNbr, (object) adjNbr, (object) adjdLineNbr, options);
    }
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgRefNbr>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjNbr>
  {
  }

  public abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdLineNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.voided>
  {
  }
}
