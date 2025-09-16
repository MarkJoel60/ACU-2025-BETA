// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.LienWaiver.ProjectEntryLienWaiverExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.PM.CacheExtensions;
using PX.Objects.CN.Compliance.PM.DAC;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions.LienWaiver;

public class ProjectEntryLienWaiverExtension : LienWaiverBaseExtension<ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXOverride]
  public void DefaultFromTemplate(
    PMProject project,
    int? templateId,
    ProjectEntry.DefaultFromTemplateSettings settings,
    Action<PMProject, int?, ProjectEntry.DefaultFromTemplateSettings> baseHandler)
  {
    baseHandler(project, templateId, settings);
    this.UpdateThroughDateSourceFieldsFromTemplate(project, templateId);
    this.InsertRecipientsFromTemplate(project, templateId);
  }

  private void UpdateThroughDateSourceFieldsFromTemplate(PMProject project, int? templateId)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, templateId);
    PmProjectExtension extension1 = PXCache<PMProject>.GetExtension<PmProjectExtension>(project);
    PmProjectExtension extension2 = PXCache<PMProject>.GetExtension<PmProjectExtension>(pmProject);
    extension1.ThroughDateSourceConditional = extension2.ThroughDateSourceConditional;
    extension1.ThroughDateSourceUnconditional = extension2.ThroughDateSourceUnconditional;
  }

  private void InsertRecipientsFromTemplate(PMProject project, int? templateId)
  {
    ((PXSelectBase) this.LienWaiverRecipients).Cache.InsertAll((IEnumerable<object>) this.CreateProjectRecipients(project, templateId));
  }

  private IEnumerable<LienWaiverRecipient> CreateProjectRecipients(
    PMProject project,
    int? templateId)
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    return (IEnumerable<LienWaiverRecipient>) ((PXGraph) this.Base).Select<LienWaiverRecipient>().Where<LienWaiverRecipient>((Expression<Func<LienWaiverRecipient, bool>>) (lwr => lwr.ProjectId == templateId)).Select<LienWaiverRecipient, LienWaiverRecipient>(Expression.Lambda<Func<LienWaiverRecipient, LienWaiverRecipient>>((Expression) Expression.MemberInit(Expression.New(typeof (LienWaiverRecipient)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (LienWaiverRecipient.set_VendorClassId)), )))); // Unable to render the statement
  }
}
