// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBUserPasswordAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBUserPasswordAttribute : PXDBCalcedAttribute, IPXFieldUpdatingSubscriber
{
  public static string DefaultVeil => new string('*', 8);

  public PXDBUserPasswordAttribute()
    : base(typeof (Users.password), typeof (string))
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (sender.BypassAuditFields.Contains(this.FieldName))
      return;
    sender.BypassAuditFields.Add(this.FieldName);
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row != null)
      e.ReturnValue = (object) this.GetViewString(e.ReturnValue as string);
    base.FieldSelecting(sender, e);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || string.IsNullOrWhiteSpace(((Users) e.Row).Password) || !(e.NewValue as string == PXDBUserPasswordAttribute.DefaultVeil))
      return;
    e.NewValue = (object) ((Users) e.Row).Password;
    e.Cancel = true;
  }

  private string GetViewString(string str)
  {
    return !string.IsNullOrEmpty(str) ? PXDBUserPasswordAttribute.DefaultVeil : str;
  }
}
