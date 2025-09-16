// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLeadSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class CRLeadSelectorAttribute(System.Type type, params System.Type[] fieldList) : 
  PXSelectorAttribute(type, fieldList)
{
  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.SelectorMode = (PXSelectorMode) 0;
  }
}
