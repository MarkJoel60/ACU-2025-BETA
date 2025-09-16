// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ClassExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.SO;

public static class ClassExtensions
{
  internal static void ClearPOFlags(this SOLineSplit split)
  {
    split.POCompleted = new bool?(false);
    split.POCancelled = new bool?(false);
    split.POCreate = new bool?(false);
    split.POSource = (string) null;
  }

  internal static void ClearPOReferences(this SOLineSplit split)
  {
    split.POType = (string) null;
    split.PONbr = (string) null;
    split.POLineNbr = new int?();
    split.POReceiptType = (string) null;
    split.POReceiptNbr = (string) null;
  }

  internal static void ClearSOReferences(this SOLineSplit split)
  {
    split.SOOrderType = (string) null;
    split.SOOrderNbr = (string) null;
    split.SOLineNbr = new int?();
    split.SOSplitLineNbr = new int?();
  }
}
