// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ProjectWipAdjustmentEntry_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.AR;

public class ProjectWipAdjustmentEntry_ActivityDetailsExt : 
  ActivityDetailsExt<ProjectWipAdjustmentEntry, PMWipAdjustment, PMWipAdjustment.noteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public override string GetDefaultMailTo()
  {
    List<string> source = new List<string>();
    foreach (PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Base.Items).Select(Array.Empty<object>()))
    {
      if (PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact>.op_Implicit(pxResult).Selected.GetValueOrDefault())
      {
        PX.Objects.CR.Contact contact = PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        if (contact != null)
        {
          string str = contact.Address?.Trim();
          if (!string.IsNullOrWhiteSpace(str))
            source.Add(PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(str, contact.DisplayName?.Trim()));
        }
      }
    }
    return string.Join("; ", source.Distinct<string>());
  }

  public override string GetBody()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("<ul>");
    foreach (PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Base.Items).Select(Array.Empty<object>()))
    {
      if (PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact>.op_Implicit(pxResult).Selected.GetValueOrDefault())
      {
        PMProject pmProject = PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        PX.Objects.CR.Contact contact = PXResult<PMWipAdjustmentLine, PMProject, PMCostProjectionByDate, PX.Objects.CR.Contact>.op_Implicit(pxResult);
        stringBuilder.Append("<li>");
        stringBuilder.Append($"{pmProject?.ContractCD} ({contact.DisplayName})");
        stringBuilder.AppendLine("</li>");
      }
    }
    stringBuilder.AppendLine("</ul>");
    return PXRichTextConverter.NormalizeHtml(MailAccountManager.AppendSignature(stringBuilder.ToString(), (PXGraph) this.Base, (MailAccountManager.SignatureOptions) 0));
  }
}
