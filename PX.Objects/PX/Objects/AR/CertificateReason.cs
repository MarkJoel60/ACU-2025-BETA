// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CertificateReason
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An unbound DAC that is used to collect certificate exemption reasons.
/// </summary>
[PXHidden]
public class CertificateReason : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The exemption reason ID.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual 
  #nullable disable
  string ReasonID { get; set; }

  /// <summary>A description of the exemption reason.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Exemption Reason")]
  public virtual string ReasonName { get; set; }

  public abstract class reasonID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CertificateReason.reasonID>
  {
  }

  public abstract class reasonName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CertificateReason.reasonName>
  {
  }
}
