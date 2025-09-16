// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceDocumentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXLocalizable]
public class ComplianceDocumentType
{
  public const 
  #nullable disable
  string Certificate = "Certificate";
  public const string Insurance = "Insurance";
  public const string LienWaiver = "Lien Waiver";
  public const string Notice = "Notice";
  public const string Other = "Other";
  public const string Status = "Status";

  public class status : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocumentType.status>
  {
    public status()
      : base("Status")
    {
    }
  }

  public class certificate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocumentType.certificate>
  {
    public certificate()
      : base("Certificate")
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocumentType.other>
  {
    public other()
      : base("Other")
    {
    }
  }

  public class lienWaiver : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocumentType.lienWaiver>
  {
    public lienWaiver()
      : base("Lien Waiver")
    {
    }
  }

  public class insurance : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocumentType.insurance>
  {
    public insurance()
      : base("Insurance")
    {
    }
  }

  public class notice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocumentType.notice>
  {
    public notice()
      : base("Notice")
    {
    }
  }
}
