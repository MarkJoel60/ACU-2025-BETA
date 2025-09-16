// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CertificateTemplate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An unbound DAC that is used to collect certificate templates.
/// </summary>
[PXHidden]
public class CertificateTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The certificate template ID.</summary>
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  public virtual 
  #nullable disable
  string TemplateID { get; set; }

  /// <summary>A description of the certificate template.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Template Name")]
  public virtual string TemplateName { get; set; }

  public abstract class templateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CertificateTemplate.templateID>
  {
  }

  public abstract class templateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CertificateTemplate.templateName>
  {
  }
}
