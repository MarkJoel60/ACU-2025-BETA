// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterOrigRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select<APRegister>))]
[PXHidden]
public class APRegisterOrigRetainage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool(BqlField = typeof (APRegister.retainageApply))]
  public virtual bool? RetainageApply { get; set; }

  [PXDBBool(BqlField = typeof (APRegister.released))]
  public virtual bool? Released { get; set; }

  [PXDBBool(BqlField = typeof (APRegister.openDoc))]
  public virtual bool? OpenDoc { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (APRegister.refNbr))]
  public virtual string RefNbr { get; set; }

  public abstract class retainageApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterOrigRetainage.retainageApply>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterOrigRetainage.released>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterOrigRetainage.openDoc>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterOrigRetainage.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterOrigRetainage.refNbr>
  {
  }
}
