// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FieldLookupNoteText
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class FieldLookupNoteText(string noteText) : FieldLookup(typeof (FieldLookupNoteText), (object) noteText), ICustomFieldLookup
{
  object ICustomFieldLookup.GetValue(PXGraph graph, Type itemType, object data)
  {
    return data == null ? (object) null : (object) PXNoteAttribute.GetNote(graph.Caches[itemType], data);
  }
}
