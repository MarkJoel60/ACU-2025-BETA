// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Specific.AuthnetProcCenterTypeName
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR.CCPaymentProcessing.Specific;

public static class AuthnetProcCenterTypeName
{
  public class APIPluginFullName : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    AuthnetProcCenterTypeName.APIPluginFullName>
  {
    public APIPluginFullName()
      : base("PX.CCProcessing.V2.AuthnetProcessingPlugin")
    {
    }
  }

  public class CIMPluginFullName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AuthnetProcCenterTypeName.CIMPluginFullName>
  {
    public CIMPluginFullName()
      : base("PX.CCProcessing.AuthorizeNetTokenizedProcessing")
    {
    }
  }

  public class AIMPluginFullName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AuthnetProcCenterTypeName.AIMPluginFullName>
  {
    public AIMPluginFullName()
      : base("PX.CCProcessing.AuthorizeNetProcessing")
    {
    }
  }
}
