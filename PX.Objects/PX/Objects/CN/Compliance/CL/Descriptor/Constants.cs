// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Constants
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.Descriptor;

public class Constants
{
  public const 
  #nullable disable
  string ComplianceDocumentViewName = "ComplianceDocuments";
  public const string LienWaiverReportFileNamePattern = "{0}\\LW-{1}-{2}-{3:MM-dd-yyyy}{4}";
  public const string LienWaiverReportFileNameSearchPattern = "{0}\\LW-{1}-{2}";

  public class LienWaiverDocumentTypeValues
  {
    public const string All = "All";
    public const string ConditionalPartial = "Conditional Partial";
    public const string ConditionalFinal = "Conditional Final";
    public const string UnconditionalPartial = "Unconditional Partial";
    public const string UnconditionalFinal = "Unconditional Final";

    public class all : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Constants.LienWaiverDocumentTypeValues.all>
    {
      public all()
        : base("All")
      {
      }
    }
  }

  public class LienWaiverReportParameters
  {
    public const string ComplianceDocumentId = "ComplianceDocumentId";
    public const string DeviceHubComplianceDocumentId = "ComplianceDocument.ComplianceDocumentID";
    public const string IsJointCheck = "IsJointCheck";
  }

  public class ComplianceNotification
  {
    public const string LienWaiverNotificationSourceCd = "Vendor";
  }
}
