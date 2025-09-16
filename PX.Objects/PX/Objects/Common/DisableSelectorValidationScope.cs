// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DisableSelectorValidationScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Represents a scope used to shut down <see cref="T:PX.Data.PXSelectorAttribute" />
/// verification (e.g. for performance reasons).
/// </summary>
public class DisableSelectorValidationScope(PXCache cache, params Type[] fields) : 
  OverrideAttributePropertyScope<PXSelectorAttribute, bool>(cache, (IEnumerable<Type>) fields, (Action<PXSelectorAttribute, bool>) ((attribute, validateValue) => attribute.ValidateValue = validateValue), (Func<PXSelectorAttribute, bool>) (attribute => attribute.ValidateValue), false)
{
}
