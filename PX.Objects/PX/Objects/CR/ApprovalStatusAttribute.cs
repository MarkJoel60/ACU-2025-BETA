// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ApprovalStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class ApprovalStatusAttribute : ActivityStatusListAttribute
{
  protected override string[] AllowedState(PXCache sender, object row)
  {
    bool flag = false;
    if (row is PMTimeActivity)
      flag = sender.GetStatus(row) == 2;
    object obj = flag ? (object) null : sender.GetValueOriginal(row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (row == null || obj == null)
      obj = (object) "OP";
    string str = (string) obj;
    if (str != null && str.Length == 2)
    {
      switch (str[0])
      {
        case 'A':
          if (str == "AP")
            return new string[3]{ "OP", "AP", "CL" };
          goto label_16;
        case 'C':
          if (str == "CL" || str == "CD")
            break;
          goto label_16;
        case 'O':
          if (str == "OP")
            break;
          goto label_16;
        case 'P':
          if (str == "PA")
            return new string[3]{ "OP", "PA", "CL" };
          goto label_16;
        case 'R':
          switch (str)
          {
            case "RJ":
              return new string[3]{ "OP", "RJ", "CL" };
            case "RL":
              return new string[1]{ "RL" };
            default:
              goto label_16;
          }
        default:
          goto label_16;
      }
      return new string[3]{ "OP", "CD", "CL" };
    }
label_16:
    return base.AllowedState(sender, row);
  }
}
