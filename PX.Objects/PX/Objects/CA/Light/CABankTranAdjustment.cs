// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.CABankTranAdjustment
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
public class CABankTranAdjustment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? TranID { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual 
  #nullable disable
  string AdjdModule { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string AdjdDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? AdjNbr { get; set; }

  [PXDBBool]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  public virtual bool? Voided { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.tranID, CABankTranAdjustment.adjNbr>
  {
    public static CABankTranAdjustment Find(
      PXGraph graph,
      int? tranID,
      int? adjNbr,
      PKFindOptions options = 0)
    {
      return (CABankTranAdjustment) PrimaryKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.tranID, CABankTranAdjustment.adjNbr>.FindBy(graph, (object) tranID, (object) adjNbr, options);
    }
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.tranID>
  {
  }

  public abstract class adjdModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdModule>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranAdjustment.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranAdjustment.voided>
  {
  }
}
