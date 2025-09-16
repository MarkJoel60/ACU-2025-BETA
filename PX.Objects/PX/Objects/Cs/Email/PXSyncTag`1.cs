// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncTag`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncTag<T> : PXSyncTag
{
  public T Row;
  public EMailSyncReference Ref;
  public PXSyncMailbox Mailbox;

  public PXSyncTag(T row, PXSyncMailbox mailbox, EMailSyncReference reference, PXSyncTag tag = null)
  {
    this.Row = row;
    this.Ref = reference;
    this.Mailbox = mailbox;
    if (tag == null)
      return;
    this.SkipReqired = tag.SkipReqired;
    this.CancelRequired = tag.CancelRequired;
    this.SendRequired = tag.SendRequired;
    this.SendSeparateRequired = tag.SendSeparateRequired;
  }
}
