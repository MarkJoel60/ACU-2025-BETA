// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivityPinCacheExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Web.UI;
using System;

#nullable enable
namespace PX.Objects.CR;

[Serializable]
public sealed class CRActivityPinCacheExtension : PXCacheExtension<
#nullable disable
CRActivity>
{
  [PXDBCustomImage(HeaderImage = "ac@pin")]
  [PXUIField(DisplayName = "Is Pinned", IsReadOnly = true, Visible = false)]
  public string IsPinned { get; set; }

  public abstract class isPinned : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRActivityPinCacheExtension.isPinned>
  {
    public static string Pinned = Sprite.Ac.GetFullUrl("pin");
    public static string Unpinned = Sprite.Control.GetFullUrl("Empty");
  }
}
