// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSequentialNoteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSequentialNoteAttribute : PXNoteAttribute
{
  public PXSequentialNoteAttribute()
  {
  }

  [Obsolete("The constructor is obsolete. Use the parameterless constructor instead. The PXSequentialNoteAttribute constructor is exactly the same as the parameterless one. It does not provide any additional functionality and does not save values of provided fields in the note. The constructor will be removed in a future version of Acumatica ERP.")]
  public PXSequentialNoteAttribute(params System.Type[] searches)
    : base(searches)
  {
  }

  protected override Guid newGuid() => SequentialGuid.Generate();
}
