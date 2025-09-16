// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.EPExpenseAllowProjectTaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField]
public class EPExpenseAllowProjectTaskAttribute : 
  EPBaseAllowProjectTaskAttribute,
  IPXFieldSelectingSubscriber
{
  public EPExpenseAllowProjectTaskAttribute(Type projectID, string Module)
    : base(projectID)
  {
    this.module = !string.IsNullOrEmpty(Module) ? Module : throw new ArgumentNullException("Source");
    if (!(Module == "EA"))
      throw new ArgumentOutOfRangeException("Source", (object) Module, "ProjectTaskAttribute does not support the given module.");
    Type type = typeof (PMTask.visibleInEA);
    this.Filterable = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[3]
    {
      typeof (Where<,>),
      type,
      typeof (Equal<True>)
    }), PXMessages.LocalizeFormatNoPrefixNLA("Project Task '{0}' is invisible in {1} module.", new object[2]
    {
      (object) "{0}",
      (object) PXMessages.LocalizeNoPrefix(this.module)
    }), new Type[1]{ typeof (PMTask.taskCD) }));
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    returnState.Visible = ProjectAttribute.IsPMVisible(this.module);
    if (e.Row == null)
      return;
    PMProject pmProject = PMProject.PK.Find(sender.Graph, (int?) sender.GetValue(e.Row, this.projectIDField.Name));
    returnState.Enabled = pmProject != null && !pmProject.NonProject.GetValueOrDefault() && pmProject.BaseType == "P";
  }
}
