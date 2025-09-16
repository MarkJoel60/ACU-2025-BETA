// Decompiled with JetBrains decompiler
// Type: PX.SM.FilesDialogExtender
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Services;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class FilesDialogExtender : IFilesDialogExtender
{
  private const string DeviceHubFeatureName = "PX.Objects.CS.FeaturesSet+deviceHub";

  public IEnumerable<ToolbarButtonDescriptor> GetToolbarButtons(PXGraph graph, string view)
  {
    ToolbarButtonDescriptor deviceHubButton = this.CreateDeviceHubButton(graph, view);
    ToolbarButtonDescriptor capturingImageButton = this.CreateCapturingImageButton(graph, view);
    List<ToolbarButtonDescriptor> toolbarButtons = new List<ToolbarButtonDescriptor>();
    if (deviceHubButton != null)
      toolbarButtons.Add(deviceHubButton);
    if (capturingImageButton != null)
      toolbarButtons.Add(capturingImageButton);
    return (IEnumerable<ToolbarButtonDescriptor>) toolbarButtons;
  }

  public void AddAction(PXGraph graph, string view)
  {
    if (!PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") || graph.Actions.Contains((object) "Scan"))
      return;
    ScanJobInfoMaint.AddScanAction(graph);
  }

  private ToolbarButtonDescriptor CreateDeviceHubButton(PXGraph graph, string view)
  {
    if (!PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") || !(graph?.GetType() != typeof (PXGenericInqGrph)))
      return (ToolbarButtonDescriptor) null;
    return new ToolbarButtonDescriptor((string) null, "Scan", PXMessages.LocalizeNoPrefix("Scan"))
    {
      CommandArgument = view
    };
  }

  private ToolbarButtonDescriptor CreateCapturingImageButton(PXGraph graph, string view)
  {
    return new ToolbarButtonDescriptor((string) null, "AttachFromMobile", PXMessages.LocalizeNoPrefix("Upload Using Mobile App"))
    {
      UsesSignalR = true,
      CommandArgument = view
    };
  }
}
