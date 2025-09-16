// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.DocumentContactMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
[PXHidden]
public class DocumentContactMethod : PXMappedCacheExtension
{
  public virtual 
  #nullable disable
  string Method { get; set; }

  public virtual bool? NoFax { get; set; }

  public virtual bool? NoMail { get; set; }

  public virtual bool? NoMarketing { get; set; }

  public virtual bool? NoCall { get; set; }

  public virtual bool? NoEMail { get; set; }

  public virtual bool? NoMassMail { get; set; }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentContactMethod.method>
  {
  }

  public abstract class noFax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentContactMethod.noFax>
  {
  }

  public abstract class noMail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentContactMethod.noMail>
  {
  }

  public abstract class noMarketing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentContactMethod.noMarketing>
  {
  }

  public abstract class noCall : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentContactMethod.noCall>
  {
  }

  public abstract class noEMail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentContactMethod.noEMail>
  {
  }

  public abstract class noMassMail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DocumentContactMethod.noMassMail>
  {
  }
}
