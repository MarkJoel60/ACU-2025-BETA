// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXUserSetupPerMode`7
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;

#nullable disable
namespace PX.Objects.IN;

public class PXUserSetupPerMode<TSelf, TGraph, THeader, TSetup, TUserIDField, TModeField, TModeValueField> : 
  PXSetupBase<TSelf, TGraph, THeader, TSetup, Where<TUserIDField, Equal<Current<AccessInfo.userID>>, And<TModeField, Equal<Current<TModeField>>>>>
  where TSelf : PXUserSetupPerMode<TSelf, TGraph, THeader, TSetup, TUserIDField, TModeField, TModeValueField>
  where TGraph : PXGraph
  where THeader : class, IBqlTable, new()
  where TSetup : class, IBqlTable, new()
  where TUserIDField : IBqlField
  where TModeField : class, IBqlField
  where TModeValueField : IConstant, IBqlOperand, new()
{
  public virtual void _(Events.FieldDefaulting<TSetup, TModeField> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<TSetup, TModeField>, TSetup, object>) e).NewValue = new TModeValueField().Value;
  }
}
