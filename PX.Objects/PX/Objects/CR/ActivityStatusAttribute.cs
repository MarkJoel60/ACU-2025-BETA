// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ActivityStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class ActivityStatusAttribute : ActivityStatusListAttribute, IPXFieldVerifyingSubscriber
{
  protected override string[] AllowedState(PXCache sender, object row)
  {
    bool flag = false;
    if (row is CRActivity)
      flag = sender.GetStatus(row) == 2;
    object obj = flag ? (object) null : sender.GetValueOriginal(row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (row == null || obj == null)
      obj = (object) "OP";
    switch ((string) obj)
    {
      case "DR":
        return new string[4]{ "DR", "OP", "CD", "CL" };
      case "OP":
      case "CL":
      case "CD":
        return new string[3]{ "OP", "CD", "CL" };
      default:
        return base.AllowedState(sender, row);
    }
  }
}
