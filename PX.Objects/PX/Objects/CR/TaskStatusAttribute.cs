// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.TaskStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class TaskStatusAttribute : ActivityStatusListAttribute
{
  protected override string[] AllowedState(PXCache sender, object row)
  {
    bool flag = false;
    if (row is CRActivity)
      flag = sender.GetStatus(row) == 2;
    object obj = flag ? (object) null : sender.GetValueOriginal(row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (row != null && obj == null)
      obj = (object) "OP";
    switch ((string) obj)
    {
      case "DR":
        return new string[3]{ "DR", "OP", "CL" };
      case "IP":
      case "OP":
        return new string[5]{ "OP", "DR", "IP", "CL", "CD" };
      case "CL":
        return new string[3]{ "OP", "IP", "CL" };
      case "CD":
        return new string[3]{ "OP", "IP", "CD" };
      default:
        return base.AllowedState(sender, row);
    }
  }
}
