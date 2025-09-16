// Decompiled with JetBrains decompiler
// Type: PX.Common.PXHostingEnvironment
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Web.Hosting;

#nullable disable
namespace PX.Common;

[PXInternalUseOnly]
public static class PXHostingEnvironment
{
  /// <summary>
  /// Gets a value indicating whether the current application domain is being hosted
  /// by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object, and the domain was not internally created
  /// from code for service purposes.
  /// </summary>
  /// <remarks>
  ///   <para>Some parts of the system (like CustomizationProjectBuilder) may create an application domain from the code
  ///       by using <see cref="M:System.Web.Hosting.ApplicationHost.CreateApplicationHost(System.Type,System.String,System.String)" /> method.</para>
  ///   <para>In that case, <see cref="P:System.Web.Hosting.HostingEnvironment.IsHosted" /> is true but the list of running applications
  ///       is empty because the new application is created with <see cref="F:System.Web.Hosting.HostingEnvironmentFlags.HideFromAppManager" /> flag.</para>
  /// </remarks>
  [PXInternalUseOnly]
  public static bool IsHosted
  {
    get
    {
      return HostingEnvironment.IsHosted && ApplicationManager.GetApplicationManager().GetRunningApplications().Length != 0;
    }
  }
}
