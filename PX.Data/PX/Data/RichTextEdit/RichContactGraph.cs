// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.RichContactGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.RichTextEdit;

/// <exclude />
[Serializable]
public class RichContactGraph : PXGraph<RichContactGraph>
{
  public PXSelect<RichContact> Contact;
  public PXFirst<RichContact> First;
  public PXPrevious<RichContact> Previous;
  public PXNext<RichContact> Next;
  public PXLast<RichContact> Last;
}
