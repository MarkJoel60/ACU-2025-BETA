// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRValidateDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

public sealed class CRValidateDateAttribute : PXDBDateAndTimeAttribute
{
  public static readonly DateTime DefaultDate = new DateTime(1900, 1, 1);

  public virtual void CacheAttached(
  #nullable disable
  PXCache sender)
  {
    // ISSUE: method pointer
    sender.Graph.RowPersisting.AddHandler(sender.GetItemType(), new PXRowPersisting((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_0)));
    base.CacheAttached(sender);
  }

  public class defaultDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Constant<
    #nullable disable
    CRValidateDateAttribute.defaultDate>
  {
    public defaultDate()
      : base(CRValidateDateAttribute.DefaultDate)
    {
    }
  }
}
