// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterP
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select<APRegister>))]
public class APRegisterP : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrigModule;
  protected string _DocDesc;
  protected string _DocType;
  protected string _RefNbr;

  [PXDBString(2, IsFixed = true, BqlField = typeof (APRegister.origModule))]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (APRegister.docDesc))]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBString(3, IsFixed = true, BqlField = typeof (APRegister.docType))]
  public virtual string OrigDocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (APRegister.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterP.origModule>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterP.docDesc>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterP.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterP.refNbr>
  {
  }
}
