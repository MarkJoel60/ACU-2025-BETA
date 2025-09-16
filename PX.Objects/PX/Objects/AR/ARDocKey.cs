// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP;

#nullable disable
namespace PX.Objects.AR;

public class ARDocKey : Pair<string, string>
{
  public ARDocKey(string aType, string aRefNbr)
    : base(aType, aRefNbr)
  {
  }

  public ARDocKey(ARRegister aDoc)
    : base(aDoc.DocType, aDoc.RefNbr)
  {
  }
}
