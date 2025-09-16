// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.PostGraph.AcctHistDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL.Overrides.PostGraph;

public class AcctHistDefaultAttribute(Type sourceType) : PXDefaultAttribute(sourceType)
{
  public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
  {
    return attributeLevel == 2 ? (PXEventSubscriberAttribute) this : ((PXEventSubscriberAttribute) this).Clone(attributeLevel);
  }
}
